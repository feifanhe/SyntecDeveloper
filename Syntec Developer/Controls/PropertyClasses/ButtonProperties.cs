using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;

namespace Syntec_Developer.Controls.PropertyClasses
{
	public class ButtonProperties : ItemProperties
	{
		private bool m_bEnable;

		public bool Enable
		{
			get
			{
				return this.m_bEnable;
			}
			set
			{
				this.m_bEnable = value;
			}
		}

		public ButtonProperties( BrowserItem biItemToSet ) : base( biItemToSet )
		{
		}

		public new void LoadXML()
		{
			base.LoadXML();
			foreach( XmlNode xnProperty in this.m_xnItemNode.ChildNodes ) {
				switch( xnProperty.Name ) {
					// Save Properties
					case "Enable":
						this.Enable = bool.Parse( xnProperty.InnerText );
						break;
					default:
						break;
				}
			}
		}
	}
}
