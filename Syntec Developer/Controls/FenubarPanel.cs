using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Syntec_Developer.Forms;
using Fenubars;

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

		public FenubarPanel()
		{
			InitializeComponent();
			this.m_lstFenubarHandler = new List<Fenubars.Handler>();
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
						LoadFenubar( fi.FullName );
					}
				}
			}
		}

		public void LoadFenubar( string sXMLPath )
		{
			try {
				Fenubars.Handler hdlNewFenubar = new Fenubars.Handler( sXMLPath );
				hdlNewFenubar.Canvas = this.Controls;
				hdlNewFenubar.PropertyViewer = FormMain.PropertiesWindow.PropertyDisplay;
				this.m_lstFenubarHandler.Add( hdlNewFenubar );
			}
			catch( FileLoadException ) {
				MessageBox.Show( "Cannot load file." );
			}
			catch( InvalidOperationException e ) {
				MessageBox.Show( sXMLPath + ": " + e.Message );
				ReplaceInvalidValueInXML( sXMLPath );
				LoadFenubar( sXMLPath );
			}
		}

		private void ReplaceInvalidValueInXML( string sXMLPath )
		{
			File.WriteAllText(
				sXMLPath,
				File.ReadAllText( sXMLPath )
					.Replace( ">True<", ">true<" )
					.Replace( ">False<", ">false<" )
			);
		}
	}
}
