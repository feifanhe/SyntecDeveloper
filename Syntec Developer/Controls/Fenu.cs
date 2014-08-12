using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections;
using Syntec_Developer.Controls.PropertyClasses;

namespace Syntec_Developer.Controls
{
	public partial class Fenu : UserControl
	{
		const int ESCAPE_INDEX = 0;
		const int NEXT_INDEX = 9;
		private FenuProperties m_fpProperties;
		private FenuButton[] m_afbFenuButtons;
		private ResmapTable m_rtResmapTable;

		private XElement m_xeFenu;

		public event EventHandler FenuClose;
		public event EventHandler FenuButtonClick;
		public event KeyEventHandler FenuButtonKeyLink;
		public event EventHandler FenuButtonCut;
		public event EventHandler FenuButtonCopy;
		public event EventHandler FenuButtonPaste;
		public new event EventHandler Click;

		public FenuProperties Properties
		{
			get
			{
				return this.m_fpProperties;
			}
		}

		public FenuButton[] Buttons
		{
			get
			{
				return m_afbFenuButtons;
			}
		}

		public override string Text
		{
			get
			{
				return this.lblFenuName.Text;
			}
			set
			{
				this.lblFenuName.Text = value;
			}
		}

		#region Constructor & Initialize

		public Fenu( XElement xeFenu, ResmapTable rtResmapTable )
		{
			InitializeComponent();
			this.m_xeFenu = xeFenu;
			this.m_rtResmapTable = rtResmapTable;
			this.m_fpProperties = new FenuProperties();
			this.m_afbFenuButtons =
				new FenuButton[ 10 ];
			LoadXML();
			CheckUndefinedFenuButton();
			ShowFenu();
		}

		public Fenu( XElement xeNewFenu, ResmapTable rtResmapTable, string sFenuName )
		{
			InitializeComponent();
			this.m_xeFenu = xeNewFenu;
			this.m_rtResmapTable = rtResmapTable;
			this.m_fpProperties = new FenuProperties();
			this.m_fpProperties.Name = sFenuName;
			this.m_afbFenuButtons =
				new FenuButton[ 10 ];

			CheckUndefinedFenuButton();
			ShowFenu();
		}

		#endregion

		#region Load

		private void LoadXML()
		{
			this.m_fpProperties.Name = this.m_xeFenu.Attribute( "name" ).Value;
			foreach( XElement xeButton in this.m_xeFenu.Elements() ) {
				FenuButton fbButton = new FenuButton( xeButton, this.m_rtResmapTable, true );
				SetButtonEvent( fbButton );
				AddButtonIntoList( fbButton );
			}
		}

		private void SetButtonEvent( FenuButton fbButton )
		{
			fbButton.Click += new EventHandler( FenuButton_Click );
			fbButton.KeyLink += new KeyEventHandler( FenuButton_KeyLink );
			fbButton.Cut += new EventHandler( FenuButton_Cut );
			fbButton.Copy += new EventHandler( FenuButton_Copy );
			fbButton.Paste += new EventHandler( FenuButton_Paste );
		}

		private void AddButtonIntoList( FenuButton fbButton )
		{
			switch( fbButton.Type ) {
				case FenuButtonType.escape:
					if( this.m_afbFenuButtons[ ESCAPE_INDEX ] != null ) {
						MessageBox.Show( "Escape button has been defined." );
					}
					else {
						this.m_afbFenuButtons[ ESCAPE_INDEX ] = fbButton;
					}
					break;
				case FenuButtonType.button:
					if( fbButton.Properties.Position == 0 ) {
						break;
					}
					if( this.m_afbFenuButtons[ fbButton.Properties.Position ] != null ) {
						MessageBox.Show(
							string.Format( "Button F{0} has been defined.", fbButton.Properties.Position )
						);
					}
					else {
						this.m_afbFenuButtons[ fbButton.Properties.Position ] = fbButton;
					}
					break;
				case FenuButtonType.next:
					if( this.m_afbFenuButtons[ NEXT_INDEX ] != null ) {
						MessageBox.Show( "Next button has been defined." );
					}
					else {
						this.m_afbFenuButtons[ NEXT_INDEX ] = fbButton;
					}
					break;
			}
		}

		#endregion

		#region Check Buttons

		private void CheckUndefinedFenuButton()
		{
			CheckEscapeButton();
			CheckButton();
			CheckNextButton();
		}

		private void CheckEscapeButton()
		{
			if( this.m_afbFenuButtons[ ESCAPE_INDEX ] == null ) {
				this.m_afbFenuButtons[ ESCAPE_INDEX ] =
					new FenuButton( new XElement( "escape" ), this.m_rtResmapTable, false );
				this.m_xeFenu.Add( this.m_afbFenuButtons[ ESCAPE_INDEX ].m_xeButton );
				SetButtonEvent( this.m_afbFenuButtons[ ESCAPE_INDEX ] );
			}
		}

