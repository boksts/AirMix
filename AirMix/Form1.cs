using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirMix.Grafics;


namespace AirMix {
   partial class Form1 : Form {
       private double[,] Ux;
       private double[,] Uy;
       private int X;
       private int Y;
       private int len;
       private int x0;
       private double h;
       private double tau;
       private double ro;
       private double nuM;
       private double tmax;
       private int scale;

       private AirMixSequential.PU pu;
       private AirMixSequential.WPsi wpsi;
       private AirMixParallel.PU parPU;

       private GraphicsForm grForm;
       private OutputForm outForm;

       private double SizeX { get; set; }
       private double SizeY { get; set; }

       object pressureCalcMethod;
       object navierStokesCalcMethod;

       object helmholtzCalcMethod;
       object turbulenceModel;

       public Form1() {
           InitializeComponent();
           pbImage.Image = Image.FromFile("Image.jpg");
       }


       private void CalculationSeq(object sender, DoWorkEventArgs e) {
           BackgroundWorker bw = sender as BackgroundWorker;
           double t = 0;

           do {
               t += tau;
               //состояние расчета
               bw.ReportProgress((int) (t/tmax*100));

               //если произведена отмена
               if (bw.CancellationPending) {
                   e.Cancel = true;
                   return;
               }

               //расчет в системе "давление - скорость"
               if (rbPU.Checked)
                   pu.Calculation((AirMixSequential.PU.PressureCalcMethod) pressureCalcMethod,
                       (AirMixSequential.PU.NavierStokesCalcMethod) navierStokesCalcMethod,
                       (AirMixSequential.TurbulenceModel) turbulenceModel, Ux, Uy, 0.0);

               //расчет в системе "вихрь - функция тока"
               if (rbWPsi.Checked)
                   wpsi.Calculation((AirMixSequential.WPsi.HelmholtzCalcMethod) helmholtzCalcMethod,
                       (AirMixSequential.TurbulenceModel) turbulenceModel, Ux, Uy, 0.0);

               if (cbGraphics.Checked)
                   Thread.Sleep(10);
           } while (t <= tmax);
       }

       private void CalculationParallel(object sender, DoWorkEventArgs e) {
           BackgroundWorker bw = sender as BackgroundWorker;
           double t = 0;
           double[] Ux1d = new double[X*Y];
           double[] Uy1d = new double[X*Y];

           do {
               for (int j = 0; j < Y; j++)
                   for (int i = 0; i < X; i++) {
                       Ux1d[j*X + i] = Ux[i, j];
                       Uy1d[j*X + i] = Uy[i, j];
                   }

               t += tau;
               //состояние расчета
               bw.ReportProgress((int) (t/tmax*100));

               //если произведена отмена
               if (bw.CancellationPending) {
                   e.Cancel = true;
                   return;
               }

               //расчет в системе "давление - скорость"
               if (rbPU.Checked) {
                   if (rbCUDA.Checked)
                       parPU.Calculation((AirMixParallel.PU.PressureCalcMethod) pressureCalcMethod,
                           (AirMixParallel.PU.NavierStokesCalcMethod) navierStokesCalcMethod,
                           (AirMixParallel.TurbulenceModel) turbulenceModel, Ux1d, Uy1d, 0.0);
                   
                  if (rbOpenMP.Checked)
                      parPU.Calculation((AirMixParallel.PU.PressureCalcMethod)pressureCalcMethod,
                          (AirMixParallel.PU.NavierStokesCalcMethod)navierStokesCalcMethod,
                          (AirMixParallel.TurbulenceModel)turbulenceModel, Ux1d, Uy1d, 0.0);
               }
                   
               
               //расчет в системе "вихрь - функция тока"
               if (rbWPsi.Checked) {
                   
               }

              if (cbGraphics.Checked)
                   Thread.Sleep(10);

               for (int j = 0; j < Y; j++)
                   for (int i = 0; i < X; i++) {
                       Ux[i, j] = Ux1d[j*X + i];
                       Uy[i, j] = Uy1d[j*X + i];
                   }
           } while (t <= tmax);
       }


