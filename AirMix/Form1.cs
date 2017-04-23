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
    public partial class Form1 : Form {
        protected static double[,] Ux;
        protected static double[,] Uy;
        protected static int X;
        protected static int Y;
        protected static int len;
        protected static int x0;
        protected static double h;
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

        public double SizeX { get; set; }
        public double SizeY { get; set; }

        object pressureCalcMethod;
        object navierStokesCalcMethod;

        object helmholtzCalcMethod;
        object turbulenceModel;

        public Form1() {
            InitializeComponent();
            pbImage.Image = Image.FromFile("Image.jpg");
        }


        private void Init() {
            SizeX = Convert.ToDouble(tbWidth.Text);
            SizeY = Convert.ToDouble(tbHeight.Text);
            h = Convert.ToDouble(tbH.Text);
            tau = Convert.ToDouble(tbTau.Text);
            tmax = Convert.ToDouble(tbTimeMax.Text);
            scale = Convert.ToInt32(nudScale.Value);
            ro = Convert.ToDouble(tbRo.Text);
            nuM = (rbMissingTurb.Checked) ? 1.0 : Convert.ToDouble(tbNuM.Text);

            X = (int)(SizeX / h) + 1;
            Y = (int)(SizeY / h) + 1;
            x0 = X / 8;
            len = X / 5;
            Ux = new double[X,Y];
            Uy = new double[X, Y];

            //определение начальных скоростей
            var initialValues = new InitialValues();
            initialValues.Set(_UxMax: Convert.ToDouble(tbUxMax.Text), _UyMax: Convert.ToDouble(tbUyMax.Text));

        }

        private void InitSequential() {
            Init();

            //расчет в системе "давление - скорость"
            if (rbPU.Checked) {
                //выбор метода расчета поля давления
                pressureCalcMethod = (rbWeakCompress.Checked)
                    ? AirMixSequential.PU.PressureCalcMethod.WeakСompressibility
                    : AirMixSequential.PU.PressureCalcMethod.Poisson;

                //выбор метода решения уравнения Навье-Стокса
                navierStokesCalcMethod = (rbNSEquExpScheme.Checked)
                    ? AirMixSequential.PU.NavierStokesCalcMethod.ExplicitScheme
                    : AirMixSequential.PU.NavierStokesCalcMethod.ImplicitScheme;

                pu = new AirMixSequential.PU(tau, ro, nuM, x0, len, h, X, Y);
            }

            //расчет в системе "вихрь -функция тока"
            if (rbWPsi.Checked) {
                //выбор метода решения уравнения Гельмгольца
                helmholtzCalcMethod = (rbHelmEquExpScheme.Checked)
                    ? AirMixSequential.WPsi.HelmholtzCalcMethod.ExplicitScheme
                    : AirMixSequential.WPsi.HelmholtzCalcMethod.ImplicitScheme;

                wpsi = new AirMixSequential.WPsi(tau, nuM, x0, len, h, X, Y);
            }

            //выбор модели турбулентности
            if (rbSecundova.Checked)
                turbulenceModel = AirMixSequential.TurbulenceModel.Secundova;
            if (rbKE.Checked)
                turbulenceModel = AirMixSequential.TurbulenceModel.KE;
            if (rbMissingTurb.Checked)
                turbulenceModel = 0;
        }

        private void InitParallel() {
            Init();       

            //расчет в системе "давление - скорость"
            if (rbPU.Checked) {
                //выбор метода расчета поля давления
                pressureCalcMethod = (rbWeakCompress.Checked)
                    ? AirMixParallel.PU.PressureCalcMethod.WeakСompressibility
                    : AirMixParallel.PU.PressureCalcMethod.Poisson;

                //выбор метода решения уравнения Навье-Стокса
                navierStokesCalcMethod = (rbNSEquExpScheme.Checked)
                    ? AirMixParallel.PU.NavierStokesCalcMethod.ExplicitScheme
                    : AirMixParallel.PU.NavierStokesCalcMethod.ImplicitScheme;

                parPU = new AirMixParallel.PU(tau, ro, nuM, x0, len, h, X, Y, 0.0);
            }

            //расчет в системе "вихрь -функция тока"
            if (rbWPsi.Checked) {
              
            }

        }

 
        private void CalculationSeq(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = sender as BackgroundWorker;
            double t = 0;
        
            do {
                t += tau;   
                //состояние расчета
                bw.ReportProgress((int)(t / tmax * 100));
                
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

        private void CalculationCUDA(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = sender as BackgroundWorker;
            double t = 0;
            double[] Uxx = new double[X * Y];
            double[] Uyy = new double[X * Y];
          

            do {
                for (int j = 0; j < Y; j++)
                    for (int i = 0; i < X; i++) {
                        Uxx[j * X + i] = Ux[i, j];
                        Uyy[j * X + i] = Uy[i, j];
                    }

                t += tau;
                //состояние расчета
                bw.ReportProgress((int)(t / tmax * 100));

                //если произведена отмена
                if (bw.CancellationPending) {
                    e.Cancel = true;
                    return;
                }

                //расчет в системе "давление - скорость"
                if (rbPU.Checked)
                    parPU.CalcCUDA((AirMixParallel.PU.PressureCalcMethod)pressureCalcMethod,
                        (AirMixParallel.PU.NavierStokesCalcMethod)navierStokesCalcMethod,Uxx, Uyy);

                //расчет в системе "вихрь - функция тока"
                if (rbWPsi.Checked)
                

                if (cbGraphics.Checked)
                    Thread.Sleep(10);

                for (int j = 0; j < Y; j++)
                    for (int i = 0; i < X; i++) {
                        Ux[i, j] = Uxx[j * X + i];
                        Uy[i, j] = Uyy[j * X + i];
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

            if (rbCUDA.Checked) {
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
            if (rbCUDA.Checked)
                CalculationCUDA(sender, e);
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

        private void pictureBox1_Click(object sender, EventArgs e) {

        }


    }
}
