using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections;
using Syntec_Developer.Controls;
using System.Threading;

namespace Syntec_Developer.Forms
{
	public partial class DCFenuList : DockContent
	{
		private Hashtable m_htbFenusOfFocusedDocument;
		private DCDocument m_dcdRecentFocusedDocument;

		const string CUSTOMFENU = "CUSTOMFENU_";

		public DCFenuList()
		{
			InitializeComponent();
		}

		public void Document_Activated( object sender, EventArgs e )
		{
			this.m_dcdRecentFocusedDocument = sender as DCDocument;
			if( this.m_dcdRecentFocusedDocument.Type == DocumentType.fenubar ) {
				this.m_htbFenusOfFocusedDocument = this.m_dcdRecentFocusedDocument.Fenubar.Fenus;
				ConstructFenuList();
				ConstructFenuLinkTree();
			}
		}

		public void Fenubar_XmlLoadCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			FenubarPanel fpLoadedFenubar = sender as FenubarPanel;
			this.m_htbFenusOfFocusedDocument = fpLoadedFenubar.Fenus;
			ConstructFenuList();
			ConstructFenuLinkTree();
		}

		public void Fenubar_FenuShowByKeyLink( object sender, EventArgs e )
		{
			SetFenuListCheckState();
			SetFenuLinkTreeCheckState();
		}

		public void Fenu_Close( object sender, EventArgs e )
		{
			Fenu fnFenuToClose = sender as Fenu;
			int nIndexOfFenuToClose = this.chklstFenuList.Items.IndexOf( fnFenuToClose.Properties.Name );
			this.chklstFenuList.SetItemCheckState( nIndexOfFenuToClose, CheckState.Unchecked );
			foreach( TreeNode tnFenu in this.tvwFenuLinkTree.Nodes.Find( fnFenuToClose.Properties.Name, true ) ) {
				tnFenu.Checked = false;
			}
		}

		public void FindFenu( string sFenuName )
		{
			this.m_dcdRecentFocusedDocument.Fenubar.ShowFenu( sFenuName );
			if( this.tbcSelectDisplayMode.SelectedTab == tbpFenuList ) {
				SetFenuListCheckState();
			}
			else {
				SetFenuLinkTreeCheckState();
			}
		}

		private void tbcSelectDisplayMode_Selected( object sender, TabControlEventArgs e )
		{
			if( e.TabPage == this.tbpFenuList ) {
				SetFenuListCheckState();
			}
			else {
				SetFenuLinkTreeCheckState();
			}
		}

		#region Fenu List

		private void ConstructFenuList()
		{
			this.chklstFenuList.Items.Clear();
			ArrayList arlFenuNames = new ArrayList( this.m_htbFenusOfFocusedDocument.Keys );
			arlFenuNames.Sort();
			this.chklstFenuList.Items.AddRange( arlFenuNames.ToArray() );
			SetFenuListCheckState();
		}

		private void SetFenuListCheckState()
		{
			foreach( Fenu fnFenu in this.m_dcdRecentFocusedDocument.Fenubar.OpenedFenus.ToArray() ) {
				int nIndex = this.chklstFenuList.Items.IndexOf( fnFenu.Properties.Name );
				if( nIndex != -1 ) {
					this.chklstFenuList.SetItemCheckState( nIndex, CheckState.Checked );
				}
			}
		}

		private void chklstFenuList_ItemCheck( object sender, ItemCheckEventArgs e )
		{
			if( this.m_dcdRecentFocusedDocument != null ) {
				string sFenuName = this.chklstFenuList.Items[ e.Index ].ToString();
				if( e.NewValue == CheckState.Checked ) {
					this.m_dcdRecentFocusedDocument.Fenubar.ShowFenu( sFenuName );
				}
				else if( e.NewValue == CheckState.Unchecked ) {
					this.m_dcdRecentFocusedDocument.Fenubar.HideFenu( sFenuName );
				}
			}
		}

		#endregion

		#region Fenu Link Tree

		#region Build Tree

		private void ConstructFenuLinkTree()
		{
			this.tvwFenuLinkTree.Nodes.Clear();
			if( this.m_htbFenusOfFocusedDocument.ContainsKey( "main" ) ) {
				this.tvwFenuLinkTree.Nodes.Add( "main", "main" );
				TreeNode tnRootNode = this.tvwFenuLinkTree.Nodes[ "main" ];
				AddChildNodes( tnRootNode );
				tnRootNode.ExpandAll();
			}
			SetFenuLinkTreeCheckState();
		}

