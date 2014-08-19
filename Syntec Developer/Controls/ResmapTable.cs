using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Data;

namespace Syntec_Developer.Controls
{
	public class ResmapTable
	{
		string RootPath;
		string ProcessingLanguage;
		private Dictionary<string, Messages> Contents = new Dictionary<string, Messages>();
		BackgroundWorker ResmapLoader;

		public ResmapTable()
		{
			this.RootPath = string.Empty;
			this.ResmapLoader = new BackgroundWorker();
			this.ResmapLoader.DoWork += new DoWorkEventHandler( ResmapLoader_DoWork );
			this.ResmapLoader.RunWorkerCompleted +=
				new RunWorkerCompletedEventHandler( ResmapLoader_RunWorkerCompleted );
		}

		public void Load( string RootPath )
		{
			if( Directory.Exists( RootPath ) ) {
				this.RootPath = RootPath;
				this.ResmapLoader.RunWorkerAsync();
			}
		}

		public void Clear()
		{
			Contents.Clear();
		}

		public void SaveResmap()
		{
			XmlDocument InMemory = new XmlDocument();

			foreach( Messages DummyMsg in Contents.Values ) {
				foreach( XmlElement DummyElm in DummyMsg.Values.Values ) {
					Console.WriteLine( DummyElm.GetAttribute( "Content" ) );
					InMemory = DummyElm.OwnerDocument;
					break;
				}
			}

			InMemory.Save( "C:\\TEST2.XML" );
		}

		public string GetContent( string Language, string ID )
		{
			if( Contents.ContainsKey( Language ) ) {
				if( Contents[ Language ].Values.ContainsKey( ID ) ) {
					return Contents[ Language ][ ID ];
				}
			}
			return string.Empty;
		}

		public ICollection GetLanguages()
		{
			return this.Contents.Keys;
		}

		public List<string> GetIDsByKeyWord( string Language, string KeyWord )
		{
			List<string> IDs = new List<string>();
			foreach( string Key in this.Contents[ Language ].Values.Keys ) {
				if( Key.Contains( KeyWord ) ) {
					IDs.Add( Key );
				}
			}
			return IDs;
		}

		private void ResmapLoader_DoWork( object sender, DoWorkEventArgs e )
		{
			LoadDirectory( this.RootPath );

			//SaveResmap();
		}

		private void ResmapLoader_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
		}


		private void LoadDirectory( string sPath )
		{
			DirectoryInfo difDirectoryToLoad = new DirectoryInfo( sPath );
			foreach( DirectoryInfo difChildDirectory in difDirectoryToLoad.GetDirectories() ) {
				if( string.Compare( difChildDirectory.Name.ToUpper(), "STRING" ) == 0 ) {
					ProcessingLanguage = difDirectoryToLoad.Name.ToUpper();
					if( ProcessingLanguage == "COMMON" )
						ProcessingLanguage = "ENG";
					CreateLangeageItem( ProcessingLanguage );
					LoadFiles( difChildDirectory.GetFiles() );
				}
				LoadDirectory( difChildDirectory.FullName );
			}
		}

		private void CreateLangeageItem( string Language )
		{
			if( !this.Contents.ContainsKey( Language ) ) {
				this.Contents.Add( Language, new Messages() );
			}
		}

		private void LoadFiles( FileInfo[] fifaFilesToLoad )
		{
			foreach( FileInfo fifFileToLoad in fifaFilesToLoad ) {
				if( string.Compare( fifFileToLoad.Extension.ToUpper(), ".XML" ) == 0 ) {
					LoadResmap( fifFileToLoad );
				}
			}
		}

		private void LoadResmap( FileInfo ResmapFile )
		{
			XmlDocument ResmapFileToLoad = new XmlDocument();
			try {
				ResmapFileToLoad.Load( ResmapFile.FullName );
			}
			catch( XmlException xe ) {
				Console.WriteLine( "XmlException: " + xe.Message );
			}

			XmlNode ResmapNode = ResmapFileToLoad.SelectSingleNode( "ResMap" );
			if( ResmapNode == null ) {
				return;
			}

			LoadMessages( ResmapNode, ResmapFile.FullName );
		}

		private void LoadMessages( XmlNode ResmapNode, string FilePath )
		{
			//Dictionary<string, string> Messages = this.Contents[ ProcessingLanguage ];
			XmlNodeList MessageNodes = ResmapNode.ChildNodes;
			XmlElement MessageElement;
			string ID, Content;
			foreach( XmlNode MessageNode in MessageNodes ) {
				if( MessageNode.Name == "Message" ) {
					MessageElement = MessageNode as XmlElement;
					ID = MessageElement.GetAttribute( "ID" );
					Content = MessageElement.GetAttribute( "Content" );
					//Console.WriteLine( " * " + ProcessingLanguage + " :: " + ID + " :: " + Content );
					if( !Contents[ProcessingLanguage].Values.ContainsKey( ID ) ) {

						Contents[ ProcessingLanguage ].ImportNode( MessageElement );
					}
				}
			}
		}

		internal class Messages
		{
			private Dictionary<string, XmlElement> _Values =
				new Dictionary<string, XmlElement>();

			public Messages()
			{
			}

			public Dictionary<string, XmlElement> Values
			{
				get
				{
					return _Values;
				}
				set
				{
					_Values = value;
				}
			}

			public string this[ string ID ]
			{
				get
				{
					if( _Values.ContainsKey( ID ) )
						return _Values[ ID ].GetAttribute( "Content" );
					return string.Empty;
				}
			}

			public void ImportNode( XmlElement Node )
			{
				_Values.Add( Node.GetAttribute( "ID" ), Node );
			}
		}
	}
}
