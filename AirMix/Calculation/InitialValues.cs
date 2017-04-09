using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirMix {
    class InitialValues:Form1 {
        private  double UxMax;
        private double UyMax;
 
        public  void Set(double _UxMax, double _UyMax) {
            UxMax = _UxMax;
            UyMax = _UyMax;         

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
    }
}
