using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Console = DescriptionParser.ConsoleHelper;


namespace DescriptionParser.CodeGeneration
{
	public class ServerCodeFile
	{
		public string Path { get; private set; }
		private StreamWriter writer;

		public ServerCodeFile(string _Path, string contents)
		{
			this.Path = System.IO.Path.Combine(API.Instance.ServerDirectory, _Path);
			API.Instance.AddFile(this.Path);
			Console.Write("Writing to {0}", this.Path);
			writer = new StreamWriter(Path);
			writer.Write(contents);
		}

		public void Close()
		{
			writer.Close();
		}
	}
}
