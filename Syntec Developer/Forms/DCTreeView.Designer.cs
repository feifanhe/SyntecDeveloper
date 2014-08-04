namespace Syntec_Developer.Forms
{
	partial class DCTreeView
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( DCTreeView ) );
			this.tvwMain = new System.Windows.Forms.TreeView();
			this.imgTreeView = new System.Windows.Forms.ImageList( this.components );
			this.SuspendLayout();
			// 
			// tvwMain
			// 
			this.tvwMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tvwMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwMain.ImageIndex = 0;
			this.tvwMain.ImageList = this.imgTreeView;
			this.tvwMain.Location = new System.Drawing.Point( 0, 0 );
			this.tvwMain.Name = "tvwMain";
			this.tvwMain.SelectedImageIndex = 0;
			this.tvwMain.Size = new System.Drawing.Size( 331, 262 );
			this.tvwMain.TabIndex = 0;
			this.tvwMain.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler( this.tvwMain_AfterCollapse );
			this.tvwMain.DoubleClick += new System.EventHandler( this.tvwMain_DoubleClick );
			this.tvwMain.AfterExpand += new System.Windows.Forms.TreeViewEventHandler( this.tvwMain_AfterExpand );
			// 
			// imgTreeView
			// 
			this.imgTreeView.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "imgTreeView.ImageStream" ) ) );
			this.imgTreeView.TransparentColor = System.Drawing.Color.Fuchsia;
			this.imgTreeView.Images.SetKeyName( 0, "window.bmp" );
			this.imgTreeView.Images.SetKeyName( 1, "VSFolder_closed.bmp" );
			this.imgTreeView.Images.SetKeyName( 2, "VSFolder_open.bmp" );
			this.imgTreeView.Images.SetKeyName( 3, "VSProject_xml.bmp" );
			this.imgTreeView.Images.SetKeyName( 4, "DocumentHS.png" );
			// 
			// DCTreeView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 331, 262 );
			this.Controls.Add( this.tvwMain );
			this.Font = new System.Drawing.Font( "PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ) );
			this.Name = "DCTreeView";
			this.Text = "�u�@�ؿ�";
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.ImageList imgTreeView;
		private System.Windows.Forms.TreeView tvwMain;
	}
}