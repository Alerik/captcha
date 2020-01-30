using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Console = DescriptionParser.ConsoleHelper;


namespace DescriptionParser.CodeGeneration
{
	public class ServerCodeFile : CodeFile
	{
		internal ServerCodeFile(string _Path) : base(_Path)
		{
			this.Path = System.IO.Path.Combine(API.Instance.ServerDirectory, _Path);
			string dir = System.IO.Path.GetDirectoryName(this.Path);
			Directory.CreateDirectory(dir);
			API.Instance.AddFile(this.Path);
			writer = new StreamWriter(Path);
		}
	}
}
