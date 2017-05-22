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
        private bool temp;
        private bool speeds;
        private int x0;
        private int len;

        public GraphicsForm(int X, int Y, int x0, int len, bool speeds, bool temp, int scale = 50) {
            this.X = X;
            this.Y = Y;
            this.scale = scale;
            this.temp = temp;
            this.speeds = speeds;
            this.x0 = x0;
            this.len = len;
            InitializeComponent();
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            ClientSize = new Size(X*scale + 60, Y*scale + 60);
            MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 100);
            bmp = new Bitmap(X*scale + 30, Y*scale);
            pen = new Pen(Color.Black);
            g = Graphics.FromImage(bmp);
            if (speeds) {
                pbImage.Visible = false;
            }

            if (temp) {
                ClientSize = new Size(X*scale + 180, Y*scale + 60);
                pbImage.Visible = true;
                pbImage.Image = Image.FromFile("Temp.jpg");
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
            pb.Image = bmp; 
        }

        public bool DrawDisplay(double[,] Ux, double[,] Uy, double[,] Temp) {
            g.Clear(Color.White);
            DoubleBuffered = true;
            pen.Color = Color.Black;
            pen.Width = 4.0f;
            g.DrawLine(pen, 0, 1, (X - 1)*scale, 1);
            g.DrawLine(pen, 0, (Y - 1)*scale, x0*scale, (Y - 1)*scale);
            g.DrawLine(pen, (x0 + len)*scale, (Y - 1)*scale, (X - 1)*scale, (Y - 1)*scale);

            bool error = false;
            if (speeds) {
                error = DrawSpeeds(Ux, Uy);
            }

            if (temp) {
                DrawTemp(Temp);           
            }        
            pb.Refresh();         
            return error;
        }

        void DrawTemp(double[,] Temp) {
            double minTemp = 0;
            double maxTemp = 100;
            float gridX = scale;
            float gridY = scale;
            Color color;
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++) {
                    double D = maxTemp - minTemp;
                    double x = Temp[i, j] - minTemp;
                    /* int red = (int)Math.Max(Math.Min(1020 * x / D - 255, 255), 0);
                    int green = (int) Math.Max(Math.Min(Math.Min(1020*x/D, 510 - 510*x/D), 255), 0);
                    int blue = (int) Math.Max(Math.Min(255 - 1020*x/D, 255), 0);*/
                    color = Color.FromArgb(0, 0, 255);
                    int pos = (int) (x/D*100);
                    if (pos <= 0)
                        color = Color.FromArgb(0, 0, 255);
                    if (pos > 0 && pos < 25) {
                        int green = (int) ((double) (pos - 0)/(25 - 0)*255.0);
                        color = Color.FromArgb(0, green, 255);
                    }
                    if (pos == 25)
                        color = Color.FromArgb(0, 255, 255);
                    if (pos > 25 && pos < 50) {
                        int blue = (int) ((double) (pos - 25)/(50 - 25)*255.0);
                        color = Color.FromArgb(0, 255, 255 - blue);
                    }
                    if (pos == 50)
                        color = Color.FromArgb(0, 255, 0);
                    if (pos > 50 && pos < 75) {
                        int red = (int) ((double) (pos - 50)/(75 - 50)*255.0);
                        color = Color.FromArgb(red, 255, 0);
                    }
                    if (pos == 75)
                        color = Color.FromArgb(255, 255, 0);
                    if (pos > 75 && pos < 100) {
                        int green = (int) ((double) (pos - 75)/(100 - 75)*255.0);
                        color = Color.FromArgb(255, 255 - green, 0);
                    }
                    if (pos >= 100)
                        color = Color.FromArgb(255, 0, 0);

                    //color = Color.FromArgb(red, green, blue); 

                    using (Brush br = new SolidBrush(color))
                         g.FillRectangle(br, gridX*i, gridY*j, gridX, gridY);
                }
        }

        bool DrawSpeeds(double[,] Ux, double[,] Uy) {
            float xo, yo, y1, x1, yn, xn;
            int xa, ya, v, xb, yb;
            int w = 5, h = 10;
            float L;

            for (int j = 0; j < Y; j++)
                for (int i = 0; i < X; i++) {
                    try {
                        xa = i*scale;
                        ya = j*scale;
                        xb = (int) ((xa + Ux[i, j]*scale));
                        yb = (int) ((ya + Uy[i, j]*scale));
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
            return false;
        }
    }
}
