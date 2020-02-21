using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser.CodeGeneration
{

	/// <summary>
	/// A class used by CodeCreators to easily create files
	/// </summary>
	public class CodeFile
	{
		protected StreamWriter writer;

		internal CodeFile(StreamWriter _writer)
		{
			this.writer = _writer;
		}

		public void Write(string content)
		{
			writer.Write(content);
		}

		public virtual void Close()
		{
			writer.Close();
		}
	}

	public class TemplateCodeFile : CodeFile
	{
		protected Template template;

		internal TemplateCodeFile(StreamWriter _writer, Template _template) : base(_writer)
		{
			this.template = _template;
		}

		public void Add(string name, object value)
		{
			this.template.Add(name, value);
		}

		public override void Close()
		{
			Write(template.Render());
			base.Close();
		}
	}
}
