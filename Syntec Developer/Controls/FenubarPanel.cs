using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fenubars;
using Syntec_Developer.Forms;

namespace Syntec_Developer.Controls
{
	public partial class FenubarPanel : UserControl
	{
		private Handler m_hdlFenubarHandler;

		private DCProperties m_dcpPropertiesWindow;
		public DCProperties PropertiesWindow
		{
			get
			{
				return this.m_dcpPropertiesWindow;
			}
			set
			{
				this.m_dcpPropertiesWindow = value;
			}
		}

		private DCFenuList m_dcflFenuListWindow;
		public DCFenuList FenuListWindow
		{
			get
			{
				return this.m_dcflFenuListWindow;
			}
			set
			{
				this.m_dcflFenuListWindow = value;
			}
		}

		public FenubarPanel()
		{
			InitializeComponent();
		}

		public void Load( string XMLPath )
		{
			try {
				this.m_hdlFenubarHandler = new Handler( XMLPath );
				this.m_hdlFenubarHandler.Canvas = this.Controls;
				this.m_hdlFenubarHandler.PropertyViewer = this.PropertiesWindow.PropertyDisplay;
				this.m_hdlFenubarHandler.LoadFenu( "main" );
			}
			catch( FileLoadException ) {
				MessageBox.Show( "Cannot load file." );
			}
		}
	}
}
