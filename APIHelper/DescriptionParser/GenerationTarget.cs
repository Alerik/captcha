using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public class GenerationTarget
	{
		public TargetDescription Description { get; private set; }
		//Access stuff for conversions/templates and what not

		public GenerationTarget(TargetDescription _Description)
		{
			this.Description = _Description;
		}

		public TemplateGroupFile GetTemplateFile()
		{
			string templatePath = System.IO.Path.Combine(Description.Directory, Description.Template);
			TemplateGroupFile templateFile = new TemplateGroupFile(templatePath);
			return templateFile;
		}
	}
}