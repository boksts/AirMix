using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirMix {
    class PU : InitialValues {
        private readonly double tau;
        private readonly double nuM;
        private readonly double ro;
        private double[,] P;
        private double[,] divU;
        private Turbulation turb;
        private double[,] Uxn;
        private double[,] Uyn;
        private double[,] nuT;

        public enum PressureCalcMethod {
            Poisson,//ур-е Пуассона
            WeakСompressibility //метод слабой сжимаемости
        }

        public enum NavierStokesCalcMethod{
            ExplicitScheme,//явная схема
            ImplicitScheme //неявная схема
        }


        public PU(double tau, double nuM, double ro){
            this.tau = tau;
            this.nuM = nuM;
            this.ro = ro;
            P = new double[X, Y];
            divU = new double[X, Y];
            Uxn = new double[X, Y];
            Uyn = new double[X, Y];
            nuT = new double[X, Y];
            //начальное давление
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++) {
                    P[i, j] = 0.0;
                }
            turb = new Turbulation(X,Y,h,tau,nuM); 
            
        }


        //общий расчет согласно модели "давление - скорость"
        public void Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod,Turbulation.TurbulenceModel turbulenceModel) {

            if (turbulenceModel != 0)//turbulenceModel == 0 если турбулентность не расчитывается
               nuT = turb.Calculate(turbulenceModel, Ux, Uy);

            switch (pressureMethod) {
                case PressureCalcMethod.Poisson:
                    Poisson();
                    break;
                case PressureCalcMethod.WeakСompressibility:
                    WeakСompressibility();
                    break;
            }

            Speeds();
        }

        //расчет поля скоростей
        private void Speeds() {
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Uxn[i, j] = Ux[i, j] + tau*(-(Ux[i, j] + Math.Abs(Ux[i, j]))/2.0*(Ux[i, j] - Ux[i - 1, j])/h
                                                - (Ux[i, j] - Math.Abs(Ux[i, j]))/2.0*(Ux[i + 1, j] - Ux[i, j])/h
                                                - (Uy[i, j] + Math.Abs(Uy[i, j]))/2.0*(Ux[i, j] - Ux[i, j - 1])/h
                                                - (Uy[i, j] - Math.Abs(Uy[i, j]))/2.0*(Ux[i, j + 1] - Ux[i, j])/h
                                                -
                                                (P[i + 1, j + 1] + P[i + 1, j - 1] - P[i - 1, j + 1] - P[i - 1, j - 1])/
                                                (4*h*ro)
                                                +
                                                (nuM + nuT[i,j]) *
                                                (Ux[i + 1, j] + Ux[i - 1, j] + Ux[i, j + 1] + Ux[i, j - 1] - 4*Ux[i, j])/
                                                (h*h));


            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Uyn[i, j] = Uy[i, j] + tau*(-(Ux[i, j] + Math.Abs(Ux[i, j]))/2.0*(Uy[i, j] - Uy[i - 1, j])/h
                                                - (Ux[i, j] - Math.Abs(Ux[i, j]))/2.0*(Uy[i + 1, j] - Uy[i, j])/h
                                                - (Uy[i, j] + Math.Abs(Uy[i, j]))/2.0*(Uy[i, j] - Uy[i, j - 1])/h
                                                - (Uy[i, j] - Math.Abs(Uy[i, j]))/2.0*(Uy[i, j + 1] - Uy[i, j])/h
                                                -
                                                (P[i - 1, j + 1] + P[i + 1, j + 1] - P[i - 1, j - 1] - P[i + 1, j - 1])/
                                                (4*h*ro)
                                                +
                                                (nuM + nuT[i, j]) *
                                                (Uy[i + 1, j] + Uy[i - 1, j] + Uy[i, j + 1] + Uy[i, j - 1] - 4*Uy[i, j])/
                                                (h*h));


            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Ux[i, j] = Uxn[i, j];

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Uy[i, j] = Uyn[i, j];
        }

        //расчет поля давления с помощью уравнения Пуассона
        private void Poisson() {
            const double tauP = 0.0001;//шаг уравнения Пуассона ???
            const double eps = 0.5;//допустимая погрешность ???
            const double tetta = 1.85;//для метода верхней релаксации
            double[,] A = new double[X, Y];

            double tmp;
            bool flag;
            int step = 0;

            //вычисление дивергенции скорости
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    divU[i, j] = ((Ux[i + 1, j + 1] + Ux[i + 1, j - 1]) -
                        (Ux[i - 1, j - 1] + Ux[i - 1, j + 1]) +
                        (Uy[i - 1, j + 1] + Uy[i + 1, j + 1]) -
                        (Uy[i - 1, j - 1] + Uy[i + 1, j - 1])) / (4.0 * h);

            //вычисление правой части в уравнении Пуассона
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    A[i, j] = -ro * (
                        Math.Pow((Ux[i + 1, j] - Ux[i - 1, j]) / (2.0 * h), 2.0)
                        + Math.Pow((Uy[i, j + 1] - Ux[i, j - 1]) / (2.0 * h), 2.0)
                        + (Ux[i + 1, j] - Ux[i - 1, j]) * (Uy[i, j + 1] - Ux[i, j - 1]) / (2.0 * h * h) - divU[i, j] / tauP
                        + (Ux[i, j] + Math.Abs(Ux[i, j])) / 2.0 * (divU[i, j] - divU[i - 1, j]) / h
                        + (Ux[i, j] - Math.Abs(Ux[i, j])) / 2.0 * (divU[i + 1, j] - divU[i, j]) / h
                        + (Uy[i, j] + Math.Abs(Uy[i, j])) / 2.0 * (divU[i, j] - divU[i, j - 1]) / h
                        + (Uy[i, j] - Math.Abs(Uy[i, j])) / 2.0 * (divU[i, j + 1] - divU[i, j]) / h
                        - (nuM + nuT[i, j]) * (divU[i + 1, j] + divU[i - 1, j] + divU[i, j + 1] + divU[i, j - 1] - 4.0 * divU[i, j]) / (h * h));

            do {
                flag = false;
                step++;
                //метод верхней релаксации 
                for (int i = 1; i < X - 1; i++)
                    for (int j = 1; j < Y - 1; j++) {
                        tmp = P[i, j];
                        P[i, j] = (1.0 - tetta)*P[i, j] +
                                  (tetta/4.0)*(P[i + 1, j] + P[i - 1, j] + P[i, j + 1] + P[i, j - 1] - h*h*A[i, j]);
                        if (Math.Abs(P[i, j] - tmp) >= eps)
                            flag = true;
                    }

                //давление на границах
                for (int i = 1; i < X - 1; i++) {
                    P[i, 0] = P[i, 1];
                    P[i, Y - 1] = P[i, Y - 2];
                }

                //давление в отверстиях
                for (int j = 0; j < Y; j++) {
                    P[0, j] = 2*P[1, j] - P[2, j];
                    P[X - 1, j] = 2*P[X - 2, j] - P[X - 3, j];
                }

                for (int i = 0; i < X; i++) {
                    if ((i >= x0) && (i < x0 + len))
                        P[i, Y - 1] = 2*P[i, Y - 2] - P[i, Y - 3];
                }
            } while (step != 10000); //(flag == true);

        }


        //расчет поля давления методом слабой сжимаемости
        private void WeakСompressibility() {
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    divU[i, j] = ((Ux[i + 1, j + 1] + Ux[i + 1, j - 1]) - (Ux[i - 1, j - 1] + Ux[i - 1, j + 1]) +
                                  (Uy[i - 1, j + 1] + Uy[i + 1, j + 1]) - (Uy[i - 1, j - 1] + Uy[i + 1, j - 1])) / (4.0 * h);

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    P[i, j] = P[i, j] - tau * 100.0 * divU[i, j];

            for (int i = 1; i < X - 1; i++) {
                P[i, 0] = P[i, 1];
                P[i, Y - 1] = P[i, Y - 1];
            }

            for (int j = 1; j < Y - 1; j++) {
                P[0, j] = P[1, j];
                P[X - 1, j] = P[X - 2, j];
            }

            for (int j = 0; j < Y; j++) {
                P[0, j] = 2 * P[1, j] - P[2, j]; //скорость 1 потока
                P[X - 1, j] = 2 * P[X - 2, j] - P[X - 3, j]; //скорость на выходе
            }

            for (int i = 0; i < X; i++) {
                if ((i >= x0) && (i < x0 + len))
                    P[i, Y - 1] = 2 * P[i, Y - 2] - P[Y - 3, i]; //скорость 2 потока
            }
        }

    }
}
