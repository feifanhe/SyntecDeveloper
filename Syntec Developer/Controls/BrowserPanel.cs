using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Xml;
using Syntec_Developer.Forms;
using Syntec_Developer.Controls.PropertyClasses;

namespace Syntec_Developer.Controls
{
	public partial class BrowserPanel : UserControl
	{
		private bool m_bIsNewFile;
		private bool m_bHasLoaded;
		private bool m_bMouseDown;
		private int m_nMouseDownX;
		private int m_nMouseDownY;
		private string m_sFileName;

		private ItemType m_itSelectedItemToDraw;
		private List<string> m_lstGroupName;
		private Hashtable m_htbItems;
		private List<BrowserItem> m_lstSelectItems;
		private BrowserProperties m_bpProperties;
		private Graphics m_grpMouseDragDropRegion;

		internal XmlDocument m_xdDocument;
		internal XmlNode m_xnScreenNode;

		public string FullName
		{
			get
			{
				return this.m_sFileName;
			}
			set
			{
				this.m_sFileName = value;
			}
		}

		public string FileName
		{
			get
			{
				if( this.m_sFileName.LastIndexOf( '\\' ) >= 0 ) {
					return this.m_sFileName.Substring( this.m_sFileName.LastIndexOf( '\\' ) + 1 );
				}
				else {
					return this.m_sFileName;
				}
			}
		}

		public BrowserProperties Properties
		{
			get
			{
				return this.m_bpProperties;
			}
		}

		public Hashtable Items
		{
			get
			{
				return this.m_htbItems;
			}
		}

		public List<BrowserItem> SelectedItems
		{
			get
			{
				return this.m_lstSelectItems;
			}
		}

		#region Custom Define Event

		public event EventHandler ItemMouseDown;
		public event EventHandler ItemPropertiesChanged;
		public event EventHandler ItemAddedDeleted;
		public delegate void BrowserSizeLoadHandler( int nWidth, int nHeight );
		public delegate void ItemLoadHandler( BrowserItem biItem );

		#endregion

		#region Constructor

		public BrowserPanel( string sFileName, bool bIsNewFile )
		{
			InitializeComponent();
			this.m_sFileName = sFileName;
			this.m_bIsNewFile = bIsNewFile;
			FormMain.ToolBoxWindow.SelectItemToDraw += new DCToolBox.SelectItemToDrawHandler( SelectedItemToDraw );
			InitializeMembers();
		}

		private void InitializeMembers()
		{
			this.m_bHasLoaded = false;
			this.m_bMouseDown = false;
			this.m_nMouseDownX = 0;
			this.m_nMouseDownY = 0;
			this.m_itSelectedItemToDraw = ItemType.NoType;
			this.m_lstGroupName = new List<string>();
			this.m_lstSelectItems = new List<BrowserItem>();
			this.m_htbItems = new Hashtable();
			this.m_bpProperties = new BrowserProperties( this );
			this.m_grpMouseDragDropRegion = this.CreateGraphics();
		}

		#endregion

		#region Load XML

		private void BrowserPanel_Load( object sender, EventArgs e )
		{
			if( !this.m_bIsNewFile && !this.m_bHasLoaded ) {
				this.m_xdDocument = new XmlDocument();
				this.m_xdDocument.Load( m_sFileName );
				this.bgwLoadXml.RunWorkerAsync();
			}
			else {
				this.m_xdDocument = new XmlDocument();

				XmlNode xnHeaderNode =
					this.m_xdDocument.CreateXmlDeclaration( "1.0", "UTF-8", null );
				this.m_xdDocument.AppendChild( xnHeaderNode );

				XmlComment xcDeveloperVersion =
					m_xdDocument.CreateComment( "Created by \"SyntecDeveloper\", version PreAlpha" );
				this.m_xdDocument.AppendChild( xcDeveloperVersion );

				XmlComment xcBrowserSize = m_xdDocument.CreateComment( "BROWSER SIZE|H:470|W:800" );
				this.m_xdDocument.AppendChild( xcBrowserSize );

				XmlNode xnScreenNode = this.m_xdDocument.CreateElement( "Screen" );
				this.m_xdDocument.AppendChild( xnScreenNode );

				this.m_xnScreenNode = xnScreenNode;
			}
		}

		private void bgwLoadXml_DoWork( object sender, DoWorkEventArgs e )
		{
			this.LoadXml();
			this.m_bHasLoaded = true;
		}

		private void bgwLoadXml_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			FormMain.PropertiesWindow.UpdateComboBoxWithBrowserItem( this.Browser );
		}

