using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Syntec_Developer.Controls.PropertyClasses
{
	[TypeConverter( typeof( ExpandableObjectConverter ) )]
	public class LanguagePack
	{
		private string m_sID;
		private Dictionary<string, string> m_dicValues;

		public string ID
		{
			get
			{
				return this.m_sID;
			}
			set
			{
				this.m_sID = value;
			}
		}

		public Dictionary<string, string> Values
		{
			get
			{
				return this.m_dicValues;
			}
			set
			{
				this.m_dicValues = value;
			}
		}

		public LanguagePack()
		{
			this.ID = string.Empty;
			this.m_dicValues = new Dictionary<string, string>();
		}

		public override string ToString()
		{
			return string.Empty;
		}

		public LanguagePack Clone()
		{
			LanguagePack Clone = this.MemberwiseClone() as LanguagePack;
			Clone.Values = new Dictionary<string, string>();
			foreach( string Key in this.Values.Keys ) {
				Clone.Values[ Key ] = this.Values[ Key ];
			}
			return Clone;
		}
	}
}
