using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AirMix {
    partial class Main {
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
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.Label label15;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
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
            this.tbRo = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbHelmEquImpScheme = new System.Windows.Forms.RadioButton();
            this.rbHelmEquExpScheme = new System.Windows.Forms.RadioButton();
            this.gpInitVal = new System.Windows.Forms.GroupBox();
            this.tbTimeMax = new System.Windows.Forms.TextBox();
            this.tbTau = new System.Windows.Forms.TextBox();
            this.tbH = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.rbTemp = new System.Windows.Forms.RadioButton();
            this.rbSpeeds = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.cbGraphics = new System.Windows.Forms.CheckBox();
            this.nudScale = new System.Windows.Forms.NumericUpDown();
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
            this.rbModeling = new System.Windows.Forms.RadioButton();
            this.gbModeling = new System.Windows.Forms.GroupBox();
            this.rbCUDA = new System.Windows.Forms.RadioButton();
            this.gbStressTesting = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nudStressTesting = new System.Windows.Forms.NumericUpDown();
            this.cbCUDA = new System.Windows.Forms.CheckBox();
            this.cbOpenMP = new System.Windows.Forms.CheckBox();
            this.cbSeq = new System.Windows.Forms.CheckBox();
            this.rbStressTesting = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbTmaxUy = new System.Windows.Forms.TextBox();
            this.tbTmaxUx = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.tbUyMax = new System.Windows.Forms.TextBox();
            this.tbUxMax = new System.Windows.Forms.TextBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pgb = new System.Windows.Forms.ProgressBar();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblPrc = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.gpInitVal.SuspendLayout();
            this.gbOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScale)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbPU.SuspendLayout();
            this.gbWPsi.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbModeling.SuspendLayout();
            this.gbStressTesting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStressTesting)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(36, 116);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(95, 17);
            label5.TabIndex = 9;
            label5.Text = "Шаг сетки (h)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(10, 38);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(126, 17);
            label6.TabIndex = 11;
            label6.Text = "Шаг времени (tau)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(23, 77);
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
            label11.Location = new System.Drawing.Point(9, 78);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(78, 17);
            label11.TabIndex = 21;
            label11.Text = "Плотность";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(367, 121);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(160, 17);
            label4.TabIndex = 27;
            label4.Text = "Скорость потока снизу";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(367, 93);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(161, 17);
            label3.TabIndex = 26;
            label3.Text = "Скорость потока слева";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(366, 50);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 17);
            label2.TabIndex = 31;
            label2.Text = "Ширина";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(366, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(57, 17);
            label1.TabIndex = 30;
            label1.Text = "Высота";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(6, 109);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(139, 17);
            label12.TabIndex = 17;
            label12.Text = "Число измерений N";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(602, 124);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(187, 17);
            label14.TabIndex = 35;
            label14.Text = "Температура потока снизу";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(602, 96);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(188, 17);
            label15.TabIndex = 34;
            label15.Text = "Температура потока слева";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalculate.Location = new System.Drawing.Point(147, 79);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(139, 60);
            this.btnCalculate.TabIndex = 0;
            this.btnCalculate.Text = "НАЧАТЬ РАСЧЕТ";
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
            this.groupBox5.Location = new System.Drawing.Point(292, 165);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(259, 103);
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
            this.groupBox4.Location = new System.Drawing.Point(6, 131);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(251, 84);
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
            this.groupBox3.Controls.Add(label11);
            this.groupBox3.Controls.Add(this.rbWeakCompress);
            this.groupBox3.Controls.Add(this.tbRo);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(6, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(251, 110);
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
            // tbRo
            // 
            this.tbRo.Location = new System.Drawing.Point(98, 75);
            this.tbRo.Name = "tbRo";
            this.tbRo.Size = new System.Drawing.Size(43, 22);
            this.tbRo.TabIndex = 20;
            this.tbRo.Text = "1.0";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbHelmEquImpScheme);
            this.groupBox7.Controls.Add(this.rbHelmEquExpScheme);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox7.Location = new System.Drawing.Point(7, 15);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(241, 82);
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
            this.gpInitVal.Controls.Add(label9);
            this.gpInitVal.Controls.Add(label7);
            this.gpInitVal.Controls.Add(this.tbTimeMax);
            this.gpInitVal.Controls.Add(label6);
            this.gpInitVal.Controls.Add(this.tbTau);
            this.gpInitVal.Controls.Add(label5);
            this.gpInitVal.Controls.Add(this.tbH);
            this.gpInitVal.Location = new System.Drawing.Point(6, 6);
            this.gpInitVal.Name = "gpInitVal";
            this.gpInitVal.Size = new System.Drawing.Size(226, 162);
            this.gpInitVal.TabIndex = 2;
            this.gpInitVal.TabStop = false;
            this.gpInitVal.Text = "Начальные условия";
            // 
            // tbTimeMax
            // 
            this.tbTimeMax.Location = new System.Drawing.Point(142, 76);
            this.tbTimeMax.Name = "tbTimeMax";
            this.tbTimeMax.Size = new System.Drawing.Size(43, 22);
            this.tbTimeMax.TabIndex = 12;
            this.tbTimeMax.Text = "1.0";
            // 
            // tbTau
            // 
            this.tbTau.Location = new System.Drawing.Point(142, 38);
            this.tbTau.Name = "tbTau";
            this.tbTau.Size = new System.Drawing.Size(43, 22);
            this.tbTau.TabIndex = 10;
            this.tbTau.Text = "0.001";
            // 
            // tbH
            // 
            this.tbH.Location = new System.Drawing.Point(142, 116);
            this.tbH.Name = "tbH";
            this.tbH.Size = new System.Drawing.Size(43, 22);
            this.tbH.TabIndex = 8;
            this.tbH.Text = "0.1";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(2, 79);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 60);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "ОТМЕНИТЬ РАСЧЕТ";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.rbTemp);
            this.gbOutput.Controls.Add(this.rbSpeeds);
            this.gbOutput.Controls.Add(this.label8);
            this.gbOutput.Controls.Add(this.cbGraphics);
            this.gbOutput.Controls.Add(this.nudScale);
            this.gbOutput.Controls.Add(this.cbTextFile);
            this.gbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbOutput.Location = new System.Drawing.Point(578, 221);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(290, 133);
            this.gbOutput.TabIndex = 4;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Вывод результатов";
            // 
            // rbTemp
            // 
            this.rbTemp.AutoSize = true;
            this.rbTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbTemp.Location = new System.Drawing.Point(142, 25);
            this.rbTemp.Name = "rbTemp";
            this.rbTemp.Size = new System.Drawing.Size(145, 21);
            this.rbTemp.TabIndex = 20;
            this.rbTemp.Text = "Поле температур";
            this.rbTemp.UseVisualStyleBackColor = true;
            // 
            // rbSpeeds
            // 
            this.rbSpeeds.AutoSize = true;
            this.rbSpeeds.Checked = true;
            this.rbSpeeds.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbSpeeds.Location = new System.Drawing.Point(6, 25);
            this.rbSpeeds.Name = "rbSpeeds";
            this.rbSpeeds.Size = new System.Drawing.Size(135, 21);
            this.rbSpeeds.TabIndex = 19;
            this.rbSpeeds.TabStop = true;
            this.rbSpeeds.Text = "Поле скоростей";
            this.rbSpeeds.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(133, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Масштаб (%)";
            // 
            // cbGraphics
            // 
            this.cbGraphics.AutoSize = true;
            this.cbGraphics.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbGraphics.Location = new System.Drawing.Point(6, 56);
            this.cbGraphics.Name = "cbGraphics";
            this.cbGraphics.Size = new System.Drawing.Size(165, 21);
            this.cbGraphics.TabIndex = 1;
            this.cbGraphics.Text = "В графическом виде";
            this.cbGraphics.UseVisualStyleBackColor = true;
            this.cbGraphics.CheckedChanged += new System.EventHandler(this.cbGraphics_CheckedChanged);
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
            this.nudScale.Location = new System.Drawing.Point(228, 105);
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
            // cbTextFile
            // 
            this.cbTextFile.AutoSize = true;
            this.cbTextFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbTextFile.Location = new System.Drawing.Point(6, 83);
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
            this.groupBox2.Size = new System.Drawing.Size(562, 287);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Математическая модель";
            // 
            // rbWPsi
            // 
            this.rbWPsi.AutoSize = true;
            this.rbWPsi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbWPsi.Location = new System.Drawing.Point(319, 26);
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
            this.gbPU.Location = new System.Drawing.Point(10, 46);
            this.gbPU.Name = "gbPU";
            this.gbPU.Size = new System.Drawing.Size(270, 221);
            this.gbPU.TabIndex = 0;
            this.gbPU.TabStop = false;
            // 
            // gbWPsi
            // 
            this.gbWPsi.Controls.Add(this.groupBox7);
            this.gbWPsi.Enabled = false;
            this.gbWPsi.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbWPsi.Location = new System.Drawing.Point(292, 47);
            this.gbWPsi.Name = "gbWPsi";
            this.gbWPsi.Size = new System.Drawing.Size(259, 112);
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
            this.rbSeq.Location = new System.Drawing.Point(5, 29);
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
            this.rbOpenMP.Location = new System.Drawing.Point(5, 63);
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
            this.tabPage1.Controls.Add(this.rbModeling);
            this.tabPage1.Controls.Add(this.gbModeling);
            this.tabPage1.Controls.Add(this.gbStressTesting);
            this.tabPage1.Controls.Add(this.rbStressTesting);
            this.tabPage1.Controls.Add(this.gpInitVal);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(850, 174);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные вычисления";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rbModeling
            // 
            this.rbModeling.AutoSize = true;
            this.rbModeling.Checked = true;
            this.rbModeling.Location = new System.Drawing.Point(320, 6);
            this.rbModeling.Name = "rbModeling";
            this.rbModeling.Size = new System.Drawing.Size(135, 21);
            this.rbModeling.TabIndex = 12;
            this.rbModeling.TabStop = true;
            this.rbModeling.Text = "Моделирование";
            this.rbModeling.UseVisualStyleBackColor = true;
            // 
            // gbModeling
            // 
            this.gbModeling.Controls.Add(this.rbSeq);
            this.gbModeling.Controls.Add(this.rbOpenMP);
            this.gbModeling.Controls.Add(this.rbCUDA);
            this.gbModeling.Location = new System.Drawing.Point(248, 37);
            this.gbModeling.Name = "gbModeling";
            this.gbModeling.Size = new System.Drawing.Size(257, 131);
            this.gbModeling.TabIndex = 11;
            this.gbModeling.TabStop = false;
            this.gbModeling.Text = "Система расчета";
            // 
            // rbCUDA
            // 
            this.rbCUDA.Location = new System.Drawing.Point(5, 93);
            this.rbCUDA.Name = "rbCUDA";
            this.rbCUDA.Size = new System.Drawing.Size(67, 21);
            this.rbCUDA.TabIndex = 8;
            this.rbCUDA.Text = "CUDA";
            this.rbCUDA.UseVisualStyleBackColor = true;
            // 
            // gbStressTesting
            // 
            this.gbStressTesting.Controls.Add(this.label13);
            this.gbStressTesting.Controls.Add(this.nudStressTesting);
            this.gbStressTesting.Controls.Add(label12);
            this.gbStressTesting.Controls.Add(this.cbCUDA);
            this.gbStressTesting.Controls.Add(this.cbOpenMP);
            this.gbStressTesting.Controls.Add(this.cbSeq);
            this.gbStressTesting.Enabled = false;
            this.gbStressTesting.Location = new System.Drawing.Point(519, 34);
            this.gbStressTesting.Name = "gbStressTesting";
            this.gbStressTesting.Size = new System.Drawing.Size(325, 134);
            this.gbStressTesting.TabIndex = 10;
            this.gbStressTesting.TabStop = false;
            this.gbStressTesting.Text = "Параметры";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(260, 17);
            this.label13.TabIndex = 19;
            this.label13.Text = "X = 10,20,...N*10  Y = 4,8,...N*4  h = 0.1";
            // 
            // nudStressTesting
            // 
            this.nudStressTesting.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudStressTesting.Location = new System.Drawing.Point(147, 107);
            this.nudStressTesting.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudStressTesting.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudStressTesting.Name = "nudStressTesting";
            this.nudStressTesting.Size = new System.Drawing.Size(56, 22);
            this.nudStressTesting.TabIndex = 18;
            this.nudStressTesting.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // cbCUDA
            // 
            this.cbCUDA.AutoSize = true;
            this.cbCUDA.Location = new System.Drawing.Point(101, 58);
            this.cbCUDA.Name = "cbCUDA";
            this.cbCUDA.Size = new System.Drawing.Size(68, 21);
            this.cbCUDA.TabIndex = 2;
            this.cbCUDA.Text = "CUDA";
            this.cbCUDA.UseVisualStyleBackColor = true;
            // 
            // cbOpenMP
            // 
            this.cbOpenMP.AutoSize = true;
            this.cbOpenMP.Location = new System.Drawing.Point(10, 57);
            this.cbOpenMP.Name = "cbOpenMP";
            this.cbOpenMP.Size = new System.Drawing.Size(85, 21);
            this.cbOpenMP.TabIndex = 1;
            this.cbOpenMP.Text = "OpenMP";
            this.cbOpenMP.UseVisualStyleBackColor = true;
            // 
            // cbSeq
            // 
            this.cbSeq.AutoSize = true;
            this.cbSeq.Checked = true;
            this.cbSeq.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSeq.Location = new System.Drawing.Point(10, 26);
            this.cbSeq.Name = "cbSeq";
            this.cbSeq.Size = new System.Drawing.Size(242, 21);
            this.cbSeq.TabIndex = 0;
            this.cbSeq.Text = "Последовательные вычисления";
            this.cbSeq.UseVisualStyleBackColor = true;
            // 
            // rbStressTesting
            // 
            this.rbStressTesting.AutoSize = true;
            this.rbStressTesting.Location = new System.Drawing.Point(577, 6);
            this.rbStressTesting.Name = "rbStressTesting";
            this.rbStressTesting.Size = new System.Drawing.Size(210, 21);
            this.rbStressTesting.TabIndex = 9;
            this.rbStressTesting.Text = "Нагрузочное тестирование";
            this.rbStressTesting.UseVisualStyleBackColor = true;
            this.rbStressTesting.CheckedChanged += new System.EventHandler(this.rbStressTesting_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(label14);
            this.tabPage3.Controls.Add(label15);
            this.tabPage3.Controls.Add(this.tbTmaxUy);
            this.tabPage3.Controls.Add(this.tbTmaxUx);
            this.tabPage3.Controls.Add(label2);
            this.tabPage3.Controls.Add(label1);
            this.tabPage3.Controls.Add(this.tbWidth);
            this.tabPage3.Controls.Add(this.tbHeight);
            this.tabPage3.Controls.Add(label4);
            this.tabPage3.Controls.Add(label3);
            this.tabPage3.Controls.Add(this.tbUyMax);
            this.tabPage3.Controls.Add(this.tbUxMax);
            this.tabPage3.Controls.Add(this.pbImage);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(850, 174);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Конфигурация потоков";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbTmaxUy
            // 
            this.tbTmaxUy.Location = new System.Drawing.Point(795, 124);
            this.tbTmaxUy.Name = "tbTmaxUy";
            this.tbTmaxUy.Size = new System.Drawing.Size(43, 22);
            this.tbTmaxUy.TabIndex = 33;
            this.tbTmaxUy.Text = "100";
            // 
            // tbTmaxUx
            // 
            this.tbTmaxUx.Location = new System.Drawing.Point(795, 96);
            this.tbTmaxUx.Name = "tbTmaxUx";
            this.tbTmaxUx.Size = new System.Drawing.Size(43, 22);
            this.tbTmaxUx.TabIndex = 32;
            this.tbTmaxUx.Text = "75";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(426, 50);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(47, 22);
            this.tbWidth.TabIndex = 29;
            this.tbWidth.Text = "5.0";
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(426, 18);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(47, 22);
            this.tbHeight.TabIndex = 28;
            this.tbHeight.Text = "2.0";
            // 
            // tbUyMax
            // 
            this.tbUyMax.Location = new System.Drawing.Point(534, 121);
            this.tbUyMax.Name = "tbUyMax";
            this.tbUyMax.Size = new System.Drawing.Size(43, 22);
            this.tbUyMax.TabIndex = 25;
            this.tbUyMax.Text = "-2.5";
            // 
            // tbUxMax
            // 
            this.tbUxMax.Location = new System.Drawing.Point(534, 93);
            this.tbUxMax.Name = "tbUxMax";
            this.tbUxMax.Size = new System.Drawing.Size(43, 22);
            this.tbUxMax.TabIndex = 24;
            this.tbUxMax.Text = "4.7";
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(12, 18);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(314, 140);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 23;
            this.pbImage.TabStop = false;
            // 
            // pgb
            // 
            this.pgb.Location = new System.Drawing.Point(2, 45);
            this.pgb.Name = "pgb";
            this.pgb.Size = new System.Drawing.Size(284, 28);
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
            this.groupBox6.Location = new System.Drawing.Point(578, 360);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(290, 148);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            // 
            // lblPrc
            // 
            this.lblPrc.AutoSize = true;
            this.lblPrc.Location = new System.Drawing.Point(3, 15);
            this.lblPrc.Name = "lblPrc";
            this.lblPrc.Size = new System.Drawing.Size(144, 17);
            this.lblPrc.TabIndex = 23;
            this.lblPrc.Text = "Расчет выполнен на";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(874, 519);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Моделирование смешивания потоков воздуха";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
            this.gbOutput.ResumeLayout(false);
            this.gbOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScale)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbPU.ResumeLayout(false);
            this.gbWPsi.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gbModeling.ResumeLayout(false);
            this.gbModeling.PerformLayout();
            this.gbStressTesting.ResumeLayout(false);
            this.gbStressTesting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStressTesting)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
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
        private Button btnCancel;
        private GroupBox gbOutput;
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
        private RadioButton rbCUDA;
        private GroupBox gbStressTesting;
        private CheckBox cbCUDA;
        private CheckBox cbOpenMP;
        private CheckBox cbSeq;
        private RadioButton rbStressTesting;
        private RadioButton rbModeling;
        private GroupBox gbModeling;
        private TextBox tbWidth;
        private TextBox tbHeight;
        private TextBox tbUyMax;
        private TextBox tbUxMax;
        private PictureBox pbImage;
        private NumericUpDown nudStressTesting;
        private Label label13;
        public TextBox tbTmaxUy;
        public TextBox tbTmaxUx;
        private RadioButton rbTemp;
        private RadioButton rbSpeeds;
    }
}

