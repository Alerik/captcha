using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace APIHelper.CodeGeneration
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

		private const string DATATYPE = "DATATYPE";
		private const string SERVICE_NAME = "SERVICE_NAME";
		private const string ARG_IDENTIFIERS = "ARG_IDENTIFIERS";
		private const string ARG_DEFINITIONS = "ARG_DEFINITIONS";
		private const string MEMBER_DEFINITIONS = "MEMBER_DEFINITIONS";
		private const string URL = "URL";
		private const string FUNCTION_NAME = "FUNCTION_NAME";

		public override void GenerateCode(Table dependency, string functionName, string parentPath, string path, List<Column> args, List<Column> exposed)
		{
			string name = path.Split('/').Last().Split('.').First();
			ClientCodeFile serviceFile = ClientCodeFile.CreateFile($"services/{parentPath}.service.ts");
			serviceFile.AddFunction(GenerateCall(dependency.RowName, functionName, path, args, exposed));
			ClientCodeFile dataTypeFile = ClientCodeFile.CreateFile($"datatypes/{dependency.RowName}.ts");
			dataTypeFile.Write(GenerateDataType(dependency.RowName, exposed));
		}

		public override string GenerateCall(string typeName, string functionName, string path, List<Column> args, List<Column> exposedColumns)
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

		public override string GenerateDataType(string typeName, List<Column> exposedColumns)
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
