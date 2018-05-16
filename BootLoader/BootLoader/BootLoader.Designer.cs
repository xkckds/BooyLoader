namespace BootLoader
{
    partial class BootLoader
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.FileSelect = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.UpGrade = new System.Windows.Forms.Button();
            this.ComSelect = new System.Windows.Forms.ComboBox();
            this.StatusBar = new System.Windows.Forms.TextBox();
            this.BaudRate = new System.Windows.Forms.ComboBox();
            this.StopBit = new System.Windows.Forms.ComboBox();
            this.ParityBit = new System.Windows.Forms.ComboBox();
            this.CloseCom = new System.Windows.Forms.Button();
            this.LComSelect = new System.Windows.Forms.Label();
            this.LBaudRate = new System.Windows.Forms.Label();
            this.LStopBit = new System.Windows.Forms.Label();
            this.LParityBit = new System.Windows.Forms.Label();
            this.DataBit = new System.Windows.Forms.ComboBox();
            this.LDataBit = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LPath = new System.Windows.Forms.Label();
            this.LStatusBar = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileSelect
            // 
            this.FileSelect.Enabled = false;
            this.FileSelect.Location = new System.Drawing.Point(427, 20);
            this.FileSelect.Name = "FileSelect";
            this.FileSelect.Size = new System.Drawing.Size(90, 23);
            this.FileSelect.TabIndex = 0;
            this.FileSelect.Text = "选择固件程序";
            this.FileSelect.UseVisualStyleBackColor = true;
            this.FileSelect.Click += new System.EventHandler(this.FileSelect_Click);
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(75, 21);
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Size = new System.Drawing.Size(302, 21);
            this.FilePath.TabIndex = 1;
            // 
            // UpGrade
            // 
            this.UpGrade.Enabled = false;
            this.UpGrade.Location = new System.Drawing.Point(432, 231);
            this.UpGrade.Name = "UpGrade";
            this.UpGrade.Size = new System.Drawing.Size(90, 23);
            this.UpGrade.TabIndex = 2;
            this.UpGrade.Text = "固件升级";
            this.UpGrade.UseVisualStyleBackColor = true;
            this.UpGrade.Click += new System.EventHandler(this.UpGrade_Click);
            // 
            // ComSelect
            // 
            this.ComSelect.FormattingEnabled = true;
            this.ComSelect.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.ComSelect.Location = new System.Drawing.Point(62, 24);
            this.ComSelect.Name = "ComSelect";
            this.ComSelect.Size = new System.Drawing.Size(65, 20);
            this.ComSelect.TabIndex = 3;
            this.ComSelect.SelectedIndexChanged += new System.EventHandler(this.ComSelect_SelectedIndexChanged);
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(12, 280);
            this.StatusBar.Multiline = true;
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(527, 91);
            this.StatusBar.TabIndex = 4;
            // 
            // BaudRate
            // 
            this.BaudRate.Enabled = false;
            this.BaudRate.FormattingEnabled = true;
            this.BaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "115200"});
            this.BaudRate.Location = new System.Drawing.Point(196, 24);
            this.BaudRate.Name = "BaudRate";
            this.BaudRate.Size = new System.Drawing.Size(65, 20);
            this.BaudRate.TabIndex = 3;
            this.BaudRate.Text = "115200";
            this.BaudRate.SelectedIndexChanged += new System.EventHandler(this.BaudRate_SelectedIndexChanged);
            // 
            // StopBit
            // 
            this.StopBit.Enabled = false;
            this.StopBit.FormattingEnabled = true;
            this.StopBit.Items.AddRange(new object[] {
            "1",
            "2"});
            this.StopBit.Location = new System.Drawing.Point(196, 60);
            this.StopBit.Name = "StopBit";
            this.StopBit.Size = new System.Drawing.Size(65, 20);
            this.StopBit.TabIndex = 3;
            this.StopBit.Text = "1";
            this.StopBit.SelectedIndexChanged += new System.EventHandler(this.StopBit_SelectedIndexChanged);
            // 
            // ParityBit
            // 
            this.ParityBit.Enabled = false;
            this.ParityBit.FormattingEnabled = true;
            this.ParityBit.Items.AddRange(new object[] {
            "无",
            "奇校验",
            "偶校验"});
            this.ParityBit.Location = new System.Drawing.Point(62, 60);
            this.ParityBit.Name = "ParityBit";
            this.ParityBit.Size = new System.Drawing.Size(65, 20);
            this.ParityBit.TabIndex = 3;
            this.ParityBit.Text = "无";
            this.ParityBit.SelectedIndexChanged += new System.EventHandler(this.ParityBit_SelectedIndexChanged);
            // 
            // CloseCom
            // 
            this.CloseCom.Enabled = false;
            this.CloseCom.Location = new System.Drawing.Point(435, 23);
            this.CloseCom.Name = "CloseCom";
            this.CloseCom.Size = new System.Drawing.Size(75, 23);
            this.CloseCom.TabIndex = 5;
            this.CloseCom.Text = "关闭串口";
            this.CloseCom.UseVisualStyleBackColor = true;
            this.CloseCom.Click += new System.EventHandler(this.CloseCom_Click);
            // 
            // LComSelect
            // 
            this.LComSelect.AutoSize = true;
            this.LComSelect.Location = new System.Drawing.Point(14, 28);
            this.LComSelect.Name = "LComSelect";
            this.LComSelect.Size = new System.Drawing.Size(29, 12);
            this.LComSelect.TabIndex = 6;
            this.LComSelect.Text = "串口";
            // 
            // LBaudRate
            // 
            this.LBaudRate.AutoSize = true;
            this.LBaudRate.Location = new System.Drawing.Point(145, 28);
            this.LBaudRate.Name = "LBaudRate";
            this.LBaudRate.Size = new System.Drawing.Size(41, 12);
            this.LBaudRate.TabIndex = 7;
            this.LBaudRate.Text = "波特率";
            // 
            // LStopBit
            // 
            this.LStopBit.AutoSize = true;
            this.LStopBit.Location = new System.Drawing.Point(145, 64);
            this.LStopBit.Name = "LStopBit";
            this.LStopBit.Size = new System.Drawing.Size(41, 12);
            this.LStopBit.TabIndex = 7;
            this.LStopBit.Text = "停止位";
            // 
            // LParityBit
            // 
            this.LParityBit.AutoSize = true;
            this.LParityBit.Location = new System.Drawing.Point(14, 64);
            this.LParityBit.Name = "LParityBit";
            this.LParityBit.Size = new System.Drawing.Size(41, 12);
            this.LParityBit.TabIndex = 7;
            this.LParityBit.Text = "奇偶位";
            // 
            // DataBit
            // 
            this.DataBit.Enabled = false;
            this.DataBit.FormattingEnabled = true;
            this.DataBit.Items.AddRange(new object[] {
            "7",
            "8"});
            this.DataBit.Location = new System.Drawing.Point(338, 24);
            this.DataBit.Name = "DataBit";
            this.DataBit.Size = new System.Drawing.Size(65, 20);
            this.DataBit.TabIndex = 3;
            this.DataBit.Text = "8";
            this.DataBit.SelectedIndexChanged += new System.EventHandler(this.DataBit_SelectedIndexChanged);
            // 
            // LDataBit
            // 
            this.LDataBit.AutoSize = true;
            this.LDataBit.Location = new System.Drawing.Point(285, 28);
            this.LDataBit.Name = "LDataBit";
            this.LDataBit.Size = new System.Drawing.Size(41, 12);
            this.LDataBit.TabIndex = 6;
            this.LDataBit.Text = "数据位";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StopBit);
            this.groupBox1.Controls.Add(this.LBaudRate);
            this.groupBox1.Controls.Add(this.LParityBit);
            this.groupBox1.Controls.Add(this.LDataBit);
            this.groupBox1.Controls.Add(this.LStopBit);
            this.groupBox1.Controls.Add(this.LComSelect);
            this.groupBox1.Controls.Add(this.ParityBit);
            this.groupBox1.Controls.Add(this.CloseCom);
            this.groupBox1.Controls.Add(this.ComSelect);
            this.groupBox1.Controls.Add(this.BaudRate);
            this.groupBox1.Controls.Add(this.DataBit);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 98);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LPath);
            this.groupBox2.Controls.Add(this.FilePath);
            this.groupBox2.Controls.Add(this.FileSelect);
            this.groupBox2.Location = new System.Drawing.Point(12, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 60);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "固件选取";
            // 
            // LPath
            // 
            this.LPath.AutoSize = true;
            this.LPath.Location = new System.Drawing.Point(14, 25);
            this.LPath.Name = "LPath";
            this.LPath.Size = new System.Drawing.Size(53, 12);
            this.LPath.TabIndex = 3;
            this.LPath.Text = "Codepath";
            // 
            // LStatusBar
            // 
            this.LStatusBar.AutoSize = true;
            this.LStatusBar.Location = new System.Drawing.Point(11, 263);
            this.LStatusBar.Name = "LStatusBar";
            this.LStatusBar.Size = new System.Drawing.Size(41, 12);
            this.LStatusBar.TabIndex = 10;
            this.LStatusBar.Text = "状态栏";
            // 
            // BootLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 375);
            this.Controls.Add(this.LStatusBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.UpGrade);
            this.Name = "BootLoader";
            this.Text = "固件升级软件";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FileSelect;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Button UpGrade;
        private System.Windows.Forms.ComboBox ComSelect;
        private System.Windows.Forms.TextBox StatusBar;
        private System.Windows.Forms.ComboBox BaudRate;
        private System.Windows.Forms.ComboBox StopBit;
        private System.Windows.Forms.ComboBox ParityBit;
        private System.Windows.Forms.Button CloseCom;
        private System.Windows.Forms.Label LComSelect;
        private System.Windows.Forms.Label LBaudRate;
        private System.Windows.Forms.Label LStopBit;
        private System.Windows.Forms.Label LParityBit;
        private System.Windows.Forms.ComboBox DataBit;
        private System.Windows.Forms.Label LDataBit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LPath;
        private System.Windows.Forms.Label LStatusBar;
    }
}

