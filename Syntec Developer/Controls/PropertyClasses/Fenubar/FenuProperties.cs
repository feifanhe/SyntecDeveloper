using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Syntec_Developer.Controls.PropertyClasses
{
	public class FenuProperties
	{
		private string m_nName;

		[CategoryAttribute( "Fenu" )]
		public string Name
		{
			get
			{
				return this.m_nName;
			}
			set
			{
				this.m_nName = value;
			}
		}
	}
}
