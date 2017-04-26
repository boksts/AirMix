namespace AirMix.Grafics {
    partial class OutputForm {
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
            this.tbc = new System.Windows.Forms.TabControl();
            this.tpUx = new System.Windows.Forms.TabPage();
            this.rtbUx = new System.Windows.Forms.RichTextBox();
            this.tpUy = new System.Windows.Forms.TabPage();
            this.btnSaveFileUy = new System.Windows.Forms.Button();
            this.rtbUy = new System.Windows.Forms.RichTextBox();
            this.sfdSpeeds = new System.Windows.Forms.SaveFileDialog();
            this.tbc.SuspendLayout();
            this.tpUx.SuspendLayout();
            this.tpUy.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbc
            // 
            this.tbc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbc.Controls.Add(this.tpUx);
            this.tbc.Controls.Add(this.tpUy);
            this.tbc.Location = new System.Drawing.Point(12, 12);
            this.tbc.Name = "tbc";
            this.tbc.SelectedIndex = 0;
            this.tbc.Size = new System.Drawing.Size(843, 497);
            this.tbc.TabIndex = 0;
            // 
            // tpUx
            // 
            this.tpUx.Controls.Add(this.rtbUx);
            this.tpUx.Location = new System.Drawing.Point(4, 25);
            this.tpUx.Name = "tpUx";
            this.tpUx.Padding = new System.Windows.Forms.Padding(3);
            this.tpUx.Size = new System.Drawing.Size(835, 468);
            this.tpUx.TabIndex = 0;
            this.tpUx.Text = "Скорости Ux";
            this.tpUx.UseVisualStyleBackColor = true;
            // 
            // rtbUx
            // 
            this.rtbUx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbUx.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbUx.Location = new System.Drawing.Point(6, 6);
            this.rtbUx.Name = "rtbUx";
            this.rtbUx.ReadOnly = true;
            this.rtbUx.Size = new System.Drawing.Size(823, 411);
            this.rtbUx.TabIndex = 0;
            this.rtbUx.Text = "";
            this.rtbUx.WordWrap = false;
            // 
            // tpUy
            // 
            this.tpUy.Controls.Add(this.btnSaveFileUy);
            this.tpUy.Controls.Add(this.rtbUy);
            this.tpUy.Location = new System.Drawing.Point(4, 25);
            this.tpUy.Name = "tpUy";
            this.tpUy.Padding = new System.Windows.Forms.Padding(3);
            this.tpUy.Size = new System.Drawing.Size(835, 468);
            this.tpUy.TabIndex = 1;
            this.tpUy.Text = "Скорости Uy";
            this.tpUy.UseVisualStyleBackColor = true;
            // 
            // btnSaveFileUy
            // 
            this.btnSaveFileUy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFileUy.Location = new System.Drawing.Point(677, 423);
            this.btnSaveFileUy.Name = "btnSaveFileUy";
            this.btnSaveFileUy.Size = new System.Drawing.Size(152, 39);
            this.btnSaveFileUy.TabIndex = 2;
            this.btnSaveFileUy.Text = "Сохранить в файл";
            this.btnSaveFileUy.UseVisualStyleBackColor = true;
            this.btnSaveFileUy.Click += new System.EventHandler(this.btnSaveFileUy_Click);
            // 
            // rtbUy
            // 
            this.rtbUy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbUy.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbUy.Location = new System.Drawing.Point(6, 6);
            this.rtbUy.Name = "rtbUy";
            this.rtbUy.ReadOnly = true;
            this.rtbUy.Size = new System.Drawing.Size(823, 411);
            this.rtbUy.TabIndex = 1;
            this.rtbUy.Text = "";
            this.rtbUy.WordWrap = false;
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 521);
            this.Controls.Add(this.tbc);
            this.Name = "OutputForm";
            this.Text = "Значения скоростей";
            this.tbc.ResumeLayout(false);
            this.tpUx.ResumeLayout(false);
            this.tpUy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbc;
        private System.Windows.Forms.TabPage tpUx;
        private System.Windows.Forms.RichTextBox rtbUx;
        private System.Windows.Forms.TabPage tpUy;
        private System.Windows.Forms.Button btnSaveFileUy;
        private System.Windows.Forms.RichTextBox rtbUy;
        private System.Windows.Forms.SaveFileDialog sfdSpeeds;
    }
}