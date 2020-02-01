using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Antlr4.StringTemplate;

namespace DescriptionParser.CodeGeneration
{
	public class AngularClientCodeCreator : ClientCodeCreator
	{
		private Dictionary<string, ClientCodeFile> codeFiles = new Dictionary<string, ClientCodeFile>();
		private List<APIFunction> functions = new List<APIFunction>();
		private List<Table> dependencies = new List<Table>();

		public override void AddFunction(APIFunction function)
		{
			functions.Add(function);
		}

		public override void AddDependency(Table table)
		{
			dependencies.Add(table);
		}

		public override void GenerateAll()
		{
			//Figure out how to split the service files here
			var byPath = functions.GroupBy(f => f.Path);

			TemplateGroupFile angularTemplate = new TemplateGroupFile(@"C:\xampp\htdocs\captcha\APIHelper\DescriptionParser\Templates\angular\angular.stg");

			foreach(var funcs in byPath)
			{
				Template serviceTemplate = angularTemplate.GetInstanceOf("create_service");
				string upped = Formatter.CapFirst(funcs.Key);
				serviceTemplate.Add("path", Formatter.CapFirst(funcs.Key));
				serviceTemplate.Add("lowerpath", Formatter.LowerFirst(funcs.Key));

				foreach(APIFunction func in funcs)
				{
					serviceTemplate.Add("func", func);
				}
				foreach(Table table in dependencies)
				{
					serviceTemplate.Add("datatype", table.RowName);
				}
				serviceTemplate.Add("datatype", "Row1");
				serviceTemplate.Add("datatype", "Row2");

				ClientCodeFile serviceFile = CodeFile.CreateClientFile(Path.Combine("services", Formatter.LowerFirst(funcs.Key) + ".services.ts"));
				serviceFile.Write(serviceTemplate.Render());
			}

			Template datatypeTemplate = angularTemplate.GetInstanceOf("create_datatypes");

			dependencies.Add(new Table("People", new List<Column>() { new Column("firstname", "text"), new Column("lastname", "text") }, "Person"));

			foreach(Table table in dependencies)
			{
				datatypeTemplate.Add("datatype", table);
			}

			ClientCodeFile tableFile = CodeFile.CreateClientFile(Path.Combine("datatypes", "generated.ts"));
			tableFile.Write(datatypeTemplate.Render());
		}
	}
}
