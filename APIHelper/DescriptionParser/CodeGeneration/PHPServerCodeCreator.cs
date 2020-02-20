using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Console = DescriptionParser.ConsoleHelper;
using Antlr4.StringTemplate;
using System.IO;

namespace DescriptionParser.CodeGeneration
{
	public class PHPServerCodeCreator : ServerCodeCreator
	{
		private List<FunctionDefinition> funcs = new List<FunctionDefinition>();
		public override void AddFunction(FunctionDefinition function)
		{
			funcs.Add(function);
		}

		public override void AddDependency(Table dependency)
		{
			//throw new NotImplementedException();
		}

		public override void GenerateAll()
		{
			Console.Head("Generating server code");
			TemplateGroupFile phpTemplate = new TemplateGroupFile(@"C:\xampp\htdocs\captcha\APIHelper\DescriptionParser\Templates\php\php.stg");

			foreach(FunctionDefinition func in funcs)
			{
				Template functionTemplate = phpTemplate.GetInstanceOf("create_function");
				functionTemplate.Add("func", func);
				ServerCodeFile file = CodeFile.CreateServerFile(Path.Combine(func.Path, func.Name + ".php"));
				file.Write(functionTemplate.Render());
				System.Console.ForegroundColor = ConsoleColor.Green;
				System.Console.Write(functionTemplate.Render());
				System.Console.ForegroundColor = ConsoleColor.White;
			}

			Console.End();
			//throw new NotImplementedException();
		}
	}
}
