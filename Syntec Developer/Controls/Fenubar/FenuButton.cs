using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Syntec_Developer.Controls.PropertyClasses;

namespace Syntec_Developer.Controls
{
	public enum FenuButtonType
	{
		escape,
		next,
		button
	}

	public partial class FenuButton : Button
	{
		private bool m_bValid;
		private FenuButtonType m_fbtType;
		private FenuButtonProperties m_fbpProperties;

		internal ResmapTable m_rtResmapTable;
		internal XElement m_xeButton;

		public event KeyEventHandler KeyLink;

		public bool Valid
		{
			get
			{
				return this.m_bValid;
			}
		}

		public FenuButtonProperties Properties
		{
			get
			{
				return this.m_fbpProperties;
			}
		}

		public FenuButtonType Type
		{
			get
			{
				return this.m_fbtType;
			}
			set
			{
				this.m_fbtType = value;
			}
		}

		public FenuButton(XElement xeButton, ResmapTable rtResmapTable, bool bValid )
		{
			InitializeComponent();
			this.m_bValid = bValid;
			this.m_xeButton = xeButton;
			this.m_rtResmapTable = rtResmapTable;
			InitializeButtonType( this.m_xeButton.Name.LocalName );
			this.m_fbpProperties = new FenuButtonProperties( this );
			SetText();
		}

		public FenuButton( XElement xeButton, ResmapTable rtResmapTable, int nPosition )
		{
			InitializeComponent();
			this.m_bValid = false;
			this.m_xeButton = xeButton;
			this.m_rtResmapTable = rtResmapTable;
			InitializeButtonType( this.m_xeButton.Name.LocalName );
			this.m_fbpProperties = new FenuButtonProperties( this );
			this.m_fbpProperties.Position = nPosition;
			SetText();
			
		}

		private void InitializeButtonType( string sButtonType )
		{
			switch( sButtonType ) {
				case "escape":
					this.m_fbtType = FenuButtonType.escape;
					break;
				case "next":
					this.m_fbtType = FenuButtonType.next;
					break;
				case "button":
					this.m_fbtType = FenuButtonType.button;
					break;
			}
		}

		private void SetText()
		{
			switch( this.m_fbtType ) {
				case FenuButtonType.escape:
					this.Text = "<<";
					break;
				case FenuButtonType.button:
					string sContent;
					if( this.m_fbpProperties.Title.Values.ContainsKey( "CHT" ) ) {
						sContent = this.m_fbpProperties.Title.Values[ "CHT" ];
					}
					else {
						sContent = this.m_fbpProperties.Title.ID;
					}
					this.Text =
						string.Concat(
							"F", this.m_fbpProperties.Position.ToString(),
							" ", sContent
						);
					break;
				case FenuButtonType.next:
					this.Text = ">>";
					break;
			}
		}

		private void FenuButton_KeyDown( object sender, KeyEventArgs e )
		{
			if( e.Control ) {
				switch( e.KeyData ) {
					case Keys.X:
					case Keys.C:
					case Keys.V:
						break;
				}
			}
			else {
				switch( e.KeyData ) {
					case Keys.Space:
						if( this.KeyLink != null ) {
							this.KeyLink.Invoke( this, e );
						}
						break;
					case Keys.Delete:
						break;
				}
			}
		}

		public void SaveFenuButton()
		{
			this.m_fbpProperties.SaveFenuButtonProperties();
		}
	}
}
