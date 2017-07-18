namespace Leitner_Three
{
    partial class SetBackupPath
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
			this.textUserPath = new System.Windows.Forms.TextBox();
			this.lbllabel1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnChoosePath = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textUserPath
			// 
			this.textUserPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textUserPath.Location = new System.Drawing.Point(174, 73);
			this.textUserPath.Name = "textUserPath";
			this.textUserPath.Size = new System.Drawing.Size(516, 26);
			this.textUserPath.TabIndex = 0;
			this.textUserPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textUserPath_KeyPress);
			// 
			// lbllabel1
			// 
			this.lbllabel1.AutoSize = true;
			this.lbllabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbllabel1.Location = new System.Drawing.Point(12, 76);
			this.lbllabel1.Name = "lbllabel1";
			this.lbllabel1.Size = new System.Drawing.Size(153, 20);
			this.lbllabel1.TabIndex = 1;
			this.lbllabel1.Text = "Path to Lesson Files";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(174, 12);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(50, 35);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// btnChoosePath
			// 
			this.btnChoosePath.Location = new System.Drawing.Point(16, 12);
			this.btnChoosePath.Name = "btnChoosePath";
			this.btnChoosePath.Size = new System.Drawing.Size(128, 35);
			this.btnChoosePath.TabIndex = 3;
			this.btnChoosePath.Text = "Choose Path";
			this.btnChoosePath.UseVisualStyleBackColor = true;
			this.btnChoosePath.Click += new System.EventHandler(this.button1_Click);
			// 
			// SetBackupPath
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(702, 116);
			this.Controls.Add(this.btnChoosePath);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lbllabel1);
			this.Controls.Add(this.textUserPath);
			this.Name = "SetBackupPath";
			this.Text = "SetUserPath";
			this.Load += new System.EventHandler(this.SetUserPath_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textUserPath;
        private System.Windows.Forms.Label lbllabel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnChoosePath;
    }
}