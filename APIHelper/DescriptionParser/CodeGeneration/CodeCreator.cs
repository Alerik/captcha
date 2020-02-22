using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser.CodeGeneration
{
	public abstract class CodeCreator
	{
		public const string AGGREGATE_FILE = "aggregate_file";
		public const string CLUSTER_FILE = "cluster_file";
		public const string SOLITARY_FILE = "solitary_file";

		public const string FUNCTION = "func";

		protected CodeDirectory directory;
		protected TemplateGroupFile templatefile;
		protected string extension;

		public CodeCreator(CodeDirectory _directory, TemplateGroupFile _templatefile, string _extension)
		{
			this.directory = _directory;
			this.templatefile = _templatefile;
			this.extension = _extension;
		}
		
		public void GenerateAll(List<FunctionDefinition> functionDefinitions)
		{
			PreGenerate();

			foreach (FunctionDefinition function in functionDefinitions)
				GenerateFunction(function);

			PostGenerate();
		}

		private void Save()
		{
			directory.CloseAll();
		}

		//This will do stuff to copy files such as database.php or globals.ts or what have you
		//Or stuff like settings.py
		//Pretty much, anything your generated files might want to reference
		//public void GenerateDependencies();
		protected abstract void GenerateFunction(FunctionDefinition function);

		/// <summary>
		/// Called before generation happens
		/// </summary>
		protected virtual void PreGenerate()
		{

		}
		/// <summary>
		/// Called after generation happens
		/// </summary>
		protected virtual void PostGenerate()
		{
			Save();
		}
	}

	//Each function in same file
	//i.e. Flask
	public class AggregateCodeCreator : CodeCreator
	{
		private Template template;
		private string name;

		public AggregateCodeCreator(string _name, CodeDirectory _directory, TemplateGroupFile _templatefile, string _extension) 
			: base(_directory, _templatefile, _extension)
		{
			this.name = _name;
			this.template = _templatefile.GetInstanceOf(AGGREGATE_FILE);
		}

		protected override void GenerateFunction(FunctionDefinition function)
		{
			this.template.Add(FUNCTION, function);
		}

		protected override void PostGenerate()
		{
			CodeFile aggregateFile = directory.CreateFile(name, extension);
			aggregateFile.Write(this.template.Render());
			aggregateFile.Close();
			base.PostGenerate();
		}
	}

	//Each function has own file, but sorted into directories
	//i.e. PHP
	public class ClusteredCodeCreator : CodeCreator
	{
		public ClusteredCodeCreator(CodeDirectory _directory, TemplateGroupFile _templatefile, string _extension) 
			: base(_directory, _templatefile, _extension)
		{
		}

		protected override void GenerateFunction(FunctionDefinition function)
		{
			Template fileTemplate = templatefile.GetInstanceOf(CLUSTER_FILE);
			fileTemplate.Add(FUNCTION, function);

			CodeFile file = directory.CreateFile(function.PathWithName, extension);
			file.Write(fileTemplate.Render());
		}
	}

	//Each function in same file, but sorted into files
	//i.e. Angular
	public class SolitaryCodeCreator : CodeCreator
	{
		public SolitaryCodeCreator(CodeDirectory _directory, TemplateGroupFile _templatefile, string _extension) 
			: base(_directory, _templatefile, _extension)
		{
		}

		protected override void GenerateFunction(FunctionDefinition function)
		{
			TemplateCodeFile template = directory.CreateTemplateFile(function.Path, extension, templatefile.GetInstanceOf(SOLITARY_FILE), 
				new Dictionary<string, object>() { 
				{"path", Formatter.ToPascalCase(function.Path) }
			});
			template.Add(FUNCTION, function);		
		}
	}
}
