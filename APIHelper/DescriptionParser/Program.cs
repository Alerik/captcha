using Antlr4.Runtime;
using System;
using System.Linq;
using Antlr4.StringTemplate;
using System.Collections.Generic;
using System.Diagnostics;

namespace DescriptionParser
{
	public class Program
	{
		static void Main(string[] args)
		{
			//API.Instance = new API();
			//API.Instance.BaseUrl = "www.google.com/";

			//Antlr4.StringTemplate.TemplateGroupFile file = new TemplateGroupFile(@"C:\xampp\htdocs\captcha\APIHelper\DescriptionParser\Templates\angular\angular.stg");
			//APIFunction f = new APIFunction("Concate", HttpMethods.GET, "strings/util", new List<APIArgument>() { new APIArgument("name", "TEXT"), new APIArgument("age", "INTEGER")}, null);
			//APIFunction f1 = new APIFunction("Create", HttpMethods.POST, "strings/util", new List<APIArgument>() { new APIArgument("first_name", "TEXT"), new APIArgument("dog_name", "TEXT") }, null);

			//Template t = file.GetInstanceOf("create_service");
			////t.Add("func", new APIFunction[] { f, f1 } );
			//t.Add("func", f);
			//t.Add("func", f1);

			//t.Add("path", "Create");
			//t.Add("datatype", "Row1");
			//t.Add("datatype", "Row2");
			////TemplateGroup g = file.ImportedGroups[0];
			////g.re
			////Antlr4.StringTemplate.Template t = new Template("<f.Name> at <f.Path> using <f.Method>");
			////t.Add("f", f);
			//Console.ForegroundColor = ConsoleColor.Green;
			//Console.WriteLine(t.Render());
			//Console.ForegroundColor = ConsoleColor.White;
			var full = @"#API
							main:
								ENDPOINT<'http://127.0.0.1/captcha'>;
								SERVERDIR<'C:\xampp\htdocs\captcha\DescriptionParser\DescriptionParser\bin\Debug\netcoreapp3.0\API\Server'>;
								CLIENTDIR<'C:\xampp\htdocs\captcha\DescriptionParser\DescriptionParser\bin\Debug\netcoreapp3.0\API\Client'>;
						#FUNCTION
							main: 
								POST function insert(arg1* TEXT, arg2 INTEGER) uses users as Row1;
								GET function concat(first TEXT, second TEXT, third* TEXT) uses users as Row2; 
						#TABLES
							main:
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
			Process.Start("explorer.exe", API.Instance.ClientDirectory);
			Console.Write("Done!");
		}
	}
}
