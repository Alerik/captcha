using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace APIHelper
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

		public override void GenerateCode(Table dependency, string functionName, string path, List<Column> args, List<Column> exposed)
		{
			string name = path.Split('/').Last().Split('.').First();
			ClientCodeFile serviceFile = new ClientCodeFile($"services/{name}.service.ts", GenerateCall(dependency.RowName, path, args, exposed));
			ClientCodeFile dataTypeFile = new ClientCodeFile($"datatypes/{dependency.RowName}.ts", GenerateDataType(dependency.RowName, exposed));
			serviceFile.Close();
			dataTypeFile.Close();
		}

		public override string GenerateCall(string typeName, string path, List<Column> args, List<Column> exposedColumns)
		{
			Template template = new Template(Template.ANGULAR);
			template.Replace(DATATYPE, typeName);
			template.Replace(SERVICE_NAME, typeName);
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