		private void LoadXml()
		{
			LoadBrowserSize();
			XmlNode xnScreenNode = this.m_xdDocument.SelectSingleNode( "Screen" );
			this.m_xnScreenNode = xnScreenNode;
			foreach( XmlNode xnItem in this.m_xnScreenNode.ChildNodes ) {
				switch( xnItem.Name ) {
					case "Name":
						this.m_bpProperties.Name = xnItem.InnerText;
						break;
					case "Group":
						LoadGroup( xnItem );
						break;
					case "Grid":
						LoadGrid( xnItem );
						break;
					case "Link":
						LoadLink( xnItem );
						break;
					default:
						LoadItem( xnItem, string.Empty );
						break;
				}
			}
		}

		private void LoadBrowserSize()
		{
			int nWidth = 800, nHeight = 470;
			XmlNode xnBrowserSizeCommentNode = GetBrowserSizeCommentNode();
			if( xnBrowserSizeCommentNode != null ) {
				string sBrowserSizeComment = xnBrowserSizeCommentNode.InnerText;
				string[] saBrowserSizeDataArray = sBrowserSizeComment.Split( '|' );
				foreach( string sSizeData in saBrowserSizeDataArray ) {
					if( sSizeData.Contains( "W:" ) ) {
						nWidth = GetBrowserWidth( sSizeData );
					}
					else if( sSizeData.Contains( "H:" ) ) {
						nHeight = GetBrowserHeight( sSizeData );
					}
				}
			}
			this.Invoke( new BrowserSizeLoadHandler( SetBrowserSize ), new object[] { nWidth, nHeight } );
		}

		private void SetBrowserSize( int nWidth, int nHeight )
		{
			this.Width = nWidth;
			this.Height = nHeight;
		}

		private XmlNode GetBrowserSizeCommentNode()
		{
			foreach( XmlNode xnItem in this.m_xdDocument.ChildNodes ) {
				if( xnItem.NodeType == XmlNodeType.Comment ) {
					if( xnItem.InnerText.Contains( "BROWSER SIZE" ) ) {
						return xnItem;
					}
				}
			}
			return null;
		}

		private int GetBrowserWidth( string sWidth )
		{
			int nWidth;
			string sWidthString = sWidth.Substring( sWidth.IndexOf( "W:" ) + 2 );
			if( int.TryParse( sWidthString, out nWidth ) ) {
				return nWidth;
			}
			else {
				MessageBox.Show( sWidthString );
				return 800;
			}
		}

		private int GetBrowserHeight( string sHeight )
		{
			int nHeight;
			string sHeightString = sHeight.Substring( sHeight.IndexOf( "H:" ) + 2 );
			if( int.TryParse( sHeightString, out nHeight ) ) {
				return nHeight;
			}
			else {
				MessageBox.Show( sHeightString );
				return 470;
			}
		}

		private void LoadGroup( XmlNode xnGroup )
		{
			string sGroupName = string.Empty;
			if( xnGroup.Attributes[ "Name" ] != null ) {
				sGroupName = xnGroup.Attributes[ "Name" ].Value;
				if( !this.m_lstGroupName.Contains( sGroupName ) ) {
					this.m_lstGroupName.Add( sGroupName );
				}
			}
			foreach( XmlNode xnItem in xnGroup.ChildNodes ) {
				LoadItem( xnItem, sGroupName );
			}
		}

		private void LoadGrid( XmlNode xnGrid )
		{
			foreach( XmlNode xnItem in xnGrid.ChildNodes ) {
				LoadItem( xnItem, string.Empty );
			}
		}

		private void LoadLink( XmlNode xnLink )
		{
			string sLinkType = xnLink.SelectSingleNode( "Name" ).InnerText;
			string sPageUrl = xnLink.SelectSingleNode( "PageUrl" ).InnerText;
			switch( sLinkType ) {
				case "NextPage":
					this.Properties.NextPage = sPageUrl;
					break;
				case "PrevPage":
					this.Properties.PrevPage = sPageUrl;
					break;
			}
		}

		private void LoadItem( XmlNode xnItemNode, string sGroupName )
		{
			BrowserItem biItemToAdd = new BrowserItem( this.m_xdDocument, xnItemNode );
			biItemToAdd.Properties.Group = sGroupName;
			SetItemEvents( biItemToAdd );
			this.Invoke( new ItemLoadHandler( AddItemIntoBrowser ), new object[] { biItemToAdd } );
		}

