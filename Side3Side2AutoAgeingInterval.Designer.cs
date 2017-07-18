namespace Leitner_Three
{
    partial class Side3Side2AutoAgeingInterval
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
			this.C_B_AutoAgeingInterval = new System.Windows.Forms.TextBox( );
			this.label1 = new System.Windows.Forms.Label( );
			this.buttonOK = new System.Windows.Forms.Button( );
			this.SuspendLayout( );
			// 
			// C_B_AutoAgeingInterval
			// 
			this.C_B_AutoAgeingInterval.Location = new System.Drawing.Point( 192, 26 );
			this.C_B_AutoAgeingInterval.Name = "C_B_AutoAgeingInterval";
			this.C_B_AutoAgeingInterval.Size = new System.Drawing.Size( 69, 20 );
			this.C_B_AutoAgeingInterval.TabIndex = 0;
			this.C_B_AutoAgeingInterval.TextChanged += new System.EventHandler( this.C_B_AutoAgeingInterval_TextChanged_1 );
			this.C_B_AutoAgeingInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.C_B_AutoAgeingInterval_KeyPress );
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 20, 57 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 410, 13 );
			this.label1.TabIndex = 1;
			this.label1.Text = "Enter your desired Side 3/Side 2 AutoAgeing interval in days, or \'0\' for no AutoA" +
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
			// Side3Side2AutoAgeingInterval
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 443, 146 );
			this.Controls.Add( this.buttonOK );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.C_B_AutoAgeingInterval );
			this.Name = "Side3Side2AutoAgeingInterval";
			this.Text = "SelectAutoAgeingInterval";
			this.Load += new System.EventHandler( this.Side3Side2AutoAgeingInterval_Load );
			this.ResumeLayout( false );
			this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.TextBox C_B_AutoAgeingInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOK;
    }
}