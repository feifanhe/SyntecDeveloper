namespace Syntec_Developer.Forms
{
	partial class DCFenuList
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
			this.tbcSelectDisplayMode = new System.Windows.Forms.TabControl();
			this.tbpFenuList = new System.Windows.Forms.TabPage();
			this.chklstFenuList = new System.Windows.Forms.CheckedListBox();
			this.tbpFenuLinkTree = new System.Windows.Forms.TabPage();
			this.tvwFenuLinkTree = new System.Windows.Forms.TreeView();
			this.ctmsRightClickItem = new System.Windows.Forms.ContextMenuStrip( this.components );
			this.tsmiNewFenu = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyFenu = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiDeleteFenu = new System.Windows.Forms.ToolStripMenuItem();
			this.tbcSelectDisplayMode.SuspendLayout();
			this.tbpFenuList.SuspendLayout();
			this.tbpFenuLinkTree.SuspendLayout();
			this.ctmsRightClickItem.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbcSelectDisplayMode
			// 
			this.tbcSelectDisplayMode.Controls.Add( this.tbpFenuList );
			this.tbcSelectDisplayMode.Controls.Add( this.tbpFenuLinkTree );
			this.tbcSelectDisplayMode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbcSelectDisplayMode.Location = new System.Drawing.Point( 0, 0 );
			this.tbcSelectDisplayMode.Multiline = true;
			this.tbcSelectDisplayMode.Name = "tbcSelectDisplayMode";
			this.tbcSelectDisplayMode.SelectedIndex = 0;
			this.tbcSelectDisplayMode.Size = new System.Drawing.Size( 284, 262 );
			this.tbcSelectDisplayMode.TabIndex = 0;
			this.tbcSelectDisplayMode.Selected += new System.Windows.Forms.TabControlEventHandler( this.tbcSelectDisplayMode_Selected );
			// 
			// tbpFenuList
			// 
			this.tbpFenuList.Controls.Add( this.chklstFenuList );
			this.tbpFenuList.Location = new System.Drawing.Point( 4, 22 );
			this.tbpFenuList.Name = "tbpFenuList";
			this.tbpFenuList.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbpFenuList.Size = new System.Drawing.Size( 276, 236 );
			this.tbpFenuList.TabIndex = 1;
			this.tbpFenuList.Text = "List";
			this.tbpFenuList.UseVisualStyleBackColor = true;
			// 
			// chklstFenuList
			// 
			this.chklstFenuList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.chklstFenuList.CheckOnClick = true;
			this.chklstFenuList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chklstFenuList.FormattingEnabled = true;
			this.chklstFenuList.Location = new System.Drawing.Point( 3, 3 );
			this.chklstFenuList.Name = "chklstFenuList";
			this.chklstFenuList.Size = new System.Drawing.Size( 270, 223 );
			this.chklstFenuList.TabIndex = 2;
			this.chklstFenuList.MouseUp += new System.Windows.Forms.MouseEventHandler( this.chklstFenuList_MouseUp );
			// 
			// tbpFenuLinkTree
			// 
			this.tbpFenuLinkTree.Controls.Add( this.tvwFenuLinkTree );
			this.tbpFenuLinkTree.Location = new System.Drawing.Point( 4, 22 );
			this.tbpFenuLinkTree.Name = "tbpFenuLinkTree";
			this.tbpFenuLinkTree.Padding = new System.Windows.Forms.Padding( 3 );
			this.tbpFenuLinkTree.Size = new System.Drawing.Size( 276, 236 );
			this.tbpFenuLinkTree.TabIndex = 0;
			this.tbpFenuLinkTree.Text = "Link Tree";
			this.tbpFenuLinkTree.UseVisualStyleBackColor = true;
			// 
			// tvwFenuLinkTree
			// 
			this.tvwFenuLinkTree.CheckBoxes = true;
			this.tvwFenuLinkTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwFenuLinkTree.Location = new System.Drawing.Point( 3, 3 );
			this.tvwFenuLinkTree.Name = "tvwFenuLinkTree";
			this.tvwFenuLinkTree.Size = new System.Drawing.Size( 270, 230 );
			this.tvwFenuLinkTree.TabIndex = 1;
			// 
			// ctmsRightClickItem
			// 
			this.ctmsRightClickItem.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewFenu,
            this.tssSep1,
            this.tsmiCopyFenu,
            this.tssSep2,
            this.tsmiDeleteFenu} );
			this.ctmsRightClickItem.Name = "ctmsRightClickItem";
			this.ctmsRightClickItem.Size = new System.Drawing.Size( 101, 82 );
			// 
			// tsmiNewFenu
			// 
			this.tsmiNewFenu.Name = "tsmiNewFenu";
			this.tsmiNewFenu.Size = new System.Drawing.Size( 100, 22 );
			this.tsmiNewFenu.Text = "新增";
			// 
			// tssSep1
			// 
			this.tssSep1.Name = "tssSep1";
			this.tssSep1.Size = new System.Drawing.Size( 97, 6 );
			// 
			// tsmiCopyFenu
			// 
			this.tsmiCopyFenu.Name = "tsmiCopyFenu";
			this.tsmiCopyFenu.Size = new System.Drawing.Size( 100, 22 );
			this.tsmiCopyFenu.Text = "複製";
			// 
			// tssSep2
			// 
			this.tssSep2.Name = "tssSep2";
			this.tssSep2.Size = new System.Drawing.Size( 97, 6 );
			// 
			// tsmiDeleteFenu
			// 
			this.tsmiDeleteFenu.Name = "tsmiDeleteFenu";
			this.tsmiDeleteFenu.Size = new System.Drawing.Size( 100, 22 );
			this.tsmiDeleteFenu.Text = "刪除";
			// 
			// DCFenuList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 284, 262 );
			this.Controls.Add( this.tbcSelectDisplayMode );
			this.Font = new System.Drawing.Font( "PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ) );
			this.Name = "DCFenuList";
			this.Text = "功能鍵列表";
			this.tbcSelectDisplayMode.ResumeLayout( false );
			this.tbpFenuList.ResumeLayout( false );
			this.tbpFenuLinkTree.ResumeLayout( false );
			this.ctmsRightClickItem.ResumeLayout( false );
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.TabControl tbcSelectDisplayMode;
		private System.Windows.Forms.TabPage tbpFenuLinkTree;
		private System.Windows.Forms.TabPage tbpFenuList;
		private System.Windows.Forms.CheckedListBox chklstFenuList;
		private System.Windows.Forms.TreeView tvwFenuLinkTree;
		private System.Windows.Forms.ContextMenuStrip ctmsRightClickItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiNewFenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteFenu;
		private System.Windows.Forms.ToolStripSeparator tssSep1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyFenu;
		private System.Windows.Forms.ToolStripSeparator tssSep2;

	}
}