		private void SetItemEvents( BrowserItem biItemToAdd )
		{
			biItemToAdd.MouseDown += new MouseEventHandler( Item_MouseDown );
			biItemToAdd.MouseMove += new MouseEventHandler( Item_MouseMove );
			biItemToAdd.MouseUp += new MouseEventHandler( Item_MouseUp );
			biItemToAdd.KeyDown += new KeyEventHandler( Item_KeyDown );
			biItemToAdd.PreviewKeyDown += new PreviewKeyDownEventHandler( Item_PreviewKeyDown );
			biItemToAdd.LocationChanged += new EventHandler( Item_PropertiesChanged );
			biItemToAdd.SizeChanged += new EventHandler( Item_PropertiesChanged );
			biItemToAdd.Properties.PropertiesChanged +=
				new ItemProperties.PropertiesChangedHandler( Item_PropertiesChanged );
		}

		private void AddItemIntoBrowser( BrowserItem biItemToAdd )
		{
			this.ItemMouseDown += new EventHandler( biItemToAdd.Item_MouseDown );
			this.Controls.Add( biItemToAdd );
			biItemToAdd.BringToFront();
			if( this.m_htbItems.ContainsKey( biItemToAdd.Properties.Name ) ) {
				MessageBox.Show( "Error: The same name of the item has been used" );
				return;
			}
			this.m_htbItems.Add( biItemToAdd.Properties.Name, biItemToAdd );
		}

		#endregion

		#region Save XML

		public void SaveFile()
		{
			this.bgwSaveXml.RunWorkerAsync();
		}

		private void bgwSaveXml_DoWork( object sender, DoWorkEventArgs e )
		{
			SaveBrowser();
		}

		private void SaveBrowser()
		{
			SaveBrowserProperties();
			foreach( BrowserItem biItem in this.m_htbItems.Values ) {
				biItem.SaveItem();
			}
			SaveXML();
		}

		private void SaveBrowserProperties()
		{
			SaveBrowserSize();
			SaveBrowserName();

			// TODO: Save Other Properties
			//if( string.Compare( this.Properties.NextPage, string.Empty ) != 0 ) {
			//    XmlNodeList xnlstLinkNodes = this.m_xnScreenNode.SelectNodes( "Link" );
			//    foreach
			//}
		}

		private void SaveBrowserSize()
		{
			XmlNode xnBrowserSizeCommentNode = GetBrowserSizeCommentNode();
			xnBrowserSizeCommentNode.InnerText =
				string.Concat( "BROWSER SIZE|H:", this.Height.ToString(), "|W:", this.Width.ToString() );
		}

		private void SaveBrowserName()
		{
			XmlNode xnTempNode = this.m_xnScreenNode.SelectSingleNode( "Name" );
			if( xnTempNode != null ) {
				xnTempNode.InnerText = this.m_bpProperties.Name;
			}
			else {
				xnTempNode = this.m_xdDocument.CreateElement( "Name" );
				this.m_xnScreenNode.AppendChild( xnTempNode );
			}
		}

		private void SaveXML()
		{
			if( this.m_bIsNewFile ) {
				this.sfdlgSaveXml.FileName = this.m_sFileName;
				if( this.sfdlgSaveXml.ShowDialog() == DialogResult.OK ) {
					this.m_xdDocument.Save( this.sfdlgSaveXml.FileName );
					this.m_sFileName = this.sfdlgSaveXml.FileName;
					this.m_bIsNewFile = false;
				}
			}
			else {
				this.m_xdDocument.Save( this.m_sFileName );
			}
		}

		#endregion

		#region Item Mouse Event

		private void Item_MouseDown( object sender, MouseEventArgs e )
		{
			BrowserItem biItem = sender as BrowserItem;
			this.m_bMouseDown = true;
			this.m_nMouseDownX = e.X;
			this.m_nMouseDownY = e.Y;

			if( Form.ModifierKeys == Keys.Control ) {
				if( m_lstSelectItems.Contains( biItem ) ) {
					m_lstSelectItems.Remove( biItem );
					biItem.BackColor = Color.White;
				}
				else {
					m_lstSelectItems.Add( biItem );
					biItem.BackColor = Color.Blue;
				}
			}
			else if( !m_lstSelectItems.Contains( biItem ) ) {
				foreach( BrowserItem biTemp in m_lstSelectItems ) {
					biTemp.BackColor = Color.White;
				}
				m_lstSelectItems.Clear();
				m_lstSelectItems.Add( biItem );
				biItem.BackColor = Color.Blue;
			}

			this.ItemMouseDown.Invoke( m_lstSelectItems, e );
		}

