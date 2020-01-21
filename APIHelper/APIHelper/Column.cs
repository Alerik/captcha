using System;
using System.Collections.Generic;
using System.Text;

namespace APIHelper
{
	public class Column
	{
		public string Identifier { get; private set; }
		public string DBType { get; private set; }

		public Column(string _Identifier, string _DBType)
		{
			this.Identifier = _Identifier;
			this.DBType = _DBType;
		}

		public override bool Equals(object obj)
		{
			if(obj is Column col)
				return col.Identifier.Equals(this.Identifier) && col.DBType.Equals(this.DBType);
			return false;
		}

		public override int GetHashCode()
		{
			return Identifier.GetHashCode() + DBType.GetHashCode();
		}
	}
}
