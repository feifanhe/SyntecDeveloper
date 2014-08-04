namespace Syntec_Developer.Controls
{
	partial class Fenu
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
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblFenuName = new System.Windows.Forms.Label();
			this.lblClose = new System.Windows.Forms.Label();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.pnlContent = new System.Windows.Forms.Panel();
			this.pnlFenu = new System.Windows.Forms.Panel();
			this.pnlHeader.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblFenuName
			// 
			this.lblFenuName.AutoSize = true;
			this.lblFenuName.Location = new System.Drawing.Point( 3, 3 );
			this.lblFenuName.Margin = new System.Windows.Forms.Padding( 3 );
			this.lblFenuName.Name = "lblFenuName";
			this.lblFenuName.Size = new System.Drawing.Size( 67, 12 );
			this.lblFenuName.TabIndex = 0;
			this.lblFenuName.Text = "lblFenuName";
			this.lblFenuName.Click += new System.EventHandler( this.Control_Click );
			// 
			// lblClose
			// 
			this.lblClose.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.lblClose.AutoSize = true;
			this.lblClose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblClose.Location = new System.Drawing.Point( 484, 3 );
			this.lblClose.Margin = new System.Windows.Forms.Padding( 3 );
			this.lblClose.Name = "lblClose";
			this.lblClose.Size = new System.Drawing.Size( 13, 14 );
			this.lblClose.TabIndex = 1;
			this.lblClose.Text = "��";
			this.lblClose.Click += new System.EventHandler( this.lblClose_Click );
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.pnlHeader.CausesValidation = false;
			this.pnlHeader.Controls.Add( this.lblFenuName );
			this.pnlHeader.Controls.Add( this.lblClose );
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point( 0, 0 );
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size( 500, 20 );
			this.pnlHeader.TabIndex = 2;
			this.pnlHeader.Click += new System.EventHandler( this.Control_Click );
			// 
			// pnlContent
			// 
			this.pnlContent.AutoScroll = true;
			this.pnlContent.Controls.Add( this.pnlFenu );
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Location = new System.Drawing.Point( 0, 20 );
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size( 500, 80 );
			this.pnlContent.TabIndex = 3;
			this.pnlContent.Click += new System.EventHandler( this.Control_Click );
			// 
			// pnlFenu
			// 
			this.pnlFenu.AutoSize = true;
			this.pnlFenu.Location = new System.Drawing.Point( 0, 0 );
			this.pnlFenu.Name = "pnlFenu";
			this.pnlFenu.Size = new System.Drawing.Size( 500, 60 );
			this.pnlFenu.TabIndex = 4;
			this.pnlFenu.Click += new System.EventHandler( this.Control_Click );
			// 
			// Fenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Info;
			this.Controls.Add( this.pnlContent );
			this.Controls.Add( this.pnlHeader );
			this.Name = "Fenu";
			this.Size = new System.Drawing.Size( 500, 100 );
			this.Leave += new System.EventHandler( this.Fenu_Leave );
			this.Enter += new System.EventHandler( this.Fenu_Enter );
			this.pnlHeader.ResumeLayout( false );
			this.pnlHeader.PerformLayout();
			this.pnlContent.ResumeLayout( false );
			this.pnlContent.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.Label lblFenuName;
		private System.Windows.Forms.Label lblClose;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Panel pnlContent;
		private System.Windows.Forms.Panel pnlFenu;
	}
}
