namespace ClickOperator
{
    partial class form
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.l_ip = new System.Windows.Forms.Label();
            this.t_ip = new System.Windows.Forms.TextBox();
            this.t_port1 = new System.Windows.Forms.TextBox();
            this.l_port1 = new System.Windows.Forms.Label();
            this.t_port2 = new System.Windows.Forms.TextBox();
            this.l_port2 = new System.Windows.Forms.Label();
            this.b_listen = new System.Windows.Forms.Button();
            this.networkPanel = new System.Windows.Forms.Panel();
            this.p_ss = new System.Windows.Forms.PictureBox();
            this.l_xy = new System.Windows.Forms.Label();
            this.b_xy = new System.Windows.Forms.Button();
            this.b_text = new System.Windows.Forms.Button();
            this.t_text = new System.Windows.Forms.TextBox();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.networkPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p_ss)).BeginInit();
            this.controlPanel.SuspendLayout();
            this.imagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // l_ip
            // 
            this.l_ip.AutoSize = true;
            this.l_ip.Location = new System.Drawing.Point(12, 9);
            this.l_ip.Name = "l_ip";
            this.l_ip.Size = new System.Drawing.Size(59, 12);
            this.l_ip.TabIndex = 0;
            this.l_ip.Text = "IP address";
            // 
            // t_ip
            // 
            this.t_ip.Location = new System.Drawing.Point(74, 6);
            this.t_ip.Name = "t_ip";
            this.t_ip.Size = new System.Drawing.Size(88, 19);
            this.t_ip.TabIndex = 1;
            // 
            // t_port1
            // 
            this.t_port1.Location = new System.Drawing.Point(219, 6);
            this.t_port1.Name = "t_port1";
            this.t_port1.Size = new System.Drawing.Size(59, 19);
            this.t_port1.TabIndex = 3;
            // 
            // l_port1
            // 
            this.l_port1.AutoSize = true;
            this.l_port1.Location = new System.Drawing.Point(167, 9);
            this.l_port1.Name = "l_port1";
            this.l_port1.Size = new System.Drawing.Size(49, 12);
            this.l_port1.TabIndex = 2;
            this.l_port1.Text = "CmdPort";
            // 
            // t_port2
            // 
            this.t_port2.Location = new System.Drawing.Point(333, 6);
            this.t_port2.Name = "t_port2";
            this.t_port2.Size = new System.Drawing.Size(59, 19);
            this.t_port2.TabIndex = 5;
            // 
            // l_port2
            // 
            this.l_port2.AutoSize = true;
            this.l_port2.Location = new System.Drawing.Point(280, 9);
            this.l_port2.Name = "l_port2";
            this.l_port2.Size = new System.Drawing.Size(50, 12);
            this.l_port2.TabIndex = 4;
            this.l_port2.Text = "DataPort";
            // 
            // b_listen
            // 
            this.b_listen.Location = new System.Drawing.Point(398, 4);
            this.b_listen.Name = "b_listen";
            this.b_listen.Size = new System.Drawing.Size(45, 23);
            this.b_listen.TabIndex = 6;
            this.b_listen.Text = "Go !";
            this.b_listen.UseVisualStyleBackColor = true;
            this.b_listen.Click += new System.EventHandler(this.b_listen_Click);
            // 
            // networkPanel
            // 
            this.networkPanel.Controls.Add(this.l_ip);
            this.networkPanel.Controls.Add(this.b_listen);
            this.networkPanel.Controls.Add(this.t_ip);
            this.networkPanel.Controls.Add(this.t_port2);
            this.networkPanel.Controls.Add(this.l_port1);
            this.networkPanel.Controls.Add(this.l_port2);
            this.networkPanel.Controls.Add(this.t_port1);
            this.networkPanel.Location = new System.Drawing.Point(12, 12);
            this.networkPanel.Name = "networkPanel";
            this.networkPanel.Size = new System.Drawing.Size(461, 33);
            this.networkPanel.TabIndex = 7;
            // 
            // p_ss
            // 
            this.p_ss.Location = new System.Drawing.Point(0, 0);
            this.p_ss.Name = "p_ss";
            this.p_ss.Size = new System.Drawing.Size(460, 280);
            this.p_ss.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.p_ss.TabIndex = 8;
            this.p_ss.TabStop = false;
            this.p_ss.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p_ss_MouseClick);
            this.p_ss.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.p_ss_MouseDoubleClick);
            // 
            // l_xy
            // 
            this.l_xy.AutoSize = true;
            this.l_xy.Location = new System.Drawing.Point(12, 10);
            this.l_xy.Name = "l_xy";
            this.l_xy.Size = new System.Drawing.Size(73, 12);
            this.l_xy.TabIndex = 9;
            this.l_xy.Text = "x,y click type";
            // 
            // b_xy
            // 
            this.b_xy.Location = new System.Drawing.Point(87, 5);
            this.b_xy.Name = "b_xy";
            this.b_xy.Size = new System.Drawing.Size(60, 23);
            this.b_xy.TabIndex = 10;
            this.b_xy.Text = "Control";
            this.b_xy.UseVisualStyleBackColor = true;
            this.b_xy.Click += new System.EventHandler(this.b_xy_Click);
            // 
            // b_text
            // 
            this.b_text.Location = new System.Drawing.Point(383, 5);
            this.b_text.Name = "b_text";
            this.b_text.Size = new System.Drawing.Size(60, 23);
            this.b_text.TabIndex = 11;
            this.b_text.Text = "Input";
            this.b_text.UseVisualStyleBackColor = true;
            this.b_text.Click += new System.EventHandler(this.b_text_Click);
            // 
            // t_text
            // 
            this.t_text.Location = new System.Drawing.Point(153, 7);
            this.t_text.Name = "t_text";
            this.t_text.Size = new System.Drawing.Size(224, 19);
            this.t_text.TabIndex = 12;
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.controlPanel.Controls.Add(this.l_xy);
            this.controlPanel.Controls.Add(this.b_text);
            this.controlPanel.Controls.Add(this.t_text);
            this.controlPanel.Controls.Add(this.b_xy);
            this.controlPanel.Location = new System.Drawing.Point(12, 338);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(461, 34);
            this.controlPanel.TabIndex = 13;
            // 
            // imagePanel
            // 
            this.imagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePanel.AutoScroll = true;
            this.imagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePanel.Controls.Add(this.p_ss);
            this.imagePanel.Location = new System.Drawing.Point(12, 49);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(460, 280);
            this.imagePanel.TabIndex = 14;
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 381);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.networkPanel);
            this.MinimumSize = new System.Drawing.Size(500, 420);
            this.Name = "form";
            this.Text = "ClickOperator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_FormClosing);
            this.networkPanel.ResumeLayout(false);
            this.networkPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p_ss)).EndInit();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.imagePanel.ResumeLayout(false);
            this.imagePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label l_ip;
        private System.Windows.Forms.TextBox t_ip;
        private System.Windows.Forms.TextBox t_port1;
        private System.Windows.Forms.Label l_port1;
        private System.Windows.Forms.TextBox t_port2;
        private System.Windows.Forms.Label l_port2;
        private System.Windows.Forms.Button b_listen;
        private System.Windows.Forms.Panel networkPanel;
        private System.Windows.Forms.PictureBox p_ss;
        private System.Windows.Forms.Label l_xy;
        private System.Windows.Forms.Button b_xy;
        private System.Windows.Forms.Button b_text;
        private System.Windows.Forms.TextBox t_text;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Panel imagePanel;
    }
}

