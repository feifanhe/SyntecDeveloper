using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Syntec_Developer.Controls.PropertyClasses
{
	[TypeConverter( typeof( ExpandableObjectConverter ) )]
	public class ProtectedActions
	{
		private string m_sPassword;
		private List<string> m_lstCorrectActions;
		private List<string> m_lstIncorrectActions;

		public string Password
		{
			get
			{
				return this.m_sPassword;
			}
			set
			{
				this.m_sPassword = value;
			}
		}

		public List<string> CorrectActions
		{
			get
			{
				return this.m_lstCorrectActions;
			}
		}

		public List<string> IncorrectActions
		{
			get
			{
				return this.m_lstIncorrectActions;
			}
		}

		public ProtectedActions()
		{
			this.m_sPassword = string.Empty;
			this.m_lstCorrectActions = new List<string>();
			this.m_lstIncorrectActions = new List<string>();
		}

		public override string ToString()
		{
			return string.Empty;
		}
	}
}
