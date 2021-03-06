using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using BasicAttributes;

namespace Syntec_Developer.Controls.PropertyClasses
{
	public class FenubarProperties
	{

		public event EventHandler PropertiesChanged;
		public event EventHandler AlignmentChanged;

		private ContentAlignment m_nAlignment = ContentAlignment.MiddleCenter;
		[CategoryAttribute( "Fenubar" )]
		[DefaultValue( Justification.MiddleCenter )]
		public ContentAlignment Alignment
		{
			get
			{
				return this.m_nAlignment;
			}
			set
			{
				this.m_nAlignment = value;
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
				if( this.AlignmentChanged != null )
					this.AlignmentChanged.Invoke( this, new EventArgs() );
			}
		}

		private bool m_bButton3D = false;
		[CategoryAttribute( "Fenubar" )]
		[DefaultValue( false )]
		public bool Button3D
		{
			get
			{
				return this.m_bButton3D;
			}
			set
			{
				this.m_bButton3D = value;
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
			}
		}

		private int m_nLevel3D = 0;
		[CategoryAttribute( "Fenubar" )]
		[DefaultValue( 0 )]
		public int Level3D
		{
			get
			{
				return this.m_nLevel3D;
			}
			set
			{
				this.m_nLevel3D = value;
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
			}
		}

		private bool m_bNoFunc = false;
		[CategoryAttribute( "Fenubar" )]
		[DefaultValue( false )]
		public bool NoFunc
		{
			get
			{
				return this.m_bNoFunc;
			}
			set
			{
				this.m_bNoFunc = value;
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
			}
		}

		private bool m_bNoLR = false;
		[CategoryAttribute( "Fenubar" )]
		[DefaultValue( false )]
		public bool NoLR
		{
			get
			{
				return this.m_bNoLR;
			}
			set
			{
				this.m_bNoLR = value;
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
			}
		}

		private bool m_bBigLR = false;
		[CategoryAttribute( "Fenubar" )]
		[DefaultValue( false )]
		public bool BigLR
		{
			get
			{
				return this.m_bBigLR;
			}
			set
			{
				this.m_bBigLR = value;
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
			}
		}

		private bool m_bTextOverPic = false;
		[CategoryAttribute( "Fenubar" )]
		[DefaultValue( false )]
		public bool TextOverPic
		{
			get
			{
				return this.m_bTextOverPic;
			}
			set
			{
				this.m_bTextOverPic = value;
				if( this.PropertiesChanged != null )
					this.PropertiesChanged.Invoke( this, new EventArgs() );
			}
		}

		public FenubarProperties(){
		}

	}
}
