using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Syntec_Developer.Controls.PropertyClasses;

namespace Syntec_Developer.Controls
{
	public partial class FenubarPanel : UserControl
	{
		const string CUSTOMFENU = "CUSTOMFENU_";

		private bool m_bIsNewFile;
		private bool m_bHasLoaded;
		private bool m_bIsModified;
		private string m_sFileName;
		private Hashtable m_htbFenus;
		private List<Fenu> m_lstOpenedFenu;
		private FenubarProperties m_fpProperties;
		private ResmapTable m_rtResmapTable;

		private bool isCut = false;
		private FenuButton m_fbCopySource;

		private XDocument m_xdDocument;
		private XElement m_xeRoot;

		public event RunWorkerCompletedEventHandler XmlLoadCompleted;
		public event EventHandler FenuClose;
		public event EventHandler FenuButtonClick;
		public event EventHandler FenubarPropertiesChanged;


		#region Properties

		public string FullName
		{
			get
			{
				return this.m_sFileName;
			}
			set
			{
				this.m_sFileName = value;
			}
		}

		public string FileName
		{
			get
			{
				if( this.m_sFileName.LastIndexOf( '\\' ) >= 0 ) {
					return this.m_sFileName.Substring( this.m_sFileName.LastIndexOf( '\\' ) + 1 );
				}
				else {
					return this.m_sFileName;
				}
			}
		}

		public FenubarProperties Properties
		{
			get
			{
				return this.m_fpProperties;
			}
		}

		public Hashtable Fenus
		{
			get
			{
				return this.m_htbFenus;
			}
		}

		public List<Fenu> OpenedFenus
		{
			get
			{
				return this.m_lstOpenedFenu;
			}
		}

		#endregion

		public FenubarPanel( string sFileName, bool bIsNewFile, ResmapTable rtResmapTable )
		{
			InitializeComponent();
			this.m_sFileName = sFileName;
			this.m_bIsNewFile = bIsNewFile;
			this.m_rtResmapTable = rtResmapTable;
			InitializeMembers();
		}

		private void InitializeMembers()
		{
			this.m_bHasLoaded = false;
			this.m_htbFenus = new Hashtable();
			this.m_lstOpenedFenu = new List<Fenu>();
			this.m_fpProperties = new FenubarProperties();

			this.m_fpProperties.PropertiesChanged += new EventHandler( Fenubar_PropertiesChanged );
			this.m_fpProperties.AlignmentChanged += new EventHandler( Fenubar_AlignmentChanged );
		}


		#region Load

		private void FenubarPanel_Load( object sender, EventArgs e )
		{
			if( !this.m_bIsNewFile && !this.m_bHasLoaded ) {
				this.bgwLoadXml.RunWorkerAsync();
			}
		}

		private void bgwLoadXml_DoWork( object sender, DoWorkEventArgs e )
		{
			this.m_xdDocument = XDocument.Load( this.m_sFileName );
			this.LoadXml();
		}

		private void bgwLoadXml_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			this.m_bHasLoaded = true;
			this.XmlLoadCompleted.Invoke( this, e );
		}

		private void LoadXml()
		{
			this.m_xeRoot = this.m_xdDocument.Element( "root" );
			foreach( XElement xeChildElement in this.m_xeRoot.Elements() ) {
				switch( xeChildElement.Name.LocalName ) {
					case "Alignment":
						this.Properties.Alignment = ConvertIntToContentAlignment( int.Parse( xeChildElement.Value ) );
						break;
					case "Button3D":
						this.Properties.Button3D = bool.Parse( xeChildElement.Value );
						break;
					case "Level3D":
						this.Properties.Level3D = int.Parse( xeChildElement.Value );
						break;
					case "NoFunc":
						this.Properties.NoFunc = bool.Parse( xeChildElement.Value );
						break;
					case "NoLR":
						this.Properties.NoLR = bool.Parse( xeChildElement.Value );
						break;
					case "BigLR":
						this.Properties.BigLR = bool.Parse( xeChildElement.Value );
						break;
					case "TextOverPic":
						this.Properties.TextOverPic = bool.Parse( xeChildElement.Value );
						break;
					case "fenu":
						LoadFenu( xeChildElement );
						break;
					default:
						break;
				}
			}
		}

		private void LoadFenu( XElement xeFenu )
		{
			Fenu fnFenuToAdd = new Fenu( xeFenu, m_rtResmapTable );
			AddFenu( fnFenuToAdd );
		}

