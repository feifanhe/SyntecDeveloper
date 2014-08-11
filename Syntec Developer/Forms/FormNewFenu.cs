using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Syntec_Developer.Forms
{
	public partial class FormNewFenu : Form
	{
		public string FenuName
		{
			get
			{
				return this.txtFenuName.Text;
			}
		}

		public FormNewFenu()
		{
			InitializeComponent();
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click( object sender, EventArgs e )
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}