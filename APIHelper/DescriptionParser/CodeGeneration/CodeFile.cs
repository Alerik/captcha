using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser.CodeGeneration
{
	public abstract class CodeFile
	{
		private static Dictionary<string, CodeFile> lookup = new Dictionary<string, CodeFile>();

		public string Path { get; protected set; }
		protected StreamWriter writer;

		protected CodeFile(string _Path)
		{
			//this.Path = _Path;
			//lookup.Add(this.Path, this);
		}

		public static ClientCodeFile CreateClientFile(string path)
		{
			if (lookup.ContainsKey(path))
			{
				return lookup[path] as ClientCodeFile;
			}
			else
			{
				ClientCodeFile file = new ClientCodeFile(path);
				lookup[path] = file;
				return file;
			}
		}
		public static ServerCodeFile CreateServerFile(string path)
		{
			if (lookup.ContainsKey(path))
			{
				return lookup[path] as ServerCodeFile;
			}
			else
			{
				ServerCodeFile file = new ServerCodeFile(path);
				lookup[path] = file;
				return file;
			}
		}

		public void Write(string content)
		{
			writer.Write(content);
		}

		private void Close()
		{
			writer.Close();
		}

		public static void CloseAll()
		{
			Console.Head("Saving files");
			foreach (CodeFile file in lookup.Values)
			{
				Console.Write("Saving file {0}", file.Path);
				file.Close();
			}
			lookup.Clear();
			Console.End();
		}
	}
}
