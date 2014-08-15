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
using Syntec_Developer.Controls.PropertyClasses;

namespace Syntec_Developer.Forms
{
	public partial class DCProperties : DockContent
	{
		Hashtable m_htbItemsOfFocusedDocument;
		DCDocument m_dcdRecentFocusedDocument;

		public DCProperties()
		{
			InitializeComponent();
		}

		private void cboItems_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( this.cboItems.SelectedItem != null ) {
				string sItemName = this.cboItems.SelectedItem.ToString();
				switch( this.m_dcdRecentFocusedDocument.Type ) {
					case DocumentType.browser:
						BrowserItem biItemToShow = m_htbItemsOfFocusedDocument[ sItemName ] as BrowserItem;
						prgItemProperties.SelectedObject = biItemToShow.Properties;
						break;
					case DocumentType.fenubar:
						break;
				}
			}
		}

		public void Document_Activated( object sender, EventArgs e )
		{
			this.m_dcdRecentFocusedDocument = sender as DCDocument;
			switch( this.m_dcdRecentFocusedDocument.Type ) {
				case DocumentType.browser:
					prgItemProperties.SelectedObject = this.m_dcdRecentFocusedDocument.Browser.Properties;
					UpdateComboBoxWithBrowserItem( this.m_dcdRecentFocusedDocument.Browser );
					break;
				case DocumentType.fenubar:
					//prgItemProperties.SelectedObject = this.m_dcdRecentFocusedDocument.Fenubar.Properties;
					//UpdateComboBoxWithFenu( this.m_dcdRecentFocusedDocument.Fenubar );
					break;
			}
		}

		public void Browser_XmlLoadCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			BrowserPanel bpBrowserLoadCompleted = sender as BrowserPanel;
			UpdateComboBoxWithBrowserItem( bpBrowserLoadCompleted );
		}

		public void Browser_MouseUp( object sender, MouseEventArgs e )
		{
			BrowserPanel bpBrowser = sender as BrowserPanel;
			if( bpBrowser.SelectedItems.Count > 0 ) {
				ShowSelectedItemsProperties( bpBrowser.SelectedItems );
			}
			else {
				prgItemProperties.SelectedObject = bpBrowser.Properties;
				cboItems.SelectedItem = null;
			}
		}

		public void BrowserItem_AddedDeleted( object sender, EventArgs e )
		{
			BrowserPanel bpModifiedBrowser = sender as BrowserPanel;
			UpdateComboBoxWithBrowserItem( bpModifiedBrowser );
		}

		private void UpdateComboBoxWithBrowserItem( BrowserPanel bpBrowserToLoad )
		{
			this.cboItems.Items.Clear();
			this.m_htbItemsOfFocusedDocument = bpBrowserToLoad.Items;
			ArrayList arlItemNamesOfFocusedDocument = new ArrayList( this.m_htbItemsOfFocusedDocument.Keys );
			arlItemNamesOfFocusedDocument.Sort();
			this.cboItems.Items.AddRange( arlItemNamesOfFocusedDocument.ToArray() );
		}

		public void BrowserItem_MouseDown( object sender, EventArgs e )
		{
			List<BrowserItem> lstSelectedItems = sender as List<BrowserItem>;
			ShowSelectedItemsProperties( lstSelectedItems );
		}

		public void BrowserItem_PropertiesChanged( object sender, EventArgs e )
		{
			List<BrowserItem> lstSelectedItems = sender as List<BrowserItem>;
			ShowSelectedItemsProperties( lstSelectedItems );
		}

		private void ShowSelectedItemsProperties( List<BrowserItem> lstSelectedItems )
		{
			List<ItemProperties> lstSelectedItemProperties = new List<ItemProperties>();
			foreach( BrowserItem biSelectedItem in lstSelectedItems ) {
				lstSelectedItemProperties.Add( biSelectedItem.Properties );
			}
			prgItemProperties.SelectedObjects = lstSelectedItemProperties.ToArray();
			cboItems.SelectedItem = null;
		}

		private void prgItemProperties_PropertyValueChanged( object s, PropertyValueChangedEventArgs e )
		{
			Console.WriteLine( e.ChangedItem.Label + " " + e.OldValue.ToString() + " -> " + e.ChangedItem.Value.ToString() );
		}
	}
}