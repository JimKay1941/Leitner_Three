﻿namespace Leitner_Three
{
    partial class NewLesson
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
			this.components = new System.ComponentModel.Container( );
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( NewLesson ) );
			this.label1 = new System.Windows.Forms.Label( );
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox( );
			this.button1 = new System.Windows.Forms.Button( );
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider( this.components );
			( ( System.ComponentModel.ISupportInitialize ) ( this.errorProvider1 ) ).BeginInit( );
			this.SuspendLayout( );
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 19, 29 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 41, 13 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Name :";
			// 
			// maskedTextBox1
			// 
			this.maskedTextBox1.AllowPromptAsInput = false;
			this.maskedTextBox1.BeepOnError = true;
			this.maskedTextBox1.Location = new System.Drawing.Point( 66, 26 );
			this.maskedTextBox1.Name = "maskedTextBox1";
			this.maskedTextBox1.PromptChar = ' ';
			this.maskedTextBox1.ResetOnPrompt = false;
			this.maskedTextBox1.ResetOnSpace = false;
			this.maskedTextBox1.Size = new System.Drawing.Size( 185, 20 );
			this.maskedTextBox1.TabIndex = 1;
			this.maskedTextBox1.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler( this.maskedTextBox1_MaskInputRejected );
			this.maskedTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.maskedTextBox1_KeyPress );
			this.maskedTextBox1.Click += new System.EventHandler( this.button1_Click );
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point( 80, 63 );
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size( 110, 23 );
			this.button1.TabIndex = 2;
			this.button1.Text = "Create New Lesson";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler( this.button1_Click );
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// newUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 271, 111 );
			this.Controls.Add( this.button1 );
			this.Controls.Add( this.maskedTextBox1 );
			this.Controls.Add( this.label1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject( "$this.Icon" ) ) );
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "newUser";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Lesson";
			this.ResumeLayout( false );
			this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}