using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Console = DescriptionParser.ConsoleHelper;
using DescriptionParser.CodeGeneration;

namespace DescriptionParser
{
	public enum HttpMethods
	{
		PUT,
		POST,
		GET
	}
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

	public class FunctionDefinition
	{
		public string Name { get; private set; }
		public string NameCamel => Formatter.LowerFirst(Name);
		public string PathWithName => System.IO.Path.Combine(Path, Name);
		public string Path { get; private set; }
		public string FullPath => API.Instance.BaseUrl + Path + '/' +  Name;

		public string Return = "string";//{ get; private set; }

		public HttpMethods Method { get; private set; }

		public bool IsGet => Method == HttpMethods.GET;
		public bool IsPut => Method == HttpMethods.PUT;
		public bool IsPost => Method == HttpMethods.POST;

		public List<FunctionParameter> Parameters = new List<FunctionParameter>();
		public List<SuperFunctionCall> SuperFunctions = new List<SuperFunctionCall>();
		public List<TableDefinition> Dependencies = new List<TableDefinition>();

		internal FunctionDefinition(string _Name, HttpMethods _Method, string _Path, List<FunctionParameter> _Arguments, List<SuperFunctionCall> superFunctions, List<TableDefinition> _Dependencies)
		{
			this.Name = _Name;
			this.Path = _Path;
			this.Method = _Method;

			this.Parameters = _Arguments ?? new List<FunctionParameter>();
			this.SuperFunctions = superFunctions;
			this.Dependencies = _Dependencies ?? new List<TableDefinition>();
			Console.Write("Got function {0}", Name);
			//API.Instance.Functions.Add(this);
		}
	}
}
