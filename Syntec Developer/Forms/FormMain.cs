using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using Syntec_Developer.Controls;

namespace Syntec_Developer.Forms
{
	public partial class FormMain : Form
	{
		const bool IS_NEW_FILE = true;
		const bool IS_NOT_NEW_FILE = false;
		const string SYNTEC_DEVELOPER_INI_PATH = "Syntec_Developer.ini";

		private string m_sWorkDirectory;
		private IniFile m_ifIniFile;
		private ResmapTable m_rtResmapTable;

		private DCDocument m_dcdLastFocusedDocument;
		private List<DCDocument> m_lstOpenedDocuments;

		private DCTreeView m_dcTreeView;
		private DCToolBox m_dcToolBox;
		private DCProperties m_dcProperties;
		private DCFenuList m_dcFenuList;

		#region Constructor & Initialize

		public FormMain()
		{
			InitializeComponent();

			this.m_sWorkDirectory = string.Empty;
			this.m_ifIniFile = new IniFile( SYNTEC_DEVELOPER_INI_PATH );
			this.m_rtResmapTable = new ResmapTable();

			this.m_dcdLastFocusedDocument = null;
			this.m_lstOpenedDocuments = new List<DCDocument>();

			CheckRDUser();

			InitializeTreeViewWindow();
			InitializePropertiesWindow();
			InitializeToolBoxWindow();
			InitializeFenuListWindow();

			SetWorkDirectory();
			LoadWorkDirectory();
			LoadResmapTable();
		}

		private void CheckRDUser()
		{
			if( Environment.GetEnvironmentVariable( "OCSDK" ) != null ) {
				this.Text = string.Concat( this.Text, " - R&D Mode" );
			}
		}

		private void InitializeTreeViewWindow()
		{
			this.m_dcTreeView = new DCTreeView();
			this.m_dcTreeView.FormClosing += new FormClosingEventHandler( m_dcTreeView_FormClosing );
			this.m_dcTreeView.TreeViewDoubleClick +=
				new DCTreeView.TreeViewDoubleClickHandler( WorkDirectoryTreeView_DoubleClick );
			this.m_dcTreeView.LoadDirectory( m_sWorkDirectory );
			this.m_dcTreeView.Show( this.dplMainPanel, DockState.DockLeft );

			this.tsmiTreeView.Checked = true;
		}

		private void InitializePropertiesWindow()
		{
			this.m_dcProperties = new DCProperties();
			this.m_dcProperties.FormClosing += new FormClosingEventHandler( m_dcProperties_FormClosing );
			this.m_dcProperties.Show( this.dplMainPanel, DockState.DockRight );

			this.tsmiProperties.Checked = true;
		}

		private void InitializeToolBoxWindow()
		{
			this.m_dcToolBox = new DCToolBox();
			this.m_dcToolBox.FormClosing += new FormClosingEventHandler( m_dcToolBox_FormClosing );
			this.m_dcToolBox.Show( this.m_dcProperties.Pane, DockAlignment.Top, 0.5 );

			//this.tsmiToolBox.Checked = true;
			this.m_dcToolBox.Hide();
		}

		private void InitializeFenuListWindow()
		{
			this.m_dcFenuList = new DCFenuList();
			this.m_dcFenuList.FormClosing += new FormClosingEventHandler( m_dcFenuList_FormClosing );
			this.m_dcFenuList.Show( this.m_dcProperties.Pane, DockAlignment.Top, 0.5 );
			this.m_dcFenuList.Hide();
		}

		private void SetWorkDirectory()
		{
			this.m_sWorkDirectory = this.m_ifIniFile.Read( "Path", "LastWorkDirectory" );
		}

		private void LoadWorkDirectory()
		{
			this.m_dcTreeView.LoadDirectory( this.m_sWorkDirectory );
		}

		private void LoadResmapTable()
		{
			this.m_rtResmapTable.Load( this.m_sWorkDirectory );
		}

		#endregion

		#region File Control Event

		private void NewBrowser_Click( object sender, EventArgs e )
		{
			OpenDocument( DocumentType.browser, GenerateNewNameWithIndex( "NewBrowser" ), IS_NEW_FILE );
		}

