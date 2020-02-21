using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Antlr4.StringTemplate;
using Newtonsoft.Json;
using System.Linq;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser.CodeGeneration
{
	public class CodeGenerator
	{
		public const string BASE_URL = "ENDPOINT";
		public const string CLIENTDIR = "CLIENTDIR";
		public const string SERVERDIR = "SERVERDIR";

		public const string CLIENT_TARGET = "CLIENT_TARGET";
		public const string SERVER_TARGET = "SERVER_TARGET";
		public const string DB_TARGET = "DB_TARGET";

		private Environment environment;

		public CodeGenerator(Environment _environment)
		{
			this.environment = _environment;
		}

		public void Generate()
		{
			Console.Head("Generating code");

			string clientTarget = environment.Require(CLIENT_TARGET);
			string serverTarget = environment.Require(SERVER_TARGET);

			AngularClientCodeCreator clientCreator = new AngularClientCodeCreator();
			PHPServerCodeCreator serverCreator = new PHPServerCodeCreator();

			//foreach (FunctionDefinition function in Functions)
			//{
			//	clientCreator.AddFunction(function);
			//	serverCreator.AddFunction(function);
			//}

			//foreach (TableDefinition table in Dependencies)
			//{
			//	clientCreator.AddDependency(table);
			//	serverCreator.AddDependency(table);

			//}

			clientCreator.GenerateAll();
			serverCreator.GenerateAll();


			//ClientCodeFile.CloseAll();
			//ServerCodeFile.CloseAll();

			Console.Write("Done generating code");
			Console.End();
		}

		public void LoadTargets()
		{
			string targetDir = Directory.GetDirectories(Directory.GetCurrentDirectory()).Where(d => Path.GetDirectoryName(d).ToLower() == "target").FirstOrDefault();
			string[] targetDirs = Directory.GetDirectories(targetDir);

			List<TargetDescription> targets = new List<TargetDescription>();

			foreach (string target in targetDirs)
			{
				string targetDesc = Directory.GetFiles(target).Where(d => Path.GetExtension(d) == ".desc").FirstOrDefault();
				targets.Add(LoadTarget(target));
			}

			Console.Write("Discovered {0} targets", targets.Count);
		}

		public TargetDescription LoadTarget(string path)
		{
			StreamReader reader = new StreamReader(path);
			string json = reader.ReadToEnd();
			reader.Close();

			return JsonConvert.DeserializeObject<TargetDescription>(json);
		}

		public CodeCreator CreateCodeCreator()
		{
			return null;
		}


		public CodeCreator CreateCodeCreator(string path)
		{
			TargetDescription target = LoadTarget(path);
			string targetDirectory = (target.TargetType == TargetDescription.TargetTypes.Client ? environment[CLIENTDIR] : environment[SERVERDIR]) as string;

			CodeDirectory codeDirectory = new CodeDirectory(targetDirectory);

			string templatePath = System.IO.Path.Combine(path, target.Template);
			TemplateGroupFile templateFile = new TemplateGroupFile(templatePath);

			switch (target.FileStructure)
			{
				case TargetDescription.FileStructures.Aggregate:
					return new AggregateCodeCreator(target.DefaultName, codeDirectory, templateFile, target.Extension);
				case TargetDescription.FileStructures.Solitary:
					return new SolitaryCodeCreator(codeDirectory, templateFile, target.Extension);
				default:
					return new ClusteredCodeCreator(codeDirectory, templateFile, target.Extension);
			}
		}
	}
}
