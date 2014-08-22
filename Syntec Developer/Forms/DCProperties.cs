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
using Azuria.Common.Controls;

namespace Syntec_Developer.Forms
{
	public partial class DCProperties : DockContent
	{
		DCDocument m_dcdRecentFocusedDocument;

		public FilteredPropertyGrid PropertyDisplay
		{
			get
			{
				return this.prgItemProperties;
			}
		}

		public ComboBox ListDisplay
		{
			get
			{
				return this.cboItems;
			}
		}

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
						prgItemProperties.HiddenAttributes = null;
						prgItemProperties.BrowsableAttributes = null;
						prgItemProperties.SelectedObject = 
							( this.m_dcdRecentFocusedDocument.Browser.Items[ sItemName ] as BrowserItem ).Properties;
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
					prgItemProperties.HiddenAttributes = null;
					prgItemProperties.BrowsableAttributes = null;
					prgItemProperties.SelectedObject = this.m_dcdRecentFocusedDocument.Browser.Properties;
					UpdateComboBoxWithBrowserItem( this.m_dcdRecentFocusedDocument.Browser );
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		#region Browser

		public void BrowserActivated( BrowserPanel bpBrowser )
		{
			prgItemProperties.HiddenAttributes = null;
			prgItemProperties.BrowsableAttributes = null;
			prgItemProperties.SelectedObject = bpBrowser.Properties;
			UpdateComboBoxWithBrowserItem( bpBrowser );
		}

		public void MultiSelectMouseUp( BrowserPanel bpBrowser )
		{
			if( bpBrowser.SelectedItems.Count > 0 ) {
				ShowSelectedItemsProperties( bpBrowser.SelectedItems );
			}
			else {
				prgItemProperties.SelectedObject = bpBrowser.Properties;
				cboItems.SelectedItem = null;
			}
		}

		public void UpdateComboBoxWithBrowserItem( BrowserPanel bpBrowserToLoad )
		{
			this.cboItems.Items.Clear();
			ArrayList arlItemNamesOfFocusedDocument = new ArrayList( bpBrowserToLoad.Items.Keys );
			arlItemNamesOfFocusedDocument.Sort();
			this.cboItems.Items.AddRange( arlItemNamesOfFocusedDocument.ToArray() );
		}

		public void ShowSelectedItemsProperties( List<BrowserItem> lstSelectedItems )
		{
			List<ItemProperties> lstSelectedItemProperties = new List<ItemProperties>();
			foreach( BrowserItem biSelectedItem in lstSelectedItems ) {
				lstSelectedItemProperties.Add( biSelectedItem.Properties );
			}
			prgItemProperties.SelectedObjects = lstSelectedItemProperties.ToArray();
			cboItems.SelectedItem = null;
		}

		#endregion

		private void prgItemProperties_PropertyValueChanged( object s, PropertyValueChangedEventArgs e )
		{
			Console.WriteLine( e.ChangedItem.Label + " " + e.OldValue.ToString() + " -> " + e.ChangedItem.Value.ToString() );
		}
	}
}