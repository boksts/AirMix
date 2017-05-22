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

        public OutputForm() {
            InitializeComponent();
        }

        public void OutSpeeds(double[,] Ux, double[,] Uy) {
            OutDisplay(Ux, rtbUx);
            OutDisplay(Uy, rtbUy);
            tbc.TabPages.Remove(tpTemp);
        }

        public void OutTemp(double[,] Temp) {
            OutDisplay(Temp, rtbTemp);
            tbc.TabPages.Remove(tpUx);
            tbc.TabPages.Remove(tpUy);
        }

        //вывод значений на экран
        private void OutDisplay(double[,] mass, RichTextBox rtb) {
            string razd;
            for (int j = 0; j < Y; j++) {
                for (int i = 0; i < X; i++) {
                    razd = (mass[i, j] >= 0) ? "   " : "  ";
                    rtb.AppendText(razd + mass[i, j].ToString("f6"));
                }
                rtb.AppendText(Environment.NewLine);
            }
        }
    
        private void OutSpeedsFile(RichTextBox rtb, string descr) {
            sfd.Filter = "Текстовый файл|*.txt";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            var sw = new StreamWriter(sfd.FileName); 
            sw.WriteLine(descr);
            sw.Write(rtb.Text);
            sw.Close();
        }

        //запись значений в файл
        private void btnSaveFileUy_Click(object sender, EventArgs e) {
            if (tpUx.CanFocus)
                OutSpeedsFile(rtbUx, "Скорости Ux");
            if (tpUy.CanFocus)
                OutSpeedsFile(rtbUy, "Скорости Uy");
            if (tpTemp.CanFocus)
                OutSpeedsFile(rtbTemp, "Температура");
        }
    }
}
