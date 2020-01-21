using System;
using System.Collections.Generic;
using System.Text;

namespace APIHelper
{
	public class APIArgument
	{
		public string Identifier { get; private set; }
		public string DBType { get; private set; }
		public string Description { get; private set; }

		public bool Optional { get; private set; }

		public APIArgument(string _Identifier, string _DBType, string _Description = "No description provided", bool _Optional = false)
		{
			this.Identifier = _Identifier;
			this.DBType = _DBType;
			this.Description = _Description;
			this.Optional = _Optional;
		}
	}
}
