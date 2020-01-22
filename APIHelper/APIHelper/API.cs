using APIHelper.CodeGeneration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Console = APIHelper.ConsoleHelper;

namespace APIHelper
{
	public class API
	{
		public static API Instance;
		public string BaseUrl { get; private set; }
		public List<APIFunction> Functions = new List<APIFunction>();
		private List<string> Files = new List<string>();

		public List<Table> Dependencies = new List<Table>();

		public string ClientDirectory { get; private set; }
		public string ServerDirectory { get; private set; }


		public API(string _BaseUrl, string _ClientDirectory, string _ServerDirectory)
		{
			Instance = this;
			this.BaseUrl = _BaseUrl;
			this.ClientDirectory = _ClientDirectory;
			this.ServerDirectory = _ServerDirectory;
			LoadIndex();
		}

		public void AddFile(string path)
		{
			if (!Files.Contains(path))
				Files.Add(path);
		}

		public void GenerateAll()
		{
			Console.Head("Generating code");
			foreach (APIFunction function in Functions)
			{
				function.GenerateCode();
			}
			Console.End();
			ClientCodeFile.CloseAll();
		}

		public void SaveIndex()
		{
			StreamWriter writer = new StreamWriter("files.index");
			foreach (string path in Files)
				writer.WriteLine(path);
			writer.Close();
		}
		public void LoadIndex()
		{
			try
			{
				StreamReader reader = new StreamReader("files.index");
				string line = "";

				while ((line = reader.ReadLine()) != null)
				{
					Files.Add(line);
				}
				reader.Close();
			}
			catch(FileNotFoundException e)
			{
				Console.Write("No index found");
			}
		}

		public void Clean()
		{
			Console.Head("Cleaning files");

			foreach(string path in Files)
			{
				Console.Write("Deleting {0}", path);
				try
				{
					File.Delete(path);
				}
				catch(Exception e)
				{
					Console.Error("Unable to delete {0}", path);
				}
			}
			Files.Clear();
			Console.End();
			Console.Write("Done cleaning");
		}

		//public void GenerateFunction(string path, List<APIArgument> arguments, List<Table> tables)
		//{
		//	Functions.Add(new APIFunction(this, path, arguments, tables));
		//}
	}
}
