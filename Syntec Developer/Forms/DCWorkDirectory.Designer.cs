namespace Syntec_Developer.Forms
{
	partial class DCWorkDirectory
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( DCWorkDirectory ) );
			this.tvwFile = new System.Windows.Forms.TreeView();
			this.imgTreeView = new System.Windows.Forms.ImageList( this.components );
			this.tbcWorkDirectory = new System.Windows.Forms.TabControl();
			this.tbpFiles = new System.Windows.Forms.TabPage();
			this.tbpProduct = new System.Windows.Forms.TabPage();
			this.tvwProduct = new System.Windows.Forms.TreeView();
			this.tbcWorkDirectory.SuspendLayout();
			this.tbpFiles.SuspendLayout();
			this.tbpProduct.SuspendLayout();
			this.SuspendLayout();
			// 
			// tvwFile
			// 
			this.tvwFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tvwFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwFile.ImageIndex = 0;
			this.tvwFile.ImageList = this.imgTreeView;
			this.tvwFile.Location = new System.Drawing.Point( 3, 3 );
			this.tvwFile.Name = "tvwFile";
			this.tvwFile.SelectedImageIndex = 0;
			this.tvwFile.Size = new System.Drawing.Size( 317, 230 );
			this.tvwFile.TabIndex = 0;
			this.tvwFile.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler( this.tvwFile_NodeMouseDoubleClick );
			this.tvwFile.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler( this.tvwFile_AfterCollapse );
			this.tvwFile.AfterExpand += new System.Windows.Forms.TreeViewEventHandler( this.tvwFile_AfterExpand );
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
			// tbcWorkDirectory
			// 
			this.tbcWorkDirectory.Controls.Add( this.tbpFiles );
			this.tbcWorkDirectory.Controls.Add( this.tbpProduct );
			this.tbcWorkDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbcWorkDirectory.Location = new System.Drawing.Point( 0, 0 );
			this.tbcWorkDirectory.Name = "tbcWorkDirectory";
			this.tbcWorkDirectory.SelectedIndex = 0;
			this.tbcWorkDirectory.Size = new System.Drawing.Size( 331, 262 );
			this.tbcWorkDirectory.TabIndex = 1;
			// 
			// tbpFiles
			// 
			this.tbpFiles.Controls.Add( this.tvwFile );
			this.tbpFiles.Location = new System.Drawing.Point( 4, 22 );
			this.tbpFiles.Name = "tbpFiles";
			this.tbpFiles.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbpFiles.Size = new System.Drawing.Size( 323, 236 );
			this.tbpFiles.TabIndex = 0;
			this.tbpFiles.Text = "Files";
			this.tbpFiles.UseVisualStyleBackColor = true;
			// 
			// tbpProduct
			// 
			this.tbpProduct.Controls.Add( this.tvwProduct );
			this.tbpProduct.Location = new System.Drawing.Point( 4, 22 );
			this.tbpProduct.Name = "tbpProduct";
			this.tbpProduct.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbpProduct.Size = new System.Drawing.Size( 323, 236 );
			this.tbpProduct.TabIndex = 1;
			this.tbpProduct.Text = "Product";
			this.tbpProduct.UseVisualStyleBackColor = true;
			// 
			// tvwProduct
			// 
			this.tvwProduct.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwProduct.ImageIndex = 0;
			this.tvwProduct.ImageList = this.imgTreeView;
			this.tvwProduct.Location = new System.Drawing.Point( 3, 3 );
			this.tvwProduct.Name = "tvwProduct";
			this.tvwProduct.SelectedImageIndex = 0;
			this.tvwProduct.Size = new System.Drawing.Size( 317, 230 );
			this.tvwProduct.TabIndex = 0;
			this.tvwProduct.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler( this.tvwProduct_NodeMouseDoubleClick );
			// 
			// DCWorkDirectory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 331, 262 );
			this.Controls.Add( this.tbcWorkDirectory );
			this.Font = new System.Drawing.Font( "PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ) );
			this.Name = "DCWorkDirectory";
			this.Text = "¤u§@¥Ø¿ý";
			this.tbcWorkDirectory.ResumeLayout( false );
			this.tbpFiles.ResumeLayout( false );
			this.tbpProduct.ResumeLayout( false );
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.ImageList imgTreeView;
		private System.Windows.Forms.TreeView tvwFile;
		private System.Windows.Forms.TabControl tbcWorkDirectory;
		private System.Windows.Forms.TabPage tbpFiles;
		private System.Windows.Forms.TabPage tbpProduct;
		private System.Windows.Forms.TreeView tvwProduct;
	}
}