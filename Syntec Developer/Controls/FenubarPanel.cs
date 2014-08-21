using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Syntec_Developer.Forms;

namespace Syntec_Developer.Controls
{
	public partial class FenubarPanel : UserControl
	{
		private List<Fenubars.Handler> m_lstFenubarHandler;
		public List<Fenubars.Handler> Fenubars
		{
			get
			{
				return this.m_lstFenubarHandler;
			}
		}

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
			this.m_lstFenubarHandler = new List<Fenubars.Handler>();
		}

		public void LoadFenubar( string sXMLPath )
		{
			try {
				Fenubars.Handler hdlNewFenubar = new Fenubars.Handler( sXMLPath );
				hdlNewFenubar.Canvas = this.Controls;
				hdlNewFenubar.PropertyViewer = this.PropertiesWindow.PropertyDisplay;
				this.m_lstFenubarHandler.Add( hdlNewFenubar );
			}
			catch( FileLoadException ) {
				MessageBox.Show( "Cannot load file." );
			}
			catch( InvalidOperationException ) {
			}
		}

		public void LoadProduct( string sProductPath )
		{
			DirectoryInfo di = new DirectoryInfo( sProductPath );
			while( di != null ) {
				LoadFiles( di.FullName );
				di = di.Parent;
			}
		}

		private void LoadFiles( string sPath )
		{
			string sCommon = sPath + "\\Common";
			DirectoryInfo di = new DirectoryInfo( sCommon );
			if( di.Exists ) {
				foreach( FileInfo fi in di.GetFiles() ) {
					if( fi.Extension == ".xml" ) {
						try {
							Fenubars.Handler hdlNewFenubar = new Fenubars.Handler( fi.FullName );
							hdlNewFenubar.Canvas = this.Controls;
							hdlNewFenubar.PropertyViewer = this.PropertiesWindow.PropertyDisplay;
							this.m_lstFenubarHandler.Add( hdlNewFenubar );
						}
						catch( FileLoadException ) {
							//MessageBox.Show( "Cannot load file." );
						}
						catch( InvalidOperationException ) {
						}
					}
				}
			}
		}
	}
}
