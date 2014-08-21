using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;
using Syntec_Developer.Controls;
using System.Diagnostics;

namespace Syntec_Developer.Forms
{
	public partial class DCDocument : DockContent
	{
		// Flags
		private bool m_bIsNewFile;
		private bool m_bIsModified;

		// Resources
		private ComponentResourceManager resources = new ComponentResourceManager( typeof( DCDocument ) );

		private DocumentType m_dtType;
		public DocumentType Type
		{
			get
			{
				return this.m_dtType;
			}
		}

		private string m_sFullName;
		public string FullName
		{
			get
			{
				return this.m_sFullName;
			}
		}

		private BrowserPanel m_bpBrowser;
		public BrowserPanel Browser
		{
			get
			{
				return this.m_bpBrowser;
			}
		}

		private FenubarPanel m_fpFenubar;
		public FenubarPanel Fenubar
		{
			get
			{
				return this.m_fpFenubar;
			}
		}

		#region Initialize

		public DCDocument()
		{
			InitializeComponent();
		}

		public DCDocument( DocumentType dtType, string sFullName, bool bIsNewFile )
		{
			InitializeComponent();

			this.m_dtType = dtType;
			this.m_sFullName = sFullName;
			this.m_bIsNewFile = bIsNewFile;
		}

		public static DCDocument CreateProduct( string sProductPath )
		{
			DCDocument document = new DCDocument();
			document.m_dtType = DocumentType.fenubar;
			document.m_sFullName = sProductPath;
			return document;
		}

		public void LoadProduct()
		{
			this.m_fpFenubar = new FenubarPanel();
			this.m_fpFenubar.LoadProduct( this.m_sFullName );
			this.m_fpFenubar.Dock = DockStyle.Fill;

			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "Fenubar" ) ) );
			this.Text = this.m_sFullName;
			this.Controls.Add( this.m_fpFenubar );
		}

		public void LoadFile()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					InitializeBrowser();
					break;
				case DocumentType.fenubar:
					InitializeFenubar();
					break;
			}
		}

		private void InitializeBrowser()
		{
			this.m_bpBrowser = new BrowserPanel( this.m_sFullName, this.m_bIsNewFile );
			this.m_bpBrowser.Location = new Point( 3, 3 );

			this.m_bpBrowser.MouseUp += new MouseEventHandler( Browser_MouseUp );
			this.m_bpBrowser.XmlLoadCompleted +=
				new BrowserPanel.XmlLoadCompletedHandler( Browser_XmlLoadCompleted );
			this.m_bpBrowser.ItemMouseDown +=
				new BrowserPanel.ItemMouseDownHandler( BrowserItem_MouseDown );
			this.m_bpBrowser.ItemPropertiesChanged +=
				new BrowserPanel.ItemPropertiesChangedHandler( BrowserItem_PropertiesChanged );
			this.m_bpBrowser.ItemAddedDeleted +=
				new BrowserPanel.ItemAddedDeletedHandler( BrowserItem_AddedDeleted );

			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "Browser" ) ) );
			this.Text = this.m_bpBrowser.FileName;
			this.Controls.Add( this.m_bpBrowser );
		}

		private void InitializeFenubar()
		{
			this.m_fpFenubar = new FenubarPanel();
			this.m_fpFenubar.LoadFenubar( this.m_sFullName );
			this.m_fpFenubar.Dock = DockStyle.Fill;

			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "Fenubar" ) ) );
			this.Text = this.m_sFullName;
			this.Controls.Add( this.m_fpFenubar );
		}


		private void DCDocument_Activated( object sender, EventArgs e )
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					FormMain.PropertiesWindow.BrowserActivated( this.Browser );
					break;
				case DocumentType.fenubar:
					FormMain.FenuListWindow.FenubarActivated( this.Fenubar );
					break;
			}
		}

		#endregion

		#region Browser Event

		public void BrowserItem_MouseDown( object sender, EventArgs e )
		{
			FormMain.PropertiesWindow.ShowSelectedItemsProperties( this.Browser.SelectedItems );
		}

		public void BrowserItem_PropertiesChanged( object sender, EventArgs e )
		{
			CheckModifiedState();
			FormMain.PropertiesWindow.ShowSelectedItemsProperties( this.Browser.SelectedItems );
		}

		public void BrowserItem_AddedDeleted( object sender, EventArgs e )
		{
			CheckModifiedState();
			FormMain.PropertiesWindow.UpdateComboBoxWithBrowserItem( this.Browser );
		}

		private void Browser_MouseUp( object sender, MouseEventArgs e )
		{
			FormMain.PropertiesWindow.MultiSelectMouseUp( this.Browser );
		}

		private void Browser_XmlLoadCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			FormMain.PropertiesWindow.UpdateComboBoxWithBrowserItem( this.Browser );
		}

		#endregion

		#region Save

		private void CheckModifiedState()
		{
			if( !this.m_bIsModified ) {
				this.m_bIsModified = true;
				this.Text = string.Concat( this.Text, "*" );
			}
		}

		public void SaveFile()
		{
			//if( this.m_bIsModified ) {
			switch( this.m_dtType ) {
				case DocumentType.browser:
					this.m_bpBrowser.SaveFile();
					this.m_sFullName = this.m_bpBrowser.FullName;
					this.Text = this.m_bpBrowser.FileName;
					break;
				case DocumentType.fenubar:
					break;
			}
			this.m_bIsModified = false;
			//}
		}

		#endregion

		#region FormMain-Called Functions

		public void Cut_Click()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		public void Copy_Click()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		public void Paste_Click()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		public void Delete_Click()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					this.m_bpBrowser.DeleteItems();
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		public void BringForward_Click()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					this.m_bpBrowser.BringItemForward();
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		public void SendBackward_Click()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					this.m_bpBrowser.SendItemBackward();
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		public void BringToFront_Click()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					this.m_bpBrowser.BringItemToFront();
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		public void SendToBack_Click()
		{
			switch( this.m_dtType ) {
				case DocumentType.browser:
					this.m_bpBrowser.SendItemToBack();
					break;
				case DocumentType.fenubar:
					break;
			}
		}

		#endregion

		#region Right Click

		private void tsmiBrowseInExplorer_Click( object sender, EventArgs e )
		{
			BrowseInExplorer();
		}

		private void BrowseInExplorer()
		{
			Process.Start( "EXPLORER.EXE", string.Concat( "/select, ", this.FullName ) );
		}

		private void tsmiClose_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		#endregion

	}
}