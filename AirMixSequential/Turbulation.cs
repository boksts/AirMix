using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirMixSequential {
    /// <summary>
    /// Модель турбулентности
    /// </summary>
    public enum TurbulenceModel {
        ///<summary>Секундова модель</summary>
        Secundova = 1,
        ///<summary>К-Е модель</summary>
        KE = 2
    }

    class Turbulation {

        /*для модели Секундова*/
        private const double Betta = 0.06;
        private const double Gamma = 50.0;
        private const double Lmin = 1.0;
        private const double Hi = 2.0;
        /*==========================*/

        /*для модели K-E*/
        private const double cMu = 0.09;
        private const double c1 = 1.44;
        private const double c2 = 1.92;
        private const double cK = 0.2;
        private const double cE = 0.2;
        private const double sK = 1.0;
        private const double sE = 1.3;
        private double[,] K;
        private double[,] Kn;
        private double[,] E;
        private double[,] En;
        private double[,] Sk;
        private double[,] Se;
        /*============================*/

        private readonly double h;
        private readonly double tau;
        private readonly int X;
        private readonly int Y;
        private readonly double nuM;

        private double[,] D; //дивергенция
        private double[,] nuTn; //турбулентная вязкость
        private double[,] nuT; //турбулентная вязкость

        //вспомогательная функция для модели Секундова
        private double funcTurb(double z) {
            return 0.2*(z*z + 1.47*z + 0.2)/(z*z - 1.47*z + 1.0);
        }

        public Turbulation(int X, int Y, double h, double tau, double nuM) {
            this.h = h;
            this.tau = tau;
            this.X = X;
            this.Y = Y;
            this.nuM = nuM;
            nuTn = new double[X, Y];
            nuT = new double[X, Y];
            D = new double[X, Y];
            K = new double[X, Y];
            Kn = new double[X, Y];
            E = new double[X, Y];
            En = new double[X, Y];
            Sk = new double[X, Y];
            Se = new double[X, Y];

          //начальная турбулентная вязкость
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    nuT[i, j] = 1000*nuM;

            for (int j = 0; j < Y; j++) {
                //на входе слева
                nuT[0, j] = 0;
                //на выходе cчитается по ходу расчета
            }
        }

        private void Secundova(double[,] Ux, double[,] Uy) {

            //граниичное условие на выходе
            for (int j = 0; j < Y; j++) {
                nuT[X - 1, j] = nuT[X - 2, j];
            }

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    D[i,j] = Math.Sqrt(2.0*Math.Pow((Ux[i + 1, j] - Ux[i - 1, j])/(2.0*h), 2.0)
                                  + 2.0*Math.Pow((Uy[i, j + 1] - Ux[i, j - 1])/(2.0*h), 2.0)
                                  + Math.Pow((Ux[i, j + 1] - Ux[i, j - 1] + Uy[i + 1, j] - Uy[i - 1, j])/(2.0*h), 2.0));

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    nuTn[i, j] = nuT[i, j] + tau*(
                        -Ux[i, j]*(nuT[i + 1, j] - nuT[i - 1, j])/(2.0*h) -
                        Uy[i, j]*(nuT[i, j + 1] - nuT[i, j - 1])/(2.0*h)
                        +
                        Hi*
                        (Math.Pow((nuT[i + 1, j] - nuT[i - 1, j])/(2.0*h), 2.0) +
                         Math.Pow((nuT[i, j + 1] - nuT[i, j - 1])/(2.0*h), 2.0))
                        +
                        (nuM + Betta*nuT[i, j])*
                        (nuT[i + 1, j] + nuT[i - 1, j] + nuT[i, j + 1] + nuT[i, j - 1] - 4.0*nuT[i, j])/(h*h)
                        + nuT[i, j]*funcTurb(nuT[i, j]/(8.0*nuM))*D[i,j] -
                        Gamma*Math.Pow(Lmin, -2.0)*(nuM + Betta*nuT[i, j])*nuT[i, j]);

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    nuT[i, j] = nuTn[i, j];
        }

        private void KE(double[,] Ux, double[,] Uy) {
            //начальные условия
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    K[i, j] = cK*(Ux[i, j]*Ux[i, j] + Uy[i, j]*Uy[i, j])+0.001;

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    E[i, j] = cE*Math.Pow(K[i, j], 1.5);     
            
 
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    D[i,j] = Math.Sqrt(2.0*Math.Pow((Ux[i + 1, j] - Ux[i - 1, j])/(2.0*h), 2.0)
                                  + 2.0*Math.Pow((Uy[i, j + 1] - Ux[i, j - 1])/(2.0*h), 2.0)
                                  + Math.Pow((Ux[i, j + 1] - Ux[i, j - 1] + Uy[i + 1, j] - Uy[i - 1, j])/(2.0*h), 2.0));      

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Sk[i, j] = nuT[i, j]*D[i,j]*D[i,j] - E[i, j];

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Se[i, j] = (c1*nuT[i, j]*D[i,j]*D[i,j] - c2*E[i, j])*E[i,j]/K[i,j];

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    Kn[i, j] = K[i, j] + tau*(-(Ux[i, j] + Math.Abs(Ux[i, j]))/2.0*(K[i, j] - K[i - 1, j])/h
                                              - (Ux[i, j] - Math.Abs(Ux[i, j]))/2.0*(K[i + 1, j] - K[i, j])/h
                                              - (Uy[i, j] + Math.Abs(Uy[i, j]))/2.0*(K[i, j] - K[i, j - 1])/h
                                              - (Uy[i, j] - Math.Abs(Uy[i, j]))/2.0*(K[i, j + 1] - K[i, j])/h
                                              +
                                              (nuM + nuT[i, j])/sK*
                                              (K[i + 1, j] + K[i - 1, j] + K[i, j + 1] + K[i, j - 1] - 4.0*K[i, j])/
                                              (h*h) +Sk[i,j]);

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    En[i, j] = E[i, j] + tau * (-(Ux[i, j] + Math.Abs(Ux[i, j])) / 2.0 * (E[i, j] - E[i - 1, j]) / h
                                              - (Ux[i, j] - Math.Abs(Ux[i, j])) / 2.0 * (E[i + 1, j] - E[i, j]) / h
                                              - (Uy[i, j] + Math.Abs(Uy[i, j])) / 2.0 * (E[i, j] - E[i, j - 1]) / h
                                              - (Uy[i, j] - Math.Abs(Uy[i, j])) / 2.0 * (E[i, j + 1] - E[i, j]) / h
                                              +
                                              (nuM + nuT[i, j])/sE*
                                              (E[i + 1, j] + E[i - 1, j] + E[i, j + 1] + E[i, j - 1] - 4.0 * E[i, j]) /
                                              (h * h) + Se[i, j]);

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++) {
                    E[i, j] = En[i, j];
                    K[i, j] = Kn[i, j];
                }                
                        
             for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    nuT[i, j] = cMu*K[i, j]*K[i, j]/E[i, j];      

        }


        public double[,] Calculate(TurbulenceModel model, double[,] Ux, double[,] Uy) {
            switch (model) {
                case TurbulenceModel.Secundova:
                    Secundova(Ux, Uy);
                    break;
                case TurbulenceModel.KE:
                    KE(Ux, Uy);
                    break;
            }
            return nuT;
        }
    }
}
