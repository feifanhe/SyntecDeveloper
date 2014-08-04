using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Syntec_Developer.Controls.PropertyClasses
{
	public class FenubarProperties
	{
		private int m_nAlignment;
		private bool m_bButton3D;
		private int m_nLevel3D;
		private bool m_bNoFunc;
		private bool m_bNoLR;
		private bool m_bBigLR;
		private bool m_bTextOverPic;

		public event EventHandler PropertiesChanged;

		[CategoryAttribute( "Fenubar" )]
		public int Alignment
		{
			get
			{
				return this.m_nAlignment;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_nAlignment = value;
			}
		}

		[CategoryAttribute( "Fenubar" )]
		public bool Button3D
		{
			get
			{
				return this.m_bButton3D;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_bButton3D = value;
			}
		}

		[CategoryAttribute( "Fenubar" )]
		public int Level3D
		{
			get
			{
				return this.m_nLevel3D;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_nLevel3D = value;
			}
		}

		[CategoryAttribute( "Fenubar" )]
		public bool NoFunc
		{
			get
			{
				return this.m_bNoFunc;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_bNoFunc = value;
			}
		}

		[CategoryAttribute( "Fenubar" )]
		public bool NoLR
		{
			get
			{
				return this.m_bNoLR;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_bNoLR = value;
			}
		}

		[CategoryAttribute( "Fenubar" )]
		public bool BigLR
		{
			get
			{
				return this.m_bBigLR;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_bBigLR = value;
			}
		}

		[CategoryAttribute( "Fenubar" )]
		public bool TextOverPic
		{
			get
			{
				return this.m_bTextOverPic;
			}
			set
			{
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				this.m_bTextOverPic = value;
			}
		}

		public FenubarProperties(){
			this.m_nAlignment = 0;
			this.m_bButton3D = false;
			this.m_nLevel3D = 0;
			this.m_bNoFunc = false;
			this.m_bNoLR = false;
			this.m_bBigLR = false;
			this.m_bTextOverPic = false;
		}

	}
}
