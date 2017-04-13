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
using AirMix.Calculation;
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
        private PU pu;
        private W_Psi w_psi;
        private GraphicsForm grForm;
        private OutputForm outForm;
        private PU.PressureCalcMethod pressureCalcMethod;
        private PU.NavierStokesCalcMethod navierStokesCalcMethod;
        private W_Psi.HelmholtzCalcMethod helmholtzCalcMethod;
        private Turbulation.TurbulenceModel turbulenceModel;

        public double SizeX { get; set; }
        public double SizeY { get; set; }

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

            //расчет в системе "давление - скорость"
            if (rbPU.Checked) {
                //выбор метода расчета поля давления
                pressureCalcMethod = (rbWeakCompress.Checked)
                    ? PU.PressureCalcMethod.WeakСompressibility
                    : PU.PressureCalcMethod.Poisson;

                //выбор метода решения уравнения Навье-Стокса
                navierStokesCalcMethod = (rbNSEquExpScheme.Checked)
                    ? PU.NavierStokesCalcMethod.ExplicitScheme
                    : PU.NavierStokesCalcMethod.ImplicitScheme;

                pu = new PU(tau, nuM, ro);
            }

            //расчет в системе "вихрь -функция тока"
            if (rbWPsi.Checked) {
                //выбор метода решения уравнения Гельмгольца
                helmholtzCalcMethod = (rbHelmEquExpScheme.Checked)
                    ? W_Psi.HelmholtzCalcMethod.ExplicitScheme
                    : W_Psi.HelmholtzCalcMethod.ImplicitScheme;

                w_psi = new W_Psi(nuM,tau);
            }

            //выбор модели турбулентности
            if (rbSecundova.Checked)
                turbulenceModel = Turbulation.TurbulenceModel.Secundova;
            if (rbKE.Checked)
                turbulenceModel = Turbulation.TurbulenceModel.KE;
            if (rbMissingTurb.Checked)
                turbulenceModel = 0;

        }


        private void Calculation(object sender, DoWorkEventArgs e) {
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
                    pu.Calculation(pressureCalcMethod, navierStokesCalcMethod, turbulenceModel);
                
                //расчет в системе "вихрь - функция тока"
                if (rbWPsi.Checked)
                    w_psi.Calculation(helmholtzCalcMethod, turbulenceModel);

                if (cbGraphics.Checked) 
                    Thread.Sleep(10);

            } while (t <= tmax);
          
        }

        private void btnCalculate_Click(object sender, EventArgs e) {
            btnCancel.Enabled = true;
            btnCalculate.Enabled = false;
   
            //инициализация параметров расчета
            Init();
              
            //вывод скоростей графическом виде       
            if (cbGraphics.Checked) {
                grForm = new GraphicsForm(X, Y, scale: scale);
                grForm.Show();
                grForm.Closing += grForm_Closing;
            }

            //запуск расчета в отдельном потоке
            bgw.RunWorkerAsync();
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
            Calculation(sender, e);
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
