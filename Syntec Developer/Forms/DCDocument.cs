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
	//public enum DocumentType
	//{
	//    browser,
	//    fenubar
	//};

	public partial class DCDocument : DockContent
	{
		private string m_sFullName;
		private bool m_bIsNewFile;
		private bool m_bIsModified;
		private DocumentType m_dtType;
		private BrowserPanel m_bpBrowser;
		private FenubarPanel m_fpFenubar;
		private ComponentResourceManager resources = new ComponentResourceManager( typeof( DCDocument ) );
		
		internal ResmapTable m_rtResmapTable;

		public DocumentType Type
		{
			get
			{
				return this.m_dtType;
			}
		}

		public string FullName
		{
			get
			{
				return this.m_sFullName;
			}
		}

		public BrowserPanel Browser
		{
			get
			{
				return this.m_bpBrowser;
			}
		}

		public FenubarPanel Fenubar
		{
			get
			{
				return this.m_fpFenubar;
			}
		}

		public delegate void BrowserItemMouseDownHandler( object sender, EventArgs e );
		public event BrowserItemMouseDownHandler BrowserItemMouseDown;
		public delegate void BrowserItemPropertiesChangedHandler( object sender, EventArgs e );
		public event BrowserItemPropertiesChangedHandler BrowserItemPropertiesChanged;
		public delegate void BrowserItemAddedDeletedHandler( object sender, EventArgs e );
		public event BrowserItemAddedDeletedHandler BrowserItemAddedDeleted;
		public delegate void BorwserMouseUpHandler( object sender, MouseEventArgs e );
		public event BorwserMouseUpHandler BrowserMouseUp;

		public delegate void FenubarClickHandler( object sender, EventArgs e );
		public event FenubarClickHandler FenubarClick;
		public delegate void FenuCloseHandler( object sender, EventArgs e );
		public event FenuCloseHandler FenuClose;
		public delegate void BrowserXmlLoadCompletedHandler( object sender, RunWorkerCompletedEventArgs e );
		public event BrowserXmlLoadCompletedHandler BrowserXmlLoadCompleted;
		public delegate void FenubarXmlLoadCompletedHandler( object sender, RunWorkerCompletedEventArgs e );
		public event FenubarXmlLoadCompletedHandler FenubarXmlLoadCompleted;
		public delegate void FenuButtonClickHandler( object sender, EventArgs e );
		public event FenuButtonClickHandler FenuButtonClick;

		#region Initialize

		public DCDocument( DocumentType dtType, string sFullName, bool bIsNewFile, ResmapTable rtResmapTable )
		{
			InitializeComponent();

			this.m_dtType = dtType;
			this.m_sFullName = sFullName;
			this.m_bIsNewFile = bIsNewFile;
			this.m_rtResmapTable = rtResmapTable;

			switch( dtType ) {
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
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "Browser" ) ) );
			this.m_bpBrowser = new BrowserPanel( this.m_sFullName, this.m_bIsNewFile, this.m_rtResmapTable );
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

			this.Text = this.m_bpBrowser.FileName;
			this.Controls.Add( this.m_bpBrowser );
		}

		private void InitializeFenubar()
		{
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "Fenubar" ) ) );
			this.m_fpFenubar = new FenubarPanel( this.m_sFullName, m_bIsNewFile, this.m_rtResmapTable );
			this.m_fpFenubar.Dock = DockStyle.Fill;

			this.m_fpFenubar.Click += new EventHandler( Fenubar_Click );
			this.m_fpFenubar.XmlLoadCompleted += new RunWorkerCompletedEventHandler( Fenubar_XmlLoadCompleted );
			this.m_fpFenubar.FenuClose += new EventHandler( Fenu_Close );
			this.m_fpFenubar.FenuButtonClick += new EventHandler( FenuButton_Click );
			this.m_fpFenubar.FenubarPropertiesChanged += new EventHandler(Fenubar_PropertiesChanged);

			this.Text = this.m_fpFenubar.FileName;
			this.Controls.Add( this.m_fpFenubar );
		}


		#endregion

		#region Event functions to pass up, called by browser or fenubar

		public void BrowserItem_MouseDown( object sender, EventArgs e )
		{
			BrowserItemMouseDown.Invoke( sender, e );
		}

		public void BrowserItem_PropertiesChanged( object sender, EventArgs e )
		{
			CheckModifiedState();
			BrowserItemPropertiesChanged.Invoke( sender, e );
		}

		public void BrowserItem_AddedDeleted( object sender, EventArgs e )
		{
			CheckModifiedState();
			BrowserItemAddedDeleted.Invoke( Browser, e );
		}

		private void CheckModifiedState()
		{
			if( !this.m_bIsModified ) {
				this.m_bIsModified = true;
				this.Text = string.Concat( this.Text, "*" );
			}
		}

		public void Fenubar_Click( object sender, EventArgs e )
		{
			this.FenubarClick.Invoke( sender, e );
		}

		private void Browser_MouseUp( object sender, MouseEventArgs e )
		{
			this.BrowserMouseUp.Invoke( sender, e );
		}

		private void Browser_XmlLoadCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			this.BrowserXmlLoadCompleted.Invoke( sender, e );
		}

		private void Fenubar_XmlLoadCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			this.FenubarXmlLoadCompleted.Invoke( sender, e );
		}

		private void Fenu_Close( object sender, EventArgs e )
		{
			this.FenuClose.Invoke( sender, e );
		}

		private void FenuButton_Click( object sender, EventArgs e )
		{
			this.FenuButtonClick.Invoke( sender, e );
		}

		private void Fenubar_PropertiesChanged( object sender, EventArgs e )
		{
			CheckModifiedState();
			//BrowserItemPropertiesChanged.Invoke( sender, e );
		}

		#endregion

		#region FormMain-Called Functions

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
						this.m_fpFenubar.SaveFile();
						this.m_sFullName = this.m_fpFenubar.FullName;
						this.Text = this.m_fpFenubar.FileName;
						break;
				}
				this.m_bIsModified = false;
			//}
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

		private void tsmiBrowseInExplorer_Click( object sender, EventArgs e )
		{
			BrowseInExplorer();
		}

		private void BrowseInExplorer()
		{
			Process.Start( "EXPLORER.EXE", string.Concat("/select, ", this.FullName) );
		}

		private void tsmiClose_Click( object sender, EventArgs e )
		{
			this.Close();
		}

	}
}