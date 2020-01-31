using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace DescriptionParser.CodeGeneration
{
	public class AngularClientCodeCreator : ClientCodeCreator
	{
		private Dictionary<string, string> typeConverter = new Dictionary<string, string>
		{
			{"integer", "number" },
			{"real", "number" },
			{"text", "string"},
			{"boolean", "boolean"},
			{ "uuid", "string"}
		};

		private Dictionary<string, ClientCodeFile> codeFiles = new Dictionary<string, ClientCodeFile>();

		private const string DEF_TYPE = "any";

		private const string DATATYPE = "DATATYPE";
		private const string SERVICE_NAME = "SERVICE_NAME";
		private const string ARG_IDENTIFIERS = "ARG_IDENTIFIERS";
		private const string ARG_DEFINITIONS = "ARG_DEFINITIONS";
		private const string MEMBER_DEFINITIONS = "MEMBER_DEFINITIONS";
		private const string URL = "URL";
		private const string FUNCTION_NAME = "FUNCTION_NAME";

		public override void GenerateFunction(APIFunction function)
		{
			ClientCodeFile file = null;
			string fullPath = Path.Combine("services", $"{function.Path.Replace('/','.').Replace('\\','.')}.services.ts");		
			if (codeFiles.ContainsKey(fullPath))
			{
				file = codeFiles[fullPath];
			}
			else
			{
				file = CodeFile.CreateClientFile(fullPath) as ClientCodeFile;
			}
		}

		public override void GenerateDependency(Table table)
		{
			//ClientCodeFile file = null;
			//string fullPath = Path.Combine("datatypes", $"{table.Path.Replace('/', '.').Replace('\\', '.')}.services.ts");
			//if (codeFiles.ContainsKey(fullPath))
			//{
			//	file = codeFiles[fullPath];
			//}
			//else
			//{
			//	file = CodeFile.CreateClientFile(fullPath) as ClientCodeFile;
			//}
			//throw new NotImplementedException();
		}

		public void GenerateCode(List<Table> dependencies, string functionName, string parentPath, string path, List<Column> args, List<Column> exposed)
		{
			string name = path.Split('/').Last().Split('.').First();
			
		}

		public string GenerateCall(string typeName, string functionName, string path, List<Column> args, List<Column> exposedColumns)
		{
			Template template = new Template(Template.ANGULAR_FUNCTION);
			template.Replace(DATATYPE, typeName);
			//template.Replace(SERVICE_NAME, typeName);
			template.Replace(FUNCTION_NAME, functionName);
			template.Replace(ARG_IDENTIFIERS, GenerateArgIdentifiers(args));
			template.Replace(ARG_DEFINITIONS, GenerateArgDefinitions(args));
			template.Replace(URL, $"'{API.Instance.BaseUrl.Trim('/') + "/" + path.Trim('/')}'");

			return template.Text;
		}

		public string GenerateDataType(string typeName, List<Column> exposedColumns)
		{
			Template template = new Template(Template.ANGULAR_DT);
			template.Replace(DATATYPE, typeName);
			template.Replace(MEMBER_DEFINITIONS, GenerateMemberDefinitions(exposedColumns));

			return template.Text;
		}

		private string GenerateMemberDefinitions(List<Column> exposed)
		{
			return string.Join(",\n", exposed.Select(a => $"{a.Identifier} : {Convert(a.DBType)}"));
		}

		private string Convert(string dbType)
		{
			try
			{
				return typeConverter[dbType.ToLower()];
			}
			catch (KeyNotFoundException e)
			{
				throw new NoValidConversionException(dbType.ToLower());
			}
		}

		private string GenerateArgIdentifiers(List<Column> args)
		{
			return string.Join(", ", args.Select(a => $"{a.Identifier} : {Convert(a.DBType)}"));
		}

		private string GenerateArgDefinitions(List<Column> args)
		{
			return string.Join(",\n", args.Select(a => $"\"{a.Identifier}\" : {a.Identifier}"));
		}
	}
}
