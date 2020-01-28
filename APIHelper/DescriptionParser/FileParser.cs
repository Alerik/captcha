using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Console = DescriptionParser.ConsoleHelper;
using Antlr4.Runtime;

namespace DescriptionParser
{
	public class FileParser
	{
		private const string DIRECTIVE_API = "API";
		private const string DIRECTIVE_FUNCTION = "FUNCTION";
		private const string DIRECTIVE_TABLE = "TABLE";
		private Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>();
		private string path;
		private string all = "";

		public FileParser(string _path)
		{
			this.path = _path;
			StreamReader reader = new StreamReader(path);
			all = reader.ReadToEnd();
			reader.Close();	
		}

		public void Parse()
		{
			Console.Head("Starting to parse {0}", path);
			AntlrInputStream inputStream = new AntlrInputStream(all);
			DescriptLexer lexer = new DescriptLexer(inputStream);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			DescriptParser parser = new DescriptParser(tokens);
			ErrorListener<IToken> errorListener = new ErrorListener<IToken>();
			parser.AddErrorListener(errorListener);
			DescriptParser.FileContext root = parser.file();

			if (errorListener.had_error)
			{
				Console.Write("Error parsing file");
			}
			else
			{
				Console.Write("Done parsing file");
			}
			API.Instance = new API();

			CalculatorVisitor visitor = new CalculatorVisitor();
			visitor.Visit(root);

			Console.End();
		}
	}
}
