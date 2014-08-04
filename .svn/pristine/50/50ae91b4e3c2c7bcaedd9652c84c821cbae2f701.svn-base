using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Syntec_Developer.Controls.PropertyClasses
{
	[DefaultPropertyAttribute( "Name" )]
	public class BrowserProperties
	{
		private string m_sName;
		private string m_sNextPage;
		private string m_sPrevPage;
		private BrowserPanel m_bpBrowserToSet;

		[CategoryAttribute( "Browser" )]
		public string Name
		{
			get
			{
				return this.m_sName;
			}
			set
			{
				this.m_sName = value;
			}
		}

		[CategoryAttribute( "Size" )]
		public int Width
		{
			get
			{
				return this.m_bpBrowserToSet.Width;
			}
			set
			{
				this.m_bpBrowserToSet.Width = value;
			}
		}

		[CategoryAttribute( "Size" )]
		public int Height
		{
			get
			{
				return this.m_bpBrowserToSet.Height;
			}
			set
			{
				this.m_bpBrowserToSet.Height = value;
			}
		}

		[CategoryAttribute( "LinkPage" )]
		public string NextPage
		{
			get
			{
				return this.m_sNextPage;
			}
			set
			{
				this.m_sNextPage = value;
			}
		}

		[CategoryAttribute( "LinkPage" )]
		public string PrevPage
		{
			get
			{
				return this.m_sPrevPage;
			}
			set
			{
				this.m_sPrevPage = value;
			}
		}

		public BrowserProperties( BrowserPanel bpBrowserToSet )
		{
			this.m_bpBrowserToSet = bpBrowserToSet;
		}
	}
}
