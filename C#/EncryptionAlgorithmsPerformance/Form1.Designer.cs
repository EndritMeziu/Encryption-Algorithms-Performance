namespace EncryptionAlgorithmsPerformance
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSideBar = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblExit = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pnlBarChart = new System.Windows.Forms.Panel();
            this.pnlPieChart = new System.Windows.Forms.Panel();
            this.pnlKeySize = new System.Windows.Forms.Panel();
            this.cmbKeySize = new System.Windows.Forms.ComboBox();
            this.pnlAlgorithm = new System.Windows.Forms.Panel();
            this.cmbAlgorithm = new System.Windows.Forms.ComboBox();
            this.pnlFile = new System.Windows.Forms.Panel();
            this.cmbFile = new System.Windows.Forms.ComboBox();
            this.cmbGraphType = new System.Windows.Forms.ComboBox();
            this.pieChart2 = new LiveCharts.WinForms.PieChart();
            this.label1 = new System.Windows.Forms.Label();
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pieChart3 = new LiveCharts.WinForms.PieChart();
            this.pieChart4 = new LiveCharts.WinForms.PieChart();
            this.pnlSideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlTopBar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlBarChart.SuspendLayout();
            this.pnlPieChart.SuspendLayout();
            this.pnlKeySize.SuspendLayout();
            this.pnlAlgorithm.SuspendLayout();
            this.pnlFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(58)))), ((int)(((byte)(81)))));
            this.pnlSideBar.Controls.Add(this.label3);
            this.pnlSideBar.Controls.Add(this.label2);
            this.pnlSideBar.Controls.Add(this.pictureBox1);
            this.pnlSideBar.Location = new System.Drawing.Point(3, 95);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(198, 686);
            this.pnlSideBar.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(11, 595);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Elektrike dhe Kompjuterike";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(11, 574);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "FIEK - Fakulteti i Inxhinierise";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(26, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(144, 134);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(58)))), ((int)(((byte)(81)))));
            this.pnlTopBar.Controls.Add(this.lblExit);
            this.pnlTopBar.Location = new System.Drawing.Point(3, 3);
            this.pnlTopBar.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1084, 86);
            this.pnlTopBar.TabIndex = 1;
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblExit.Location = new System.Drawing.Point(1038, 23);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(23, 25);
            this.lblExit.TabIndex = 0;
            this.lblExit.Text = "x";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.panel11);
            this.panel3.Controls.Add(this.panel9);
            this.panel3.Controls.Add(this.pnlBarChart);
            this.panel3.Controls.Add(this.pnlPieChart);
            this.panel3.Controls.Add(this.pnlKeySize);
            this.panel3.Controls.Add(this.pnlAlgorithm);
            this.panel3.Controls.Add(this.pnlFile);
            this.panel3.Controls.Add(this.pnlSideBar);
            this.panel3.Controls.Add(this.pnlTopBar);
            this.panel3.Location = new System.Drawing.Point(-2, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1086, 775);
            this.panel3.TabIndex = 2;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(71)))));
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Location = new System.Drawing.Point(828, 117);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(78, 67);
            this.panel10.TabIndex = 6;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(156)))), ((int)(((byte)(71)))));
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Location = new System.Drawing.Point(546, 117);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(78, 67);
            this.panel11.TabIndex = 6;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(176)))), ((int)(((byte)(197)))));
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Location = new System.Drawing.Point(263, 117);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(78, 67);
            this.panel9.TabIndex = 5;
            // 
            // pnlBarChart
            // 
            this.pnlBarChart.BackColor = System.Drawing.Color.White;
            this.pnlBarChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBarChart.Controls.Add(this.pieChart4);
            this.pnlBarChart.Location = new System.Drawing.Point(655, 355);
            this.pnlBarChart.Name = "pnlBarChart";
            this.pnlBarChart.Size = new System.Drawing.Size(395, 399);
            this.pnlBarChart.TabIndex = 4;
            // 
            // pnlPieChart
            // 
            this.pnlPieChart.BackColor = System.Drawing.Color.White;
            this.pnlPieChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPieChart.Controls.Add(this.pieChart3);
            this.pnlPieChart.Location = new System.Drawing.Point(240, 355);
            this.pnlPieChart.Name = "pnlPieChart";
            this.pnlPieChart.Size = new System.Drawing.Size(395, 399);
            this.pnlPieChart.TabIndex = 3;
            // 
            // pnlKeySize
            // 
            this.pnlKeySize.BackColor = System.Drawing.Color.White;
            this.pnlKeySize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKeySize.Controls.Add(this.cmbKeySize);
            this.pnlKeySize.Location = new System.Drawing.Point(806, 151);
            this.pnlKeySize.Name = "pnlKeySize";
            this.pnlKeySize.Size = new System.Drawing.Size(244, 171);
            this.pnlKeySize.TabIndex = 3;
            this.pnlKeySize.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlKeySize_Paint);
            // 
            // cmbKeySize
            // 
            this.cmbKeySize.FormattingEnabled = true;
            this.cmbKeySize.Location = new System.Drawing.Point(42, 72);
            this.cmbKeySize.Name = "cmbKeySize";
            this.cmbKeySize.Size = new System.Drawing.Size(154, 24);
            this.cmbKeySize.TabIndex = 1;
            this.cmbKeySize.SelectedIndexChanged += new System.EventHandler(this.cmbKeySize_SelectedIndexChanged);
            // 
            // pnlAlgorithm
            // 
            this.pnlAlgorithm.BackColor = System.Drawing.Color.White;
            this.pnlAlgorithm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAlgorithm.Controls.Add(this.cmbAlgorithm);
            this.pnlAlgorithm.Location = new System.Drawing.Point(526, 151);
            this.pnlAlgorithm.Name = "pnlAlgorithm";
            this.pnlAlgorithm.Size = new System.Drawing.Size(244, 171);
            this.pnlAlgorithm.TabIndex = 3;
            this.pnlAlgorithm.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAlgorithm_Paint);
            // 
            // cmbAlgorithm
            // 
            this.cmbAlgorithm.FormattingEnabled = true;
            this.cmbAlgorithm.Location = new System.Drawing.Point(44, 72);
            this.cmbAlgorithm.Name = "cmbAlgorithm";
            this.cmbAlgorithm.Size = new System.Drawing.Size(154, 24);
            this.cmbAlgorithm.TabIndex = 2;
            this.cmbAlgorithm.SelectedIndexChanged += new System.EventHandler(this.cmbAlgorithm_SelectedIndexChanged);
            // 
            // pnlFile
            // 
            this.pnlFile.BackColor = System.Drawing.Color.White;
            this.pnlFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFile.Controls.Add(this.cmbFile);
            this.pnlFile.Location = new System.Drawing.Point(240, 151);
            this.pnlFile.Name = "pnlFile";
            this.pnlFile.Size = new System.Drawing.Size(244, 171);
            this.pnlFile.TabIndex = 2;
            this.pnlFile.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFile_Paint);
            // 
            // cmbFile
            // 
            this.cmbFile.FormattingEnabled = true;
            this.cmbFile.Location = new System.Drawing.Point(44, 72);
            this.cmbFile.Name = "cmbFile";
            this.cmbFile.Size = new System.Drawing.Size(154, 24);
            this.cmbFile.TabIndex = 0;
            this.cmbFile.SelectedIndexChanged += new System.EventHandler(this.cmbFile_SelectedIndexChanged);
            // 
            // cmbGraphType
            // 
            this.cmbGraphType.FormattingEnabled = true;
            this.cmbGraphType.Location = new System.Drawing.Point(125, 22);
            this.cmbGraphType.Name = "cmbGraphType";
            this.cmbGraphType.Size = new System.Drawing.Size(154, 25);
            this.cmbGraphType.TabIndex = 1;
            this.cmbGraphType.SelectedIndexChanged += new System.EventHandler(this.cmbGraphType_SelectedIndexChanged);
            // 
            // pieChart2
            // 
            this.pieChart2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pieChart2.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.pieChart2.Location = new System.Drawing.Point(3, 52);
            this.pieChart2.Name = "pieChart2";
            this.pieChart2.Size = new System.Drawing.Size(395, 326);
            this.pieChart2.TabIndex = 1;
            this.pieChart2.Text = "pieChart2";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Encyption Results";
            // 
            // pieChart1
            // 
            this.pieChart1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pieChart1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.pieChart1.Location = new System.Drawing.Point(-5, 52);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(395, 326);
            this.pieChart1.TabIndex = 0;
            this.pieChart1.Text = "pieChart1";
            // 
            // pieChart3
            // 
            this.pieChart3.Location = new System.Drawing.Point(22, 38);
            this.pieChart3.Name = "pieChart3";
            this.pieChart3.Size = new System.Drawing.Size(353, 340);
            this.pieChart3.TabIndex = 0;
            this.pieChart3.Text = "pieChart3";
            // 
            // pieChart4
            // 
            this.pieChart4.Location = new System.Drawing.Point(20, 38);
            this.pieChart4.Name = "pieChart4";
            this.pieChart4.Size = new System.Drawing.Size(359, 340);
            this.pieChart4.TabIndex = 0;
            this.pieChart4.Text = "pieChart4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 775);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.pnlSideBar.ResumeLayout(false);
            this.pnlSideBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlBarChart.ResumeLayout(false);
            this.pnlPieChart.ResumeLayout(false);
            this.pnlKeySize.ResumeLayout(false);
            this.pnlAlgorithm.ResumeLayout(false);
            this.pnlFile.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlFile;
        private System.Windows.Forms.Panel pnlPieChart;
        private System.Windows.Forms.Panel pnlKeySize;
        private System.Windows.Forms.Panel pnlAlgorithm;
        private System.Windows.Forms.Panel pnlBarChart;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel9;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox cmbFile;
        private System.Windows.Forms.ComboBox cmbKeySize;
        private System.Windows.Forms.ComboBox cmbAlgorithm;
        private LiveCharts.WinForms.PieChart pieChart1;
        private LiveCharts.WinForms.PieChart pieChart2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbGraphType;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private LiveCharts.WinForms.PieChart pieChart4;
        private LiveCharts.WinForms.PieChart pieChart3;
    }
}

