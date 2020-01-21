using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace APIHelper
{
	public class PHPServerCodeCreator : ServerCodeCreator
	{
		private const string ARG_IDENTIFIERS = "ARG_IDENTIFIERS";
		private const string ARG_DEFINITIONS = "ARG_DEFINITIONS";
		private const string QUERY_COLUMNS = "QUERY_COLUMNS";
		private const string TABLE_NAME = "TABLE_NAME";

		public override void CreateCode(Table dependency, string functionName, List<Column> args, List<Column> internalColumns, List<Column> exposedColumns)
		{
			ServerCodeFile file = new ServerCodeFile($"{functionName}.php", CreateCode(dependency.Name, args, internalColumns, exposedColumns));
			file.Close();
		}

		private string CreateCode(string tableName, List<Column> args, List<Column> internalColumns, List<Column> exposedColumns)
		{
			Template phpTemplate = new Template(Template.PHP);

			phpTemplate.Replace(ARG_IDENTIFIERS, GenerateAPIIdentifiers(args));
			phpTemplate.Replace(ARG_DEFINITIONS, GenerateAPIDefinitions(args));
			phpTemplate.Replace(QUERY_COLUMNS, GenerateQueryColumns(exposedColumns.Concat(exposedColumns).ToList()));
			phpTemplate.Replace(TABLE_NAME, tableName);

			return phpTemplate.Text;

		}

		private string GenerateAPIIdentifiers(List<Column> args)
		{
			return string.Join(", ", args.Select(a => a.Identifier));
		}
		private string GenerateAPIDefinitions(List<Column> args)
		{
			return string.Join('\n', args.Select(a => GenerateAPIDefinition(a)));
		}
		private string GenerateAPIDefinition(Column arg)
		{
			return $"${arg.Identifier} =  $data['{arg.Identifier}'];";
		}

		private string GenerateQueryColumns(List<Column> cols)
		{
			return string.Join(", ", cols.Select(c => c.Identifier));
		}
	}
}
