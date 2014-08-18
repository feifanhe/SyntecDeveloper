using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace Syntec_Developer.Forms
{
	public partial class DCWorkDirectory : DockContent
	{
		public event EventHandler TreeViewDoubleClick;

		enum ImageIndex
		{
			ROOT,
			FOLDER,
			FOLDER_OPEN,
			XML_FILE,
			FILE
		};

		public DCWorkDirectory()
		{
			InitializeComponent();
		}

		public void LoadDirectory( string sDirectory )
		{
			this.tvwMain.Nodes.Clear();
			if( Directory.Exists( sDirectory ) ) {
				DirectoryInfo difRootDirectory = new DirectoryInfo( sDirectory );
				TreeNode tnRootNode = new TreeNode(
					difRootDirectory.FullName,
					(int)ImageIndex.ROOT,
					(int)ImageIndex.ROOT,
					GetTreeNodes( difRootDirectory.GetDirectories(), difRootDirectory.GetFiles() )
				);

				TreeNode tnProduct = new TreeNode(
					difRootDirectory.FullName,
					(int)ImageIndex.ROOT,
					(int)ImageIndex.ROOT,
					GetProductNodes( difRootDirectory.GetDirectories() )
				);

				tnRootNode.Expand();
				this.tvwMain.Nodes.Add( tnRootNode );
				this.tvwMain.Nodes.Add( tnProduct );
			}
		}

		// This function will be delete as new standards using DiskC Structure rather than directory structure
		private TreeNode[] GetTreeNodes( DirectoryInfo[] difaDirectories, FileInfo[] fifaFiles )
		{
			TreeNode[] tnaDirNodes = null;
			TreeNode[] tnaFileNodes = null;
			TreeNode[] tnaResult = null;

			// Set directory nodes
			tnaDirNodes = new TreeNode[ difaDirectories.Length ];
			for( int i = 0; i < difaDirectories.Length; i++ ) {
				tnaDirNodes[ i ] = new TreeNode(
					difaDirectories[ i ].Name,
					(int)ImageIndex.FOLDER,
					(int)ImageIndex.FOLDER,
					GetTreeNodes( difaDirectories[ i ].GetDirectories(), difaDirectories[ i ].GetFiles() )
				);
			}

			// Set file nodes
			tnaFileNodes = new TreeNode[ fifaFiles.Length ];
			for( int i = 0; i < fifaFiles.Length; i++ ) {
				if( string.Compare( fifaFiles[ i ].Extension.ToLower(), ".xml" ) == 0 ) {
					tnaFileNodes[ i ] = new TreeNode(
						fifaFiles[ i ].Name,
						(int)ImageIndex.XML_FILE,
						(int)ImageIndex.XML_FILE
					);
				}
				else {
					tnaFileNodes[ i ] = new TreeNode(
						fifaFiles[ i ].Name,
						(int)ImageIndex.FILE,
						(int)ImageIndex.FILE
					);
				}
			}
			// Combine directory nodes and file nodes
			tnaResult = new TreeNode[ tnaDirNodes.Length + tnaFileNodes.Length ];
			tnaDirNodes.CopyTo( tnaResult, 0 );
			tnaFileNodes.CopyTo( tnaResult, tnaDirNodes.Length );

			return tnaResult;

		}

		private TreeNode[] GetProductNodes( DirectoryInfo[] difaDirectories )
		{
			List<TreeNode> lstProductNodes = new List<TreeNode>();

			// Set directory nodes
			for( int i = 0; i < difaDirectories.Length; i++ ) {
				if( difaDirectories[ i ].Name.IndexOf( '_' ) == 0 ) {
					lstProductNodes.Add(
						new TreeNode(
							difaDirectories[ i ].Name,
							(int)ImageIndex.FOLDER,
							(int)ImageIndex.FOLDER,
							GetProductNodes( difaDirectories[ i ].GetDirectories() )
						)
					);
				}
			}

			return lstProductNodes.ToArray();
		}

		private void tvwMain_AfterExpand( object sender, TreeViewEventArgs e )
		{
			if( e.Node.Level != 0 )
				e.Node.ImageIndex = (int)ImageIndex.FOLDER_OPEN;
		}

		private void tvwMain_AfterCollapse( object sender, TreeViewEventArgs e )
		{
			if( e.Node.Level != 0 )
				e.Node.ImageIndex = (int)ImageIndex.FOLDER;
		}

		private void tvwMain_DoubleClick( object sender, EventArgs e )
		{
			this.TreeViewDoubleClick.Invoke( sender, e );
		}


	}
}