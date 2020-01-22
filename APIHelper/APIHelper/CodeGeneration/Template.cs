using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace APIHelper.CodeGeneration
{
	public class Template
	{
		public const string PHP = "templates/template.php";
		public const string ANGULAR = "templates/template.service.ts";
		public const string ANGULAR_DT = "templates/template.ts";

		private string path;
		public string Text { get; set; }

		public Template(string _path)
		{
			this.path = _path;
			StreamReader reader = new StreamReader(path);
			Text = reader.ReadToEnd();
			reader.Close();
		}

		public void Replace(string target, string replacement)
		{
			this.Text = this.Text.Replace(Escape(target), replacement);
		}
		public void Append(string target, string appendage)
		{
			this.Text = this.Text.Substring(0, this.Text.IndexOf(target) + target.Length) + "\n" + appendage + "\n" + this.Text.Substring(this.Text.IndexOf(target) + target.Length);
		}

		private string Escape(string str)
		{
			return "--" + str + "--";
		}
	}
}
