using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Antlr4.StringTemplate;

namespace DescriptionParser.CodeGeneration
{
	/// <summary>
	/// A class used by CodeCreators to easily create files in their directories
	/// </summary>
	public class CodeDirectory
	{
		public string Path { get; private set; }

		private Dictionary<string, CodeFile> files = new Dictionary<string, CodeFile>();
		private Dictionary<string, CodeDirectory> directories = new Dictionary<string, CodeDirectory>();

		/// <summary>
		/// Create a new code directory
		/// </summary>
		/// <param name="_Path">The absolute path of the directory</param>
		public CodeDirectory(string _Path)
		{
			this.Path = _Path;

			Directory.CreateDirectory(Path);
		}

		/// <summary>
		/// Creates a directory
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public CodeDirectory CreateDirectoryRelativePath(string path)
		{
			string absolutePath = RelativeToAbsolutePath(path);
			if (directories.ContainsKey(absolutePath))
				return directories[absolutePath];
			return directories[absolutePath] = new CodeDirectory(absolutePath);
		}
		public CodeDirectory CreateDirectoryAbsolutePath(string absolutePath)
		{
			if (directories.ContainsKey(absolutePath))
				return directories[absolutePath];
			return directories[absolutePath] = new CodeDirectory(absolutePath);
		}

		private string RelativeToAbsolutePath(string relativePath)
		{
			return System.IO.Path.Combine(Path, relativePath);
		}

		public CodeFile CreateFile(string name, string extension)
		{
			if(System.IO.Path.GetDirectoryName(name) != "")
			{
				CodeDirectory directory = CreateDirectoryRelativePath(System.IO.Path.GetDirectoryName(name));
				return directory.CreateFile(System.IO.Path.GetFileName(name), extension);
			}
			else
			{
				string path = System.IO.Path.Combine(Path, $"{name}.{extension}");
				if (files.ContainsKey(path))
					return files[path];
				StreamWriter writer = new StreamWriter(path);
				return files[path] = new CodeFile(writer);
			}
		}

		public TemplateCodeFile CreateTemplateFile(string name, string extension, Template template)
		{
			if (System.IO.Path.GetDirectoryName(name) != "")
			{
				CodeDirectory directory = CreateDirectoryRelativePath(System.IO.Path.GetDirectoryName(name));
				return directory.CreateTemplateFile(System.IO.Path.GetFileName(name), extension, template);
			}
			else
			{
				string path = System.IO.Path.Combine(Path, $"{name}.{extension}");
				if (files.ContainsKey(path))
					return files[path] as TemplateCodeFile;
				StreamWriter writer = new StreamWriter(path);
				return (files[path] = new TemplateCodeFile(writer, template)) as TemplateCodeFile;
			}
		}

		public void CloseAll()
		{
			foreach (CodeFile file in files.Values)
				file.Close();
			foreach (CodeDirectory directory in directories.Values)
				directory.CloseAll();
		}
	}
}