		private void NewFenubar_Click( object sender, EventArgs e )
		{
			OpenDocument( DocumentType.fenubar, GenerateNewNameWithIndex( "NewFenubar" ), IS_NEW_FILE );
		}

		private void OpenFile_Click( object sender, EventArgs e )
		{
			if( this.ofdlgOpenFile.ShowDialog() == DialogResult.OK ) {
				OpenFiles( ofdlgOpenFile.FileNames );
			}
		}

		private void OpenWorkDirectory_Click( object sender, EventArgs e )
		{
			if( this.fbdlgWorkDirectory.ShowDialog() == DialogResult.OK ) {
				this.m_sWorkDirectory = fbdlgWorkDirectory.SelectedPath;
				this.m_dcTreeView.LoadDirectory( this.m_sWorkDirectory );
				LoadResmapTable();
				this.m_ifIniFile.Write( "Path", "LastWorkDirectory", this.m_sWorkDirectory );
			}
		}

		private void SaveFile_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.SaveFile();
			}
			else {
				MessageBox.Show( "Fail to save the document." );
			}
			this.m_rtResmapTable.SaveResmap();
			this.m_dcTreeView.LoadDirectory( this.m_sWorkDirectory );
		}

		private void SaveAll_Click( object sender, EventArgs e )
		{
			foreach( DCDocument dcdOpenedDocument in this.m_lstOpenedDocuments ) {
				dcdOpenedDocument.SaveFile();
			}
		}

		private void Exit_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		#endregion

		#region Edit Control Event

		private void Cut_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.Cut_Click();
			}
		}

		private void Copy_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.Copy_Click();
			}
		}

		private void Paste_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.Paste_Click();
			}
		}

		private void Delete_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.Delete_Click();
			}
		}

		private void Search_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				if( this.m_dcdLastFocusedDocument.Type == DocumentType.fenubar ) {
					SearchFenubar.Show( this.m_dcdLastFocusedDocument, this.m_dcFenuList );
					return;
				}
			}
			MessageBox.Show( "N/A" );
		}

		#endregion

		#region View Control Event

		private void tsmiTreeView_Click( object sender, EventArgs e )
		{
			this.tsmiTreeView.Checked = !this.tsmiTreeView.Checked;
			if( this.tsmiTreeView.Checked ) {
				this.m_dcTreeView.Show( this.dplMainPanel );
			}
			else {
				this.m_dcTreeView.Hide();
			}
		}

		private void tsmiToolBox_Click( object sender, EventArgs e )
		{
			this.tsmiToolBox.Checked = !this.tsmiToolBox.Checked;
			if( this.tsmiToolBox.Checked ) {
				this.m_dcToolBox.Show( this.dplMainPanel );
			}
			else {
				this.m_dcToolBox.Hide();
			}
		}

		private void tsmiProperties_Click( object sender, EventArgs e )
		{
			this.tsmiProperties.Checked = !this.tsmiProperties.Checked;
			if( this.tsmiProperties.Checked ) {
				this.m_dcProperties.Show( this.dplMainPanel );
			}
			else {
				this.m_dcProperties.Hide();
			}
		}

		private void tsmiFenuList_Click( object sender, EventArgs e )
		{
			this.tsmiFenuList.Checked = !this.tsmiFenuList.Checked;
			if( this.tsmiFenuList.Checked ) {
				this.m_dcFenuList.Show( this.dplMainPanel );
			}
			else {
				this.m_dcFenuList.Hide();
			}
		}

		#endregion

		#region Tool Windows Control Event

		private void m_dcTreeView_FormClosing( object sender, FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.tsmiTreeView.Checked = false;
			this.m_dcTreeView.Hide();
		}

		private void m_dcProperties_FormClosing( object sender, FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.tsmiProperties.Checked = false;
			this.m_dcProperties.Hide();
		}

		private void m_dcToolBox_FormClosing( object sender, FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.tsmiToolBox.Checked = false;
			this.m_dcToolBox.Hide();
		}

		private void m_dcFenuList_FormClosing( object sender, FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.tsmiFenuList.Checked = false;
			this.m_dcFenuList.Hide();
		}

		#endregion

		#region Open file

		private void OpenFiles( string[] saFileNames )
		{
			for( int i = 0; i < saFileNames.Length; i++ ) {
				OpenFile( saFileNames[ i ] );
			}
		}

		private void WorkDirectoryTreeView_DoubleClick( object sender, EventArgs e )
		{
			TreeView tvwWorkDirectoryTreeView = sender as TreeView;
			if( tvwWorkDirectoryTreeView.SelectedNode != null ) {
				string sFileName = tvwWorkDirectoryTreeView.SelectedNode.FullPath;
				if( IsXmlFile( sFileName ) ) {
					OpenFile( sFileName );
				}
			}
		}

		private void OpenFile( string sFileName )
		{
			foreach( DCDocument dcDocument in this.m_lstOpenedDocuments ) {
				if( string.Compare( dcDocument.FullName, sFileName ) == 0 ) {
					dcDocument.Focus();
					return;
				}
			}
			XmlDocument xdDocument = new XmlDocument();
			try {
				xdDocument.Load( sFileName );
			}
			catch( XmlException ) {
				MessageBox.Show( "illegal XML file: " + sFileName );
				return;
			}

			if( IsBrowserXmlDocument( xdDocument ) ) {
				OpenDocument( DocumentType.browser, sFileName, IS_NOT_NEW_FILE );
			}
			else if( IsFenubarXmlDocument( xdDocument ) ) {
				OpenDocument( DocumentType.fenubar, sFileName, IS_NOT_NEW_FILE );
			}
			else {
				MessageBox.Show( "NOT Browser nor Fenubar: " + sFileName );
				return;
			}
		}

		private void OpenDocument( DocumentType dtType, string sFileName, bool bIsNewFile )
		{
			DCDocument dcdDocumentToShow = new DCDocument( dtType, sFileName, bIsNewFile, m_rtResmapTable );
			this.m_lstOpenedDocuments.Add( dcdDocumentToShow );
			SetDocumentEvents( dcdDocumentToShow );
			dcdDocumentToShow.Show( this.dplMainPanel, DockState.Document );
		}

		private void SetDocumentEvents( DCDocument dcdDocumentToShow )
		{
			// TODO: send message to Document:Browser when user ckick ToolBox
			if( dcdDocumentToShow.Type == DocumentType.browser ) {
				this.m_dcToolBox.SelectItemToDraw +=
					new DCToolBox.SelectItemToDrawHandler( dcdDocumentToShow.Browser.SelectedItemToDraw );
			}

			// TODO: classify the functions...
			dcdDocumentToShow.FormClosing += new FormClosingEventHandler( Document_FormClosing );
			dcdDocumentToShow.Activated += new EventHandler( Document_Activated );
			dcdDocumentToShow.Activated += new EventHandler( m_dcFenuList.Document_Activated );
			dcdDocumentToShow.Activated += new EventHandler( m_dcProperties.Document_Activated );
			dcdDocumentToShow.BrowserItemMouseDown +=
				new DCDocument.BrowserItemMouseDownHandler( m_dcProperties.BrowserItem_MouseDown );
			dcdDocumentToShow.BrowserItemPropertiesChanged +=
				new DCDocument.BrowserItemPropertiesChangedHandler
					( m_dcProperties.BrowserItem_PropertiesChanged );
			dcdDocumentToShow.BrowserMouseUp +=
				new DCDocument.BorwserMouseUpHandler( m_dcProperties.Browser_MouseUp );
			dcdDocumentToShow.BrowserXmlLoadCompleted +=
				new DCDocument.BrowserXmlLoadCompletedHandler( m_dcProperties.Browser_XmlLoadCompleted );
			dcdDocumentToShow.BrowserItemAddedDeleted +=
				new DCDocument.BrowserItemAddedDeletedHandler( m_dcProperties.BrowserItem_AddedDeleted );
			dcdDocumentToShow.FenubarClick +=
				new DCDocument.FenubarClickHandler( m_dcProperties.Fenubar_Click );
			dcdDocumentToShow.FenubarXmlLoadCompleted +=
				new DCDocument.FenubarXmlLoadCompletedHandler( m_dcProperties.Fenubar_XmlLoadCompleted );
			dcdDocumentToShow.FenubarXmlLoadCompleted +=
				new DCDocument.FenubarXmlLoadCompletedHandler( m_dcFenuList.Fenubar_XmlLoadCompleted );
			dcdDocumentToShow.FenuClose += new DCDocument.FenuCloseHandler( m_dcFenuList.Fenu_Close );
			dcdDocumentToShow.FenuButtonClick +=
				new DCDocument.FenuButtonClickHandler( m_dcProperties.FenuButton_Click );
			dcdDocumentToShow.FenuShowByKeyLink += new EventHandler( m_dcFenuList.Fenubar_FenuShowByKeyLink );
		}

		#endregion

		#region Custom Functions

		private string GenerateNewNameWithIndex( string sDefaultName )
		{
			int nIndex = 1;
			bool bExist;
			string sResultName;
			do {
				sResultName = string.Concat( sDefaultName, nIndex.ToString() );
				bExist = false;

				foreach( DCDocument dcdOpenedDocument in m_lstOpenedDocuments ) {
					if( string.Compare( dcdOpenedDocument.FullName, sResultName ) == 0 ) {
						bExist = true;
						nIndex++;
						break;
					}
				}
			} while( bExist );
			return sResultName;
		}

		private bool IsXmlFile( string sFileName )
		{
			return ( string.Compare( GetFileExtension( sFileName ).ToLower(), ".xml" ) == 0 );
		}

		private bool IsBrowserXmlDocument( XmlDocument xdDocument )
		{
			return ( xdDocument.SelectSingleNode( "Screen" ) != null );
		}

		private bool IsFenubarXmlDocument( XmlDocument xdDocument )
		{
			return ( xdDocument.SelectSingleNode( "root" ) != null );
		}

		private DCDocument GetFocusedDocument()
		{
			foreach( DCDocument dcdOpenedDocument in m_lstOpenedDocuments ) {
				if( dcdOpenedDocument.ContainsFocus ) {
					return dcdOpenedDocument;
				}
			}
			return null;
		}

		private void Document_Activated( object sender, EventArgs e )
		{
			DCDocument dcdActivated = sender as DCDocument;
			this.m_dcdLastFocusedDocument = dcdActivated;
			if( dcdActivated != null ) {
				switch( dcdActivated.Type ) {
					case DocumentType.browser:
						ShowBrowserUI();
						break;
					case DocumentType.fenubar:
						ShowFenubarUI();
						break;
				}
			}
		}

		private void Document_FormClosing( object sender, FormClosingEventArgs e )
		{
			DCDocument dcDocumentToClose = sender as DCDocument;
			if( this.m_dcdLastFocusedDocument == dcDocumentToClose ) {
				this.m_dcdLastFocusedDocument = null;
			}
			this.m_lstOpenedDocuments.Remove( dcDocumentToClose );
			this.Controls.Remove( dcDocumentToClose );
		}

		private void ShowBrowserUI()
		{
			if( !tsmiToolBox.Checked ) {
				m_dcToolBox.Show();
				tsmiToolBox.Checked = true;
				tsmiToolBox.Enabled = true;
			}
			if( tsmiFenuList.Checked ) {
				m_dcFenuList.Hide();
				tsmiFenuList.Checked = false;
				tsmiFenuList.Enabled = false;
			}
		}
		private void ShowFenubarUI()
		{
			if( tsmiToolBox.Checked ) {
				m_dcToolBox.Hide();
				tsmiToolBox.Checked = false;
				tsmiToolBox.Enabled = false;
			}
			if( !tsmiFenuList.Checked ) {
				m_dcFenuList.Show();
				tsmiFenuList.Checked = true;
				tsmiFenuList.Enabled = true;
			}
		}

		private string GetFileExtension( string sFullName )
		{
			int nDotIndex = sFullName.LastIndexOf( '.' );
			if( nDotIndex < 0 ) {
				return string.Empty;
			}
			else {
				return sFullName.Substring( nDotIndex );
			}
		}

		#endregion

		#region Layout Control Event

		private void tsbBringForward_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.BringForward_Click();
			}
		}

		private void tsbSendBackward_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.SendBackward_Click();
			}
		}

		private void tsbBringToFront_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.BringToFront_Click();
			}
		}

		private void tsbSendToBack_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				this.m_dcdLastFocusedDocument.SendToBack_Click();
			}
		}

		#endregion



	}
}