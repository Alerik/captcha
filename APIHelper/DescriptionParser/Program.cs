using Antlr4.Runtime;
using System;
using System.Linq;

namespace DescriptionParser
{
	public class Program
	{
		static void Main(string[] args)
		{
			var full = @"#API
							ENDPOINT<'http://127.0.0.1/captcha'>;
							SERVERDIR<'C:\xampp\htdocs\captcha\DescriptionParser\DescriptionParser\bin\Debug\netcoreapp3.0\API\Server'>;
							CLIENTDIR<'C:\xampp\htdocs\captcha\DescriptionParser\DescriptionParser\bin\Debug\netcoreapp3.0\API\Client'>;
						#FUNCTION
							main: 
								POST function insert(arg1* TEXT, arg2 INTEGER) uses users as Row1;
								GET function concat(first TEXT, second TEXT, third* TEXT) uses users as Row2; 
						#TABLES
								table users(
									username TEXT,
									id UUID,
									last_login TIMESTAMP);";
			var str = new AntlrInputStream(full);
			var lexer = new DescriptLexer(str);
			var tokens = new CommonTokenStream(lexer);
			var parser = new DescriptParser(tokens);
			var listener = new ErrorListener<IToken>();
			parser.AddErrorListener(listener);

			var tree = parser.file();
			if (listener.had_error)
			{
				System.Console.WriteLine("error in parse.");
			}
			else
			{
				System.Console.WriteLine("parse completed.");
				System.Console.WriteLine(tokens.OutputTokens());
				System.Console.WriteLine(tree.OutputTree(tokens));
			}
			API.Instance = new API();
			var visitor = new CalculatorVisitor();
			visitor.Visit(tree);

			if (args.Length > 0)
			{
				if (args[0] == "clean")
				{
					API api = new API("", "", "");
					api.Clean();
					api.SaveIndex();
					return;
				}
			}
			API.Instance.GenerateAll();
			API.Instance.SaveIndex();
			Console.Write("Done!");
		}
	}
}
