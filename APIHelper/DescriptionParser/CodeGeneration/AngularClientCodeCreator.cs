using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Antlr4.StringTemplate;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser.CodeGeneration
{
	public class AngularClientCodeCreator : ClientCodeCreator
	{
		private Dictionary<string, ClientCodeFile> codeFiles = new Dictionary<string, ClientCodeFile>();
		private List<FunctionDefinition> functions = new List<FunctionDefinition>();
		private List<Table> dependencies = new List<Table>();

		public override void AddFunction(FunctionDefinition function)
		{
			functions.Add(function);
		}

		public override void AddDependency(Table table)
		{
			dependencies.Add(table);
		}

		public override void GenerateAll()
		{
			Console.Head("Generating client code");
			//Figure out how to split the service files here
			var byPath = functions.GroupBy(f => f.Path);

			TemplateGroupFile angularTemplate = new TemplateGroupFile(@"C:\xampp\htdocs\captcha\APIHelper\DescriptionParser\Templates\angular\angular.stg");

			foreach(var funcs in byPath)
			{
				Template serviceTemplate = angularTemplate.GetInstanceOf("create_service");
				string upped = Formatter.CapFirst(funcs.Key);
				serviceTemplate.Add("path", Formatter.CapFirst(funcs.Key));
				serviceTemplate.Add("lowerpath", Formatter.LowerFirst(funcs.Key));

				//Generate functions
				foreach(FunctionDefinition func in funcs)
				{
					serviceTemplate.Add("func", func);
				}

				//Generate datatypes for tables
				foreach(Table table in dependencies)
				{
					serviceTemplate.Add("datatype", table.RowName);
				}
				
				ClientCodeFile serviceFile = CodeFile.CreateClientFile(Path.Combine("services", Formatter.LowerFirst(funcs.Key) + ".services.ts"));
				serviceFile.Write(serviceTemplate.Render());
			}

			Template datatypeTemplate = angularTemplate.GetInstanceOf("create_datatypes");

			dependencies.Add(new Table("People", new List<APIColumn>() { new APIColumn("firstname", "text"), new APIColumn("lastname", "text") }, "Person"));

			foreach(Table table in dependencies)
			{
				datatypeTemplate.Add("datatype", table);
			}

			ClientCodeFile tableFile = CodeFile.CreateClientFile(Path.Combine("datatypes", "generated.ts"));
			tableFile.Write(datatypeTemplate.Render());

			Console.Write("Done generating client code");
			Console.End();
		}
	}
}
