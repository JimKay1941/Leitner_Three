namespace Leitner_Three
{
    partial class Side1Side3AutoAgeingInterval
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
			this.A_C_AutoAgeingInterval = new System.Windows.Forms.TextBox( );
			this.label1 = new System.Windows.Forms.Label( );
			this.buttonOK = new System.Windows.Forms.Button( );
			this.SuspendLayout( );
			// 
			// A_C_AutoAgeingInterval
			// 
			this.A_C_AutoAgeingInterval.Location = new System.Drawing.Point( 192, 26 );
			this.A_C_AutoAgeingInterval.Name = "A_C_AutoAgeingInterval";
			this.A_C_AutoAgeingInterval.Size = new System.Drawing.Size( 69, 20 );
			this.A_C_AutoAgeingInterval.TabIndex = 0;
			this.A_C_AutoAgeingInterval.TextChanged += new System.EventHandler( this.A_C_AutoAgeingInterval_TextChanged );
			this.A_C_AutoAgeingInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.A_C_AutoAgeingInterval_KeyPress );
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 20, 57 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 410, 13 );
			this.label1.TabIndex = 1;
			this.label1.Text = "Enter your desired Side 1/Side 3 AutoAgeing interval in days, or \'0\' for no AutoA" +
				"geing.";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point( 364, 82 );
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size( 46, 34 );
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler( this.buttonOK_Click );
			// 
			// Side1Side3AutoAgeingInterval
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 443, 146 );
			this.Controls.Add( this.buttonOK );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.A_C_AutoAgeingInterval );
			this.Name = "Side1Side3AutoAgeingInterval";
			this.Text = "SelectAutoAgeingInterval";
			this.Load += new System.EventHandler( this.Side1Side3AutoAgeingInterval_Load );
			this.ResumeLayout( false );
			this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.TextBox A_C_AutoAgeingInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOK;
    }
}