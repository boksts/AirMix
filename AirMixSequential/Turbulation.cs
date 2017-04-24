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
        private const double Betta = 0.06;
        private const double Gamma = 50.0;
        private const double Lmin = 1.0;
        private const double Hi = 2.0;

        private readonly double h;
        private readonly double tau;
        private readonly int X;
        private readonly int Y;
        private readonly double nuM;

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

            //начальная турбулентная вязкость
            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    nuT[i,j] =  1000*nuM;

            for (int j = 0; j < Y; j++) {
                //на входе слева
                nuT[0,j] = 0;
                //на выходе cчитается по ходу расчета
            }
        }

        private void Secundova(double[,] Ux, double[,] Uy) {
            double D = 1.0;

            //граниичное условие на выходе
            for (int j = 0; j < Y; j++) {
                nuT[X - 1,j] = nuT[X - 2,j];
            }

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    D = Math.Sqrt(2.0 * Math.Pow((Ux[i + 1,j] - Ux[i - 1,j]) / (2.0 * h), 2.0)
                    + 2.0 * Math.Pow((Uy[i, j + 1] - Ux[i, j - 1]) / (2.0 * h), 2.0)
                    + Math.Pow((Ux[i, j + 1] - Ux[i, j - 1] + Uy[i + 1, j] - Uy[i - 1, j]) / (2.0 * h), 2.0));

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    nuTn[i, j] = nuT[i,j] + tau * (
                        -Ux[i,j] * (nuT[i + 1,j] - nuT[i - 1,j]) / (2.0 * h) - Uy[i,j] * (nuT[i,j + 1] - nuT[i,j - 1]) / (2.0 * h)
                        + Hi * (Math.Pow((nuT[i + 1, j] - nuT[i - 1, j]) / (2.0 * h), 2.0) + Math.Pow((nuT[i, j + 1] - nuT[i, j - 1]) / (2.0 * h), 2.0))
                        + (nuM + Betta * nuT[i,j]) * (nuT[i + 1,j] + nuT[i - 1,j] + nuT[i,j + 1] + nuT[i,j - 1] - 4.0 * nuT[i,j]) / (h * h)
                        + nuT[i, j] * funcTurb(nuT[i, j] / (8.0 * nuM)) * D - Gamma * Math.Pow(Lmin, -2.0) * (nuM + Betta * nuT[i, j]) * nuT[i, j]);

            for (int i = 1; i < X - 1; i++)
                for (int j = 1; j < Y - 1; j++)
                    nuT[i,j] = nuTn[i,j];

        }
        private void KE() {
            
        }

        public double[,] Calculate(TurbulenceModel model, double[,] Ux, double[,] Uy) {
            switch (model) {
                case TurbulenceModel.Secundova:
                    Secundova(Ux,Uy);
                    break;
                case TurbulenceModel.KE:
                     KE();
                    break;
            }
            return nuT;
        }
    }
}