		public void Item_MouseMove( object sender, MouseEventArgs e )
		{
			if( this.m_bMouseDown ) {
				foreach( BrowserItem biItem in this.m_lstSelectItems ) {
					biItem.Left += e.X - this.m_nMouseDownX;
					biItem.Top += e.Y - this.m_nMouseDownY;
				}
			}
		}

		private void Item_MouseUp( object sender, MouseEventArgs e )
		{
			BrowserItem biItem = sender as BrowserItem;
			this.m_bMouseDown = false;
		}

		#endregion

		#region Item Key Event

		private void Item_KeyDown( object sender, KeyEventArgs e )
		{
			BrowserItem biItem = sender as BrowserItem;
			switch( e.KeyCode ) {
				case Keys.Up:
					biItem.Properties.Y -= 1;
					break;
				case Keys.Down:
					biItem.Properties.Y += 1;
					break;
				case Keys.Left:
					biItem.Properties.X -= 1;
					break;
				case Keys.Right:
					biItem.Properties.X += 1;
					break;
				default:
					break;
			}
		}

		private void Item_PreviewKeyDown( object sender, PreviewKeyDownEventArgs e )
		{
			switch( e.KeyCode ) {
				case Keys.Up:
				case Keys.Down:
				case Keys.Left:
				case Keys.Right:
					e.IsInputKey = true;
					break;
			}
		}

		#endregion

		#region Browser Mouse Event And Drawing

		#region Set Draw Type

		public void SelectedItemToDraw( ItemType itSelectedItemType )
		{
			this.m_itSelectedItemToDraw = itSelectedItemType;
			SetCursor();
		}

		private void SetCursor()
		{
			if( this.m_itSelectedItemToDraw == ItemType.NoType ) {
				this.Cursor = Cursors.Default;
			}
			else {
				this.Cursor = Cursors.Cross;
			}
		}

		#endregion

		#region Browser Mouse Event

		private void BrowserPanel_MouseDown( object sender, MouseEventArgs e )
		{
			this.m_nMouseDownX = e.X;
			this.m_nMouseDownY = e.Y;
			this.m_bMouseDown = true;

			if( Form.ModifierKeys != Keys.Control ) {
				foreach( BrowserItem biTemp in m_lstSelectItems ) {
					biTemp.BackColor = Color.White;
				}
				m_lstSelectItems.Clear();
			}
		}

		private void BrowserPanel_MouseMove( object sender, MouseEventArgs e )
		{
			if( this.m_bMouseDown ) {
				this.m_grpMouseDragDropRegion.Clear( this.BackColor );
				if( this.m_itSelectedItemToDraw == ItemType.NoType ) {
					DrawMouseDragDropRegion( e.Location );
				}
				else {
					DrawNewItemRegion( e.Location );
				}
			}
		}

		private void BrowserPanel_MouseUp( object sender, MouseEventArgs e )
		{
			this.m_grpMouseDragDropRegion.Clear( this.BackColor );
			this.m_bMouseDown = false;
			if( this.m_itSelectedItemToDraw == ItemType.NoType ) {
				MouseDragDropSelectItems( e.Location );
			}
			else {
				CreateNewItem( e.Location );
			}
		}

		# endregion

		#region Draw Region

		private void DrawMouseDragDropRegion( Point pntMouseMoveLocation )
		{
			int nDeltaX = pntMouseMoveLocation.X - this.m_nMouseDownX;
			int nDeltaY = pntMouseMoveLocation.Y - this.m_nMouseDownY;
			int nOriginX = ( nDeltaX < 0 ) ? this.m_nMouseDownX + nDeltaX : this.m_nMouseDownX;
			int nOriginY = ( nDeltaY < 0 ) ? this.m_nMouseDownY + nDeltaY : this.m_nMouseDownY;
			int nWidth = Math.Abs( nDeltaX );
			int nHeight = Math.Abs( nDeltaY );
			Pen penMouseDragDropRegion = new Pen( Color.Black, 1 );
			penMouseDragDropRegion.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			this.m_grpMouseDragDropRegion.DrawRectangle( penMouseDragDropRegion, nOriginX, nOriginY, nWidth, nHeight );
		}

		private void DrawNewItemRegion( Point pntMouseMoveLocation )
		{
			Pen penNewItemRegion = new Pen( Color.Black, 1 );
			penNewItemRegion.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			this.m_grpMouseDragDropRegion.DrawRectangle(
				penNewItemRegion,
				this.m_nMouseDownX, this.m_nMouseDownY,
				pntMouseMoveLocation.X - this.m_nMouseDownX,
				pntMouseMoveLocation.Y - this.m_nMouseDownY
			);
		}

		#endregion

		#region Select Items

