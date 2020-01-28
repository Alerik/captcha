using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser.CodeGeneration
{
	public class ClientCodeFile
	{
		private static Dictionary<string, ClientCodeFile> lookup = new Dictionary<string, ClientCodeFile>();
		public string Path { get; private set; }
		private StreamWriter writer;

		private ClientCodeFile(string _Path)
		{
			lookup.Add(_Path, this);
			this.Path = System.IO.Path.Combine(API.Instance.ClientDirectory, _Path);
			string dir = System.IO.Path.GetDirectoryName(this.Path);
			Directory.CreateDirectory(dir);
			API.Instance.AddFile(this.Path);
			writer = new StreamWriter(this.Path);
		}

		public static ClientCodeFile CreateFile(string path)
		{
			string probablePath = path;
			if (lookup.ContainsKey(probablePath))
			{
				return lookup[probablePath];
			}
			else
			{
				return new ClientCodeFile(path);
			}
			
		}

		public static void CloseAll()
		{
			foreach (ClientCodeFile file in lookup.Values)
				file.Close();
		}

		public void AddFunction(string functionCode)
		{

		}

		public void Write(string content)
		{
			Console.Write("Writing to {0}", this.Path);

			writer.Write(content);
		}

	

		public void Close()
		{
			writer.Close();
		}
	}
}
