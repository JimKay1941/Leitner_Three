namespace Leitner_Three
{
	partial class SelectStudyMode
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose( );
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( )
		{
			this.radioButton1 = new System.Windows.Forms.RadioButton( );
			this.radioButton2 = new System.Windows.Forms.RadioButton( );
			this.radioButton3 = new System.Windows.Forms.RadioButton( );
			this.radioButton4 = new System.Windows.Forms.RadioButton( );
			this.radioButton5 = new System.Windows.Forms.RadioButton( );
			this.radioButton6 = new System.Windows.Forms.RadioButton( );
			this.label1 = new System.Windows.Forms.Label( );
			this.button1 = new System.Windows.Forms.Button( );
			this.SuspendLayout( );
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Font = new System.Drawing.Font( "Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
			this.radioButton1.Location = new System.Drawing.Point( 16, 30 );
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size( 253, 17 );
			this.radioButton1.TabIndex = 0;
			this.radioButton1.Text = "Side 1          Side 2          Side 3";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler( this.radioButton1_CheckedChanged );
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Font = new System.Drawing.Font( "Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
			this.radioButton2.Location = new System.Drawing.Point( 16, 53 );
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size( 253, 17 );
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "Side 1          Side 3          Side 2";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler( this.radioButton2_CheckedChanged );
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Font = new System.Drawing.Font( "Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
			this.radioButton3.Location = new System.Drawing.Point( 16, 76 );
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size( 253, 17 );
			this.radioButton3.TabIndex = 2;
			this.radioButton3.Text = "Side 2          Side 1          Side 3";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton3.CheckedChanged += new System.EventHandler( this.radioButton3_CheckedChanged );
			// 
			// radioButton4
			// 
			this.radioButton4.AutoSize = true;
			this.radioButton4.Font = new System.Drawing.Font( "Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
			this.radioButton4.Location = new System.Drawing.Point( 16, 99 );
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size( 253, 17 );
			this.radioButton4.TabIndex = 3;
			this.radioButton4.Text = "Side 2          Side 3          Side 1";
			this.radioButton4.UseVisualStyleBackColor = true;
			this.radioButton4.CheckedChanged += new System.EventHandler( this.radioButton4_CheckedChanged );
			// 
			// radioButton5
			// 
			this.radioButton5.AutoSize = true;
			this.radioButton5.Font = new System.Drawing.Font( "Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
			this.radioButton5.Location = new System.Drawing.Point( 16, 122 );
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.Size = new System.Drawing.Size( 253, 17 );
			this.radioButton5.TabIndex = 4;
			this.radioButton5.Text = "Side 3          Side 1          Side 2";
			this.radioButton5.UseVisualStyleBackColor = true;
			this.radioButton5.CheckedChanged += new System.EventHandler( this.radioButton5_CheckedChanged );
			// 
			// radioButton6
			// 
			this.radioButton6.AutoSize = true;
			this.radioButton6.Font = new System.Drawing.Font( "Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
			this.radioButton6.Location = new System.Drawing.Point( 16, 145 );
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.Size = new System.Drawing.Size( 253, 17 );
			this.radioButton6.TabIndex = 5;
			this.radioButton6.Text = "Side 3          Side 2          Side 1";
			this.radioButton6.UseVisualStyleBackColor = true;
			this.radioButton6.CheckedChanged += new System.EventHandler( this.radioButton6_CheckedChanged );
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font( "Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
			this.label1.Location = new System.Drawing.Point( 31, 6 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 223, 13 );
			this.label1.TabIndex = 6;
			this.label1.Text = "Question        Answer          Hint";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point( 121, 168 );
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size( 78, 24 );
			this.button1.TabIndex = 1;
			this.button1.Text = "Continue";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler( this.button1_Click );
			this.button1.Enter += new System.EventHandler( this.button1_Enter );
			// 
			// SelectStudyMode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 348, 201 );
			this.Controls.Add( this.button1 );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.radioButton6 );
			this.Controls.Add( this.radioButton5 );
			this.Controls.Add( this.radioButton4 );
			this.Controls.Add( this.radioButton3 );
			this.Controls.Add( this.radioButton2 );
			this.Controls.Add( this.radioButton1 );
			this.Name = "SelectStudyMode";
			this.Text = "Select Study Mode";
			this.ResumeLayout( false );
			this.PerformLayout( );

		}

		#endregion

		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton4;
		private System.Windows.Forms.RadioButton radioButton5;
		private System.Windows.Forms.RadioButton radioButton6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
	}
}