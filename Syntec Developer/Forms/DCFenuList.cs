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
		private FenubarPanel m_fpFocusedFenubar;

		public CheckedListBox FenuList
		{
			get
			{
				return this.chklstFenuList;
			}
		}

		const string CUSTOMFENU = "CUSTOMFENU_";

		public DCFenuList()
		{
			InitializeComponent();
		}

		public void FindFenu( string sFenuName )
		{
			//this.m_dcdRecentFocusedDocument.Fenubar.ShowFenu( sFenuName );
			//if( this.tbcSelectDisplayMode.SelectedTab == tbpFenuList ) {
			//    SetFenuListCheckState();
			//}
			//else {
			//    SetFenuLinkTreeCheckState();
			//}
		}

		private void tbcSelectDisplayMode_Selected( object sender, TabControlEventArgs e )
		{
			//if( e.TabPage == this.tbpFenuList ) {
			//    SetFenuListCheckState();
			//}
			//else {
			//    SetFenuLinkTreeCheckState();
			//}
		}


		public void FenubarActivated( FenubarPanel fpFenubar )
		{
			this.m_fpFocusedFenubar = fpFenubar;
			GenerateList();
		}

		#region Fenu List

		private void GenerateList()
		{
			foreach( Fenubars.Handler handler in this.m_fpFocusedFenubar.Fenubars ) {
				foreach( Fenubars.XML.FenuState fs in handler.LoadedFenus ) {
					this.chklstFenuList.Items.Add( fs.Name );
				}
			}
		}

		private void chklstFenuList_ItemCheck( object sender, ItemCheckEventArgs e )
		{
			if( e.NewValue == CheckState.Checked ) {
				string FenuName = this.chklstFenuList.Items[ e.Index ] as string;
				foreach( Fenubars.Handler handler in this.m_fpFocusedFenubar.Fenubars ) {
					foreach( Fenubars.XML.FenuState fs in handler.LoadedFenus ) {
						if( string.Compare( FenuName, fs.Name ) == 0 ) {
							handler.LoadFenu( FenuName );
							return;
						}
					}
				}
			}
		}

		#endregion

		#region Fenu Link Tree

		private void GenerateTree()
		{
			foreach( Fenubars.Handler handler in this.m_fpFocusedFenubar.Fenubars ) {
				foreach( Fenubars.XML.FenuState fs in handler.LoadedFenus ) {
					if( string.Compare( FenuName, "main" ) == 0 ) {
						handler.LoadFenu( FenuName );
						return;
					}
				}
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
	}
}