		private void AddFenu( Fenu fnFenuToAdd )
		{
			if( this.m_htbFenus.ContainsKey( fnFenuToAdd.Properties.Name ) ) {
				MessageBox.Show( "Error: The same fenu name has been used!" );
				return;
			}
			this.m_htbFenus.Add( fnFenuToAdd.Properties.Name, fnFenuToAdd );
			fnFenuToAdd.FenuClose += new EventHandler( Fenu_Close );
			fnFenuToAdd.FenuButtonClick += new EventHandler( FenuButton_Click );
			fnFenuToAdd.FenuButtonKeyLink += new KeyEventHandler( FenuButton_KeyLink );
			fnFenuToAdd.FenuButtonCut += new EventHandler( FenuButton_Cut );
			fnFenuToAdd.FenuButtonCopy += new EventHandler( FenuButton_Copy );
			fnFenuToAdd.FenuButtonPaste += new EventHandler( FenuButton_Paste );
		}

		private ContentAlignment ConvertIntToContentAlignment( int nAlignment )
		{
			switch( nAlignment ) {
				case 1:
					return ContentAlignment.TopLeft;
				case 2:
					return ContentAlignment.TopCenter;
				case 3:
					return ContentAlignment.TopRight;
				case 4:
					return ContentAlignment.MiddleLeft;
				case 5:
					return ContentAlignment.MiddleCenter;
				case 6:
					return ContentAlignment.MiddleRight;
				case 7:
					return ContentAlignment.BottomLeft;
				case 8:
					return ContentAlignment.BottomCenter;
				case 9:
					return ContentAlignment.BottomRight;
				default:
					return ContentAlignment.MiddleCenter;
			}
		}

		#endregion

		#region Open Linked Fenu

		private void FenuButton_KeyLink( object sender, KeyEventArgs e )
		{
			FenuButton fbButton = sender as FenuButton;
			if( !fbButton.Properties.Link.Equals( string.Empty ) ) {
				ShowFenu( fbButton.Properties.Link );
			}
			else {
				string sActionLinkFenuName = GetActionLinkedFenuOfButton( fbButton );
				if( !sActionLinkFenuName.Equals( string.Empty ) ) {
					ShowFenu( sActionLinkFenuName );
				}
			}
		}

		private string GetActionLinkedFenuOfButton( FenuButton fbButton )
		{
			string sActionLinkFenuName;

			foreach( List<string> lstActions
				in new List<string>[] { 
					fbButton.Properties.Actions, 
					fbButton.Properties.ActionsWithPassword.CorrectActions, 
					fbButton.Properties.ActionsWithPassword.IncorrectActions } ) {
				sActionLinkFenuName = GetActionLinkedFenu( lstActions );
				if( !sActionLinkFenuName.Equals( string.Empty ) ) {
					return sActionLinkFenuName;
				}
			}
			return string.Empty;
		}

		private string GetActionLinkedFenu( List<string> lstActions )
		{
			foreach( string sAction in lstActions ) {
				if( sAction.IndexOf( CUSTOMFENU ) == 0 ) {
					return sAction.Substring( CUSTOMFENU.Length );
				}
			}
			return string.Empty;
		}

		#endregion

		public void ShowFenu( string sFenuName )
		{
			Fenu fnFenuToShow = this.m_htbFenus[ sFenuName ] as Fenu;
			if( fnFenuToShow != null && !this.m_lstOpenedFenu.Contains( fnFenuToShow ) ) {
				this.Controls.Add( fnFenuToShow );
				this.m_lstOpenedFenu.Add( fnFenuToShow );
				fnFenuToShow.BringToFront();
				fnFenuToShow.Dock = DockStyle.Top;
				fnFenuToShow.Focus();
			}
		}

		public void HideFenu( string sFenuName )
		{
			Fenu fnFenuToHide = this.m_htbFenus[ sFenuName ] as Fenu;
			this.Controls.Remove( fnFenuToHide );
			this.m_lstOpenedFenu.Remove( fnFenuToHide );
		}

		public void DeleteFenu( string sFenuName )
		{
			Fenu fnFenuToDelete = this.m_htbFenus[ sFenuName ] as Fenu;
			this.Controls.Remove( fnFenuToDelete );
			this.m_lstOpenedFenu.Remove( fnFenuToDelete );
			this.m_htbFenus.Remove( sFenuName );
			fnFenuToDelete.Remove();
		}

