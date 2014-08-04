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
		private FenuProperties m_fpProperties;
		private Hashtable m_htbFenuButtons;
		private ResmapTable m_rtResmapTable;
		private int m_nNumberOfButtons = 8;

		internal XmlDocument m_xdDocument;
		internal XmlNode m_xnFenuNode;

		public event EventHandler FenuClose;
		public event EventHandler FenuButtonClick;
		public event KeyEventHandler FenuButtonKeyLink;
		public new event EventHandler  Click;

		public FenuProperties Properties
		{
			get
			{
				return this.m_fpProperties;
			}
		}

		public Hashtable Buttons
		{
			get
			{
				return m_htbFenuButtons;
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
			this.m_htbFenuButtons = new Hashtable();
			this.m_fpProperties = new FenuProperties();
			LoadXML();
			ShowFenu();
		}

		#endregion

		#region Load

		private void LoadXML()
		{
			this.m_fpProperties.Name = this.m_xnFenuNode.Attributes[ "name" ].Value;
			foreach( XmlNode xnButtonNode in this.m_xnFenuNode ) {
				FenuButton fbButton = new FenuButton( m_xdDocument, xnButtonNode, this.m_rtResmapTable );
				fbButton.Click += new EventHandler( FenuButton_Click );
				fbButton.KeyLink += new FenuButton.KeyLinkHandler( FenuButton_KeyLink );
				switch( fbButton.Type ) {
					case FenuButtonType.escape:
						if( this.m_htbFenuButtons.ContainsKey( "escape" ) ) {
							MessageBox.Show( "Escape button has been defined." );
						}
						else {
							this.m_htbFenuButtons.Add( "escape", fbButton );
						}
						break;
					case FenuButtonType.button:
						if( this.m_htbFenuButtons.ContainsKey( fbButton.Properties.Position ) ) {
							MessageBox.Show( 
								String.Format( "Button F{0} has been defined.", fbButton.Properties.Position ) 
							);
						}
						else {
							this.m_htbFenuButtons.Add( fbButton.Properties.Position, fbButton );
						}
						break;
					case FenuButtonType.next:
						if( this.m_htbFenuButtons.ContainsKey( "next" ) ) {
							MessageBox.Show( "Next button has been defined." );
						}
						else {
							this.m_htbFenuButtons.Add( "next", fbButton );
						}
						break;
				}
			}
		}

		#endregion

		#region Show

		private void ShowFenu()
		{
			this.lblFenuName.Text = this.m_fpProperties.Name;
			ShowEscapeButton();
			ShowButton();
			ShowNextButton();
		}

		private void ShowEscapeButton()
		{
			FenuButton fbEscapeButton = this.m_htbFenuButtons[ "escape" ] as FenuButton;
			if( fbEscapeButton != null ) {
				this.pnlFenu.Controls.Add( fbEscapeButton );
				fbEscapeButton.BringToFront();
			}
			else {
				FenuButton fbNewEscapeButton =
					new FenuButton( m_xdDocument, this.m_rtResmapTable, FenuButtonType.escape );
				this.m_htbFenuButtons.Add( "escape", fbNewEscapeButton );
				this.pnlFenu.Controls.Add( fbNewEscapeButton );
				fbNewEscapeButton.BringToFront();
			}
		}

		private void ShowNextButton()
		{
			FenuButton fbNextButton = this.m_htbFenuButtons[ "next" ] as FenuButton;
			if( fbNextButton != null ) {
				this.pnlFenu.Controls.Add( fbNextButton );
				fbNextButton.BringToFront();
			}
			else {
				FenuButton fbNewNextButton =
					new FenuButton( m_xdDocument, this.m_rtResmapTable, FenuButtonType.next );
				this.m_htbFenuButtons.Add( "next", fbNewNextButton );
				this.pnlFenu.Controls.Add( fbNewNextButton );
				fbNewNextButton.BringToFront();
			}
		}

		private void ShowButton()
		{
			for( int nPosition = 1; nPosition <= m_nNumberOfButtons; nPosition++ ) {
				FenuButton fbButton = this.m_htbFenuButtons[ nPosition ] as FenuButton;
				if( fbButton != null ) {
					this.pnlFenu.Controls.Add( fbButton );
					fbButton.BringToFront();
				}
				else {
					FenuButton fbNewButton = new FenuButton( m_xdDocument, this.m_rtResmapTable, nPosition );
					this.m_htbFenuButtons.Add( nPosition, fbNewButton );
					this.pnlFenu.Controls.Add( fbNewButton );
					fbNewButton.BringToFront();
					fbNewButton.Enabled = false;
				}
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
		}
	}
}
