using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DescriptionParser.CodeGeneration
{
	public abstract class CodeFile
	{
		private static Dictionary<string, CodeFile> lookup = new Dictionary<string, CodeFile>();

		public string Path { get; protected set; }
		protected StreamWriter writer;

		protected CodeFile(string _Path)
		{
			this.Path = _Path;
			lookup.Add(this.Path, this);
		}

		public static ClientCodeFile CreateClientFile(string path)
		{
			if (lookup.ContainsKey(path))
			{
				return lookup[path] as ClientCodeFile;
			}
			else
			{
				return new ClientCodeFile(path);
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
				return new ServerCodeFile(path);
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
			foreach (CodeFile file in lookup.Values)
				file.Close();
			lookup.Clear();
		}
	}
}
