using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Linq;
using System.Reflection;
namespace Syntec_Developer.Controls.PropertyClasses
{
	class EscapeButtonProperties : FenuButtonProperties
	{
		[BrowsableAttribute( false )]
		[CategoryAttribute( "Button" )]
		public new LanguagePack Title
		{
			get
			{
				return this.m_lpTitle;
			}
		}

		[BrowsableAttribute( false )]
		[CategoryAttribute( "Button" )]
		public new int Position
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

		[BrowsableAttribute( false )]
		[CategoryAttribute( "Button" )]
		public new string Picture
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

		[BrowsableAttribute( false )]
		[CategoryAttribute( "Button" )]
		public new Color ForeColor
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

		[BrowsableAttribute( false )]
		[CategoryAttribute( "Button" )]
		public new Color BackColor
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

		[BrowsableAttribute( false )]
		[CategoryAttribute( "Button" )]
		public new bool HoldMode
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

		[BrowsableAttribute( false )]
		[CategoryAttribute( "Button" )]
		public new Color LightOnColor
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

		public EscapeButtonProperties( FenuButton fbButton )
			: base( fbButton )
		{
		}

		public new void SaveFenuButtonProperties()
		{
			SaveActions();
			SaveActionsWithPassword();
			SaveSatate();
			SaveLink();
			SaveVisible();
			SaveUserLevel();
		}
	}
}
