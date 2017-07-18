namespace Leitner_Three
{
    partial class AboutMe
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutMe));
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabelMD = new System.Windows.Forms.LinkLabel();
			this.linkLabelJK = new System.Windows.Forms.LinkLabel();
			this.linkLabelCP = new System.Windows.Forms.LinkLabel();
			this.Copywrite = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabelCPOL = new System.Windows.Forms.LinkLabel();
			this.richTextBoxBackground = new System.Windows.Forms.RichTextBox();
			this.linkLabelSL = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(139, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Version: 20 November 2012";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// linkLabelMD
			// 
			this.linkLabelMD.AutoSize = true;
			this.linkLabelMD.LinkColor = System.Drawing.Color.Blue;
			this.linkLabelMD.Location = new System.Drawing.Point(121, 176);
			this.linkLabelMD.Name = "linkLabelMD";
			this.linkLabelMD.Size = new System.Drawing.Size(101, 13);
			this.linkLabelMD.TabIndex = 2;
			this.linkLabelMD.TabStop = true;
			this.linkLabelMD.Text = "Mohammad Dayyan";
			this.linkLabelMD.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMD_LinkClicked);
			// 
			// linkLabelJK
			// 
			this.linkLabelJK.AutoSize = true;
			this.linkLabelJK.LinkColor = System.Drawing.Color.Blue;
			this.linkLabelJK.Location = new System.Drawing.Point(142, 163);
			this.linkLabelJK.Name = "linkLabelJK";
			this.linkLabelJK.Size = new System.Drawing.Size(58, 13);
			this.linkLabelJK.TabIndex = 3;
			this.linkLabelJK.TabStop = true;
			this.linkLabelJK.Text = "James Kay";
			this.linkLabelJK.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelJK_LinkClicked);
			// 
			// linkLabelCP
			// 
			this.linkLabelCP.AutoSize = true;
			this.linkLabelCP.LinkColor = System.Drawing.Color.Blue;
			this.linkLabelCP.Location = new System.Drawing.Point(121, 189);
			this.linkLabelCP.Name = "linkLabelCP";
			this.linkLabelCP.Size = new System.Drawing.Size(90, 13);
			this.linkLabelCP.TabIndex = 4;
			this.linkLabelCP.TabStop = true;
			this.linkLabelCP.Text = "The Code Project";
			this.linkLabelCP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCP_LinkClicked);
			// 
			// Copywrite
			// 
			this.Copywrite.AutoSize = true;
			this.Copywrite.ForeColor = System.Drawing.Color.Red;
			this.Copywrite.Location = new System.Drawing.Point(73, 202);
			this.Copywrite.Name = "Copywrite";
			this.Copywrite.Size = new System.Drawing.Size(196, 13);
			this.Copywrite.TabIndex = 5;
			this.Copywrite.Text = "This program, along with any associated";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(73, 215);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(190, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "source code and files is licensed under";
			// 
			// linkLabelCPOL
			// 
			this.linkLabelCPOL.AutoSize = true;
			this.linkLabelCPOL.LinkColor = System.Drawing.Color.Blue;
			this.linkLabelCPOL.Location = new System.Drawing.Point(73, 228);
			this.linkLabelCPOL.Name = "linkLabelCPOL";
			this.linkLabelCPOL.Size = new System.Drawing.Size(196, 13);
			this.linkLabelCPOL.TabIndex = 7;
			this.linkLabelCPOL.TabStop = true;
			this.linkLabelCPOL.Text = "The Code Project Open License (CPOL)";
			this.linkLabelCPOL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCPOL_LinkClicked);
			// 
			// richTextBoxBackground
			// 
			this.richTextBoxBackground.BackColor = System.Drawing.SystemColors.InfoText;
			this.richTextBoxBackground.CausesValidation = false;
			this.richTextBoxBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBoxBackground.ForeColor = System.Drawing.Color.Yellow;
			this.richTextBoxBackground.Location = new System.Drawing.Point(13, 19);
			this.richTextBoxBackground.Name = "richTextBoxBackground";
			this.richTextBoxBackground.ReadOnly = true;
			this.richTextBoxBackground.Size = new System.Drawing.Size(314, 128);
			this.richTextBoxBackground.TabIndex = 8;
			this.richTextBoxBackground.Text = resources.GetString("richTextBoxBackground.Text");
			// 
			// linkLabelSL
			// 
			this.linkLabelSL.AutoSize = true;
			this.linkLabelSL.Location = new System.Drawing.Point(95, 150);
			this.linkLabelSL.Name = "linkLabelSL";
			this.linkLabelSL.Size = new System.Drawing.Size(153, 13);
			this.linkLabelSL.TabIndex = 9;
			this.linkLabelSL.TabStop = true;
			this.linkLabelSL.Text = "Sebastian Leitner @ Wikipedia";
			this.linkLabelSL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSL_LinkClicked);
			// 
			// aboutMe
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 246);
			this.Controls.Add(this.linkLabelSL);
			this.Controls.Add(this.richTextBoxBackground);
			this.Controls.Add(this.linkLabelCPOL);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Copywrite);
			this.Controls.Add(this.linkLabelCP);
			this.Controls.Add(this.linkLabelJK);
			this.Controls.Add(this.linkLabelMD);
			this.Controls.Add(this.label1);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(139)))), ((int)(((byte)(109)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutMe";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Leitner Three";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelMD;
		private System.Windows.Forms.LinkLabel linkLabelJK;
		private System.Windows.Forms.LinkLabel linkLabelCP;
		private System.Windows.Forms.Label Copywrite;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabelCPOL;
		private System.Windows.Forms.RichTextBox richTextBoxBackground;
		private System.Windows.Forms.LinkLabel linkLabelSL;
    }
}