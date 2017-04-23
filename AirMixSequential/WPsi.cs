using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirMixSequential {
    public class WPsi {

        const double epsPsi = 0.001;
        private readonly double nuM;
        private readonly double tau;
        private readonly double h;
        private readonly double tmax;
        private readonly int X;
        private readonly int Y;
        private readonly int x0;
        private readonly int len;

        private Turbulation turb;

        //функция тока
        private double[,] psi;
        //вихрь
        private double[,] w;

        private double[,] nuT;
        private double[,] Ux;
        private double[,] Uy;

        public enum HelmholtzCalcMethod {
            ExplicitScheme,//явная схема
            ImplicitScheme //неявная схема
        }

        public WPsi(double tau, double nuM, int x0, int len, double h, int X, int Y) {
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
            
            Init();
            turb = new Turbulation(X, Y, h, tau, nuM); 
        }

        public void Calculation(HelmholtzCalcMethod helmholtzCalcMethod, TurbulenceModel turbulenceModel,
            double[,] Ux, double[,] Uy, double tmax) {
           
            this.Ux = Ux;
            this.Uy = Uy;

            double t = 0;
            do {
                if (turbulenceModel != 0) //turbulenceModel == 0 если турбулентность не расчитывается
                    nuT = turb.Calculate(turbulenceModel, Ux, Uy);

                Vortex();
                CurrentFunction();
                Speeds();

                t += tau;
            } while (t <= tmax);
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
                    psi[i, Y - 1] = psi[i + 1, Y - 1] + Uy[i, Y - 1] * h;
                if (i < x0)
                    psi[i, Y - 1] = psi[i + 1, Y - 1];
            }

            for (int j = Y - 2; j >= 0; j--)
                psi[0, j] = psi[0, j + 1] + Ux[0, j] * h;

            for (int i = 1; i < X; i++)
                psi[i, 0] = psi[i - 1, 0];

            for (int j = Y - 2; j >= 0; j--)
                psi[X - 1, j] = psi[X - 1, j + 1] + Ux[X - 1, j] * h;
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
                        + (nuM + nuT[i, j])*(w[i + 1, j] + w[i - 1, j] + w[i, j + 1] + w[i, j - 1] - 4.0 * w[i, j]) / (h * h));
                }

            for (int j = 1; j < Y - 1; j++)
                for (int i = 1; i < X - 1; i++)
                    w[i, j] = wn[i, j];
        }

        private void Speeds() {
            for (int j = 1; j < Y - 1; j++)
                for (int i = 1; i < X - 1; i++) {
                    Ux[i, j] = -(psi[i + 1, j + 1] + psi[i - 1, j + 1] - psi[i + 1, j - 1] - psi[i - 1, j - 1])/(4.0*h);
                    Uy[i, j] = -(psi[i + 1, j + 1] - psi[i - 1, j + 1] + psi[i + 1, j - 1] - psi[i - 1, j - 1])/(4.0*h);
                }
        }   

    }
}
