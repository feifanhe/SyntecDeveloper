using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using System.Reflection;

namespace Syntec_Developer.Controls.PropertyClasses
{
	public class FenuButtonProperties
	{
		protected XmlDocument m_xdDocument;
		protected XmlNode m_xnButtonNode;
		protected FenuButton m_fbButtonToSet;

		protected List<string> m_lstActions;
		protected ProtectedActions m_ActionsWithPassword;
		protected bool m_bState;
		protected string m_sLink = string.Empty;
		protected bool m_bVisible;
		protected int m_nUserLevel;
		protected LanguagePack m_lpTitle;
		protected int m_nPosition;
		protected string m_sPicture;
		protected Color m_clrForeColor;
		protected Color m_clrBackColor;
		protected bool m_bHoldMode;
		protected Color m_clrLightOnColor;

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Common" )]
		public List<string> Actions
		{
			get
			{
				return this.m_lstActions;
			}
			set
			{
				this.m_lstActions = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Common" )]
		public ProtectedActions ActionsWithPassword
		{
			get
			{
				return this.m_ActionsWithPassword;
			}
			set
			{
				this.m_ActionsWithPassword = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Common" )]
		public bool State
		{
			get
			{
				return this.m_bState;
			}
			set
			{
				this.m_bState = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Common" )]
		public string Link
		{
			get
			{
				return this.m_sLink;
			}
			set
			{
				this.m_sLink = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Common" )]
		public bool Visible
		{
			get
			{
				return this.m_bVisible;
			}
			set
			{
				this.m_bVisible = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Common" )]
		public int UserLevel
		{
			get
			{
				return this.m_nUserLevel;
			}
			set
			{
				this.m_nUserLevel = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Button" )]
		public LanguagePack Title
		{
			get
			{
				return this.m_lpTitle;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Button" )]
		public int Position
		{
			get
			{
				return this.m_nPosition;
			}
			set
			{
				this.m_nPosition = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Button" )]
		public string Picture
		{
			get
			{
				return this.m_sPicture;
			}
			set
			{
				this.m_sPicture = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Button" )]
		public Color ForeColor
		{
			get
			{
				return this.m_clrForeColor;
			}
			set
			{
				this.m_clrForeColor = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Button" )]
		public Color BackColor
		{
			get
			{
				return this.m_clrBackColor;
			}
			set
			{
				this.m_clrBackColor = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Button" )]
		public bool HoldMode
		{
			get
			{
				return this.m_bHoldMode;
			}
			set
			{
				this.m_bHoldMode = value;
			}
		}

		[BrowsableAttribute( true )]
		[CategoryAttribute( "Button" )]
		public Color LightOnColor
		{
			get
			{
				return this.m_clrLightOnColor;
			}
			set
			{
				this.m_clrLightOnColor = value;
			}
		}

		public FenuButtonProperties( FenuButton fbButton )
		{
			this.m_fbButtonToSet = fbButton;
			this.m_xdDocument = this.m_fbButtonToSet.m_xdDocument;
			this.m_xnButtonNode = this.m_fbButtonToSet.m_xnButton;

			m_lstActions = new List<string>();
			m_ActionsWithPassword = new ProtectedActions();
			m_bState = false;
			m_sLink = string.Empty;
			m_bVisible = false;
			m_nUserLevel = 0;
			m_lpTitle = new LanguagePack();
			m_nPosition = 0;
			m_sPicture = string.Empty;
			m_clrForeColor = Color.Black;
			m_clrBackColor = Color.White;
			m_bHoldMode = false;
			m_clrLightOnColor = Color.White;

			LoadXml();
		}

		public void SetProperties()
		{
			switch( this.m_fbButtonToSet.Type ) {
				case FenuButtonType.escape:
					SetBrowsableAttribute( "UserLevel", false );
					SetBrowsableAttribute( "Title", false );
					SetBrowsableAttribute( "Position", false );
					SetBrowsableAttribute( "Picture", false );
					SetBrowsableAttribute( "ForeColor", false );
					SetBrowsableAttribute( "BackColor", false );
					SetBrowsableAttribute( "HoldMode", false );
					SetBrowsableAttribute( "LightOnColor", false );
					break;
				case FenuButtonType.button:
					SetBrowsableAttribute( "UserLevel", true );
					SetBrowsableAttribute( "Title", true );
					SetBrowsableAttribute( "Position", true );
					SetBrowsableAttribute( "Picture", true );
					SetBrowsableAttribute( "ForeColor", true );
					SetBrowsableAttribute( "BackColor", true );
					SetBrowsableAttribute( "HoldMode", true );
					SetBrowsableAttribute( "LightOnColor", true );
					break;
				case FenuButtonType.next:
					SetBrowsableAttribute( "Title", false );
					SetBrowsableAttribute( "Position", false );
					SetBrowsableAttribute( "Picture", false );
					SetBrowsableAttribute( "ForeColor", false );
					SetBrowsableAttribute( "BackColor", false );
					SetBrowsableAttribute( "HoldMode", false );
					SetBrowsableAttribute( "LightOnColor", false );
					break;
			}
		}

		private void LoadXml()
		{
			foreach( XmlNode xnPropertyNode in this.m_xnButtonNode.ChildNodes ) {
				switch( xnPropertyNode.Name ) {
					case "action":
						this.Actions.Add( xnPropertyNode.InnerText );
						break;
					case "actions":
						LoadActions( this.m_lstActions, xnPropertyNode );
						break;
					case "pwd":
						LoadActionsWithPassword( xnPropertyNode );
						break;
					case "state":
						this.State = ParseStateToBool( xnPropertyNode.InnerText );
						break;
					case "link":
						this.Link = xnPropertyNode.InnerText;
						break;
					case "visible":
						this.Visible = bool.Parse( xnPropertyNode.InnerText );
						break;
					case "userlevel":
						this.UserLevel = int.Parse( xnPropertyNode.InnerText );
						break;
					case "title":
						this.Title.ID = xnPropertyNode.InnerText;
						if( this.Title.ID.ToUpper().IndexOf( "STR::" ) == 0 ) {
							string sID = this.Title.ID.Substring( 5 );

							foreach( string sLanguage in this.m_fbButtonToSet.m_rtResmapTable.GetLanguages() ) {
								this.Title.Values.Add(
									sLanguage, this.m_fbButtonToSet.m_rtResmapTable.GetContent( sLanguage, sID )
								);
							}
						}
						break;
					case "position":
						this.Position = int.Parse( xnPropertyNode.InnerText );
						break;
					case "picture":
						this.Picture = xnPropertyNode.InnerText;
						break;
					case "forecolor":
						this.ForeColor = ParseColorFromRGB( xnPropertyNode.InnerText );
						break;
					case "backcolor":
						this.BackColor = ParseColorFromRGB( xnPropertyNode.InnerText );
						break;
					case "HoldMode":
						this.HoldMode = bool.Parse( xnPropertyNode.InnerText );
						break;
					case "LightOnColor":
						this.LightOnColor = ParseColorFromRGB( xnPropertyNode.InnerText );
						break;
				}
			}
		}

		private bool ParseStateToBool( string sStateValue )
		{
			switch( sStateValue ) {
				case "enable":
					return true;
				case "disable":
					return false;
				default:
					return false;
			}
		}

		private Color ParseColorFromRGB(string sColor)
		{
			string[] asRGB = sColor.Split( ',' );
			return Color.FromArgb( int.Parse( asRGB[ 0 ] ), int.Parse( asRGB[ 1 ] ), int.Parse( asRGB[ 2 ] ) );
		}

		private void LoadActions( List<string> lstActions, XmlNode xnActionsNode )
		{
			foreach( XmlNode xnActionNode in xnActionsNode.ChildNodes ) {
				if( String.Compare( xnActionNode.Name, "Action" ) == 0 ) {
					lstActions.Add( xnActionNode.InnerText );
				}
			}
		}

		private void LoadActionsWithPassword( XmlNode xnPasswordNode )
		{
			this.m_ActionsWithPassword.Password = xnPasswordNode[ "value" ].InnerText;
			XmlNode xnCorrectActions = xnPasswordNode.SelectSingleNode( "cor" );
			if( xnCorrectActions != null ) {
				LoadActions( this.m_ActionsWithPassword.CorrectActions, xnCorrectActions );
			}
			XmlNode xnIncorrectActions = xnPasswordNode.SelectSingleNode( "incor" );
			if( xnIncorrectActions != null ) {
				LoadActions( this.m_ActionsWithPassword.IncorrectActions, xnIncorrectActions );
			}
		}

		private void SetBrowsableAttribute( string sPropertyName, bool bIsBrowsable )
		{
			Type typAttributeType = typeof( BrowsableAttribute );
			PropertyDescriptorCollection pdcProperties = TypeDescriptor.GetProperties( this );
			AttributeCollection atcPropertyAttributes = pdcProperties[ sPropertyName ].Attributes;
			FieldInfo fBrowsableField = typAttributeType.GetField( "browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance );
			fBrowsableField.SetValue( atcPropertyAttributes[ typAttributeType ], bIsBrowsable );
		}
	}
}
