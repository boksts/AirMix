using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirMix {
     partial class Main {
          
         //инициализация основных параметров расчета
         void Init(double sizeX = 5.0, double sizeY = 2.0, bool stressTesting = false) {
      
             if (!stressTesting) {
                  sizeX = Convert.ToDouble(tbWidth.Text);
                  sizeY = Convert.ToDouble(tbHeight.Text);
             }                
             h = Convert.ToDouble(tbH.Text);
             tau = Convert.ToDouble(tbTau.Text);
             tmax = Convert.ToInt32(tbTimeMax.Text)*tau;
             scale = Convert.ToInt32(nudScale.Value);
             ro = Convert.ToDouble(tbRo.Text);
             nuM =  Convert.ToDouble(tbNuM.Text);

             X = (int) (sizeX/h) + 1;
             Y = (int) (sizeY/h) + 1;
             x0 = X/8;
             len = X/5;
             Ux = new double[X, Y];
             Uy = new double[X, Y];
             Temp = new double[X, Y];

             SetSpeeds(UxMax: Convert.ToDouble(tbUxMax.Text), UyMax: Convert.ToDouble(tbUyMax.Text));
             SetTemp(TMaxUx: Convert.ToDouble(tbTmaxUx.Text), TMaxUy: Convert.ToDouble(tbTmaxUy.Text));
             
         }
         
         //Установка скоростей
         void SetSpeeds(double UxMax, double UyMax) {
             // начальные скорости
             for (int i = 0; i < X; i++)
                 for (int j = 0; j < Y; j++) {
                     Ux[i, j] = 0.0;
                     Uy[i, j] = 0.0;
                 }

             for (int j = 0; j < Y; j++) {
                 //скорость на входе (1 поток)
                 Ux[0, j] = 4.0*UxMax*j*(Y - 1 - j)/((Y - 1)*(Y - 1));
                 //скорость на выходе
                 Ux[X - 1, j] = 4.0*(UxMax*(Y - 1) + Math.Abs(UyMax*len))*j*(Y - 1 - j)/(Math.Pow((Y - 1), 3.0));
             }

             for (int i = 0; i < X; i++) {
                 if (i < x0)
                     Uy[i, Y - 1] = 0.0;
                 if ((i >= x0) && (i < x0 + len))
                     //скорость 2 потока
                     Uy[i, Y - 1] = 4.0*UyMax*(i - x0)*(len - (i - x0))/(len*len);
                 if (i > x0 + len)
                     Uy[i, Y - 1] = 0.0;

                 Uy[i, 0] = 0.0;
             }

             lblRe.Text = Convert.ToString((Ux[X - 1, (Y - 1) / 2] / nuM *  Y).ToString("f1"));
         }

         //установка температуры
         void SetTemp(double TMaxUx, double TMaxUy) {
             // начальная температура
             for (int i = 0; i < X; i++)
                 for (int j = 0; j < Y; j++) {
                     Temp[i, j] = 0.0;
                 }

             for (int j = 1; j < Y - 1; j++) {
                 //температура на входе (1 поток)
                 Temp[0, j] = TMaxUx;
             }

             for (int i = 0; i < X; i++) {
                 if ((i >= x0) && (i < x0 + len))
                     //температура 2 потока
                     Temp[i, Y - 1] = TMaxUy;
             }
         }

         //инициализация последовательных вычислений
         void InitSequential() {
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

                 pu = new AirMixSequential.PU(tau, ro, nuM, x0, len, h, X, Y, Ux, Uy,Temp);
             }

             //расчет в системе "вихрь -функция тока"
             if (rbWPsi.Checked) {
                 //выбор метода решения уравнения Гельмгольца
                 helmholtzCalcMethod = (rbHelmEquExpScheme.Checked)
                     ? AirMixSequential.WPsi.HelmholtzCalcMethod.ExplicitScheme
                     : AirMixSequential.WPsi.HelmholtzCalcMethod.ImplicitScheme;

                 wpsi = new AirMixSequential.WPsi(tau, nuM, x0, len, h, X, Y, Ux, Uy, Temp);
             }

             //выбор модели турбулентности
             if (rbSecundova.Checked)
                 turbulenceModel = AirMixSequential.TurbulenceModel.Secundova;
             if (rbKE.Checked)
                 turbulenceModel = AirMixSequential.TurbulenceModel.KE;
             if (rbMissingTurb.Checked)
                 turbulenceModel = 0;
         }

         //инициализация параллельных вычислений
         void InitParallel(bool stressTestingOMP = false, bool stressTestingCUDA = false) {
             AirMixParallel.PPT ppt;

             bool omp = (stressTestingCUDA || stressTestingOMP) ? stressTestingOMP : rbOpenMP.Checked;
             bool cuda = (stressTestingCUDA || stressTestingOMP) ? stressTestingCUDA : rbCUDA.Checked;

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

                 if (omp) 
                    parPU = new AirMixParallel.PU(AirMixParallel.PPT.OpenMP, tau, ro, nuM, x0, len, h,X,Y );
                 if (cuda)
                    parPU = new AirMixParallel.PU(AirMixParallel.PPT.CUDA,tau, ro, nuM, x0, len, h, X, Y);
             }

             //расчет в системе "вихрь -функция тока"
             if (rbWPsi.Checked) {
                 //выбор метода решения уравнения Гельмгольца
                 helmholtzCalcMethod = (rbHelmEquExpScheme.Checked)
                     ? AirMixSequential.WPsi.HelmholtzCalcMethod.ExplicitScheme
                     : AirMixSequential.WPsi.HelmholtzCalcMethod.ImplicitScheme;
                 
                 double[] Ux1d = new double[X * Y];
                 double[] Uy1d = new double[X * Y];

                 for (int j = 0; j < Y; j++)
                     for (int i = 0; i < X; i++) {
                         Ux1d[j * X + i] = Ux[i, j];
                         Uy1d[j * X + i] = Uy[i, j];
                     }

                 if (omp)
                     parWPsi = new AirMixParallel.WPsi(AirMixParallel.PPT.OpenMP, tau, nuM, x0, len, h, X, Y,Ux1d,Uy1d);
                 if (cuda)
                     parWPsi = new AirMixParallel.WPsi(AirMixParallel.PPT.CUDA, tau, nuM, x0, len, h, X, Y, Ux1d, Uy1d);
             }

             //выбор модели турбулентности
             if (rbSecundova.Checked)
                 turbulenceModel = AirMixParallel.TurbulenceModel.Secundova;
             if (rbKE.Checked)
                 turbulenceModel = AirMixParallel.TurbulenceModel.KE;
             if (rbMissingTurb.Checked)
                 turbulenceModel = 0;
         }
     }
}
