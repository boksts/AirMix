using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirMix.Grafics;
using AirMix.GraficsText;

namespace AirMix {
    partial class Main {

        private ChartForm chartForm;
        //флаг для отмены вычислений
        private bool flag;

        private void StressTesting() {
            btnCancel.Enabled = true;
            btnCalculate.Enabled = false;
            flag = true;
            
            chartForm = new ChartForm();

            //запуск расчета в отдельном потоке
            bgw.RunWorkerAsync();
     
            //пока идет расчет
            while (bgw.IsBusy) {
                Application.DoEvents();
            }

            if (flag)
                chartForm.Show();
        }

        //нагрузочное тестирование
        private void StressTestingCalculate(object sender, DoWorkEventArgs e) {
            //число измерений
            int N = Convert.ToInt32(nudStressTesting.Value);
            
            //размеры области расчета
            int[] Xdim = new int[N];
            int[] Ydim = new int[N];
            //массив строк для отображения делений у графика
            string[] dimension = new string[N];
            Xdim[0] = 10;
            Ydim[0] = 4;
            double h = Convert.ToDouble(tbH.Text);
            dimension[0] = (int) (Xdim[0]/h) + ":" + (int)(Ydim[0]/h);

            //установка размеров области
            for (int i = 1; i < N; i++) {
                Xdim[i] = Xdim[i - 1] + 10;
                Ydim[i] = Ydim[i - 1] + 4;
                dimension[i] = (int)(Xdim[i] / h) + ":" + (int)(Ydim[i] / h);
            }
        
            if (cbSeq.Checked)
                chartForm.Graph(seconds(Xdim, Ydim, N, 0,sender,e), N, Color.Blue, dimension, "Последовательный");
            if (cbOpenMP.Checked)
                chartForm.Graph(seconds(Xdim, Ydim, N, 1, sender, e), N, Color.Red, dimension, "OpenMP");
            if (cbCUDA.Checked)
                chartForm.Graph(seconds(Xdim, Ydim, N, 2, sender, e), N, Color.Green, dimension, "CUDA");
   
            //освобождение ресурсов, если были использованы параллельные вычисления
            if (rbPU.Checked) {
                if (cbCUDA.Checked || cbOpenMP.Checked) {
                    parPU.Dispose();
                }
            }
            if (rbWPsi.Checked) {
                if (cbCUDA.Checked || cbOpenMP.Checked) {
                    parWPsi.Dispose();
                }
            }     

        }

        //замеры времени
        private double[] seconds(int[] Xdim, int[] Ydim, int N, int calcSystem, object sender, DoWorkEventArgs e) {
            Stopwatch swSeq = new Stopwatch();
            double timePar =0.0;
            BackgroundWorker bw = sender as BackgroundWorker;
            double[] sec = new double[N];
            bw.ReportProgress(1);

            //серия нагрузочных экспериментов
            for (int k = 0; k < N; k++) {
                //если произошла отмена вычислений
                if (bw.CancellationPending) {
                    e.Cancel = true;
                     flag = false;
                    break;
                }
                
                //инициализация
                Init(sizeX: Xdim[k], sizeY: Ydim[k], stressTesting: true);

                double[] Ux1d = new double[X * Y];
                double[] Uy1d = new double[X * Y];
                double[] Temp1d = new double[X * Y];            
              
                //последовательные вычисления  
                if (calcSystem == 0) {
                    swSeq.Start();
                    InitSequential();
                    if (rbPU.Checked)
                        pu.Calculation((AirMixSequential.PU.PressureCalcMethod) pressureCalcMethod,
                            (AirMixSequential.PU.NavierStokesCalcMethod) navierStokesCalcMethod,
                            (AirMixSequential.TurbulenceModel) turbulenceModel, tmax);
                    if (rbWPsi.Checked)
                        wpsi.Calculation((AirMixSequential.WPsi.HelmholtzCalcMethod) helmholtzCalcMethod,
                            (AirMixSequential.TurbulenceModel) turbulenceModel, tmax);

                    swSeq.Stop();
                    sec[k] = swSeq.ElapsedMilliseconds/1000.0;
                }

                if (calcSystem == 1 || calcSystem == 2) {
                    //с применением OpenMP
                    if (calcSystem == 1)
                        InitParallel(stressTestingOMP: true);
                    //с применением CUDA
                    if (calcSystem == 2)
                        InitParallel(stressTestingCUDA: true);

                    for (int j = 0; j < Y; j++)
                        for (int i = 0; i < X; i++) {
                            Ux1d[j*X + i] = Ux[i, j];
                            Uy1d[j*X + i] = Uy[i, j];
                            Temp1d[j*X + i] = Temp[i, j];
                        }

                    if (rbPU.Checked)
                        timePar = parPU.Calculation((AirMixParallel.PU.PressureCalcMethod) pressureCalcMethod,
                            (AirMixParallel.PU.NavierStokesCalcMethod) navierStokesCalcMethod,
                            (AirMixParallel.TurbulenceModel) turbulenceModel, Ux1d, Uy1d, Temp1d, tmax);
                    if (rbWPsi.Checked)
                        timePar = parWPsi.Calculation((AirMixParallel.WPsi.HelmholtzCalcMethod) helmholtzCalcMethod,
                            (AirMixParallel.TurbulenceModel) turbulenceModel, Ux1d, Uy1d, Temp1d, tmax);

                    sec[k] = timePar;
                }
            }
            return sec;
        }
       
    }
}