		private void AddChildNodes( TreeNode tnParentNode )
		{
			Fenu fnParentFenu = this.m_htbFenusOfFocusedDocument[ tnParentNode.Text ] as Fenu;
			if( fnParentFenu == null ) {
				return;
			}
			foreach( FenuButton fbButton in fnParentFenu.Buttons ) {
				AddChildNodesFromLink( tnParentNode, fbButton.Properties.Link );
				foreach( List<string> lstActions
					in new List<string>[] { 
						fbButton.Properties.Actions, 
						fbButton.Properties.ActionsWithPassword.CorrectActions, 
						fbButton.Properties.ActionsWithPassword.IncorrectActions } ) {
					AddChildNodesFromActions( tnParentNode, lstActions );
				}
			}
		}

		private void AddChildNodesFromLink( TreeNode tnParentNode, string sLinkTarget )
		{
			if( string.Compare( sLinkTarget, string.Empty ) != 0 && NoLinkLoop( tnParentNode, sLinkTarget ) ) {
				tnParentNode.Nodes.Add( sLinkTarget, sLinkTarget );
				TreeNode tnChildNode = tnParentNode.Nodes[ sLinkTarget ];
				AddChildNodes( tnChildNode );
			}
		}

		private void AddChildNodesFromActions( TreeNode tnParentNode, List<string> lstActions )
		{
			foreach( string sAction in lstActions ) {
				if( sAction.IndexOf( CUSTOMFENU ) == 0 ) {
					string sTargetFenuName = sAction.Substring( CUSTOMFENU.Length );
					if( NoLinkLoop( tnParentNode, sTargetFenuName ) ) {
						tnParentNode.Nodes.Add( sTargetFenuName, sTargetFenuName );
						TreeNode tnChildNode = tnParentNode.Nodes[ sTargetFenuName ];
						AddChildNodes( tnChildNode );
					}
				}
			}
		}

		private bool NoLinkLoop( TreeNode tnParentNode, string sFenuName )
		{
			if( tnParentNode == null ) {
				return true;
			}
			else {
				if( string.Compare( tnParentNode.Text, sFenuName ) == 0 ) {
					return false;
				}
				else {
					return NoLinkLoop( tnParentNode.Parent, sFenuName );
				}
			}
		}

		#endregion

		private void SetFenuLinkTreeCheckState()
		{
			foreach( Fenu fnFenu in this.m_dcdRecentFocusedDocument.Fenubar.OpenedFenus.ToArray() ) {
				foreach( TreeNode tnOpenedFenu in this.tvwFenuLinkTree.Nodes.Find( fnFenu.Properties.Name, true ) ) {
					tnOpenedFenu.Checked = true;
				}
			}
		}

		private void tvwFenuLinkTree_AfterCheck( object sender, TreeViewEventArgs e )
		{
			if( e.Node.Checked ) {
				this.m_dcdRecentFocusedDocument.Fenubar.ShowFenu( e.Node.Text );
			}
			else {
				this.m_dcdRecentFocusedDocument.Fenubar.HideFenu( e.Node.Text );
			}
		}

		private void tvwFenuLinkTree_BeforeCheck( object sender, TreeViewCancelEventArgs e )
		{
			if( !this.m_dcdRecentFocusedDocument.Fenubar.Fenus.ContainsKey( e.Node.Text ) ) {
				MessageBox.Show( "The Fenu does not exist in current file." );
				e.Cancel = true;
			}
		}

		#endregion

		private void chklstFenuList_MouseUp( object sender, MouseEventArgs e )
		{
			if( e.Button == MouseButtons.Right ) {
				int nItemIndex = chklstFenuList.IndexFromPoint( e.Location );
				if( nItemIndex >= 0 ) {
					chklstFenuList.SelectedIndex = nItemIndex;
					ctmsRightClickItem.Show( chklstFenuList, e.Location );
				}
			}
		}

		private void tsmiNewFenu_Click( object sender, EventArgs e )
		{
			FormNewFenu frmNewFenu = new FormNewFenu();
			if( frmNewFenu.ShowDialog( this ) == DialogResult.OK ) {
				this.m_dcdRecentFocusedDocument.Fenubar.NewFenu( frmNewFenu.FenuName );
				ConstructFenuList();
			}

		}

		private void tsmiDeleteFenu_Click( object sender, EventArgs e )
		{
			string sFenuName = this.chklstFenuList.SelectedItem.ToString();
			DialogResult dlgrsDelete = MessageBox.Show(
				string.Concat( "刪除", sFenuName, "?" ), "刪除功能鍵", MessageBoxButtons.YesNo
			);
			if( dlgrsDelete == DialogResult.Yes ) {
				this.m_dcdRecentFocusedDocument.Fenubar.DeleteFenu( sFenuName );
				ConstructFenuList();
			}
		}

		private void tsmiCopyFenu_Click( object sender, EventArgs e )
		{
			string sFenuName = this.chklstFenuList.SelectedItem.ToString();
			this.m_dcdRecentFocusedDocument.Fenubar.CopyFenu( sFenuName );
			ConstructFenuList();
		}

	}
}