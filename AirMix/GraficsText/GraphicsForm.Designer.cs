namespace AirMix {
    partial class GraphicsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pb = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(12, 12);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(169, 182);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;

            // 
            // GraphicsForm
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(439, 255);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "GraphicsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Визуализация вычислений";
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;




    }
}