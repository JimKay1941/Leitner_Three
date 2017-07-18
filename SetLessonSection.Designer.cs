namespace Leitner_Three
{
    partial class SetLessonSection
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
			this.label1 = new System.Windows.Forms.Label( );
			this.textLessonSection = new System.Windows.Forms.TextBox( );
			this.buttonOK = new System.Windows.Forms.Button( );
			this.SuspendLayout( );
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 51, 70 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 185, 13 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter your Lesson Section information";
			// 
			// textLessonSection
			// 
			this.textLessonSection.Location = new System.Drawing.Point( 98, 29 );
			this.textLessonSection.Name = "textLessonSection";
			this.textLessonSection.Size = new System.Drawing.Size( 90, 20 );
			this.textLessonSection.TabIndex = 1;
			this.textLessonSection.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.textLessonSection_KeyPress );
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point( 234, 88 );
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size( 34, 22 );
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler( this.buttonOK_Click );
			// 
			// SetLessonSection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 284, 116 );
			this.Controls.Add( this.buttonOK );
			this.Controls.Add( this.textLessonSection );
			this.Controls.Add( this.label1 );
			this.Name = "SetLessonSection";
			this.Text = "SetLessonSection";
			this.Load += new System.EventHandler( this.SetLessonSection_Load );
			this.ResumeLayout( false );
			this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textLessonSection;
        private System.Windows.Forms.Button buttonOK;
    }
}