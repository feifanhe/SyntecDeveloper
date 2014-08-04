using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Syntec_Developer.Controls
{
	public class IniFile
	{
		[DllImport( "kernel32" )]
		private static extern long WritePrivateProfileString( string section,
		   string key, string val, string filePath );
		[DllImport( "kernel32" )]
		private static extern int GetPrivateProfileString( string section,
			  string key, string def, StringBuilder retVal,
		   int size, string filePath );

		const int READ_STRING_SIZE = 1023;

		private string fileName;

		public IniFile( string FileName )
		{
			this.fileName = Path.GetFullPath( FileName );
		}

		public void Write( string Section, string Key, string Value )
		{
			WritePrivateProfileString( Section, Key, Value, this.fileName );
		}

		public string Read( string Section, string Key )
		{
			StringBuilder Value = new StringBuilder( READ_STRING_SIZE );
			GetPrivateProfileString( Section, Key, string.Empty, Value, READ_STRING_SIZE, this.fileName );
			return Value.ToString();
		}
	}
}
