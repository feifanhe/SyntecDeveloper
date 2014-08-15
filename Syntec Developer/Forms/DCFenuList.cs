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

			}
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

		#region Fenu List

		#endregion

		#region Fenu Link Tree

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