       private void btnCalculate_Click(object sender, EventArgs e) {
           btnCancel.Enabled = true;
           btnCalculate.Enabled = false;

           //инициализация параметров расчета
           Init();

           if (rbSeq.Checked) {
               InitSequential();
           }

           if (rbCUDA.Checked || rbOpenMP.Checked) {
               InitParallel();
           }

           //запуск расчета в отдельном потоке
           bgw.RunWorkerAsync();

           //вывод скоростей графическом виде       
           if (cbGraphics.Checked) {
               grForm = new GraphicsForm(X, Y, scale: scale);
               grForm.Show();
               grForm.Closing += grForm_Closing;
           }

           bool error;

           //пока идет расчет
           while (bgw.IsBusy) {
               //вывод графики
               if (cbGraphics.Checked) {
                   error = grForm.DrawDisplay(Ux, Uy, x0, len);
                   if (error)
                       bgw.CancelAsync();
               }
               Application.DoEvents();
           }

           //вывод результатов  в текстовом виде      
           if (cbTextFile.Checked) {
               outForm = new OutputForm {X = X, Y = Y};
               outForm.Show();
               outForm.OutSpeeds(Ux, Uy);
           }

           if (rbCUDA.Checked || rbOpenMP.Checked) {
               parPU.Dispose();
           }
           
       }

       //метод при закрытии формы с графикой
       void grForm_Closing(object sender, CancelEventArgs e) {
           if (bgw.IsBusy)
               switch (MessageBox.Show("Прекратить расчет и закрыть форму?", "", MessageBoxButtons.YesNo)) {
                   case DialogResult.Yes: {
                       bgw.CancelAsync();
                       break;
                   }

                   case DialogResult.No:
                       e.Cancel = true;
                       break;
               }
       }

       private void btnCancel_Click(object sender, EventArgs e) {
           bgw.CancelAsync();
       }


       //-----------BackgroundWorker---------------------------------
       private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
           pgb.Value = e.ProgressPercentage;
           lblPrc.Text = "Расчет выполнен на " + pgb.Value + "%";
       }

       private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
           string str = e.Cancelled ? "Расчет отменен!" : "Расчет завершен!";
           pgb.Value = 0;
           btnCancel.Enabled = false;
           btnCalculate.Enabled = true;
           MessageBox.Show(str);
       }

       private void bgw_DoWork(object sender, DoWorkEventArgs e) {
           if (rbSeq.Checked)
               CalculationSeq(sender, e);
           if (rbCUDA.Checked || rbOpenMP.Checked)
               CalculationParallel(sender, e);
       }

       //===================================================================


       private void cbGraphics_CheckedChanged(object sender, EventArgs e) {
           nudScale.Enabled = cbGraphics.Checked;
       }

       private void rbTurbNO_CheckedChanged(object sender, EventArgs e) {
           tbNuM.Enabled = !rbMissingTurb.Checked;
       }

       private void rbPU_CheckedChanged(object sender, EventArgs e) {
           gbPU.Enabled = rbPU.Checked;
       }

       private void rbWPsi_CheckedChanged(object sender, EventArgs e) {
           gbWPsi.Enabled = rbWPsi.Checked;
       }

       private void button1_Click(object sender, EventArgs e) {
           Init();
           InitParallel();
        double[] Ux1d = new double[X*Y];
           double[] Uy1d = new double[X*Y];


               for (int j = 0; j < Y; j++)
                   for (int i = 0; i < X; i++) {
                       Ux1d[j*X + i] = Ux[i, j];
                       Uy1d[j*X + i] = Uy[i, j];
                   }

               parPU.Calculation((AirMixParallel.PU.PressureCalcMethod)pressureCalcMethod,
                   (AirMixParallel.PU.NavierStokesCalcMethod)navierStokesCalcMethod,
                   (AirMixParallel.TurbulenceModel)turbulenceModel, Ux1d, Uy1d, 0.01);

               for (int j = 0; j < Y; j++)
                   for (int i = 0; i < X; i++) {
                       Ux[i, j] = Ux1d[j * X + i];
                       Uy[i, j] = Uy1d[j * X + i];
                   }
       }
   }
}
