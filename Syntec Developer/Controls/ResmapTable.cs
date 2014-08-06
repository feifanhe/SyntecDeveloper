using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace Syntec_Developer.Controls
{
	public class ResmapTable
	{
		bool isLoading;
		string RootPath;
		string ProcessingLanguage;
		Hashtable Languages;
		BackgroundWorker ResmapLoader;

		public ResmapTable()
		{
			this.isLoading = false;
			this.RootPath = string.Empty;
			this.Languages = new Hashtable();
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
			foreach( Hashtable Messages in this.Languages.Values ) {
				Messages.Clear();
			}
			this.Languages.Clear();
		}

		public string GetContent( string Language, string ID )
		{
			if( Languages.ContainsKey( Language ) ) {
				Hashtable Messages = Languages[ Language ] as Hashtable;
				if( Messages.ContainsKey( ID ) ) {
					return Messages[ ID ] as string;
				}
			}
			return string.Empty;
		}

		public ICollection GetLanguages()
		{
			return this.Languages.Keys;
		}

		public List<string> GetIDsByKeyWord( string Language, string KeyWord )
		{
			List<string> IDs = new List<string>();
			Hashtable Messages = this.Languages[ Language ] as Hashtable;
			foreach( string Key in Messages.Keys ) {
				string Message = Messages[ Key ] as string;
				if( Message.Contains( KeyWord ) ) {
					IDs.Add( Key );
				}
			}
			return IDs;
		}

		private void ResmapLoader_DoWork( object sender, DoWorkEventArgs e )
		{
			this.isLoading = true;
			LoadDirectory( this.RootPath );
		}

		private void ResmapLoader_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			this.isLoading = false;
		}


		private void LoadDirectory( string sPath )
		{
			DirectoryInfo difDirectoryToLoad = new DirectoryInfo( sPath );
			foreach( DirectoryInfo difChildDirectory in difDirectoryToLoad.GetDirectories() ) {
				if( String.Compare( difChildDirectory.Name.ToUpper(), "STRING" ) == 0 ) {
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
			if( !this.Languages.ContainsKey( Language ) ) {
				this.Languages.Add( Language, new Hashtable() );
			}
		}

		private void LoadFiles( FileInfo[] fifaFilesToLoad )
		{
			foreach( FileInfo fifFileToLoad in fifaFilesToLoad ) {
				if( String.Compare( fifFileToLoad.Extension.ToUpper(), ".XML" ) == 0 ) {
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
			Hashtable Messages = this.Languages[ ProcessingLanguage ] as Hashtable;
			XmlNodeList MessageNodes = ResmapNode.ChildNodes;
			XmlElement MessageElement;
			string ID, Content;
			foreach( XmlNode MessageNode in MessageNodes ) {
				if( MessageNode.Name == "Message" ) {
					MessageElement = MessageNode as XmlElement;
					ID = MessageElement.GetAttribute( "ID" );
					Content = MessageElement.GetAttribute( "Content" );
					if( !Messages.ContainsKey( ID ) ) {
						Messages.Add( ID, Content );
					}
				}
			}
		}

	}
}
