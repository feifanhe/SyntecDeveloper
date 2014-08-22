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
		// Resmap Table
		public static ResmapTable ResmapTable;
		// Tool Windows
		public static DCWorkDirectory WorkDirectoryWindow;
		public static DCToolBox ToolBoxWindow;
		public static DCProperties PropertiesWindow;
		public static DCFenuList FenuListWindow;

		public const bool IS_NEW_FILE = true;
		public const bool IS_NOT_NEW_FILE = false;
		public const string SYNTEC_DEVELOPER_INI_PATH = "Syntec_Developer.ini";

		private string m_sWorkDirectory;
		private IniFile m_ifIniFile;

		private DCDocument m_dcdLastFocusedDocument;
		private List<DCDocument> m_lstOpenedDocuments;


		#region Constructor & Initialize

		public FormMain()
		{
			InitializeComponent();

			this.m_sWorkDirectory = string.Empty;
			this.m_ifIniFile = new IniFile( SYNTEC_DEVELOPER_INI_PATH );

			this.m_dcdLastFocusedDocument = null;
			this.m_lstOpenedDocuments = new List<DCDocument>();

			FormMain.ResmapTable = new ResmapTable();

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
				// TODO: RD MODE CONTROL
			}
		}

		private void InitializeTreeViewWindow()
		{
			FormMain.WorkDirectoryWindow = new DCWorkDirectory();
			FormMain.WorkDirectoryWindow.FormClosing += new FormClosingEventHandler( WorkDirectoryWindow_FormClosing );
			FormMain.WorkDirectoryWindow.FileNodeMouseDoubleClick +=
				new TreeNodeMouseClickEventHandler( WorkDirectoryFileNodeMouseDoubleClick );
			FormMain.WorkDirectoryWindow.ProductNodeMouseDoubleClick +=
				new TreeNodeMouseClickEventHandler( WorkDirectoryProductNodeMouseDoubleClick );
			FormMain.WorkDirectoryWindow.LoadDirectory( m_sWorkDirectory );
			FormMain.WorkDirectoryWindow.Show( this.dplMainPanel, DockState.DockLeft );

			this.tsmiTreeView.Checked = true;
		}

		private void InitializePropertiesWindow()
		{
			FormMain.PropertiesWindow = new DCProperties();
			FormMain.PropertiesWindow.FormClosing += new FormClosingEventHandler( PropertiesWindow_FormClosing );
			FormMain.PropertiesWindow.Show( this.dplMainPanel, DockState.DockRight );

			this.tsmiProperties.Checked = true;
		}

		private void InitializeToolBoxWindow()
		{
			FormMain.ToolBoxWindow = new DCToolBox();
			FormMain.ToolBoxWindow.FormClosing += new FormClosingEventHandler( ToolBoxWindow_FormClosing );
			FormMain.ToolBoxWindow.Show( FormMain.PropertiesWindow.Pane, DockAlignment.Top, 0.5 );

			this.tsmiToolBox.Checked = true;
		}

		private void InitializeFenuListWindow()
		{
			FormMain.FenuListWindow = new DCFenuList();
			FormMain.FenuListWindow.FormClosing += new FormClosingEventHandler( FenuListWindow_FormClosing );
			FormMain.FenuListWindow.Show( FormMain.PropertiesWindow.Pane, DockAlignment.Top, 0.5 );
			FormMain.FenuListWindow.Hide();
		}

		private void SetWorkDirectory()
		{
			this.m_sWorkDirectory = this.m_ifIniFile.Read( "Path", "LastWorkDirectory" );
		}

		private void LoadWorkDirectory()
		{
			FormMain.WorkDirectoryWindow.LoadDirectory( this.m_sWorkDirectory );
		}

		private void LoadResmapTable()
		{
			FormMain.ResmapTable.Load( this.m_sWorkDirectory );
		}

		#endregion

		#region File Control Event

		private void NewBrowser_Click( object sender, EventArgs e )
		{
			NewBrowser( GenerateNewNameWithIndex( "NewBrowser" ) );
		}

		private void NewFenubar_Click( object sender, EventArgs e )
		{
			NewFenubar( GenerateNewNameWithIndex( "NewFenubar" ) );
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
				FormMain.WorkDirectoryWindow.LoadDirectory( this.m_sWorkDirectory );
				LoadResmapTable();
				this.m_ifIniFile.Write( "Path", "LastWorkDirectory", this.m_sWorkDirectory );
			}
		}

		private void SaveFile_Click( object sender, EventArgs e )
		{
			if( this.m_dcdLastFocusedDocument != null ) {
				m_dcdLastFocusedDocument.SaveFile();
			}
			else {
				MessageBox.Show( "Fail to save the document." );
			}

			FormMain.ResmapTable.SaveResmap();
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
					SearchFenubar.Show( this.m_dcdLastFocusedDocument, FormMain.FenuListWindow );
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
				FormMain.WorkDirectoryWindow.Show( this.dplMainPanel );
			}
			else {
				FormMain.WorkDirectoryWindow.Hide();
			}
		}

		private void tsmiToolBox_Click( object sender, EventArgs e )
		{
			this.tsmiToolBox.Checked = !this.tsmiToolBox.Checked;
			if( this.tsmiToolBox.Checked ) {
				FormMain.ToolBoxWindow.Show( this.dplMainPanel );
			}
			else {
				FormMain.ToolBoxWindow.Hide();
			}
		}

		private void tsmiProperties_Click( object sender, EventArgs e )
		{
			this.tsmiProperties.Checked = !this.tsmiProperties.Checked;
			if( this.tsmiProperties.Checked ) {
				FormMain.PropertiesWindow.Show( this.dplMainPanel );
			}
			else {
				FormMain.PropertiesWindow.Hide();
			}
		}

		private void tsmiFenuList_Click( object sender, EventArgs e )
		{
			this.tsmiFenuList.Checked = !this.tsmiFenuList.Checked;
			if( this.tsmiFenuList.Checked ) {
				FormMain.FenuListWindow.Show( this.dplMainPanel );
			}
			else {
				FormMain.FenuListWindow.Hide();
			}
		}

		#endregion

		#region Tool Windows Control Event

		private void WorkDirectoryWindow_FormClosing( object sender, FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.tsmiTreeView.Checked = false;
			FormMain.WorkDirectoryWindow.Hide();
		}

		private void PropertiesWindow_FormClosing( object sender, FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.tsmiProperties.Checked = false;
			FormMain.PropertiesWindow.Hide();
		}

		private void ToolBoxWindow_FormClosing( object sender, FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.tsmiToolBox.Checked = false;
			FormMain.ToolBoxWindow.Hide();
		}

		private void FenuListWindow_FormClosing( object sender, FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.tsmiFenuList.Checked = false;
			FormMain.FenuListWindow.Hide();
		}

		#endregion

		#region Open file

		private void OpenFiles( string[] saFileNames )
		{
			for( int i = 0; i < saFileNames.Length; i++ ) {
				OpenFile( saFileNames[ i ] );
			}
		}

		private void WorkDirectoryFileNodeMouseDoubleClick( object sender, TreeNodeMouseClickEventArgs e )
		{
			if( IsXmlFile( e.Node.FullPath ) ) {
				OpenFile( e.Node.FullPath );
			}
		}

		private void WorkDirectoryProductNodeMouseDoubleClick( object sender, TreeNodeMouseClickEventArgs e )
		{
			OpenProduct( e.Node.FullPath );
		}

		private void OpenFile( string sFileName )
		{
			if( HasBeenOpened( sFileName ) ) {
				return;
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
				OpenBrowser( sFileName );
			}
			else if( IsFenubarXmlDocument( xdDocument ) ) {
				OpenFenubar( sFileName );
			}
			else {
				MessageBox.Show( "NOT Browser nor Fenubar: " + sFileName );
			}
		}

		private void OpenProduct( string sPath )
		{
			if( HasBeenOpened( sPath ) ) {
				return;
			}

			DCDocument dcdProduct = DCDocument.CreateProduct( sPath );
			dcdProduct.LoadProduct();
			this.m_lstOpenedDocuments.Add( dcdProduct );
			SetDocumentEvents( dcdProduct );
			dcdProduct.Show( this.dplMainPanel, DockState.Document );
		}

		private void OpenBrowser( string sPath )
		{
			DCDocument dcdBrowser = DCDocument.CreateBrowser( sPath );
			dcdBrowser.LoadBrowser();
			this.m_lstOpenedDocuments.Add( dcdBrowser );
			SetDocumentEvents( dcdBrowser );
			dcdBrowser.Show( this.dplMainPanel, DockState.Document );
		}

		private void OpenFenubar( string sPath )
		{
			DCDocument dcdFenubar = DCDocument.CreateFenubar( sPath );
			dcdFenubar.LoadFenubar();
			this.m_lstOpenedDocuments.Add( dcdFenubar );
			SetDocumentEvents( dcdFenubar );
			dcdFenubar.Show( this.dplMainPanel, DockState.Document );
		}

		private void NewBrowser( string sName )
		{
			DCDocument dcdBrowser = DCDocument.CreateBrowser( sName );
			dcdBrowser.IsNewFile = true;
			dcdBrowser.LoadBrowser();
			this.m_lstOpenedDocuments.Add( dcdBrowser );
			SetDocumentEvents( dcdBrowser );
			dcdBrowser.Show( this.dplMainPanel, DockState.Document );
		}

		private void NewFenubar( string sName )
		{
			DCDocument dcdFenubar = DCDocument.CreateFenubar( sName );
			dcdFenubar.IsNewFile = true;
			dcdFenubar.LoadFenubar();
			this.m_lstOpenedDocuments.Add( dcdFenubar );
			SetDocumentEvents( dcdFenubar );
			dcdFenubar.Show( this.dplMainPanel, DockState.Document );
		}

		private bool HasBeenOpened( string sFullName )
		{
			foreach( DCDocument dcDocument in this.m_lstOpenedDocuments ) {
				if( string.Compare( dcDocument.FullName, sFullName ) == 0 ) {
					dcDocument.Focus();
					return true;
				}
			}
			return false;
		}

		private void SetDocumentEvents( DCDocument dcdDocumentToShow )
		{
			dcdDocumentToShow.FormClosing += new FormClosingEventHandler( Document_FormClosing );
			dcdDocumentToShow.Activated += new EventHandler( Document_Activated );
		}

		#endregion

		#region Document

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
			dcDocumentToClose.Dispose();
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

		private void ShowBrowserUI()
		{
			if( !tsmiToolBox.Checked ) {
				ToolBoxWindow.Show();
				tsmiToolBox.Checked = true;
				tsmiToolBox.Enabled = true;
			}
			if( tsmiFenuList.Checked ) {
				FenuListWindow.Hide();
				tsmiFenuList.Checked = false;
				tsmiFenuList.Enabled = false;
			}
		}

		private void ShowFenubarUI()
		{
			if( tsmiToolBox.Checked ) {
				ToolBoxWindow.Hide();
				tsmiToolBox.Checked = false;
				tsmiToolBox.Enabled = false;
			}
			if( !tsmiFenuList.Checked ) {
				FenuListWindow.Show();
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