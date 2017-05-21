using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirMix.Grafics;

namespace AirMix {
    partial class Form1 {

        private void Modeling() {
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
                    grForm = new GraphicsForm(X, Y, rbSpeeds.Checked, rbTemp.Checked, scale: scale);
                    grForm.Show();
                    grForm.Closing += grForm_Closing;
                      
            }

            bool error;

            //пока идет расчет
            while (bgw.IsBusy) {
                //вывод графики
                if (cbGraphics.Checked) {
                    error = grForm.DrawDisplay(Ux, Uy, Temp, x0, len);
                    if (error) {
                        bgw.CancelAsync();
                    }
                        
                }
                Application.DoEvents();
            }

            //вывод результатов  в текстовом виде      
            if (cbTextFile.Checked) {
                outForm = new OutputForm { X = X, Y = Y };
                outForm.Show();
                if (rbSpeeds.Checked)
                    outForm.OutSpeeds(Ux, Uy);
                if (rbTemp.Checked)
                    outForm.OutTemp(Temp);
            }

           /* if (rbCUDA.Checked || rbOpenMP.Checked) {
                parPU.Dispose();
            }*/
        }

        //последовательные вычисления
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
                    pu.Calculation((AirMixSequential.PU.PressureCalcMethod)pressureCalcMethod,
                        (AirMixSequential.PU.NavierStokesCalcMethod)navierStokesCalcMethod,
                        (AirMixSequential.TurbulenceModel)turbulenceModel, 0.0);

                //расчет в системе "вихрь - функция тока"
                if (rbWPsi.Checked)
                    wpsi.Calculation((AirMixSequential.WPsi.HelmholtzCalcMethod)helmholtzCalcMethod,
                        (AirMixSequential.TurbulenceModel)turbulenceModel, 0.0);

               if (cbGraphics.Checked)
                    Thread.Sleep(10);
            } while (t < tmax);
        }


        //параллельные вычисления
        private void CalculationParallel(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = sender as BackgroundWorker;
            double t = 0;
            double[] Ux1d = new double[X * Y];
            double[] Uy1d = new double[X * Y];
            double[] Temp1d = new double[X * Y];

            do {
                for (int j = 0; j < Y; j++)
                    for (int i = 0; i < X; i++) {
                        Ux1d[j * X + i] = Ux[i, j];
                        Uy1d[j * X + i] = Uy[i, j];
                        Temp1d[j * X + i] = Temp[i, j];
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
                if (rbPU.Checked) {
                    if (rbCUDA.Checked || rbOpenMP.Checked)
                        parPU.Calculation((AirMixParallel.PU.PressureCalcMethod)pressureCalcMethod,
                            (AirMixParallel.PU.NavierStokesCalcMethod)navierStokesCalcMethod,
                            (AirMixParallel.TurbulenceModel)turbulenceModel, Ux1d, Uy1d, Temp1d, 0.0);

                }

                //расчет в системе "вихрь - функция тока"
                if (rbWPsi.Checked) {
                    if (rbCUDA.Checked || rbOpenMP.Checked)
                        parWPsi.Calculation((AirMixParallel.WPsi.HelmholtzCalcMethod) helmholtzCalcMethod,
                            (AirMixParallel.TurbulenceModel)turbulenceModel, Ux1d, Uy1d, Temp1d, 0.0);

                }

                if (cbGraphics.Checked)
                    Thread.Sleep(10);

                for (int j = 0; j < Y; j++)
                    for (int i = 0; i < X; i++) {
                        Ux[i, j] = Ux1d[j * X + i];
                        Uy[i, j] = Uy1d[j * X + i];
                        Temp[i, j] = Temp1d[j * X + i];
                    }
            } while (t <= tmax);
        }
    }
}
