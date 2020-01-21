using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Console = APIHelper.ConsoleHelper;

namespace APIHelper
{
	public class FileParser
	{
		private const string DIRECTIVE_API = "API";
		private const string DIRECTIVE_FUNCTION = "FUNCTION";
		private const string DIRECTIVE_TABLE = "TABLE";
		private Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>();
		private string path;

		public FileParser(string _path)
		{
			this.path = _path;
			StreamReader reader = new StreamReader(path);
			string all = reader.ReadToEnd();
			reader.Close();

			string[] lines = all.Split(new[] {'\r', '\n' });
			commands = SplitDirectives(lines);
			AggregateDictionary(commands);
		}

		public void Parse()
		{
			Console.Head("Starting to parse {0}", path);
			ParseAPI();
			ParseFunction();
			ParseDependencies();
			Console.End();
		}

		private void ParseAPI()
		{
			Console.Head("Parsing API directive");
			if (commands.ContainsKey(DIRECTIVE_API))
			{
				string line = commands[DIRECTIVE_API][0];
				string[] split = Regex.Split(line, @"\s+");
				API api = new API(split[0], split[1], split[2]);
			}
			else
			{
				Console.Error("No API directive found");
			}
			Console.End();
		}
		private void ParseFunction()
		{
			Console.Head("Parsing functions");
			if (commands.ContainsKey(DIRECTIVE_FUNCTION))
			{
				foreach(string line in commands[DIRECTIVE_FUNCTION])
				{
					Parser.ParseFunction(line);
				}
			}
			else
			{
				Console.Warn("No FUNCTION directive found");
			}
			Console.End();
		}
		private void ParseDependencies()
		{
			Console.Head("Parsing dependencies");
			if (commands.ContainsKey(DIRECTIVE_TABLE))
			{
				foreach (string line in commands[DIRECTIVE_TABLE])
				{
					Parser.ParseDependencyDefinition(line);
				}
			}
			else
			{
				Console.Warn("No TABLE directive found");
			}
			Console.End();
		}


		private Dictionary<string, List<string>> SplitDirectives(string[] rawlines)
		{
			Dictionary<string, List<String>> lines = new Dictionary<string, List<string>>();
			string directive = "";

			foreach (string line in rawlines)
			{
				if (line.StartsWith('#'))
				{
					directive = line.Substring(1);
					if (!lines.ContainsKey(directive))
					{
						lines.Add(directive, new List<string>());
					}
				}
				else
				{
					if (directive != "" && line != "")
						lines[directive].Add(line);

				}
		}

			return lines;
		}

		private List<string> AggregateLines(List<string> lines)
		{
			List<string> nLines = new List<string>();
			string current = "";


			foreach(string line in lines)
			{
				if (line.Contains(';'))
				{
					string[] split = line.Split(';');
					nLines.Add(current + split[0]);
					current = "";

					for(int i = 1; i < split.Length - 1; i++)
					{
						nLines.Add(split[i]);
					}

					if (split.Length > 1 && split.Last() != "" && line.EndsWith(';'))
					{
						nLines.Add(split[split.Length - 1]);
					}
				}

				current += line;
			}
			return nLines;
		}

		private void AggregateDictionary(Dictionary<string, List<string>> dict)
		{
			for (int i = 0; i < dict.Keys.Count; i++)
			{
				string key = dict.Keys.ElementAt(i);
				dict[key] = AggregateLines(dict[key]);
			}
		}
	}
}
