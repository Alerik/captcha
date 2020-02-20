using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public class FunctionParameter
	{
		public string Identifier { get; private set; }
		public string DBType { get; private set; }
		public string Description { get; private set; }

		public string ClientType => API.Instance.ConvertToClient(DBType);
		public string ServerType => API.Instance.ConvertToServer(DBType);

		public bool Optional { get; private set; }

		public FunctionParameter(string _Identifier, string _DBType, string _Description = "No description provided", bool _Optional = false)
		{
			this.Identifier = _Identifier;
			this.DBType = _DBType;
			this.Description = _Description;
			this.Optional = _Optional;
		}
	}

	public class ColumnFunctionParameter : FunctionParameter
	{
		public List<FunctionParameter> Parameters { get; private set; }
		public ColumnFunctionParameter(string _Identifier, List<FunctionParameter> _Parameters) : base(_Identifier, "COLUMN")
		{
			this.Parameters = _Parameters;
		}
	}
}
