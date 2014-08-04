namespace Syntec_Developer.Forms
{
	partial class DCDocument
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
			this.ctmsRightClickMenu = new System.Windows.Forms.ContextMenuStrip( this.components );
			this.tsmiBrowseInExplorer = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
			this.ctmsRightClickMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctmsRightClickMenu
			// 
			this.ctmsRightClickMenu.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBrowseInExplorer,
            this.tsmiClose} );
			this.ctmsRightClickMenu.Name = "ctmsRightClickMenu";
			this.ctmsRightClickMenu.Size = new System.Drawing.Size( 161, 48 );
			// 
			// tsmiBrowseInExplorer
			// 
			this.tsmiBrowseInExplorer.Name = "tsmiBrowseInExplorer";
			this.tsmiBrowseInExplorer.Size = new System.Drawing.Size( 160, 22 );
			this.tsmiBrowseInExplorer.Text = "在資料夾中顯示";
			this.tsmiBrowseInExplorer.Click += new System.EventHandler( this.tsmiBrowseInExplorer_Click );
			// 
			// tsmiClose
			// 
			this.tsmiClose.Name = "tsmiClose";
			this.tsmiClose.Size = new System.Drawing.Size( 160, 22 );
			this.tsmiClose.Text = "關閉";
			this.tsmiClose.Click += new System.EventHandler( this.tsmiClose_Click );
			// 
			// DCDocument
			// 
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size( 884, 462 );
			this.DockAreas = ( (WeifenLuo.WinFormsUI.Docking.DockAreas)( ( ( ( ( WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight )
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop )
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom )
						| WeifenLuo.WinFormsUI.Docking.DockAreas.Document ) ) );
			this.Font = new System.Drawing.Font( "PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ) );
			this.Name = "DCDocument";
			this.TabPageContextMenuStrip = this.ctmsRightClickMenu;
			this.ctmsRightClickMenu.ResumeLayout( false );
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip ctmsRightClickMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseInExplorer;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;

	}
}