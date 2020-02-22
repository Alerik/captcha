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
	public class NoDescriptionFileException : Exception
	{
		public NoDescriptionFileException(string path) : base($"No description files were found in {path}")
		{

		}
	}
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

			CodeCreator clientCreator = CreateCodeCreator(clientTarget);
			CodeCreator serverCreator = CreateCodeCreator(serverTarget);

			clientCreator.GenerateAll(environment.Functions);
			serverCreator.GenerateAll(environment.Functions);


			Console.Write("Done generating code");
			Console.End();
		}

		public void LoadTargets()
		{
			string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());
			string targetDir = dirs.Where(d => Path.GetFileName(d).ToLower() == "targets").FirstOrDefault();
			string[] targetDirs = Directory.GetDirectories(targetDir);


			foreach (string target in targetDirs)
			{
				string targetDesc = Directory.GetFiles(target).Where(d => Path.GetExtension(d) == ".desc").FirstOrDefault();
				environment.RegisterEnvironmentTarget(Path.GetFileName(target),LoadTargetFolder(target));
			}

//			Console.Write("Discovered {0} targets", targets.Count);
		}
		private TargetDescription LoadTargetFolder(string path)
		{
			string[] descriptionFiles = Directory.GetFiles(path, "*.desc");

			if (descriptionFiles.Length == 0)
				throw new NoDescriptionFileException(path);
			//if (descriptionFiles.Length > 1)
			//  Console.Write("Found several description files. Using the first one.");
			string targetDescriptionPath = descriptionFiles[0];
			StreamReader reader = new StreamReader(targetDescriptionPath);
			string json = reader.ReadToEnd();
			reader.Close();

			TargetDescription description = JsonConvert.DeserializeObject<TargetDescription>(json);
			description.Directory = path;
			return description;
		}

		public CodeCreator CreateCodeCreator(string targetKey)
		{
			GenerationTarget target = environment.GetGenerationTarget(targetKey);//LoadTargetFolder(path);
			TargetDescription description = target.Description;
			string targetDirectory = (description.TargetType == TargetDescription.TargetTypes.Client ? environment[CLIENTDIR] : environment[SERVERDIR]) as string;

			CodeDirectory codeDirectory = new CodeDirectory(targetDirectory);

			//string templatePath = System.IO.Path.Combine(path, target.Template);
			TemplateGroupFile templateFile = target.GetTemplateFile();// new TemplateGroupFile(templatePath);

			switch (description.FileStructure)
			{
				case TargetDescription.FileStructures.Aggregate:
					return new AggregateCodeCreator(description.DefaultName, codeDirectory, templateFile, description.Extension);
				case TargetDescription.FileStructures.Solitary:
					return new SolitaryCodeCreator(codeDirectory, templateFile, description.Extension);
				default:
					return new ClusteredCodeCreator(codeDirectory, templateFile, description.Extension);
			}
		}
	}
}