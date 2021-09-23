namespace webSniff
{
    partial class MainForm
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
            this.tryIP = new System.Windows.Forms.Button();
            this.sniffIP = new System.Windows.Forms.Button();
            this.tryTelnet = new System.Windows.Forms.Button();
            this.tryTracert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox4IP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox4Port = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4url = new System.Windows.Forms.Button();
            this.richTextBox4url = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox4ip = new System.Windows.Forms.ComboBox();
            this.sniffAllIP = new System.Windows.Forms.Button();
            this.button4RichClear = new System.Windows.Forms.Button();
            this.getPasNetAddr = new System.Windows.Forms.Button();
            this.getNetCard = new System.Windows.Forms.Button();
            this.getLocIP = new System.Windows.Forms.Button();
            this.richTextBox4return = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.banIPButtonTextBox = new System.Windows.Forms.TextBox();
            this.banIPButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tryIP
            // 
            this.tryIP.Location = new System.Drawing.Point(13, 15);
            this.tryIP.Name = "tryIP";
            this.tryIP.Size = new System.Drawing.Size(87, 23);
            this.tryIP.TabIndex = 0;
            this.tryIP.Text = "地址可达检查";
            this.tryIP.UseVisualStyleBackColor = true;
            this.tryIP.Click += new System.EventHandler(this.tryIP_Click);
            // 
            // sniffIP
            // 
            this.sniffIP.Location = new System.Drawing.Point(13, 189);
            this.sniffIP.Name = "sniffIP";
            this.sniffIP.Size = new System.Drawing.Size(87, 23);
            this.sniffIP.TabIndex = 1;
            this.sniffIP.Text = "同段地址查询";
            this.sniffIP.UseVisualStyleBackColor = true;
            this.sniffIP.Click += new System.EventHandler(this.sniffIP_Click);
            // 
            // tryTelnet
            // 
            this.tryTelnet.Location = new System.Drawing.Point(13, 44);
            this.tryTelnet.Name = "tryTelnet";
            this.tryTelnet.Size = new System.Drawing.Size(87, 23);
            this.tryTelnet.TabIndex = 2;
            this.tryTelnet.Text = "端口连接检查";
            this.tryTelnet.UseVisualStyleBackColor = true;
            this.tryTelnet.Click += new System.EventHandler(this.tryTelnet_Click);
            // 
            // tryTracert
            // 
            this.tryTracert.Location = new System.Drawing.Point(13, 73);
            this.tryTracert.Name = "tryTracert";
            this.tryTracert.Size = new System.Drawing.Size(87, 23);
            this.tryTracert.TabIndex = 3;
            this.tryTracert.Text = "追踪路由检查";
            this.tryTracert.UseVisualStyleBackColor = true;
            this.tryTracert.Click += new System.EventHandler(this.tryTracert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP地址：";
            // 
            // textBox4IP
            // 
            this.textBox4IP.Location = new System.Drawing.Point(66, 20);
            this.textBox4IP.Name = "textBox4IP";
            this.textBox4IP.Size = new System.Drawing.Size(94, 21);
            this.textBox4IP.TabIndex = 5;
            this.textBox4IP.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "端口号：";
            // 
            // textBox4Port
            // 
            this.textBox4Port.Location = new System.Drawing.Point(229, 20);
            this.textBox4Port.Name = "textBox4Port";
            this.textBox4Port.Size = new System.Drawing.Size(71, 21);
            this.textBox4Port.TabIndex = 7;
            this.textBox4Port.Text = "3306";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4url);
            this.panel1.Controls.Add(this.richTextBox4url);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox4Port);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox4IP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 62);
            this.panel1.TabIndex = 8;
            // 
            // button4url
            // 
            this.button4url.Location = new System.Drawing.Point(325, 18);
            this.button4url.Name = "button4url";
            this.button4url.Size = new System.Drawing.Size(87, 23);
            this.button4url.TabIndex = 9;
            this.button4url.Text = "链接可用验证";
            this.button4url.UseVisualStyleBackColor = true;
            this.button4url.Click += new System.EventHandler(this.button4url_Click);
            // 
            // richTextBox4url
            // 
            this.richTextBox4url.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox4url.Location = new System.Drawing.Point(418, 0);
            this.richTextBox4url.Name = "richTextBox4url";
            this.richTextBox4url.Size = new System.Drawing.Size(186, 62);
            this.richTextBox4url.TabIndex = 8;
            this.richTextBox4url.Text = "http://";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBox4ip);
            this.panel2.Controls.Add(this.sniffAllIP);
            this.panel2.Controls.Add(this.button4RichClear);
            this.panel2.Controls.Add(this.getPasNetAddr);
            this.panel2.Controls.Add(this.getNetCard);
            this.panel2.Controls.Add(this.getLocIP);
            this.panel2.Controls.Add(this.richTextBox4return);
            this.panel2.Controls.Add(this.sniffIP);
            this.panel2.Controls.Add(this.tryIP);
            this.panel2.Controls.Add(this.tryTracert);
            this.panel2.Controls.Add(this.tryTelnet);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 139);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(604, 314);
            this.panel2.TabIndex = 9;
            // 
            // comboBox4ip
            // 
            this.comboBox4ip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4ip.Font = new System.Drawing.Font("宋体", 9F);
            this.comboBox4ip.FormattingEnabled = true;
            this.comboBox4ip.Items.AddRange(new object[] {
            "A类地址",
            "B类地址",
            "C类地址"});
            this.comboBox4ip.Location = new System.Drawing.Point(13, 247);
            this.comboBox4ip.Name = "comboBox4ip";
            this.comboBox4ip.Size = new System.Drawing.Size(87, 20);
            this.comboBox4ip.Sorted = true;
            this.comboBox4ip.TabIndex = 56;
            // 
            // sniffAllIP
            // 
            this.sniffAllIP.Location = new System.Drawing.Point(13, 218);
            this.sniffAllIP.Name = "sniffAllIP";
            this.sniffAllIP.Size = new System.Drawing.Size(87, 23);
            this.sniffAllIP.TabIndex = 9;
            this.sniffAllIP.Text = "私网地址查询";
            this.sniffAllIP.UseVisualStyleBackColor = true;
            this.sniffAllIP.Click += new System.EventHandler(this.sniffAllIP_Click);
            // 
            // button4RichClear
            // 
            this.button4RichClear.Location = new System.Drawing.Point(40, 279);
            this.button4RichClear.Name = "button4RichClear";
            this.button4RichClear.Size = new System.Drawing.Size(75, 23);
            this.button4RichClear.TabIndex = 8;
            this.button4RichClear.Text = "清空内容";
            this.button4RichClear.UseVisualStyleBackColor = true;
            this.button4RichClear.Click += new System.EventHandler(this.button4RichClear_Click);
            // 
            // getPasNetAddr
            // 
            this.getPasNetAddr.Location = new System.Drawing.Point(13, 160);
            this.getPasNetAddr.Name = "getPasNetAddr";
            this.getPasNetAddr.Size = new System.Drawing.Size(87, 23);
            this.getPasNetAddr.TabIndex = 7;
            this.getPasNetAddr.Text = "重要网卡筛选";
            this.getPasNetAddr.UseVisualStyleBackColor = true;
            this.getPasNetAddr.Click += new System.EventHandler(this.getPasNetAddr_Click);
            // 
            // getNetCard
            // 
            this.getNetCard.Location = new System.Drawing.Point(13, 131);
            this.getNetCard.Name = "getNetCard";
            this.getNetCard.Size = new System.Drawing.Size(87, 23);
            this.getNetCard.TabIndex = 6;
            this.getNetCard.Text = "获取网卡信息";
            this.getNetCard.UseVisualStyleBackColor = true;
            this.getNetCard.Click += new System.EventHandler(this.getNetCard_Click);
            // 
            // getLocIP
            // 
            this.getLocIP.Location = new System.Drawing.Point(13, 102);
            this.getLocIP.Name = "getLocIP";
            this.getLocIP.Size = new System.Drawing.Size(87, 23);
            this.getLocIP.TabIndex = 5;
            this.getLocIP.Text = "获取网卡地址";
            this.getLocIP.UseVisualStyleBackColor = true;
            this.getLocIP.Click += new System.EventHandler(this.getLocIP_Click);
            // 
            // richTextBox4return
            // 
            this.richTextBox4return.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox4return.Location = new System.Drawing.Point(121, 0);
            this.richTextBox4return.Name = "richTextBox4return";
            this.richTextBox4return.Size = new System.Drawing.Size(483, 314);
            this.richTextBox4return.TabIndex = 4;
            this.richTextBox4return.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.banIPButtonTextBox);
            this.panel3.Controls.Add(this.banIPButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(604, 77);
            this.panel3.TabIndex = 10;
            // 
            // banIPButtonTextBox
            // 
            this.banIPButtonTextBox.Location = new System.Drawing.Point(418, 30);
            this.banIPButtonTextBox.Name = "banIPButtonTextBox";
            this.banIPButtonTextBox.Size = new System.Drawing.Size(94, 21);
            this.banIPButtonTextBox.TabIndex = 6;
            this.banIPButtonTextBox.Text = "127.0.0.1";
            // 
            // banIPButton
            // 
            this.banIPButton.Location = new System.Drawing.Point(325, 28);
            this.banIPButton.Name = "banIPButton";
            this.banIPButton.Size = new System.Drawing.Size(87, 23);
            this.banIPButton.TabIndex = 0;
            this.banIPButton.Text = "禁止访问IP";
            this.banIPButton.UseVisualStyleBackColor = true;
            this.banIPButton.Click += new System.EventHandler(this.banIPButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 453);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "微齐嗅探工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button tryIP;
        private System.Windows.Forms.Button sniffIP;
        private System.Windows.Forms.Button tryTelnet;
        private System.Windows.Forms.Button tryTracert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox4IP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4Port;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox richTextBox4return;
        private System.Windows.Forms.Button getLocIP;
        private System.Windows.Forms.Button getNetCard;
        private System.Windows.Forms.Button getPasNetAddr;
        private System.Windows.Forms.Button button4RichClear;
        private System.Windows.Forms.Button sniffAllIP;
        private System.Windows.Forms.ComboBox comboBox4ip;
        private System.Windows.Forms.Button button4url;
        private System.Windows.Forms.RichTextBox richTextBox4url;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox banIPButtonTextBox;
        private System.Windows.Forms.Button banIPButton;
    }
}

