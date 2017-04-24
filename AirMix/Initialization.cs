using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirMix {
     partial class Form1 {
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
                 Ux[X - 1, j] = 4.0*(UxMax*(Y - 1) + UyMax*len)*j*(Y - 1 - j)/(Math.Pow((Y - 1), 3.0));
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
         }

         //Инициализация основных параметров расчета
         void Init() {
             SizeX = Convert.ToDouble(tbWidth.Text);
             SizeY = Convert.ToDouble(tbHeight.Text);
             h = Convert.ToDouble(tbH.Text);
             tau = Convert.ToDouble(tbTau.Text);
             tmax = Convert.ToDouble(tbTimeMax.Text);
             scale = Convert.ToInt32(nudScale.Value);
             ro = Convert.ToDouble(tbRo.Text);
             nuM = (rbMissingTurb.Checked) ? 1.0 : Convert.ToDouble(tbNuM.Text);

             X = (int) (SizeX/h) + 1;
             Y = (int) (SizeY/h) + 1;
             x0 = X/8;
             len = X/5;
             Ux = new double[X, Y];
             Uy = new double[X, Y];

             SetSpeeds(UxMax: Convert.ToDouble(tbUxMax.Text), UyMax: Convert.ToDouble(tbUyMax.Text));
         }


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

         void InitParallel() {

             AirMixParallel.PPT ppt; 
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

                 if (rbOpenMP.Checked)
                    parPU = new AirMixParallel.PU(AirMixParallel.PPT.OpenMP, tau, ro, nuM, x0, len, h, X, Y);
                 if (rbCUDA.Checked)
                     parPU = new AirMixParallel.PU(AirMixParallel.PPT.CUDA,tau, ro, nuM, x0, len, h, X, Y);
             }

             //расчет в системе "вихрь -функция тока"
             if (rbWPsi.Checked) {
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
