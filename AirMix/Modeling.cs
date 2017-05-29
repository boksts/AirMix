using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AirMix.Grafics;

namespace AirMix {
    partial class Main {

        //моделирование
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
            //просходит вызов метода CalculationSeq или CalculationParallel
            //подробнее смотри в файле Main.cs метод bgw_DoWork() 
            bgw.RunWorkerAsync();

            //вывод скоростей графическом виде       
            if (сbGraphicsProcess.Checked) {
                    grForm = new GraphicsForm(X, Y, x0,len,rbSpeeds.Checked, rbTemp.Checked, scale: scale);
                    grForm.Show();
                    grForm.Closing += grForm_Closing;                  
            }

            bool error;

            //пока идет расчет
            while (bgw.IsBusy) {
                //вывод графики в процессе
                if (сbGraphicsProcess.Checked) {
                    error = grForm.DrawDisplay(Ux, Uy, Temp);
                    if (error) {
                        bgw.CancelAsync();
                    }                 
                }
                //если расчет до установления
                if (rbSetSpeeds.Checked) {
                     pgb.Style = ProgressBarStyle.Marquee;
                     lblPrc.Text = "Расчеты выполняются ...";
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

      
            //вывод результатов графики
            if (сbGraphicsResult.Checked) {
                grForm = new GraphicsForm(X, Y, x0, len, rbSpeeds.Checked, rbTemp.Checked, scale: scale);
                grForm.Show();
                grForm.DrawDisplay(Ux, Uy, Temp);
            }              

            //освобождение ресурсов, если были использованы параллельные вычисления
            if (rbPU.Checked) {
                if (rbCUDA.Checked || rbOpenMP.Checked) {
                    parPU.Dispose();
                }
            }

            if (rbWPsi.Checked) {
                if (rbCUDA.Checked || rbOpenMP.Checked) {
                    parWPsi.Dispose();
                }
            }           
        }

        //последовательные вычисления
        private void CalculationSeq(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = sender as BackgroundWorker;
            double t = 0;
            double [,] Utmp = new double[X,Y];
            const double procU = 0.03;
            bool flag;
            bool FLAG;

            do {
                //если произведена отмена
                if (bw.CancellationPending) {
                    e.Cancel = true;
                    return;
                }
           
                 //если расчет до установления
                if (rbSetSpeeds.Checked) {
                    for (int i = 1; i < X - 1; i++)
                        for (int j = 1; j < Y - 1; j++)
                            Utmp[i, j] = Ux[i, j] + Uy[i, j];
                }
                else {
                    t += tau;
                    //состояние расчета
                    bw.ReportProgress((int) (t / tmax * 100));
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

               //чтобы графика замедлялась
               if (сbGraphicsProcess.Checked)
                    Thread.Sleep(10);

                //если расчет до установления
                if (rbSetSpeeds.Checked) {
                    flag = false;
                    for (int i = 1; i < X - 1; i++)
                        for (int j = 1; j < Y - 1; j++) {
                            if (Math.Abs(Utmp[i, j]-(Ux[i, j] + Uy[i, j]))/Math.Abs(Ux[i, j] + Uy[i, j])*100.0 > procU)
                                flag = true;
                        }
                    FLAG = flag;
                }
                else {
                    FLAG = t < tmax;
                }

            } while (FLAG);
        }


        //параллельные вычисления
        private void CalculationParallel(object sender, DoWorkEventArgs e) {
            BackgroundWorker bw = sender as BackgroundWorker;
            double t = 0;
            double[] Ux1d = new double[X * Y];
            double[] Uy1d = new double[X * Y];
            double[] Temp1d = new double[X * Y];
            double[,] Utmp = new double[X, Y];
            const double procU = 0.03;
            bool flag;
            bool FLAG;

            do {
                //создание одномерных массивов для параллельных вычислений
                for (int j = 0; j < Y; j++)
                    for (int i = 0; i < X; i++) {
                        Ux1d[j * X + i] = Ux[i, j];
                        Uy1d[j * X + i] = Uy[i, j];
                        Temp1d[j * X + i] = Temp[i, j];
                    }

                //если произведена отмена
                if (bw.CancellationPending) {
                    e.Cancel = true;
                    return;
                }

                //если расчет до установления
                if (rbSetSpeeds.Checked) {
                    for (int i = 1; i < X - 1; i++)
                        for (int j = 1; j < Y - 1; j++)
                            Utmp[i, j] = Ux[i, j] + Uy[i, j];
                }
                else {
                    t += tau;
                    //состояние расчета
                    bw.ReportProgress((int)(t / tmax * 100));
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

                //чтобы графика замедлялась
                if (сbGraphicsProcess.Checked)
                    Thread.Sleep(10);

                //копирование данных из парралельных вычислений
                for (int j = 0; j < Y; j++)
                    for (int i = 0; i < X; i++) {
                        Ux[i, j] = Ux1d[j * X + i];
                        Uy[i, j] = Uy1d[j * X + i];
                        Temp[i, j] = Temp1d[j * X + i];
                    }

                //если расчет до установления
                if (rbSetSpeeds.Checked) {
                    flag = false;
                    for (int i = 1; i < X - 1; i++)
                        for (int j = 1; j < Y - 1; j++) {
                            if (Math.Abs(Utmp[i, j] - (Ux[i, j] + Uy[i, j])) / Math.Abs(Ux[i, j] + Uy[i, j]) * 100.0 > procU)
                                flag = true;
                        }
                    FLAG = flag;
                }
                else {
                    FLAG = t < tmax;
                }

            } while (FLAG);
        }
    }
}
