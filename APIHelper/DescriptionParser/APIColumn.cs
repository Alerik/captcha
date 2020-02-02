using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public class APIColumn
	{

		public string Identifier { get; private set; }
		public string DBType { get; private set; }

		public string ClientType => API.Instance.ConvertToClient(DBType);
		public string ServerType => API.Instance.ConvertToServer(DBType);

		public APIColumn(string _Identifier, string _DBType)
		{
			this.Identifier = _Identifier;
			this.DBType = _DBType;
		}

		public override bool Equals(object obj)
		{
			if(obj is APIColumn col)
				return col.Identifier.Equals(this.Identifier) && col.DBType.Equals(this.DBType);
			return false;
		}

		public override int GetHashCode()
		{
			return Identifier.GetHashCode() + DBType.GetHashCode();
		}
	}
}
