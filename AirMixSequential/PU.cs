using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirMixSequential {      
    
    public class PU  {

        private readonly double tau;
        private readonly double nuM;
        private readonly double ro;
        private readonly double h;
        private readonly double tmax;
        private readonly int X;
        private readonly int Y;
        private readonly int x0;
        private readonly int len;

        private double[,] P;
        private double[,] divU;
        private double[,] Uxn;
        private double[,] Uyn;
        private double[,] nuT;
        private double[,] Ux;
        private double[,] Uy;
        private double[,] Temp;

        private Turbulation turb;
        private Temperature temp;

        //ускорение свободного падения
        const double g = 9.8;
        //коэффициент для метода слабой сжимаемости
        const double b = 100.0;
        //коэффициент объемного расширения воздуха
        private readonly double betta = 0.003665;

        /// <summary>
        /// Метод расчета поля давления
        /// </summary>
        public enum PressureCalcMethod {
            ///<summary>ур-е Пуассона</summary>
            Poisson,

            ///<summary>метод слабой сжимаемости</summary>
            WeakСompressibility
        }

        /// <summary>
        /// Схема расчета уравнения Навье-Стокса
        /// </summary>
        public enum NavierStokesCalcMethod {
            ///<summary>явная схема</summary>
            ExplicitScheme,

            ///<summary>неявная схема</summary>
            ImplicitScheme
        }

        ///<summary>Установка параметров расчета</summary>
        /// <param name="tau">шаг по времени</param>
        /// <param name="ro">плотность</param>
        /// <param name="nuM">молекулярная вязкость</param>
        /// <param name="x0">расположение отвестия снизу от точки х0...</param>
        /// <param name="len">...длиной len</param>
        /// <param name="h">шаг по сетке</param>
        /// <param name="X">число точек по оси Х</param>
        /// <param name="Y">число точек по оси У</param>
        /// <param name="Ux">скорости Ux</param>
        /// <param name="Uy">скорости Uy</param>
        /// <param name="Temp">температура</param>
        public PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y, double[,] Ux, double[,] Uy,
            double[,] Temp) {

            this.tau = tau;
            this.nuM = nuM;
            this.ro = ro;
            this.h = h;
            this.X = X;
            this.Y = Y;
            this.x0 = x0;
            this.len = len;
            this.Ux = Ux;
            this.Uy = Uy;

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

            turb = new Turbulation(X, Y, h, tau, nuM);
            temp = new Temperature(tau, nuM, x0, len, h, X, Y, Ux, Uy, Temp, nuT);
        }

        ///<summary>Расчет поля скоростей</summary>
        /// <param name="pcm">метод расчета поля давления</param>
        /// <param name="nscm">схема расчета уравнения Навье-Стокса</param>
        /// <param name="tm">модель турбулентности (turbulenceModel = 0 если турбулентность не расчитывается)</param>
        /// <param name="tmax">время расчета</param>
        public void Calculation(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, TurbulenceModel tm, double tmax) {
            double t = 0;
            do {
                if (tm != 0) //turbulenceModel = 0 если турбулентность не расчитывается
                    nuT = turb.Calculate(tm, Ux, Uy);

                switch (pcm) {
                    case PressureCalcMethod.Poisson:
                        Poisson();
                        break;
                    case PressureCalcMethod.WeakСompressibility:
                        WeakСompressibility();
                        break;
                }

                Temp = temp.CalcTemp();
                switch (nscm) {
                    case NavierStokesCalcMethod.ExplicitScheme:
                        ExplicitSpeeds();
                        break;
                    case NavierStokesCalcMethod.ImplicitScheme:
                        ImplicitSpeeds();
                        break;
                }            
               
                t += tau;
            } while (t <= tmax);
        }

        //расчет поля скоростей
        private void ExplicitSpeeds() {
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
                                                (nuM + nuT[i, j])*
                                                (Ux[i + 1, j] + Ux[i - 1, j] + Ux[i, j + 1] + Ux[i, j - 1] - 4*Ux[i, j])/
                                                (h*h)
                                                - g*betta*Temp[i, j]);

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
                                                (nuM + nuT[i, j])*
                                                (Uy[i + 1, j] + Uy[i - 1, j] + Uy[i, j + 1] + Uy[i, j - 1] - 4*Uy[i, j])/
                                                (h*h)
                                                - g*betta*Temp[i, j]);


            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Ux[i, j] = Uxn[i, j];

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Uy[i, j] = Uyn[i, j];
        }

        //расчет поля давления с помощью уравнения Пуассона
        private void Poisson() {
            const double tauP = 0.001; //шаг уравнения Пуассона ???
            const double eps = 0.01; //допустимая погрешность ???
            const double tetta = 1.85; //для метода верхней релаксации
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
                                  (Uy[i - 1, j - 1] + Uy[i + 1, j - 1]))/(4.0*h);

            //вычисление правой части в уравнении Пуассона
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    A[i, j] = -ro*(
                        Math.Pow((Ux[i + 1, j] - Ux[i - 1, j])/(2.0*h), 2.0) + Math.Pow((Uy[i, j + 1] - Uy[i, j - 1])/(2.0*h), 2.0)
                        + (Ux[i, j+1] - Ux[i, j-1])*(Uy[i+1, j] - Uy[i-1, j])/(2.0*h*h) - divU[i, j]/tauP
                        + (Ux[i, j] + Math.Abs(Ux[i, j]))/2.0*(divU[i, j] - divU[i - 1, j])/h
                        + (Ux[i, j] - Math.Abs(Ux[i, j]))/2.0*(divU[i + 1, j] - divU[i, j])/h
                        + (Uy[i, j] + Math.Abs(Uy[i, j]))/2.0*(divU[i, j] - divU[i, j - 1])/h
                        + (Uy[i, j] - Math.Abs(Uy[i, j]))/2.0*(divU[i, j + 1] - divU[i, j])/h
                        -
                        (nuM + nuT[i, j])*
                        (divU[i + 1, j] + divU[i - 1, j] + divU[i, j + 1] + divU[i, j - 1] - 4.0*divU[i, j])/(h*h));

            do {
                flag = false;
                step++;
                //метод верхней релаксации 
                for (int i = 1; i < X - 1; i++)
                    for (int j = 1; j < Y - 1; j++) {                   
                        tmp = (1.0 - tetta)*P[i, j] +
                                  (tetta/4.0)*(P[i + 1, j] + P[i - 1, j] + P[i, j + 1] + P[i, j - 1] - h*h*A[i, j]);
                        if (Math.Abs(tmp - P[i, j]) >= eps)
                            flag = true;
                        P[i, j] = tmp;
                    }

                //давление на границах
                for (int i = 1; i < X - 1; i++) {
                    P[i, 0] = P[i, 1];
                }

                //давление в отверстиях
                for (int j = 0; j < Y; j++) {
                    P[0, j] = 2*P[1, j] - P[2, j];
                    P[X - 1, j] = 2*P[X - 2, j] - P[X - 3, j];
                }

                for (int i = 1; i < X - 1; i++) {
                    if ((i >= x0) && (i < x0 + len))
                        P[i, Y - 1] = 2*P[i, Y - 2] - P[i, Y - 3];
                    else
                        P[i, Y - 1] = P[i, Y - 2];
                }
            } while (flag); //(flag); 
        }

        //расчет поля давления методом слабой сжимаемости
        private void WeakСompressibility() {
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    divU[i, j] = ((Ux[i + 1, j + 1] + Ux[i + 1, j - 1]) - (Ux[i - 1, j - 1] + Ux[i - 1, j + 1]) +
                                  (Uy[i - 1, j + 1] + Uy[i + 1, j + 1]) - (Uy[i - 1, j - 1] + Uy[i + 1, j - 1]))/(4.0*h);

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    P[i, j] = P[i, j] - tau*b*divU[i, j];

            //давление на горизонтальных границах
            for (int i = 0; i < X; i++) {
                P[i, 0] = P[i, 1];
                P[i, Y - 1] = P[i, Y - 2];
            }

            for (int j = 0; j < Y; j++) {
                P[0, j] = 2*P[1, j] - P[2, j]; //1 поток
                P[X - 1, j] = 2*P[X - 2, j] - P[X - 3, j]; // выход
            }

            for (int i = 0; i < X; i++) {
                if ((i >= x0) && (i <= x0 + len))
                    P[i, Y - 1] = 2*P[i, Y - 2] - P[i, Y - 3]; //2 поток
            }
        }

        private void SweepMethod(int k, double[,] Ux1, double [,] Uy1)  {
             
            double[][] L = new double[2][];
            L[0] = new double[X];
            L[1] = new double[Y];
            double[][] M = new double[2][];
            M[0] = new double[X];
            M[1] = new double[Y];
            double a, b, c, f;


            if (k == 1) {
                //по всем строкам расчетной области
                for (int j = 1; j < Y - 1; j++) {
                    
                    //находим прогоночные коэффициенты слева направо
                    for (int i = 1; i < X - 1; i++) {

                        //начальные значения для прогоночных коэффициентов
                        a = -tau * (Ux[i, j] + Math.Abs(Ux[i, j]) / (2 * h) - (nuM + nuT[i, j]) * tau / (h * h));
                        b = 1 + tau * Math.Abs(Ux[i, j]) / h + 2 * (nuM + nuT[i, j]) * tau / (h * h);
                        c = tau * (Ux[i, j] - Math.Abs(Ux[i, j]) / (2 * h) - (nuM + nuT[i, j]) * tau / (h * h));
                        f = Ux[i, j];
                        L[0][i] = 0;
                        M[0][i] = Ux[0, j];
                
                        L[0][i + 1] = -c / (a * L[0][i] + b);
                        M[0][i + 1] = (f - a * M[0][i]) / (a * L[0][i] + b);
                    }

                    //находим значения скоростей справа налево
                    for (int i = X - 2; i > 0; i--) {
                        Ux1[i, j] = L[0][i + 1] * Ux1[i + 1, j] + M[0][i + 1];
                    }
                }
            }

            if (k == 2) {
                //по всем столбцам расчетной области
                for (int i = 1; i < X - 1; i++) {

                    //находим прогоночные коэффициенты сверху внизу
                    for (int j = 1; j < Y - 1; j++) {

                        //начальные значения для прогоночных коэффициентов
                        a = -tau * (Ux[i, j] + Math.Abs(Ux[i, j]) / (2 * h) - (nuM + nuT[i, j]) * tau / (h * h));
                        b = 1 + tau * Math.Abs(Ux[i, j]) / h + 2 * (nuM + nuT[i, j]) * tau / (h * h);
                        c = tau * (Ux[i, j] - Math.Abs(Ux[i, j]) / (2 * h) - (nuM + nuT[i, j]) * tau / (h * h));
                        f = Ux[i, j];
                        L[1][j] = 0;
                        M[1][j] = Ux[i, Y-1];

                        L[1][j + 1] = -c / (a * L[1][j] + b);
                        M[1][j + 1] = (f - a * M[1][j]) / (a * L[1][j] + b);
                    }

                    //находим значения скоростей справа налево
                    for (int j = Y - 2; j > 0; j--) {
                        Ux1[i, j] = L[1][j + 1] * Ux1[i, j+1] + M[1][j + 1];
                    }
                }

            }
        }

        //метод расщепления и прогонки
        private void ImplicitSpeeds() {
            double[,] Ux1 = new double[X, Y];
            double[,] Uy1 = new double[X, Y];

            for (int j = 0; j < Y; j++)
                Ux1[X - 1, j] = Ux[X - 1, j];

            for (int i = 0; i < X; i++)
                Uy1[i, Y - 1] = Uy[i, Y - 1];
            
            //метод расщепления 1 этап
            SweepMethod(1,Ux1,Uy1);      
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Ux1[i, j] = Ux[i, j] - tau * (-(Ux[i, j] + Math.Abs(Ux[i, j])) / 2.0 * (Ux1[i, j] - Ux1[i - 1, j]) / h
                                                - (Ux[i, j] - Math.Abs(Ux[i, j])) / 2.0 * (Ux1[i + 1, j] - Ux1[i, j]) / h
                                                + (nuM + nuT[i, j]) * (Ux1[i + 1, j] - 2 * Ux1[i, j] + Ux1[i - 1, j]) / (h * h));
          
            
            SweepMethod(1, Uy1,Ux1);  
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Uy1[i, j] = Uy[i, j] - tau * (-(Ux[i, j] + Math.Abs(Ux[i, j])) / 2.0 * (Uy1[i, j] - Uy1[i - 1, j]) / h
                                                - (Ux[i, j] - Math.Abs(Ux[i, j])) / 2.0 * (Uy1[i + 1, j] - Uy1[i, j]) / h
                                                + (nuM + nuT[i, j]) * (Uy1[i + 1, j] - 2 * Uy1[i, j] + Uy1[i - 1, j]) / (h * h));


            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++) {
                    Ux[i, j] = Ux1[i, j];
                    Uy[i, j] = Uy1[i, j];
                }
                    

            //метод расщепления 2 этап
            SweepMethod(2, Ux1, Uy1);   
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Ux1[i, j] = Ux[i, j] - tau * (-(Uy[i, j] + Math.Abs(Uy[i, j])) / 2.0 * (Ux1[i, j] - Ux1[i, j - 1]) / h
                                                 - (Uy[i, j] - Math.Abs(Uy[i, j])) / 2.0 * (Ux1[i, j + 1] - Ux1[i, j]) / h
                                                 + (nuM + nuT[i, j]) * (Ux1[i, j + 1] - 2 * Ux1[i, j] + Ux1[i, j - 1]) / (h * h));

            SweepMethod(2, Uy1, Ux1);  
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Uy1[i, j] = Uy[i, j] - tau * (-(Uy[i, j] + Math.Abs(Uy[i, j])) / 2.0 * (Uy1[i, j] - Uy1[i, j - 1]) / h
                                                 - (Uy[i, j] - Math.Abs(Uy[i, j])) / 2.0 * (Uy1[i, j + 1] - Uy1[i, j]) / h
                                                 + (nuM + nuT[i, j]) * (Uy1[i, j + 1] - 2 * Uy1[i, j] + Uy1[i, j - 1]) / (h * h));


           for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++) {
                    Ux[i, j] = Ux1[i, j];
                    Uy[i, j] = Uy1[i, j];
                }
                    

            //метод расщепления 3 этап
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Ux[i, j] = Ux[i, j] -
                                tau * ((P[i - 1, j + 1] + P[i + 1, j + 1] - P[i - 1, j - 1] - P[i + 1, j - 1]) / (4 * h * ro));

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Uy[i, j] = Uy[i, j] -
                                tau * ((P[i - 1, j + 1] + P[i + 1, j + 1] - P[i - 1, j - 1] - P[i + 1, j - 1]) / (4 * h * ro));

        }
    }
}
