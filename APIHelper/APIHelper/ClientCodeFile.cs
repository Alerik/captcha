using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Console = APIHelper.ConsoleHelper;

namespace APIHelper
{
	public class ClientCodeFile
	{
		public string Path { get; private set; }
		private StreamWriter writer;

		public ClientCodeFile(string _Path, string contents)
		{
			this.Path = System.IO.Path.Combine(API.Instance.ClientDirectory,  _Path);
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
