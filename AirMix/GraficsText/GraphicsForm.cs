using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirMix {
    public partial class GraphicsForm : Form {
        private Bitmap bmp;
        private Pen pen;
        private Graphics g;
        private int X;
        private int Y;
        private int scale; //масштаб

        public GraphicsForm(int X, int Y, int scale = 50) {
            this.X = X;
            this.Y = Y;
            this.scale = scale;
            InitializeComponent();
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            ClientSize = new Size(X*scale + 60, Y*scale + 60);
            MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 100);
            this.
            bmp = new Bitmap(X*scale+30, Y*scale+30);
            pen = new Pen(Color.Black);
            g = Graphics.FromImage(bmp);
        }


        public bool DrawDisplay(double[,] Ux, double[,] Uy, float x0, float len) {
            g.Clear(Color.White);
            DoubleBuffered = true;
            float xo, yo, y1, x1, yn, xn;
            int xa, ya, v, xb, yb;
            int w = 5, h = 10;
            float L;
            pen.Color = Color.Black;
            pen.Width = 4.0f;
            g.DrawLine(pen, 0, 1, (X - 1)*scale, 1);
            g.DrawLine(pen, 0, (Y - 1)*scale, x0*scale, (Y - 1)*scale);
            g.DrawLine(pen, (x0 + len)*scale, (Y - 1)*scale, (X - 1)*scale, (Y - 1)*scale);

            for (int j = 0; j < Y; j++)
                for (int i = 0; i < X; i++) {
                    try {
                        xa = i*scale;
                        ya = j*scale;
                        xb = (int) ((xa + Ux[i, j]*scale));
                        yb = (int) ((ya - Uy[i, j]*scale));
                        if (xa != xb || ya != yb) {
                            L = (float) Math.Sqrt(Math.Pow((xb - xa), 2.0) + Math.Pow((yb - ya), 2.0));
                            x1 = (xb - xa)/L;
                            y1 = (yb - ya)/L;
                            xo = xb - x1*h;
                            yo = yb - y1*h;
                            xn = y1;
                            yn = -x1;
                            pen.Width = 2.0f;
                            pen.Color = Color.Black;
                            g.DrawLine(pen, xa, ya, xb, yb);
                            pen.Color = Color.Blue;
                            g.DrawLine(pen, xb, yb, (xo + xn*w), (yo + yn*w));
                            g.DrawLine(pen, xb, yb, (xo - xn*w), (yo - yn*w));
                        }
                    }
                    catch (System.OverflowException) {
                        MessageBox.Show("Выход за пределы области");
                        return true;
                    }
                }
      
                
            // g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pb.Image = bmp;
            pb.Refresh();
            return false;

        }

    }
}
