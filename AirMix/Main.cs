using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirMix.Grafics;
using AirMix.GraficsText;
using ZedGraph;


namespace AirMix {
   partial class Main : Form {
       private double[,] Ux;
       private double[,] Uy;
       private double[,] Temp;
       private int X;
       private int Y;
       private int len;
       private int x0;
       private double h;
       private double tau;
       private double ro;
       private double nuM;
       private double tmax;
       private int scale;

       private AirMixSequential.PU pu;
       private AirMixSequential.WPsi wpsi;
       private AirMixParallel.PU parPU;
       private AirMixParallel.WPsi parWPsi;

       private GraphicsForm grForm;
       private OutputForm outForm;

       private double SizeX { get; set; }
       private double SizeY { get; set; }

       object pressureCalcMethod;
       object navierStokesCalcMethod;

       object helmholtzCalcMethod;
       object turbulenceModel;

       public Main() {
           InitializeComponent();
           pbImage.Image = Image.FromFile("Image.jpg");
       }

       private void btnCalculate_Click(object sender, EventArgs e) {
           if (rbModeling.Checked)
               Modeling();
           if (rbStressTesting.Checked)
               StressTesting();
       }

       //метод при закрытии формы с графикой
       void grForm_Closing(object sender, CancelEventArgs e) {
           if (bgw.IsBusy)
               switch (MessageBox.Show("Прекратить расчет и закрыть форму?", "", MessageBoxButtons.YesNo)) {
                   case DialogResult.Yes: {
                       bgw.CancelAsync();
                       break;
                   }

                   case DialogResult.No:
                       e.Cancel = true;
                       break;
               }
       }

       //если произведена отмена
       private void btnCancel_Click(object sender, EventArgs e) {
           bgw.CancelAsync();
       }

       //-----------BackgroundWorker---------------------------------
       private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
           if (rbModeling.Checked) {
               pgb.Value = e.ProgressPercentage;
                lblPrc.Text = "Расчет выполнен на " + pgb.Value + "%";
           }
           if (rbStressTesting.Checked || rbSetSpeeds.Checked) {
               pgb.Style = ProgressBarStyle.Marquee;
               lblPrc.Text = "Расчеты выполняются ...";
           }
       }
       private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
           string str = e.Cancelled ? "Расчет отменен!" : "Расчет завершен!";
           lblPrc.Text = "";
           pgb.Value = 0;
           pgb.Style = ProgressBarStyle.Blocks;
           btnCancel.Enabled = false;
           btnCalculate.Enabled = true;
           MessageBox.Show(str);
        
       }
       private void bgw_DoWork(object sender, DoWorkEventArgs e) {
           if (rbModeling.Checked) {
               if (rbSeq.Checked)
                   CalculationSeq(sender, e);
               if (rbCUDA.Checked || rbOpenMP.Checked)
                   CalculationParallel(sender, e);
           }
           if (rbStressTesting.Checked) {
               StressTestingCalculate(sender, e);
           }
           
       }

       //===================================================================



       private void rbTurbNO_CheckedChanged(object sender, EventArgs e) {
           if (!rbMissingTurb.Checked)
                tbNuM.Text = Convert.ToString(0.0000151);
           else
               tbNuM.Text = Convert.ToString(1.0);
       }

       private void rbPU_CheckedChanged(object sender, EventArgs e) {
           gbPU.Enabled = rbPU.Checked;
       }

       private void rbWPsi_CheckedChanged(object sender, EventArgs e) {
           gbWPsi.Enabled = rbWPsi.Checked;
       }

       private void rbStressTesting_CheckedChanged(object sender, EventArgs e) {
           gbStressTesting.Enabled = rbStressTesting.Checked;
           gbModeling.Enabled = gbOutput.Enabled = !rbStressTesting.Checked;
           tbH.Text = "0.1";
           tbH.Enabled = !rbStressTesting.Checked;
           rbTimeSpeeds.Checked = true;
           rbSetSpeeds.Enabled = !rbStressTesting.Checked;
       }

       private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
           bgw.CancelAsync();
       }

       private void сbGraphicsResult_CheckedChanged(object sender, EventArgs e) {
           nudScale.Enabled = сbGraphicsResult.Checked;
       }

       private void сbGraphicsProcess_CheckedChanged(object sender, EventArgs e) {
           nudScale.Enabled = сbGraphicsProcess.Checked;
       }

       private void rbTimeSpeeds_CheckedChanged(object sender, EventArgs e) {
           tbTimeMax.Enabled = rbTimeSpeeds.Checked;
       }
    
    
   }
}
