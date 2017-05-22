using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirMixSequential {
    public class WPsi {    
        private readonly double nuM;
        private readonly double tau;
        private readonly double h;
        private readonly double tmax;
        private readonly int X;
        private readonly int Y;
        private readonly int x0;
        private readonly int len;

        private Turbulation turb;
        private Temperature temp;
        
        //точность решения уравнения Пуассона
        const double epsPsi = 0.001;

        private double[,] psi;
        private double[,] w;
        private double[,] nuT;
        private double[,] Ux;
        private double[,] Uy;
        private double[,] Temp;

        //ускорение свободного падения
        const double g = 9.8;
        //коэффициент объемного расширения воздуха
        private readonly double betta = 3.665 * Math.Pow(10.0, -3.0);

        /// <summary>
        /// Схема расчета уравнения Гельмгольца
        /// </summary>
        public enum HelmholtzCalcMethod {
            ///<summary>явная схема</summary>
            ExplicitScheme,
            ///<summary>неявная схема</summary>
            ImplicitScheme 
        }

        ///<summary>Установка параметров расчета</summary>
        /// <param name="tau">шаг по времени</param>
        ///  <param name="nuM">молекулярная вязкость</param>
        ///  <param name="x0">расположение отвестия снизу от точки х0...</param>
        ///  <param name="len">...длиной len</param>
        /// <param name="h">шаг по сетке</param>
        /// <param name="X">число точек по оси Х</param>
        ///  <param name="Ux">скорости Ux</param>
        ///  <param name="Uy">скорости Uy</param>
        /// <param name="Temp">температура</param>
        public WPsi(double tau, double nuM, int x0, int len, double h, int X, int Y, double[,] Ux, double[,] Uy, double[,] Temp) {
            this.nuM = nuM;
            this.tau = tau;
            this.h = h;
            this.X = X;
            this.Y = Y;
            this.x0 = x0;
            this.len = len;
            psi = new double[X, Y];
            w = new double[X, Y];  
            nuT = new double[X, Y];

            this.Ux = Ux;
            this.Uy = Uy;
            
            Init();
            turb = new Turbulation(X, Y, h, tau, nuM);
            temp = new Temperature(tau, nuM, x0, len, h, X, Y, Ux, Uy, Temp, nuT);   
        }

        //начальные значения и граничные условия
        private void Init() {
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++) {
                    psi[i, j] = 0.0;
                    w[i, j] = 0.0;
                }

            //функция тока на границах
            for (int i = X - 2; i >= 0; i--) {
                if (i > x0 + len)
                    psi[i, Y - 1] = 0.0;
                if ((i >= x0) && (i <= x0 + len))
                    psi[i, Y - 1] = psi[i + 1, Y - 1] + Uy[i, Y - 1]*h;
                if (i < x0)
                    psi[i, Y - 1] = psi[i + 1, Y - 1];
            }

            for (int j = Y - 2; j >= 0; j--)
                psi[0, j] = psi[0, j + 1] + Ux[0, j]*h;

            for (int i = 1; i < X; i++)
                psi[i, 0] = psi[i - 1, 0];

            for (int j = Y - 2; j >= 0; j--)
                psi[X - 1, j] = psi[X - 1, j + 1] + Ux[X - 1, j]*h;
        }

        ///<summary>Расчет поля скоростей</summary>
        /// <param name="hcm">схема расчета уравнения Гельмгольца</param>
        ///  <param name="tm">модель турбулентности (turbulenceModel = 0 если турбулентность не расчитывается)</param>
        /// <param name="tmax">время расчета</param>
        public void Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, double tmax) {
            double t = 0;
            do {
                if (tm != 0) //turbulenceModel == 0 если турбулентность не расчитывается
                    nuT = turb.Calculate(tm, Ux, Uy);

                Temp = temp.CalcTemp();
                Vortex();
                CurrentFunction();
                Speeds();
                t += tau;
            } while (t <= tmax);
        }

        //расчет поля функции тока
        private void CurrentFunction() {
            bool flag;
            double[,] psin = new double[X, Y];
            do {
                flag = false;
                for (int j = 1; j < Y - 1; j++)
                    for (int i = 1; i < X - 1; i++) {
                        psin[i, j] = 0.25*(psi[i + 1, j] + psi[i - 1, j]+ psi[i, j + 1] + psi[i, j - 1] + h*h*w[i, j]);

                        if (Math.Abs(psin[i, j] - psi[i, j]) >= epsPsi)
                            flag = true;
                    }

                for (int j = 1; j < Y - 1; j++)
                    for (int i = 1; i < X - 1; i++)
                        psi[i, j] = psin[i, j];
            } while (flag);
        }

        //расчет вихревого поля
        private void Vortex() {
            double[,] wn = new double[X, Y];

            for (int i = 0; i < X; i++) {
                w[i,0] = -(psi[i,1] - psi[i,0]) / (h * h);
                w[i,Y-1] = -(psi[i,Y-2] - psi[i,Y-1]) / (h * h);
            }

            for (int j = 0; j < Y; j++) {
                w[0,j] = 0;
                w[X-1,j] = w[X-2,j];
            }

            //уравнение Гельмгольца(противоточные производные)
            for (int j = 1; j < Y - 1; j++)
                for (int i = 1; i < X - 1; i++) {
                    wn[i, j] = w[i, j] + tau*(
                        - (Ux[i, j] + Math.Abs(Ux[i, j]))/2.0*(w[i, j] - w[i - 1, j])/h
                        - (Ux[i, j] - Math.Abs(Ux[i, j]))/2.0*(w[i + 1, j] - w[i, j])/h
                        - (Uy[i, j] + Math.Abs(Uy[i, j]))/2.0*(w[i, j] - w[i, j - 1])/h
                        - (Uy[i, j] - Math.Abs(Uy[i, j]))/2.0*(w[i, j + 1] - w[i, j])/h
                        + (nuM + nuT[i, j])*(w[i + 1, j] + w[i - 1, j] + w[i, j + 1] + w[i, j - 1] - 4.0 * w[i, j]) / (h * h)
                        - g*betta*Temp[i,j]);
                }

            for (int j = 1; j < Y - 1; j++)
                for (int i = 1; i < X - 1; i++)
                    w[i, j] = wn[i, j];
        }

        //расчет скоростей
        private void Speeds() {
            for (int j = 1; j < Y - 1; j++)
                for (int i = 1; i < X - 1; i++) {
                    Ux[i, j] = -(psi[i + 1, j + 1] + psi[i - 1, j + 1] - psi[i + 1, j - 1] - psi[i - 1, j - 1])/(4.0*h);
                    Uy[i, j] = -(psi[i + 1, j + 1] - psi[i - 1, j + 1] + psi[i + 1, j - 1] - psi[i - 1, j - 1])/(4.0*h);
                }
        }   

    }
}
