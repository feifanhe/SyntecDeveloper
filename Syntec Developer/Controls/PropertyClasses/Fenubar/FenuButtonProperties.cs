using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Linq;
using System.Reflection;

namespace Syntec_Developer.Controls.PropertyClasses
{
	public class FenuButtonProperties
	{
		private XElement m_xeButton;
		private FenuButton m_fbButtonToSet;

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

		public event EventHandler PropertiesChanged;

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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
			set
			{
				this.m_lpTitle = value;
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
			}
		}

		[BrowsableAttribute( true )]
		[ReadOnlyAttribute( true )]
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
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
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
			}
		}

		public FenuButtonProperties( FenuButton fbButton )
		{
			this.m_fbButtonToSet = fbButton;
			this.m_xeButton = this.m_fbButtonToSet.m_xeButton;

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
			foreach( XElement xeProperty in this.m_xeButton.Elements() ) {
				switch( xeProperty.Name.LocalName ) {
					case "action":
						this.Actions.Add( xeProperty.Value );
						break;
					case "actions":
						LoadActions( this.m_lstActions, xeProperty );
						break;
					case "pwd":
						LoadActionsWithPassword( xeProperty );
						break;
					case "state":
						this.State = ParseStateToBool( xeProperty.Value );
						break;
					case "link":
						this.Link = xeProperty.Value;
						break;
					case "visible":
						this.Visible = bool.Parse( xeProperty.Value );
						break;
					case "userlevel":
						this.UserLevel = int.Parse( xeProperty.Value );
						break;
					case "title":
						this.Title.ID = xeProperty.Value;
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
						this.Position = int.Parse( xeProperty.Value );
						break;
					case "picture":
						this.Picture = xeProperty.Value;
						break;
					case "forecolor":
						this.ForeColor = ParseColorFromRGB( xeProperty.Value );
						break;
					case "backcolor":
						this.BackColor = ParseColorFromRGB( xeProperty.Value );
						break;
					case "HoldMode":
						this.HoldMode = bool.Parse( xeProperty.Value );
						break;
					case "LightOnColor":
						this.LightOnColor = ParseColorFromRGB( xeProperty.Value );
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

		private string ConvertBoolToState( bool bState )
		{
			if( bState ) {
				return "enable";
			}
			else {
				return "disable";
			}
		}

		private Color ParseColorFromRGB( string sColor )
		{
			string[] asRGB = sColor.Split( ',' );
			return Color.FromArgb( int.Parse( asRGB[ 0 ] ), int.Parse( asRGB[ 1 ] ), int.Parse( asRGB[ 2 ] ) );
		}

		private void LoadActions( List<string> lstActions, XElement xeActions )
		{
			foreach( XElement xeAction in xeActions.Elements() ) {
				if( string.Compare( xeAction.Name.LocalName.ToUpper(), "ACTION" ) == 0 ) {
					lstActions.Add( xeAction.Value );
				}
			}
		}

		private void LoadActionsWithPassword( XElement xePassword )
		{
			this.m_ActionsWithPassword.Password = xePassword.Element( "value" ).Value;
			XElement xeCorrectActions = xePassword.Element( "cor" );
			if( xeCorrectActions != null ) {
				LoadActions( this.m_ActionsWithPassword.CorrectActions, xeCorrectActions );
			}
			XElement xeIncorrectActions = xePassword.Element( "incor" );
			if( xeIncorrectActions != null ) {
				LoadActions( this.m_ActionsWithPassword.IncorrectActions, xeIncorrectActions );
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

		public void SaveFenuButtonProperties()
		{
			SaveActions();
			SaveActionsWithPassword();
			SaveState();
			SaveLink();
			SaveVisible();
			SaveUserLevel();
			SaveTitle();
			SavePosition();
			SavePicture();
			SaveForeColor();
			SaveBackColor();
			SaveHoldMode();
			SaveLightOnColor();
		}

		protected void SaveActions()
		{
			SaveActions( this.m_xeButton, this.Actions );
		}

		protected void SaveActions( XElement xeActionsOwner, List<string> lstActions )
		{
			if( this.Actions.Count == 0 ) {
				if( xeActionsOwner.Element( "action" ) != null ) {
					xeActionsOwner.Element( "action" ).Remove();
				}
				if( xeActionsOwner.Element( "actions" ) != null ) {
					xeActionsOwner.Element( "actions" ).Remove();
				}
			}
			else {
				if( this.Actions.Count == 1 ) {
					if( xeActionsOwner.Element( "actions" ) != null ) {
						xeActionsOwner.Element( "actions" ).Remove();
					}
					if( xeActionsOwner.Element( "action" ) == null ) {
						xeActionsOwner.Add( new XElement( "action" ) );
					}
					xeActionsOwner.Element( "action" ).Value = this.Actions[ 0 ];
				}
				else {
					if( xeActionsOwner.Element( "action" ) != null ) {
						xeActionsOwner.Element( "action" ).Remove();
					}
					if( xeActionsOwner.Element( "actions" ) == null ) {
						xeActionsOwner.Add( new XElement( "actions" ) );
					}
					xeActionsOwner.Element( "actions" ).RemoveAll();
					foreach( string sAction in lstActions ) {
						xeActionsOwner.Element( "actions" ).Add( new XElement( "action", sAction ) );
					}
				}
			}
		}

		protected void SaveActionsWithPassword()
		{
			XElement xePassword = this.m_xeButton.Element( "pwd" );
			if( this.ActionsWithPassword.Password == string.Empty ) {
				if( xePassword != null ) {
					xePassword.Remove();
				}
			}
			else {
				if( xePassword == null ) {
					this.m_xeButton.Add( new XElement( "pwd" ) );
				}
				if( xePassword.Element( "value" ) == null ) {
					xePassword.Add( new XElement( "value" ) );
				}
				xePassword.Element( "value" ).Value = this.ActionsWithPassword.Password;

				if( xePassword.Element( "cor" ) == null ) {
					xePassword.Add( new XElement( "cor" ) );
				}
				SaveActions( xePassword.Element( "cor" ), this.ActionsWithPassword.CorrectActions );

				if( xePassword.Element( "incor" ) == null ) {
					xePassword.Add( new XElement( "incor" ) );
				}
				SaveActions( xePassword.Element( "incor" ), this.ActionsWithPassword.IncorrectActions );
			}
		}

		protected void SaveState()
		{
			if( this.m_xeButton.Element( "state" ) == null ) {
				this.m_xeButton.Add( new XElement( "state" ) );
			}
			this.m_xeButton.Element( "state" ).Value = this.State.ToString();
		}

		protected void SaveLink()
		{
			if( this.Link == string.Empty ) {
				if( this.m_xeButton.Element( "link" ) != null ) {
					this.m_xeButton.Element( "link" ).Remove();
				}
			}
			else {
				if( this.m_xeButton.Element( "link" ) == null ) {
					this.m_xeButton.Add( new XElement( "link" ) );
				}
				this.m_xeButton.Element( "link" ).Value = this.Link;
			}
		}

		protected void SaveVisible()
		{
			if( this.m_xeButton.Element( "visible" ) == null ) {
				this.m_xeButton.Add( new XElement( "visible" ) );
			}
			this.m_xeButton.Element( "visible" ).Value = this.Visible.ToString();
		}

		protected void SaveUserLevel()
		{
			if( this.UserLevel == 0 ) {
				if( this.m_xeButton.Element( "userlevel" ) != null ) {
					this.m_xeButton.Element( "userlevel" ).Remove();
				}
			}
			else {
				if( this.m_xeButton.Element( "userlevel" ) == null ) {
					this.m_xeButton.Add( new XElement( "userlevel" ) );
				}
				this.m_xeButton.Element( "userlevel" ).Value = this.UserLevel.ToString();
			}
		}
		protected void SaveTitle()
		{
			if( this.Title.ID == string.Empty ) {
				if( this.m_xeButton.Element( "title" ) != null ) {
					this.m_xeButton.Element( "title" ).Remove();
				}
			}
			else {
				if( this.m_xeButton.Element( "title" ) == null ) {
					this.m_xeButton.Add( new XElement( "title" ) );
				}
				this.m_xeButton.Element( "title" ).Value = this.Title.ID;
			}
		}

		protected void SavePosition()
		{
			if( this.m_xeButton.Element( "position" ) == null ) {
				this.m_xeButton.Add( new XElement( "position" ) );
			}
			this.m_xeButton.Element( "position" ).Value = this.Position.ToString();
		}

		protected void SavePicture()
		{
			if( this.Picture == string.Empty ) {
				if( this.m_xeButton.Element( "picture" ) != null ) {
					this.m_xeButton.Element( "picture" ).Remove();
				}
			}
			else {
				if( this.m_xeButton.Element( "picture" ) == null ) {
					this.m_xeButton.Add( new XElement( "picture" ) );
				}
				this.m_xeButton.Element( "picture" ).Value = this.Picture;
			}
		}

		protected void SaveForeColor()
		{
			if( this.ForeColor == Color.Black ) {
				if( this.m_xeButton.Element( "forecolor" ) != null ) {
					this.m_xeButton.Element( "forecolor" ).Remove();
				}
			}
			else {
				if( this.m_xeButton.Element( "forecolor" ) == null ) {
					this.m_xeButton.Add( new XElement( "forecolor" ) );
				}
				this.m_xeButton.Element( "forecolor" ).Value = ColorToRGB( this.ForeColor );
			}
		}

		protected void SaveBackColor()
		{
			if( this.BackColor == SystemColors.Control ) {
				if( this.m_xeButton.Element( "backcolor" ) != null ) {
					this.m_xeButton.Element( "backcolor" ).Remove();
				}
			}
			else {
				if( this.m_xeButton.Element( "backcolor" ) == null ) {
					this.m_xeButton.Add( new XElement( "backcolor" ) );
				}
				this.m_xeButton.Element( "backcolor" ).Value = ColorToRGB( this.BackColor );
			}
		}

		protected void SaveHoldMode()
		{
			if( this.HoldMode == false ) {
				if( this.m_xeButton.Element( "HoldMode" ) != null ) {
					this.m_xeButton.Element( "HoldMode" ).Remove();
				}
			}
			else {
				if( this.m_xeButton.Element( "HoldMode" ) == null ) {
					this.m_xeButton.Add( new XElement( "HoldMode" ) );
				}
				this.m_xeButton.Element( "HoldMode" ).Value = this.HoldMode.ToString();
			}
		}

		protected void SaveLightOnColor()
		{
			if( this.LightOnColor == SystemColors.Control ) {
				if( this.m_xeButton.Element( "LightOnColor" ) != null ) {
					this.m_xeButton.Element( "LightOnColor" ).Remove();
				}
			}
			else {
				if( this.m_xeButton.Element( "LightOnColor" ) == null ) {
					this.m_xeButton.Add( new XElement( "LightOnColor" ) );
				}
				this.m_xeButton.Element( "LightOnColor" ).Value = ColorToRGB( this.LightOnColor );
			}
		}

		protected string ColorToRGB( Color clrSource )
		{
			return string.Format( "{0},{1},{2}", clrSource.R, clrSource.G, clrSource.B );
		}

		public FenuButtonProperties Clone()
		{
			FenuButtonProperties fbpClone = this.MemberwiseClone() as FenuButtonProperties;
			fbpClone.Title = this.Title.Clone();
			return fbpClone;
		}
	}
}
