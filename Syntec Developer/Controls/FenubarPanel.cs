using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
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

		internal XmlDocument m_xdDocument;
		internal XmlNode m_xnRoot;

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
		}
		

		#region Load

		private void FenubarPanel_Load( object sender, EventArgs e )
		{
			if( !this.m_bIsNewFile && !this.m_bHasLoaded ) {
				this.m_xdDocument = new XmlDocument();
				this.m_xdDocument.Load( m_sFileName );
				this.bgwLoadXml.RunWorkerAsync();
			}
		}

		private void bgwLoadXml_DoWork( object sender, DoWorkEventArgs e )
		{
			this.LoadXml();
			this.m_bHasLoaded = true;
		}

		private void bgwLoadXml_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			this.XmlLoadCompleted.Invoke( this, e );
		}

		private void LoadXml()
		{
			XmlNode xnRoot = this.m_xdDocument.SelectSingleNode( "root" );
			this.m_xnRoot = xnRoot;
			foreach( XmlElement xnItem in this.m_xnRoot.ChildNodes ) {
				switch( xnItem.Name ) {
					case "Alignment":
						this.Properties.Alignment = int.Parse(xnItem.InnerText);
						break;
					case "Button3D":
						this.Properties.Button3D = bool.Parse( xnItem.InnerText );
						break;
					case "Level3D":
						this.Properties.Level3D = int.Parse( xnItem.InnerText );
						break;
					case "NoFunc":
						this.Properties.NoFunc = bool.Parse( xnItem.InnerText );
						break;
					case "NoLR":
						this.Properties.NoLR = bool.Parse( xnItem.InnerText );
						break;
					case "BigLR":
						this.Properties.BigLR = bool.Parse( xnItem.InnerText );
						break;
					case "TextOverPic":
						this.Properties.TextOverPic = bool.Parse( xnItem.InnerText );
						break;
					case "fenu":
						LoadFenu( xnItem );
						break;
					default:
						break;
				}
			}
		}

		private void LoadFenu( XmlNode xnFenuNode )
		{
			Fenu fnFenuToAdd = new Fenu( m_xdDocument, xnFenuNode, m_rtResmapTable );
			if( this.m_htbFenus.ContainsKey( fnFenuToAdd.Properties.Name ) ) {
				MessageBox.Show( "Error: The same fenu name has been used!" );
			}
			else {
				this.m_htbFenus.Add( fnFenuToAdd.Properties.Name, fnFenuToAdd );
				fnFenuToAdd.FenuClose += new EventHandler( Fenu_Close );
				fnFenuToAdd.FenuButtonClick += new EventHandler( FenuButton_Click );
				fnFenuToAdd.FenuButtonKeyLink += new KeyEventHandler( FenuButton_KeyLink );
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

		private void Fenu_Close( object sender, EventArgs e )
		{
			FenuClose.Invoke( sender, e );
			Fenu fnFenuToClose = sender as Fenu;
			this.Controls.Remove( fnFenuToClose );
			this.m_lstOpenedFenu.Remove( fnFenuToClose );
		}

		private void FenuButton_Click( object sender, EventArgs e )
		{
			this.FenuButtonClick.Invoke( sender, e );
		}

		private void Fenubar_PropertiesChanged( object sneder, EventArgs e )
		{
			if( this.m_bHasLoaded ) {
				this.FenubarPropertiesChanged.Invoke( this, e );
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
			SaveValueIntoXmlNode( "Alignment", this.Properties.Alignment.ToString() );
			SaveValueIntoXmlNode( "Button3D", this.Properties.Button3D.ToString() );
			SaveValueIntoXmlNode( "Level3D", this.Properties.Level3D.ToString() );
			SaveValueIntoXmlNode( "NoFunc", this.Properties.NoFunc.ToString() );
			SaveValueIntoXmlNode( "NoLR", this.Properties.NoLR.ToString() );
			SaveValueIntoXmlNode( "BigLR", this.Properties.BigLR.ToString() );
			SaveValueIntoXmlNode( "TextOverPic", this.Properties.TextOverPic.ToString() );
		}

		private void SaveValueIntoXmlNode( string sNodeName, string sValue )
		{
			XmlNode xnNodeToWrite = this.m_xnRoot.SelectSingleNode( sNodeName );
			if( xnNodeToWrite == null ) {
				xnNodeToWrite = this.m_xdDocument.CreateElement( sNodeName ) as XmlNode;
				this.m_xnRoot.AppendChild( xnNodeToWrite );
			}
			xnNodeToWrite.InnerText = sValue;
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
