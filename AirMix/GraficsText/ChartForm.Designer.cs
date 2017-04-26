namespace AirMix.GraficsText {
    partial class ChartForm {
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
            this.components = new System.ComponentModel.Container();
            this.zgcChart = new ZedGraph.ZedGraphControl();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zgcChart
            // 
            this.zgcChart.Location = new System.Drawing.Point(13, 13);
            this.zgcChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zgcChart.Name = "zgcChart";
            this.zgcChart.ScrollGrace = 0D;
            this.zgcChart.ScrollMaxX = 0D;
            this.zgcChart.ScrollMaxY = 0D;
            this.zgcChart.ScrollMaxY2 = 0D;
            this.zgcChart.ScrollMinX = 0D;
            this.zgcChart.ScrollMinY = 0D;
            this.zgcChart.ScrollMinY2 = 0D;
            this.zgcChart.Size = new System.Drawing.Size(794, 505);
            this.zgcChart.TabIndex = 0;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFile.Location = new System.Drawing.Point(658, 525);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(152, 39);
            this.btnSaveFile.TabIndex = 2;
            this.btnSaveFile.Text = "Сохранить в файл";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 572);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.zgcChart);
            this.Name = "ChartForm";
            this.Text = "Нагрузочное тестирование";
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgcChart;
        private System.Windows.Forms.Button btnSaveFile;
    }
}