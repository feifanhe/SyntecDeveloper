using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Syntec_Developer.Controls;

namespace Syntec_Developer.Forms
{
	public partial class SearchFenubar : Form
	{
		DCDocument m_dcFenubarDocument;
		DCFenuList m_dcFenuList;
		List<string> lstIDs;

		public SearchFenubar()
		{
			InitializeComponent();
		}

		public static void Show( DCDocument dcFenubarDocument, DCFenuList dcFenuList )
		{
			SearchFenubar searchFenuBar = new SearchFenubar();
			searchFenuBar.m_dcFenubarDocument = dcFenubarDocument;
			searchFenuBar.m_dcFenuList = dcFenuList;
			searchFenuBar.Show();
		}


		private void btnSearch_Click( object sender, EventArgs e )
		{
			DoSearch();
		}

		private void DoSearch()
		{
			if( string.Compare( this.txtFenuButtonName.Text, string.Empty ) == 0 ) {
				return;
			}

			this.lstIDs =
				this.m_dcFenubarDocument.m_rtResmapTable.GetIDsByKeyWord( "CHT", this.txtFenuButtonName.Text );

			ArrayList arlFenus = new ArrayList( this.m_dcFenubarDocument.Fenubar.Fenus.Values );
			for( int i = 0; i < arlFenus.Count; i++ ) {
				FenuButton[] afbButtons = ( (Fenu)arlFenus[ i ] ).Buttons;
				for( int j = 0; j < afbButtons.Length; j++ ) {
					string sButtonTitleID = afbButtons[ j ].Properties.Title.ID;
					if( sButtonTitleID != null && sButtonTitleID.ToUpper().Contains( "STR::" ) ) {
						if( lstIDs.Contains( sButtonTitleID.Substring( 5 ) ) ) {
							this.m_dcFenuList.FindFenu( ( (Fenu)arlFenus[ i ] ).Properties.Name );
						}
					}
				}
			}
		}

		private void txtFenuButtonName_KeyDown( object sender, KeyEventArgs e )
		{
			switch( e.KeyData ) {
				case Keys.Enter:
					DoSearch();
					break;
				case Keys.Escape:
					this.Close();
					break;
			}
		}
	}
}