using DescriptionParser.CodeGeneration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser
{
	public class API
	{
		public const string BASE_URL = "ENDPOINT";
		public const string CLIENTDIR = "CLIENTDIR";
		public const string SERVERDIR = "SERVERDIR";

		public static API Instance;
		public string BaseUrl { get; internal set; }
		public List<APIFunction> Functions = new List<APIFunction>();
		private List<string> Files = new List<string>();

		public List<Table> Dependencies = new List<Table>();

		public string ClientDirectory { get; internal set; }
		public string ServerDirectory { get; internal set; }


		internal API()
		{
			Instance = this;
			LoadIndex();
		}
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
			AngularClientCodeCreator clientCreator = new AngularClientCodeCreator();
			PHPServerCodeCreator serverCreator = new PHPServerCodeCreator();

			foreach (APIFunction function in Functions)
			{
   
			}
			
			Console.End();
			ClientCodeFile.CloseAll();
			ServerCodeFile.CloseAll();
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