		private void CheckButton()
		{
			for( int i = 1; i <= 8; i++ ) {
				if( this.m_afbFenuButtons[ i ] == null ) {
					this.m_afbFenuButtons[ i ] =
						new FenuButton( new XElement( "button" ), this.m_rtResmapTable, i );
					this.m_xeFenu.Add( this.m_afbFenuButtons[ i ].m_xeButton );
					SetButtonEvent( this.m_afbFenuButtons[ i ] );
				}
			}
		}

		private void CheckNextButton()
		{
			if( this.m_afbFenuButtons[ NEXT_INDEX ] == null ) {
				this.m_afbFenuButtons[ NEXT_INDEX ] =
					new FenuButton( new XElement( "next" ), this.m_rtResmapTable, false );
				this.m_xeFenu.Add( this.m_afbFenuButtons[ NEXT_INDEX ].m_xeButton );
				SetButtonEvent( this.m_afbFenuButtons[ NEXT_INDEX ] );
			}
		}

		#endregion

		#region Show

		private void ShowFenu()
		{
			this.lblFenuName.Text = this.m_fpProperties.Name;
			for( int i = 0; i <= 9; i++ ) {
				this.pnlFenu.Controls.Add( this.m_afbFenuButtons[ i ] );
				this.m_afbFenuButtons[ i ].BringToFront();
			}
		}

		#endregion

		#region Events

		private void Fenu_Enter( object sender, EventArgs e )
		{
			this.pnlHeader.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		}

		private void Fenu_Leave( object sender, EventArgs e )
		{
			this.pnlHeader.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		}

		private void lblClose_Click( object sender, EventArgs e )
		{
			if( this.FenuClose != null ) {
				this.FenuClose.Invoke( this, e );
			}
		}

		private void FenuButton_Click( object sender, EventArgs e )
		{
			if( this.FenuButtonClick != null ) {
				this.FenuButtonClick.Invoke( sender, e );
			}
		}

		private void FenuButton_KeyLink( object sender, KeyEventArgs e )
		{
			if( this.FenuButtonKeyLink != null ) {
				this.FenuButtonKeyLink.Invoke( sender, e );
			}
		}

		private void FenuButton_Cut( object sender, EventArgs e )
		{
			if( this.FenuButtonCut != null ) {
				this.FenuButtonCut.Invoke( sender, e );
			}
		}

		private void FenuButton_Copy( object sender, EventArgs e )
		{
			if( this.FenuButtonCopy != null ) {
				this.FenuButtonCopy.Invoke( sender, e );
			}
		}

		private void FenuButton_Paste( object sender, EventArgs e )
		{
			if( this.FenuButtonPaste != null ) {
				this.FenuButtonPaste.Invoke( sender, e );
			}
		}

		private void Control_Click( object sender, EventArgs e )
		{
			this.Focus();
			if( this.Click != null ) {
				this.Click.Invoke( this, e );
			}
		}

		public void UpdateButtonAlignment( ContentAlignment Alignment )
		{
			foreach( FenuButton fbButton in this.m_afbFenuButtons ) {
				fbButton.UpdateAlignment( Alignment );
			}
		}

		#endregion

		#region Save

		public void SaveFenu()
		{
			SaveFenuName();
			foreach( FenuButton fbButton in this.m_afbFenuButtons ) {
				if( fbButton.Valid ) {
					if( fbButton.m_xeButton.Parent == null ) {
						this.m_xeFenu.Add( fbButton.m_xeButton );
					}
					fbButton.SaveFenuButton();
				}
				else {
					if( fbButton.m_xeButton.Parent != null ) {
						fbButton.m_xeButton.Remove();
					}
				}
			}
		}

		private void SaveFenuName()
		{
			this.m_xeFenu.SetAttributeValue( "name", this.m_fpProperties.Name );
		}

		#endregion

		public void Remove()
		{
			this.m_xeFenu.Remove();
			this.Dispose();
		}

		public Fenu Clone()
		{
			XElement xeCloneFenu = new XElement( "fenu" );
			this.m_xeFenu.Parent.Add( xeCloneFenu );

			Fenu fnCloneFenu = new Fenu( xeCloneFenu, this.m_rtResmapTable, this.Properties.Name );

			fnCloneFenu.CloneFenuButtons( this );

			return fnCloneFenu;
		}

		private void CloneFenuButtons( Fenu fnSourceFenu )
		{
			for( int i = ESCAPE_INDEX; i <= NEXT_INDEX; i++ ) {
				this.Buttons[ i ].CopyProperties( fnSourceFenu.Buttons[ i ] );
				this.m_xeFenu.Add( this.Buttons[ i ].m_xeButton );
			}
		}

		private void Fenu_SizeChanged( object sender, EventArgs e )
		{
			for( int i = ESCAPE_INDEX; i <= NEXT_INDEX; i++ ) {
				this.m_afbFenuButtons[ i ].Width = this.Width / 10;
			}
		}
	}
}
