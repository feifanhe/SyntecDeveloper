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
		public event EventHandler Cut;
		public event EventHandler Copy;
		public event EventHandler Paste;

		public bool Valid
		{
			get
			{
				return this.m_bValid;
			}
			set
			{
				this.m_bValid = value;
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

		public FenuButton( XElement xeButton, ResmapTable rtResmapTable, bool bValid )
		{
			InitializeComponent();
			this.m_bValid = bValid;
			this.m_xeButton = xeButton;
			this.m_rtResmapTable = rtResmapTable;
			InitializeButtonType( this.m_xeButton.Name.LocalName );
			switch( this.Type ) {
				case FenuButtonType.button:
					this.m_fbpProperties = new FenuButtonProperties( this );
					break;
				case FenuButtonType.escape:
					this.m_fbpProperties = new EscapeButtonProperties( this );
					break;
				case FenuButtonType.next:
					this.m_fbpProperties = new NextButtonProperties( this );
					break;
			}
			this.m_fbpProperties.PropertiesChanged += new EventHandler( FenuButton_PropertiesChanged );
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
			this.m_fbpProperties.PropertiesChanged += new EventHandler( FenuButton_PropertiesChanged );
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

		public void UpdateAlignment( ContentAlignment Alignment )
		{
			this.TextAlign = Alignment;
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
			List<XElement> lst = new List<XElement>( this.m_xeButton.Elements() );
			if( lst.Count < 2 ) {
				this.m_xeButton.Remove();
			}
		}

		public void CopyProperties( FenuButton fbSource )
		{
			int nPosition = this.m_fbpProperties.Position;
			this.m_fbpProperties = fbSource.m_fbpProperties.Clone();
			this.m_fbpProperties.Position = nPosition;
			SetText();
		}

		private void FenuButton_MouseUp( object sender, MouseEventArgs e )
		{
			if( e.Button == MouseButtons.Right ) {
				this.ctmsRightClick.Show( this, e.Location );
			}
		}

		private void FenuButton_PropertiesChanged( object sender, EventArgs e )
		{
			this.Valid = true;
		}

		private void tsmiClear_Click( object sender, EventArgs e )
		{
			Clear();
		}

		private void tsmiCut_Click( object sender, EventArgs e )
		{
			CutFenuButton();
		}

		private void tsmiCopy_Click( object sender, EventArgs e )
		{
			CopyFenuButton();
		}

		private void tsmiPaste_Click( object sender, EventArgs e )
		{
			PasteFenuButton();
		}

		public void Clear()
		{
			this.m_xeButton.RemoveNodes();
			this.m_xeButton.Add( new XElement( "position", this.Properties.Position.ToString() ) );
			FenuButtonProperties fbpNewProperties = new FenuButtonProperties( this );
			this.m_fbpProperties = fbpNewProperties;
			this.m_bValid = false;
			SetText();
		}

		private void CutFenuButton()
		{
			if( this.Cut != null ) {
				this.Cut.Invoke( this, new EventArgs() );
			}
		}

		private void CopyFenuButton()
		{
			if( this.Copy != null ) {
				this.Copy.Invoke( this, new EventArgs() );
			}
		}

		private void PasteFenuButton()
		{
			if( this.Paste != null ) {
				this.Paste.Invoke( this, new EventArgs() );
			}
		}

	}
}
