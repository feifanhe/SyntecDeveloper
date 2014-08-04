using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Syntec_Developer.Controls.PropertyClasses;

namespace Syntec_Developer.Controls
{
	public enum ItemType
	{
		Button,
		CheckBox,
		CoordBox,
		Display,
		FileList,
		Input,
		Inputline,
		Label,
		Lamp,
		ListInput,
		Meter,
		OCSimu,
		OCText,
		Panel,
		Picture,
		Radio,
		Vision,
		NoType
	}

	public partial class BrowserItem : Label
	{
		public const int MIN_ITEM_SIZE = 15;

		private int m_nMouseDownX;
		private int m_nMouseDownY;
		private bool m_bIsMouseDown;
		private ItemType m_itType;
		private ItemProperties m_ipProperties;

		internal XmlDocument m_xdDocument;
		internal XmlNode m_xnItemNode;

		public ItemType Type
		{
			get
			{
				return this.m_itType;
			}
		}

		public ItemProperties Properties
		{
			get
			{
				return this.m_ipProperties;
			}
			set
			{
				this.m_ipProperties = value;
			}
		}

		public BrowserItem( XmlDocument xdDocument, XmlNode xnItemNode )
		{
			InitializeComponent();
			this.Location = new Point( 0, 0 );
			this.Size = new Size( 0, 0 );
			this.m_xdDocument = xdDocument;
			this.m_xnItemNode = xnItemNode;
			InitializeTypeAndProperty( this.m_xnItemNode.Name );
			LoadXML();
		}

		private void InitializeTypeAndProperty( string sItemTypeName )
		{
			switch( sItemTypeName ) {
				case "Button":
					this.m_itType = ItemType.Button;
					this.m_ipProperties = new ButtonProperties( this );
					break;
				case "CheckBox":
					this.m_itType = ItemType.CheckBox;
					this.m_ipProperties = new CheckBoxProperties( this );
					break;
				case "CoordBox":
					this.m_itType = ItemType.CoordBox;
					this.m_ipProperties = new CoordBoxProperties( this );
					break;
				case "Display":
					this.m_itType = ItemType.Display;
					this.m_ipProperties = new DisplayProperties( this );
					break;
				case "FileList":
					this.m_itType = ItemType.FileList;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Input":
					this.m_itType = ItemType.Input;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Inputline":
					this.m_itType = ItemType.Inputline;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Label":
					this.m_itType = ItemType.Label;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Lamp":
					this.m_itType = ItemType.Lamp;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "ListInput":
					this.m_itType = ItemType.ListInput;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Meter":
					this.m_itType = ItemType.Meter;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "OCSimu":
					this.m_itType = ItemType.OCSimu;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "OCText":
					this.m_itType = ItemType.OCText;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Panel":
					this.m_itType = ItemType.Panel;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Picture":
					this.m_itType = ItemType.Picture;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Radio":
					this.m_itType = ItemType.Radio;
					this.m_ipProperties = new ItemProperties( this );
					break;
				case "Vision":
					this.m_itType = ItemType.Vision;
					this.m_ipProperties = new ItemProperties( this );
					break;
				default:
					this.m_itType = ItemType.NoType;
					this.m_ipProperties = new ItemProperties( this );
					break;
			}
		}

		public void LoadXML()
		{
			this.m_ipProperties.LoadXML();
		}

		public void SaveItem()
		{
			this.m_ipProperties.SaveXML();
		}

		public void RemoveXmlNode()
		{
			this.m_xnItemNode.ParentNode.RemoveChild( this.m_xnItemNode );
		}

		#region Resize Box

		public void Item_MouseDown( object sender, EventArgs e )
		{
			List<BrowserItem> lstSelectedItem = sender as List<BrowserItem>;
			if( lstSelectedItem.Count > 1 ) {
				DisableResizeBoxes();
			}
			else {
				EnableResizeBoxes();
			}
		}

		private void EnableResizeBoxes()
		{
			this.pnlTopResizeBox.Enabled = true;
			this.pnlBottomResizeBox.Enabled = true;
			this.pnlLeftResizeBox.Enabled = true;
			this.pnlRightResizeBox.Enabled = true;
			this.pnlTopLeftResizeBox.Enabled = true;
			this.pnlTopRightResizeBox.Enabled = true;
			this.pnlBottomLeftResizeBox.Enabled = true;
			this.pnlBottomRightResizeBox.Enabled = true;
		}

		private void DisableResizeBoxes()
		{
			this.pnlTopResizeBox.Enabled = false;
			this.pnlBottomResizeBox.Enabled = false;
			this.pnlLeftResizeBox.Enabled = false;
			this.pnlRightResizeBox.Enabled = false;
			this.pnlTopLeftResizeBox.Enabled = false;
			this.pnlTopRightResizeBox.Enabled = false;
			this.pnlBottomLeftResizeBox.Enabled = false;
			this.pnlBottomRightResizeBox.Enabled = false;
		}

		private void ResizeBoxMouseDown( object sender, MouseEventArgs e )
		{
			this.m_nMouseDownX = e.X;
			this.m_nMouseDownY = e.Y;
			this.m_bIsMouseDown = true;
		}

		private void ResizeBoxMouseUp( object sender, MouseEventArgs e )
		{
			this.m_bIsMouseDown = false;
		}

		private void TopResizeBoxMouseMove( object sender, MouseEventArgs e )
		{
			if( this.m_bIsMouseDown ) {
				int nDeltaY = e.Y - this.m_nMouseDownY;
				if( this.Height - nDeltaY >= MIN_ITEM_SIZE ) {
					this.Top += nDeltaY;
					this.Height -= nDeltaY;
				}
			}
		}

		private void BottomResizeBoxMouseMove( object sender, MouseEventArgs e )
		{
			if( this.m_bIsMouseDown ) {
				if( this.Height + e.Y - this.m_nMouseDownY >= MIN_ITEM_SIZE ) {
					this.Height += e.Y - this.m_nMouseDownY;
				}
			}
		}

		private void LeftResizeBoxMouseMove( object sender, MouseEventArgs e )
		{
			if( this.m_bIsMouseDown ) {
				int nDeltaX = e.X - this.m_nMouseDownX;
				if( this.Width - nDeltaX >= MIN_ITEM_SIZE ) {
					this.Left += nDeltaX;
					this.Width -= nDeltaX;
				}
			}
		}

		private void RightResizeBoxMouseMove( object sender, MouseEventArgs e )
		{
			if( this.m_bIsMouseDown ) {
				if( this.Width + e.X - this.m_nMouseDownX >= MIN_ITEM_SIZE ) {
					this.Width += e.X - this.m_nMouseDownX;
				}
			}
		}

		private void TopLeftResizeBoxMouseMove( object sender, MouseEventArgs e )
		{
			TopResizeBoxMouseMove( sender, e );
			LeftResizeBoxMouseMove( sender, e );
		}

		private void TopRightResizeBoxMouseMove( object sender, MouseEventArgs e )
		{
			TopResizeBoxMouseMove( sender, e );
			RightResizeBoxMouseMove( sender, e );
		}

		private void BottomLeftResizeBoxMouseMove( object sender, MouseEventArgs e )
		{
			BottomResizeBoxMouseMove( sender, e );
			LeftResizeBoxMouseMove( sender, e );
		}

		private void BottomRightResizeBoxMouseMove( object sender, MouseEventArgs e )
		{
			BottomResizeBoxMouseMove( sender, e );
			RightResizeBoxMouseMove( sender, e );
		}

		#endregion
	}
}