		public void NewFenu( string sFenuName )
		{
			XElement xeNewFenu = new XElement( "fenu" );
			this.m_xeRoot.Add( xeNewFenu );
			Fenu fnNewFenu = new Fenu( xeNewFenu, this.m_rtResmapTable, sFenuName );
			AddFenu( fnNewFenu );

		}

		public void CopyFenu( string sFenuName )
		{
			Fenu fnSourceFenu = this.m_htbFenus[ sFenuName ] as Fenu;
			Fenu fnCloneFenu = fnSourceFenu.Clone();
			fnCloneFenu.Properties.Name = string.Concat( fnCloneFenu.Properties.Name, " - Copy" );
			AddFenu( fnCloneFenu );
		}

		private void Fenu_Close( object sender, EventArgs e )
		{
			FenuClose.Invoke( sender, e );
			Fenu fnFenuToClose = sender as Fenu;
			this.Controls.Remove( fnFenuToClose );
			this.m_lstOpenedFenu.Remove( fnFenuToClose );
		}

		private void FenuButton_Click( object sender, EventArgs e )
		{
			if( this.FenuButtonClick != null ) {
				this.FenuButtonClick.Invoke( sender, e );
			}
		}

		private void FenuButton_Cut( object sender, EventArgs e )
		{
			Console.WriteLine( "CUT" );
			this.isCut = true;
			this.m_fbCopySource = sender as FenuButton;
		}

		private void FenuButton_Copy( object sender, EventArgs e )
		{
			this.isCut = false;
			this.m_fbCopySource = sender as FenuButton;
		}

		private void FenuButton_Paste( object sender, EventArgs e )
		{
			if( this.m_fbCopySource == null ) {
				return;
			}
			FenuButton fbPasteTarget = sender as FenuButton;
			fbPasteTarget.CopyProperties( this.m_fbCopySource );
			if( isCut ) {
				Console.WriteLine( "CLEAR" );
				this.m_fbCopySource.Clear();
			}
		}

		private void Fenubar_PropertiesChanged( object sneder, EventArgs e )
		{
			if( this.m_bHasLoaded ) {
				this.FenubarPropertiesChanged.Invoke( this, e );
			}
		}

		private void Fenubar_AlignmentChanged( object sneder, EventArgs e )
		{
			foreach( Fenu fnFenu in this.m_htbFenus.Values ) {
				fnFenu.UpdateButtonAlignment( this.Properties.Alignment );
			}
		}

		#region Save

		public void SaveFile()
		{
			SaveFenubarProperties();
			foreach( Fenu fnFenu in this.m_htbFenus.Values ) {
				fnFenu.SaveFenu();
			}
			SaveXML();
		}

		private void SaveFenubarProperties()
		{
			SaveValueIntoXmlNode( "Alignment", ( (int)this.Properties.Alignment ).ToString() );
			SaveValueIntoXmlNode( "Button3D", this.Properties.Button3D.ToString() );
			SaveValueIntoXmlNode( "Level3D", this.Properties.Level3D.ToString() );
			SaveValueIntoXmlNode( "NoFunc", this.Properties.NoFunc.ToString() );
			SaveValueIntoXmlNode( "NoLR", this.Properties.NoLR.ToString() );
			SaveValueIntoXmlNode( "BigLR", this.Properties.BigLR.ToString() );
			SaveValueIntoXmlNode( "TextOverPic", this.Properties.TextOverPic.ToString() );
		}

		private void SaveValueIntoXmlNode( string sNodeName, string sValue )
		{
			XElement xeElementToWrite = this.m_xeRoot.Element( sNodeName );
			if( xeElementToWrite == null ) {
				xeElementToWrite = new XElement( sNodeName );
				this.m_xeRoot.Add( xeElementToWrite );
			}
			xeElementToWrite.Value = sValue;
		}

		private void SaveXML()
		{
			if( this.m_bIsNewFile ) {
				this.sfdlgSaveXml.FileName = this.m_sFileName;
				if( this.sfdlgSaveXml.ShowDialog() == DialogResult.OK ) {
					this.m_xdDocument.Save( this.sfdlgSaveXml.FileName );
					this.m_sFileName = this.sfdlgSaveXml.FileName;
					this.m_bIsNewFile = false;
				}
			}
			else {
				this.m_xdDocument.Save( this.m_sFileName );
			}
		}

		#endregion
	}
}
