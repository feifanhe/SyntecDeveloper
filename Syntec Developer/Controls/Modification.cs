using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec_Developer.Controls
{
	public class Modification
	{
		public object ModifiedTarget;
		public object OldValue;
		public object NewValue;

		public Modification( object ModifiedTarget, object OldValue, object NewValue )
		{
			this.ModifiedTarget = ModifiedTarget;
			this.OldValue = OldValue;
			this.NewValue = NewValue;
		}
	}
}
