using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Console = APIHelper.ConsoleHelper;

namespace APIHelper
{
	public static class Parser
	{
		//first_name* TEXT (description),
		public static APIArgument ParseArgument(string arg)
		{
			string pattern = @"(?<id>\w+)(?<optional>\*?)\s+(?<dbtype>\w+)(?:\s*\((?<description>(?:\w+\s*)+)\))?";
			Match match = Regex.Match(arg, pattern);
			Group id = match.Groups["id"];
			bool optional = match.Groups.ContainsKey("optional");
			Group dbType = match.Groups["dbtype"];
			Group description = match.Groups.ContainsKey("description") ? match.Groups["description"] : null;

			if (description != null)
				return new APIArgument(id.Value, dbType.Value, description.Value, optional);
			else
				return new APIArgument(id.Value, dbType.Value, _Optional: optional);
		}

		public static List<APIArgument> ParseArguments(string args)
		{
			string[] split = Regex.Split(args, "\\s*,\\s*");
			List<APIArgument> api_args = new List<APIArgument>();

			foreach (string s in split)
				api_args.Add(ParseArgument(s));

			return api_args;

		}

		public static Table ParseDependency(string table)
		{
			string pattern = @"(?<table>\w+)\s*(?:as\s+(?<rowname>\w+))?";
			Match match = Regex.Match(table, pattern);

			Group tab = match.Groups["table"];
			Group row = match.Groups.ContainsKey("rowname") ? match.Groups["rowname"] : null;

			if (row != null)
				return new Table(tab.Value, row.Value);
			else
				return new Table(tab.Value);
		}

		public static List<Table> ParseDependencies(string tables)
		{
			string[] split = Regex.Split(tables, "\\s*,\\s*");
			List<Table> tabs = new List<Table>();
			foreach (string s in split)
				tabs.Add(ParseDependency(s));

			return tabs;
		}

		public static Table ParseDependencyDefinition(string definition)
		{
			string pattern = @"(?<name>\w+)\s*\{(?<inner>[^\{\}]+)\}";
			Match match = Regex.Match(definition, pattern);

			Group name = match.Groups["name"];
			Group inner = match.Groups["inner"];

			List<APIArgument> args = ParseArguments(inner.Value);
			List<Column> cols = args.Select(a => new Column(a.Identifier, a.DBType)).ToList();
			Console.Write("Parsed dependency definition {0}", name.Value);
			return new Table(name.Value, cols, cols);
		}

		//	create/datasets { first_name TEXT, last_name TEXT, id* INTEGER } uses users;
		public static APIFunction ParseFunction(string func)
		{
			string pattern = @"(?<path>(?:\w+\.?\/?)+)\s*\{(?<args>[^\}\{]+)\}\s*(?:uses(?<uses>[^']+))";
			Match match = Regex.Match(func, pattern);
			Group path = match.Groups["path"];
			Group args_group = match.Groups["args"];
			Group uses = match.Groups.ContainsKey("uses")? match.Groups["uses"] : null;

			List<APIArgument> args = ParseArguments(args_group.Value);
			List<Table> dependenceies = uses == null ? new List<Table>() : ParseDependencies(uses.Value);
			Console.Write("Parsed function {0}", path.Value);
			return new APIFunction(null, path.Value, args, dependenceies);
		}
	}
}
