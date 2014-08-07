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
	public partial class Fenu : UserControl
	{
		const int ESCAPE_INDEX = 0;
		const int NEXT_INDEX = 9;
		private FenuProperties m_fpProperties;
		private FenuButton[] m_afbFenuButtons;
		private ResmapTable m_rtResmapTable;

		internal XmlDocument m_xdDocument;
		internal XmlNode m_xnFenuNode;

		public event EventHandler FenuClose;
		public event EventHandler FenuButtonClick;
		public event KeyEventHandler FenuButtonKeyLink;
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

		public Fenu( XmlDocument xdDocument, XmlNode xnFenuNode, ResmapTable rtResmapTable )
		{
			InitializeComponent();
			this.m_xdDocument = xdDocument;
			this.m_xnFenuNode = xnFenuNode;
			this.m_rtResmapTable = rtResmapTable;
			this.m_fpProperties = new FenuProperties();
			this.m_afbFenuButtons =
				new FenuButton[ 10 ];
			LoadXML();
			CheckUndefinedFenuButton();
			ShowFenu();
		}

		#endregion

		#region Load

		private void LoadXML()
		{
			this.m_fpProperties.Name = this.m_xnFenuNode.Attributes[ "name" ].Value;
			foreach( XmlNode xnButtonNode in this.m_xnFenuNode.ChildNodes ) {
				FenuButton fbButton = new FenuButton( m_xdDocument, xnButtonNode, this.m_rtResmapTable );
				fbButton.Click += new EventHandler( FenuButton_Click );
				fbButton.KeyLink += new FenuButton.KeyLinkHandler( FenuButton_KeyLink );
				AddButtonIntoTable( fbButton );
			}
		}

		private void AddButtonIntoTable( FenuButton fbButton )
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

		private void CheckUndefinedFenuButton()
		{
			CheckEscapeButton();
			CheckButton();
			CheckNextButton();
		}

		private void CheckEscapeButton()
		{
			if( this.m_afbFenuButtons[ ESCAPE_INDEX ] == null ) {
				XmlNode xnEscapeButton = this.m_xdDocument.CreateElement( "escape" );
				this.m_xnFenuNode.AppendChild( xnEscapeButton );
				this.m_afbFenuButtons[ ESCAPE_INDEX ] =
					new FenuButton( this.m_xdDocument, xnEscapeButton, this.m_rtResmapTable );
			}
		}

		private void CheckButton()
		{
			for( int i = 1; i <= 8; i++ ) {
				if( this.m_afbFenuButtons[ i ] == null ) {
					XmlNode xnButton = this.m_xdDocument.CreateElement( "button" );
					this.m_xnFenuNode.AppendChild( xnButton );
					this.m_afbFenuButtons[ i ] =
						new FenuButton( this.m_xdDocument, xnButton, this.m_rtResmapTable, i );
				}
			}
		}

		private void CheckNextButton()
		{
			if( this.m_afbFenuButtons[ NEXT_INDEX ] == null ) {
				XmlNode xnNextButton = this.m_xdDocument.CreateElement( "next" );
				this.m_xnFenuNode.AppendChild( xnNextButton );
				this.m_afbFenuButtons[ NEXT_INDEX ] =
					new FenuButton( this.m_xdDocument, xnNextButton, this.m_rtResmapTable );
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

		private void Control_Click( object sender, EventArgs e )
		{
			this.Focus();
			if( this.Click != null ) {
				this.Click.Invoke( this, e );
			}
		}

		public void SaveFenu()
		{
			SaveFenuName();
			foreach( FenuButton fbButton in this.m_afbFenuButtons ) {
				fbButton.SaveFenuButton();
			}
		}

		private void SaveFenuName()
		{
			this.m_xnFenuNode.Attributes[ "name" ].Value = this.m_fpProperties.Name;
		}
	}
}