		private void MouseDragDropSelectItems( Point pntMouseUpLocation )
		{
			foreach( BrowserItem biItem in m_htbItems.Values ) {
				if( IsItemInDragDropRegion( biItem, pntMouseUpLocation ) ) {
					m_lstSelectItems.Add( biItem );
					biItem.BackColor = Color.Blue;
				}
			}
		}

		private bool IsItemInDragDropRegion( BrowserItem biItem, Point pntMouseUpLocation )
		{
			int nLeftMost = Math.Min( this.m_nMouseDownX, pntMouseUpLocation.X );
			int nTopMost = Math.Min( this.m_nMouseDownY, pntMouseUpLocation.Y );
			int nRightMost = Math.Abs( pntMouseUpLocation.X - this.m_nMouseDownX ) + nLeftMost;
			int nDownMost = Math.Abs( pntMouseUpLocation.Y - this.m_nMouseDownY ) + nTopMost;
			return (
				biItem.Properties.X >= nLeftMost &&
				biItem.Properties.X <= nRightMost &&
				biItem.Properties.Y >= nTopMost &&
				biItem.Properties.Y <= nDownMost
			);
		}

		#endregion

		#region Create Item

		private void CreateNewItem( Point pntMouseUpLocation )
		{
			int nWidth = Math.Max( pntMouseUpLocation.X - this.m_nMouseDownX, 15 );
			int nHeight = Math.Max( pntMouseUpLocation.Y - this.m_nMouseDownY, 15 );
			string sItemTypeName = this.m_itSelectedItemToDraw.ToString();
			BrowserItem biNewItem = new BrowserItem( this.m_xdDocument, CreateNewItemNode( sItemTypeName ) );
			biNewItem.Location = new Point( this.m_nMouseDownX, this.m_nMouseDownY );
			biNewItem.Size = new Size( nWidth, nHeight );
			biNewItem.Properties.Name = GenerateNewItemWithIndex( sItemTypeName );
			SetItemEvents( biNewItem );
			AddItemIntoBrowser( biNewItem );
			this.ItemAddedDeleted.Invoke( biNewItem, new EventArgs() );
		}

		private XmlNode CreateNewItemNode( string sNodeName )
		{
			XmlNode nxItemNode = this.m_xdDocument.CreateElement( sNodeName );
			this.m_xnScreenNode.AppendChild( nxItemNode );
			return nxItemNode;
		}

		private string GenerateNewItemWithIndex( string sDefaultName )
		{
			int nIndex = 1;
			bool bExist;
			string sResultName;
			do {
				sResultName = string.Concat( sDefaultName, nIndex.ToString() );
				bExist = false;

				foreach( string sExistingItemName in this.m_htbItems.Keys ) {
					if( string.Compare( sExistingItemName, sResultName ) == 0 ) {
						bExist = true;
						nIndex++;
						break;
					}
				}
			} while( bExist );
			return sResultName;
		}

		#endregion

		#endregion

		#region ItemLayout

		public void BringItemForward()
		{
			foreach( BrowserItem biItem in this.m_lstSelectItems ) {
				int nItemIndex = this.Controls.IndexOf( biItem );
				if( nItemIndex > 0 ) {
					this.Controls.SetChildIndex( biItem, nItemIndex - 1 );
				}
			}
		}

		public void SendItemBackward()
		{
			foreach( BrowserItem biItem in this.m_lstSelectItems ) {
				int nItemIndex = this.Controls.IndexOf( biItem );
				if( nItemIndex < this.Controls.Count - 1 ) {
					this.Controls.SetChildIndex( biItem, nItemIndex + 1 );
				}
			}
		}

		public void BringItemToFront()
		{
			foreach( BrowserItem biItem in this.m_lstSelectItems ) {
				biItem.BringToFront();
			}
		}

		public void SendItemToBack()
		{
			foreach( BrowserItem biItem in this.m_lstSelectItems ) {
				biItem.SendToBack();
			}
		}

		#endregion

		private void Item_PropertiesChanged( object sender, EventArgs e )
		{
			this.ItemPropertiesChanged.Invoke( this.m_lstSelectItems, e );
		}

		public void DeleteItems()
		{
			foreach( BrowserItem biItemToDelete in this.m_lstSelectItems ) {

				this.m_htbItems.Remove( biItemToDelete.Properties.Name );
				this.ItemAddedDeleted.Invoke( biItemToDelete, new EventArgs() );
				biItemToDelete.RemoveXmlNode();
				biItemToDelete.Dispose();
			}
			this.m_lstSelectItems.Clear();
		}

	}
}
