using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace Syntec_Developer.Controls.PropertyClasses
{
	[DefaultPropertyAttribute( "Name" )]
	public class ItemProperties
	{
		protected BrowserItem m_biItemToSet;
		protected string m_sGroup;
		protected XmlDocument m_xdDocument;
		protected XmlNode m_xnItemNode;

		public delegate void PropertiesChangedHandler( object sender, EventArgs e );
		public event PropertiesChangedHandler PropertiesChanged;

		[CategoryAttribute( "Common" )]
		public string Name
		{
			get
			{
				return this.m_biItemToSet.Name;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_biItemToSet.Name = value;
				this.m_biItemToSet.Text = value;
			}
		}

		[CategoryAttribute( "Common" )]
		public string Group
		{
			get
			{
				return this.m_sGroup;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_sGroup = value;
			}
		}

		[CategoryAttribute( "Common" )]
		public int X
		{
			get
			{
				return this.m_biItemToSet.Location.X;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_biItemToSet.Left = value;
			}
		}

		[CategoryAttribute( "Common" )]
		public int Y
		{
			get
			{
				return this.m_biItemToSet.Location.Y;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_biItemToSet.Location = new System.Drawing.Point( this.X, value );
			}
		}

		[CategoryAttribute( "Common" )]
		public int Width
		{
			get
			{
				return this.m_biItemToSet.Size.Width;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_biItemToSet.Size = new System.Drawing.Size( value, this.Height );
			}
		}

		[CategoryAttribute( "Common" )]
		public int Height
		{
			get
			{
				return this.m_biItemToSet.Size.Height;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_biItemToSet.Size = new System.Drawing.Size( this.Width, value );
			}
		}

		public ItemProperties( BrowserItem biItem )
		{
			this.m_biItemToSet = biItem;
			this.m_xdDocument = this.m_biItemToSet.m_xdDocument;
			this.m_xnItemNode = this.m_biItemToSet.m_xnItemNode;
		}

		public void LoadXML()
		{
			foreach( XmlNode xnProperty in this.m_xnItemNode.ChildNodes ) {
				switch( xnProperty.Name ) {
					case "Name":
						this.Name = xnProperty.InnerText;
						break;
					case "Left":
						this.X = int.Parse( xnProperty.InnerText );
						break;
					case "Top":
						this.Y = int.Parse( xnProperty.InnerText );
						break;
					case "Width":
						this.Width = int.Parse( xnProperty.InnerText );
						break;
					case "Height":
						this.Height = int.Parse( xnProperty.InnerText );
						break;
					default:
						break;
				}
			}
		}

		public void SaveXML()
		{
			SaveProperty( "Name", this.Name );
			SaveProperty( "Left", this.X.ToString() );
			SaveProperty( "Top", this.Y.ToString() );
			SaveProperty( "Width", this.Width.ToString() );
			SaveProperty( "Height", this.Height.ToString() );
		}

		protected void SaveProperty( string nNodeName, string nValue )
		{
			XmlNode xnTempNode = this.m_xnItemNode.SelectSingleNode( nNodeName );
			if( xnTempNode != null ) {
				xnTempNode.InnerText = nValue;
			}
			else {
				xnTempNode = m_xdDocument.CreateElement( nNodeName );
				xnTempNode.InnerText = nValue;
				this.m_xnItemNode.AppendChild( xnTempNode );
			}
		}
	}
}
