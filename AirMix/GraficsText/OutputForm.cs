using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirMix.Grafics {
    public partial class OutputForm : Form {
        public int X { get; set; }
        public int Y { get; set; }
        private double[,] Ux;
        private double[,] Uy;

        public OutputForm() {
            InitializeComponent();
        }

        public void OutSpeeds(double[,] Ux, double[,] Uy) {
            this.Ux = Ux;
            this.Uy = Uy;
            OutSpeedsDisplay(Ux, rtbUx);
            OutSpeedsDisplay(Uy, rtbUy);         
        }

        //вывод значений скоростей на экран
        private void OutSpeedsDisplay(double[,] mass, RichTextBox rtb) {
            string razd;
            for (int j = 0; j < Y; j++) {
                for (int i = 0; i < X; i++) {
                    razd = (mass[i, j] >= 0) ? "   " : "  ";
                    rtb.AppendText(razd + mass[i, j].ToString("f6"));
                }
                rtb.AppendText(Environment.NewLine);
            }
        }

        //запись значений скоростей в файл
        private void OutSpeedsFile(RichTextBox rtb, string descr) {
            sfdSpeeds.Filter = "Текстовый файл|*.txt";
            if (sfdSpeeds.ShowDialog() != DialogResult.OK)
                return;
            var swU = new StreamWriter(sfdSpeeds.FileName); 
            swU.WriteLine(descr);
            swU.Write(rtb.Text);
            swU.Close();
        }


        private void btnSaveFileUx_Click(object sender, EventArgs e) {
            OutSpeedsFile(rtbUx, "Скорости Ux");
        }

        private void btnSaveFileUy_Click(object sender, EventArgs e) {
            OutSpeedsFile(rtbUy, "Скорости Uy");
        }
    }
}
