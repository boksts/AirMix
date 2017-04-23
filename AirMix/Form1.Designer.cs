using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AirMix {
    partial class Form1 {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            this.btnCalculate = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbMissingTurb = new System.Windows.Forms.RadioButton();
            this.tbNuM = new System.Windows.Forms.TextBox();
            this.rbKE = new System.Windows.Forms.RadioButton();
            this.rbSecundova = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbNSEquImpScheme = new System.Windows.Forms.RadioButton();
            this.rbNSEquExpScheme = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbPoisson = new System.Windows.Forms.RadioButton();
            this.rbWeakCompress = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbHelmEquImpScheme = new System.Windows.Forms.RadioButton();
            this.rbHelmEquExpScheme = new System.Windows.Forms.RadioButton();
            this.gpInitVal = new System.Windows.Forms.GroupBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.tbRo = new System.Windows.Forms.TextBox();
            this.tbTimeMax = new System.Windows.Forms.TextBox();
            this.tbTau = new System.Windows.Forms.TextBox();
            this.tbH = new System.Windows.Forms.TextBox();
            this.tbUyMax = new System.Windows.Forms.TextBox();
            this.tbUxMax = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudScale = new System.Windows.Forms.NumericUpDown();
            this.cbGraphics = new System.Windows.Forms.CheckBox();
            this.cbTextFile = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbWPsi = new System.Windows.Forms.RadioButton();
            this.rbPU = new System.Windows.Forms.RadioButton();
            this.gbPU = new System.Windows.Forms.GroupBox();
            this.gbWPsi = new System.Windows.Forms.GroupBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.rbSeq = new System.Windows.Forms.RadioButton();
            this.rbOpenMP = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rbCUDA = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pgb = new System.Windows.Forms.ProgressBar();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblPrc = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.gpInitVal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScale)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbPU.SuspendLayout();
            this.gbWPsi.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(9, 26);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(57, 17);
            label1.TabIndex = 2;
            label1.Text = "Высота";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(9, 55);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 17);
            label2.TabIndex = 3;
            label2.Text = "Ширина";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(23, 94);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(145, 17);
            label3.TabIndex = 6;
            label3.Text = "Скорость потока №1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(236, 94);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(145, 17);
            label4.TabIndex = 7;
            label4.Text = "Скорость потока №2";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(334, 24);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(95, 17);
            label5.TabIndex = 9;
            label5.Text = "Шаг сетки (h)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(147, 23);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(126, 17);
            label6.TabIndex = 11;
            label6.Text = "Шаг времени (tau)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(160, 52);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(108, 17);
            label7.TabIndex = 13;
            label7.Text = "Время расчета";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(532, 26);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(0, 17);
            label9.TabIndex = 15;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label10.Location = new System.Drawing.Point(4, 76);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(167, 17);
            label10.TabIndex = 17;
            label10.Text = "Молекулярная вязкость";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(346, 51);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(78, 17);
            label11.TabIndex = 21;
            label11.Text = "Плотность";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalculate.Location = new System.Drawing.Point(24, 119);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(172, 39);
            this.btnCalculate.TabIndex = 0;
            this.btnCalculate.Text = "Начать расчет";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbMissingTurb);
            this.groupBox5.Controls.Add(label10);
            this.groupBox5.Controls.Add(this.tbNuM);
            this.groupBox5.Controls.Add(this.rbKE);
            this.groupBox5.Controls.Add(this.rbSecundova);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox5.Location = new System.Drawing.Point(317, 164);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(284, 103);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Модель турбулентности";
            // 
            // rbMissingTurb
            // 
            this.rbMissingTurb.AutoSize = true;
            this.rbMissingTurb.Checked = true;
            this.rbMissingTurb.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbMissingTurb.Location = new System.Drawing.Point(7, 21);
            this.rbMissingTurb.Name = "rbMissingTurb";
            this.rbMissingTurb.Size = new System.Drawing.Size(114, 21);
            this.rbMissingTurb.TabIndex = 18;
            this.rbMissingTurb.TabStop = true;
            this.rbMissingTurb.Text = "Без моделей";
            this.rbMissingTurb.UseVisualStyleBackColor = true;
            this.rbMissingTurb.CheckedChanged += new System.EventHandler(this.rbTurbNO_CheckedChanged);
            // 
            // tbNuM
            // 
            this.tbNuM.Enabled = false;
            this.tbNuM.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNuM.Location = new System.Drawing.Point(177, 76);
            this.tbNuM.Name = "tbNuM";
            this.tbNuM.Size = new System.Drawing.Size(71, 22);
            this.tbNuM.TabIndex = 16;
            this.tbNuM.Text = "0.0000151";
            // 
            // rbKE
            // 
            this.rbKE.AutoSize = true;
            this.rbKE.Enabled = false;
            this.rbKE.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbKE.Location = new System.Drawing.Point(113, 48);
            this.rbKE.Name = "rbKE";
            this.rbKE.Size = new System.Drawing.Size(52, 21);
            this.rbKE.TabIndex = 1;
            this.rbKE.Text = "K-E";
            this.toolTip.SetToolTip(this.rbKE, "Методом верхней релаксации");
            this.rbKE.UseVisualStyleBackColor = true;
            // 
            // rbSecundova
            // 
            this.rbSecundova.AutoSize = true;
            this.rbSecundova.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbSecundova.Location = new System.Drawing.Point(6, 48);
            this.rbSecundova.Name = "rbSecundova";
            this.rbSecundova.Size = new System.Drawing.Size(99, 21);
            this.rbSecundova.TabIndex = 0;
            this.rbSecundova.Text = "Секундова";
            this.rbSecundova.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbNSEquImpScheme);
            this.groupBox4.Controls.Add(this.rbNSEquExpScheme);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(6, 107);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(272, 84);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Метод решения ур. Навье-Стокса";
            // 
            // rbNSEquImpScheme
            // 
            this.rbNSEquImpScheme.AutoSize = true;
            this.rbNSEquImpScheme.Enabled = false;
            this.rbNSEquImpScheme.Location = new System.Drawing.Point(6, 48);
            this.rbNSEquImpScheme.Name = "rbNSEquImpScheme";
            this.rbNSEquImpScheme.Size = new System.Drawing.Size(126, 21);
            this.rbNSEquImpScheme.TabIndex = 1;
            this.rbNSEquImpScheme.Text = "неявная схема";
            this.toolTip.SetToolTip(this.rbNSEquImpScheme, "Неявная схема (метод переменных напрвлений и расщепления) с противоточными произв" +
        "одными");
            this.rbNSEquImpScheme.UseVisualStyleBackColor = true;
            // 
            // rbNSEquExpScheme
            // 
            this.rbNSEquExpScheme.AutoSize = true;
            this.rbNSEquExpScheme.Checked = true;
            this.rbNSEquExpScheme.Location = new System.Drawing.Point(6, 21);
            this.rbNSEquExpScheme.Name = "rbNSEquExpScheme";
            this.rbNSEquExpScheme.Size = new System.Drawing.Size(110, 21);
            this.rbNSEquExpScheme.TabIndex = 0;
            this.rbNSEquExpScheme.TabStop = true;
            this.rbNSEquExpScheme.Text = "явная схема";
            this.toolTip.SetToolTip(this.rbNSEquExpScheme, "Явная схема с противоточными производными");
            this.rbNSEquExpScheme.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Controls.Add(this.rbPoisson);
            this.groupBox3.Controls.Add(this.rbWeakCompress);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(6, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(272, 82);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Метод расчета давления";
            // 
            // rbPoisson
            // 
            this.rbPoisson.AutoSize = true;
            this.rbPoisson.Location = new System.Drawing.Point(6, 48);
            this.rbPoisson.Name = "rbPoisson";
            this.rbPoisson.Size = new System.Drawing.Size(242, 21);
            this.rbPoisson.TabIndex = 1;
            this.rbPoisson.Text = "с помощью уравнения Пуассона";
            this.toolTip.SetToolTip(this.rbPoisson, "Методом верхней релаксации");
            this.rbPoisson.UseVisualStyleBackColor = true;
            // 
            // rbWeakCompress
            // 
            this.rbWeakCompress.AutoSize = true;
            this.rbWeakCompress.Checked = true;
            this.rbWeakCompress.Location = new System.Drawing.Point(6, 21);
            this.rbWeakCompress.Name = "rbWeakCompress";
            this.rbWeakCompress.Size = new System.Drawing.Size(168, 21);
            this.rbWeakCompress.TabIndex = 0;
            this.rbWeakCompress.TabStop = true;
            this.rbWeakCompress.Text = "слабой сжимаемости";
            this.rbWeakCompress.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbHelmEquImpScheme);
            this.groupBox7.Controls.Add(this.rbHelmEquExpScheme);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox7.Location = new System.Drawing.Point(7, 15);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(271, 82);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Метод решения ур. Гельмгольца";
            // 
            // rbHelmEquImpScheme
            // 
            this.rbHelmEquImpScheme.AutoSize = true;
            this.rbHelmEquImpScheme.Enabled = false;
            this.rbHelmEquImpScheme.Location = new System.Drawing.Point(6, 48);
            this.rbHelmEquImpScheme.Name = "rbHelmEquImpScheme";
            this.rbHelmEquImpScheme.Size = new System.Drawing.Size(126, 21);
            this.rbHelmEquImpScheme.TabIndex = 1;
            this.rbHelmEquImpScheme.Text = "неявная схема";
            this.toolTip.SetToolTip(this.rbHelmEquImpScheme, "Неявная схема (метод переменных напрвлений и расщепления) с противоточными произв" +
        "одными");
            this.rbHelmEquImpScheme.UseVisualStyleBackColor = true;
            // 
            // rbHelmEquExpScheme
            // 
            this.rbHelmEquExpScheme.AutoSize = true;
            this.rbHelmEquExpScheme.Checked = true;
            this.rbHelmEquExpScheme.Location = new System.Drawing.Point(6, 21);
            this.rbHelmEquExpScheme.Name = "rbHelmEquExpScheme";
            this.rbHelmEquExpScheme.Size = new System.Drawing.Size(110, 21);
            this.rbHelmEquExpScheme.TabIndex = 0;
            this.rbHelmEquExpScheme.TabStop = true;
            this.rbHelmEquExpScheme.Text = "явная схема";
            this.toolTip.SetToolTip(this.rbHelmEquExpScheme, "Явная схема с противоточными производными");
            this.rbHelmEquExpScheme.UseVisualStyleBackColor = true;
            // 
            // gpInitVal
            // 
            this.gpInitVal.Controls.Add(this.pbImage);
            this.gpInitVal.Controls.Add(label11);
            this.gpInitVal.Controls.Add(label9);
            this.gpInitVal.Controls.Add(this.tbRo);
            this.gpInitVal.Controls.Add(label7);
            this.gpInitVal.Controls.Add(this.tbTimeMax);
            this.gpInitVal.Controls.Add(label6);
            this.gpInitVal.Controls.Add(this.tbTau);
            this.gpInitVal.Controls.Add(label5);
            this.gpInitVal.Controls.Add(this.tbH);
            this.gpInitVal.Controls.Add(label4);
            this.gpInitVal.Controls.Add(label3);
            this.gpInitVal.Controls.Add(this.tbUyMax);
            this.gpInitVal.Controls.Add(this.tbUxMax);
            this.gpInitVal.Controls.Add(label2);
            this.gpInitVal.Controls.Add(label1);
            this.gpInitVal.Controls.Add(this.tbWidth);
            this.gpInitVal.Controls.Add(this.tbHeight);
            this.gpInitVal.Location = new System.Drawing.Point(6, 34);
            this.gpInitVal.Name = "gpInitVal";
            this.gpInitVal.Size = new System.Drawing.Size(838, 134);
            this.gpInitVal.TabIndex = 2;
            this.gpInitVal.TabStop = false;
            this.gpInitVal.Text = "Начальные условия";
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(510, 16);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(314, 111);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 22;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // tbRo
            // 
            this.tbRo.Location = new System.Drawing.Point(435, 48);
            this.tbRo.Name = "tbRo";
            this.tbRo.Size = new System.Drawing.Size(43, 22);
            this.tbRo.TabIndex = 20;
            this.tbRo.Text = "1.0";
            // 
            // tbTimeMax
            // 
            this.tbTimeMax.Location = new System.Drawing.Point(279, 49);
            this.tbTimeMax.Name = "tbTimeMax";
            this.tbTimeMax.Size = new System.Drawing.Size(43, 22);
            this.tbTimeMax.TabIndex = 12;
            this.tbTimeMax.Text = "1.0";
            // 
            // tbTau
            // 
            this.tbTau.Location = new System.Drawing.Point(279, 23);
            this.tbTau.Name = "tbTau";
            this.tbTau.Size = new System.Drawing.Size(43, 22);
            this.tbTau.TabIndex = 10;
            this.tbTau.Text = "0.001";
            // 
            // tbH
            // 
            this.tbH.Location = new System.Drawing.Point(435, 23);
            this.tbH.Name = "tbH";
            this.tbH.Size = new System.Drawing.Size(43, 22);
            this.tbH.TabIndex = 8;
            this.tbH.Text = "0.1";
            // 
            // tbUyMax
            // 
            this.tbUyMax.Location = new System.Drawing.Point(387, 94);
            this.tbUyMax.Name = "tbUyMax";
            this.tbUyMax.Size = new System.Drawing.Size(43, 22);
            this.tbUyMax.TabIndex = 5;
            this.tbUyMax.Text = "0.5";
            // 
            // tbUxMax
            // 
            this.tbUxMax.Location = new System.Drawing.Point(174, 94);
            this.tbUxMax.Name = "tbUxMax";
            this.tbUxMax.Size = new System.Drawing.Size(43, 22);
            this.tbUxMax.TabIndex = 4;
            this.tbUxMax.Text = "0.7";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(69, 55);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(47, 22);
            this.tbWidth.TabIndex = 1;
            this.tbWidth.Text = "5.0";
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(69, 23);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(47, 22);
            this.tbHeight.TabIndex = 0;
            this.tbHeight.Text = "2.0";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(24, 74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(172, 39);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отменить расчет";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.nudScale);
            this.groupBox1.Controls.Add(this.cbGraphics);
            this.groupBox1.Controls.Add(this.cbTextFile);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(641, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 114);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вывод результатов";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(9, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Масштаб (%)";
            // 
            // nudScale
            // 
            this.nudScale.Enabled = false;
            this.nudScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudScale.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudScale.Location = new System.Drawing.Point(104, 84);
            this.nudScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudScale.Name = "nudScale";
            this.nudScale.Size = new System.Drawing.Size(56, 22);
            this.nudScale.TabIndex = 2;
            this.nudScale.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // cbGraphics
            // 
            this.cbGraphics.AutoSize = true;
            this.cbGraphics.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbGraphics.Location = new System.Drawing.Point(7, 53);
            this.cbGraphics.Name = "cbGraphics";
            this.cbGraphics.Size = new System.Drawing.Size(165, 21);
            this.cbGraphics.TabIndex = 1;
            this.cbGraphics.Text = "В графическом виде";
            this.cbGraphics.UseVisualStyleBackColor = true;
            this.cbGraphics.CheckedChanged += new System.EventHandler(this.cbGraphics_CheckedChanged);
            // 
            // cbTextFile
            // 
            this.cbTextFile.AutoSize = true;
            this.cbTextFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbTextFile.Location = new System.Drawing.Point(7, 25);
            this.cbTextFile.Name = "cbTextFile";
            this.cbTextFile.Size = new System.Drawing.Size(146, 21);
            this.cbTextFile.TabIndex = 0;
            this.cbTextFile.Text = "В текстовом виде";
            this.cbTextFile.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Controls.Add(this.rbWPsi);
            this.groupBox2.Controls.Add(this.rbPU);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.gbPU);
            this.groupBox2.Controls.Add(this.gbWPsi);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(10, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(612, 287);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Система расчета";
            // 
            // rbWPsi
            // 
            this.rbWPsi.AutoSize = true;
            this.rbWPsi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbWPsi.Location = new System.Drawing.Point(344, 25);
            this.rbWPsi.Name = "rbWPsi";
            this.rbWPsi.Size = new System.Drawing.Size(225, 24);
            this.rbWPsi.TabIndex = 10;
            this.rbWPsi.Text = "\"Вихрь - функция тока\"";
            this.rbWPsi.UseVisualStyleBackColor = true;
            this.rbWPsi.CheckedChanged += new System.EventHandler(this.rbWPsi_CheckedChanged);
            // 
            // rbPU
            // 
            this.rbPU.AutoSize = true;
            this.rbPU.Checked = true;
            this.rbPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbPU.Location = new System.Drawing.Point(45, 25);
            this.rbPU.Name = "rbPU";
            this.rbPU.Size = new System.Drawing.Size(218, 24);
            this.rbPU.TabIndex = 2;
            this.rbPU.TabStop = true;
            this.rbPU.Text = "\"Давление - скорость\"";
            this.rbPU.UseVisualStyleBackColor = true;
            this.rbPU.CheckedChanged += new System.EventHandler(this.rbPU_CheckedChanged);
            // 
            // gbPU
            // 
            this.gbPU.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.gbPU.Controls.Add(this.groupBox3);
            this.gbPU.Controls.Add(this.groupBox4);
            this.gbPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbPU.Location = new System.Drawing.Point(10, 60);
            this.gbPU.Name = "gbPU";
            this.gbPU.Size = new System.Drawing.Size(284, 207);
            this.gbPU.TabIndex = 0;
            this.gbPU.TabStop = false;
            // 
            // gbWPsi
            // 
            this.gbWPsi.Controls.Add(this.groupBox7);
            this.gbWPsi.Enabled = false;
            this.gbWPsi.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbWPsi.Location = new System.Drawing.Point(317, 60);
            this.gbWPsi.Name = "gbWPsi";
            this.gbWPsi.Size = new System.Drawing.Size(284, 98);
            this.gbWPsi.TabIndex = 1;
            this.gbWPsi.TabStop = false;
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // rbSeq
            // 
            this.rbSeq.AutoSize = true;
            this.rbSeq.Checked = true;
            this.rbSeq.Location = new System.Drawing.Point(6, 7);
            this.rbSeq.Name = "rbSeq";
            this.rbSeq.Size = new System.Drawing.Size(241, 21);
            this.rbSeq.TabIndex = 2;
            this.rbSeq.TabStop = true;
            this.rbSeq.Text = "Последовательные вычисления";
            this.rbSeq.UseVisualStyleBackColor = true;
            // 
            // rbOpenMP
            // 
            this.rbOpenMP.AutoSize = true;
            this.rbOpenMP.Enabled = false;
            this.rbOpenMP.Location = new System.Drawing.Point(253, 7);
            this.rbOpenMP.Name = "rbOpenMP";
            this.rbOpenMP.Size = new System.Drawing.Size(84, 21);
            this.rbOpenMP.TabIndex = 7;
            this.rbOpenMP.Text = "OpenMP";
            this.rbOpenMP.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(10, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(858, 203);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rbCUDA);
            this.tabPage1.Controls.Add(this.gpInitVal);
            this.tabPage1.Controls.Add(this.rbSeq);
            this.tabPage1.Controls.Add(this.rbOpenMP);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(850, 174);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные вычисления";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rbCUDA
            // 
            this.rbCUDA.Location = new System.Drawing.Point(368, 7);
            this.rbCUDA.Name = "rbCUDA";
            this.rbCUDA.Size = new System.Drawing.Size(67, 21);
            this.rbCUDA.TabIndex = 8;
            this.rbCUDA.Text = "CUDA";
            this.rbCUDA.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(850, 174);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Нагрузочное тестирование";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pgb
            // 
            this.pgb.Location = new System.Drawing.Point(7, 33);
            this.pgb.Name = "pgb";
            this.pgb.Size = new System.Drawing.Size(207, 28);
            this.pgb.TabIndex = 22;
            // 
            // bgw
            // 
            this.bgw.WorkerReportsProgress = true;
            this.bgw.WorkerSupportsCancellation = true;
            this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            this.bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ProgressChanged);
            this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblPrc);
            this.groupBox6.Controls.Add(this.pgb);
            this.groupBox6.Controls.Add(this.btnCancel);
            this.groupBox6.Controls.Add(this.btnCalculate);
            this.groupBox6.Location = new System.Drawing.Point(641, 341);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(220, 167);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            // 
            // lblPrc
            // 
            this.lblPrc.AutoSize = true;
            this.lblPrc.Location = new System.Drawing.Point(10, 11);
            this.lblPrc.Name = "lblPrc";
            this.lblPrc.Size = new System.Drawing.Size(144, 17);
            this.lblPrc.TabIndex = 23;
            this.lblPrc.Text = "Расчет выполнен на";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(880, 513);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Моделирование смешивания потоков воздуха";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.gpInitVal.ResumeLayout(false);
            this.gpInitVal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScale)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbPU.ResumeLayout(false);
            this.gbWPsi.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnCalculate;
        private GroupBox gpInitVal;
        private TextBox tbTimeMax;
        private TextBox tbTau;
        private TextBox tbH;
        private TextBox tbUyMax;
        private TextBox tbUxMax;
        private TextBox tbWidth;
        private TextBox tbHeight;
        private Button btnCancel;
        private GroupBox groupBox1;
        private CheckBox cbGraphics;
        private CheckBox cbTextFile;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RadioButton rbWeakCompress;
        private GroupBox groupBox4;
        private RadioButton rbNSEquImpScheme;
        private RadioButton rbNSEquExpScheme;
        private RadioButton rbPoisson;
        private ToolTip toolTip;
        private GroupBox groupBox5;
        private RadioButton rbKE;
        private RadioButton rbSecundova;
        private Label label8;
        private NumericUpDown nudScale;
        private RadioButton rbSeq;
        private RadioButton rbOpenMP;
        private TextBox tbNuM;
        private TextBox tbRo;
        private RadioButton rbMissingTurb;
        private GroupBox groupBox7;
        private RadioButton rbHelmEquImpScheme;
        private RadioButton rbHelmEquExpScheme;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage3;
        private RadioButton rbPU;
        private RadioButton rbWPsi;
        private GroupBox gbPU;
        private GroupBox gbWPsi;
        private BackgroundWorker bgw;
        private ProgressBar pgb;
        private GroupBox groupBox6;
        private Label lblPrc;
        private PictureBox pbImage;
        private RadioButton rbCUDA;
    }
}

