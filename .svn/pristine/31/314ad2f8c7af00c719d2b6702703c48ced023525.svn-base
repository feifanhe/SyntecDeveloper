using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Syntec_Developer.Controls;

namespace Syntec_Developer.Forms
{
	public partial class DCToolBox : DockContent
	{
		public delegate void SelectItemToDrawHandler( ItemType itSelectedItemType );
		public event SelectItemToDrawHandler SelectItemToDraw;

		public DCToolBox()
		{
			InitializeComponent();
		}

		private void btnCursor_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.NoType );
		}

		private void btnButton_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null)
				this.SelectItemToDraw.Invoke( ItemType.Button );
		}

		private void btnCheckBox_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.CheckBox );
		}

		private void btnCoordBox_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.CoordBox );
		}

		private void btnDisplay_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Display );
		}

		private void btnInput_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Input );
		}

		private void btnLabel_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Label );
		}

		private void btnLamp_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Lamp );

		}

		private void btnListInput_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.ListInput );
		}

		private void btnMeter_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Meter );
		}

		private void btnPanel_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Panel );
		}

		private void btnPicture_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Picture );
		}

		private void btnRadio_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Radio );
		}

		private void btnVision_Click( object sender, EventArgs e )
		{
			if( this.SelectItemToDraw != null )
				this.SelectItemToDraw.Invoke( ItemType.Vision );
		}

		private void Button_Enter( object sender, EventArgs e )
		{
			Button btn = sender as Button;
			btn.BackColor = Color.AliceBlue;
		}

		private void Button_Leave( object sender, EventArgs e )
		{
			Button btn = sender as Button;
			btn.BackColor = System.Drawing.SystemColors.Window;
		}

	}
}