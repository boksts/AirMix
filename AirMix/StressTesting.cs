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
    partial class Form1 {

        private ChartForm chartForm;
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
            string[] dimension = new string[N];
            Xdim[0] = 10;
            Ydim[0] = 4;
            double h = Convert.ToDouble(tbH.Text);

            dimension[0] = (int) (Xdim[0]/h) + ":" + (int)(Ydim[0]/h);

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

            if (cbCUDA.Checked || cbOpenMP.Checked) {
                parPU.Dispose();
            }

        }


        //замер времени
        private double[] seconds(int[] Xdim, int[] Ydim, int N, int calcSystem, object sender, DoWorkEventArgs e) {
            Stopwatch swSeq = new Stopwatch();
            BackgroundWorker bw = sender as BackgroundWorker;
            double[] sec = new double[N];
            bw.ReportProgress(1);
            for (int k = 0; k < N; k++) {
                 if (bw.CancellationPending) {
                    e.Cancel = true;
                     flag = false;
                    break;
                }
                
                Init(sizeX: Xdim[k], sizeY: Ydim[k], stressTesting: true);

                double[] Ux1d = new double[X * Y];
                double[] Uy1d = new double[X * Y];

               swSeq.Start();
                switch (calcSystem) {

                    case 0:
                        InitSequential();
                        if (rbPU.Checked)
                            pu.Calculation((AirMixSequential.PU.PressureCalcMethod)pressureCalcMethod,
                               (AirMixSequential.PU.NavierStokesCalcMethod)navierStokesCalcMethod,
                                (AirMixSequential.TurbulenceModel)turbulenceModel, Ux, Uy, tmax);
                        break;

                    case 1:
                        InitParallel(stressTestingOMP: true, stressTestingCUDA: false);

                        for (int j = 0; j < Y; j++)
                            for (int i = 0; i < X; i++) {
                                Ux1d[j * X + i] = Ux[i, j];
                                Uy1d[j * X + i] = Uy[i, j];
                            }

                        if (rbPU.Checked)
                            parPU.Calculation((AirMixParallel.PU.PressureCalcMethod)pressureCalcMethod,
                                (AirMixParallel.PU.NavierStokesCalcMethod)navierStokesCalcMethod,
                                (AirMixParallel.TurbulenceModel)turbulenceModel, Ux1d, Uy1d, tmax);
                        break;

                    case 2:
                        InitParallel(stressTestingCUDA: true);

                        for (int j = 0; j < Y; j++)
                            for (int i = 0; i < X; i++) {
                                Ux1d[j * X + i] = Ux[i, j];
                                Uy1d[j * X + i] = Uy[i, j];
                            }

                        if (rbPU.Checked)
                            parPU.Calculation((AirMixParallel.PU.PressureCalcMethod)pressureCalcMethod,
                                (AirMixParallel.PU.NavierStokesCalcMethod)navierStokesCalcMethod,
                                (AirMixParallel.TurbulenceModel)turbulenceModel, Ux1d, Uy1d, tmax);
                        break;
                }

                swSeq.Stop();
                sec[k] = swSeq.ElapsedMilliseconds / 1000.0;
            }

            return sec;
        }


       
    }
}
