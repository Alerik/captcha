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

		public const string CLIENT_TARGET = "CLIENT_TARGET";
		public const string SERVER_TARGET = "SERVER_TARGET";
		public const string DB_TARGET = "DB_TARGET";

		public static API Instance;
		public string BaseUrl { get; internal set; }
		private List<string> Files = new List<string>();

		//List of user defined functions
		public List<FunctionDefinition> Functions = new List<FunctionDefinition>();
		//List of user defined tables
		public List<Table> Dependencies = new List<Table>();

		//List of loaded super functions
		public List<SuperFunctionCall> SuperFunctions = new List<SuperFunctionCall>();
		//List of loaded super tables
		public List<SuperTable> SuperTables = new List<SuperTable>();

		public string ClientDirectory { get; internal set; }
		public string ServerDirectory { get; internal set; }


		private DBTypeConverter serverTypeConverter = new DBTypeConverter(null, null);
		private DBTypeConverter clientTypeConverter = new DBTypeConverter(new Dictionary<string, string>
		{
			{"integer", "number" },
			{"real", "number" },
			{"text", "string"},
			{"boolean", "boolean"},
			{ "uuid", "string"}
		}, "any");

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

		public void SetClientTarget(string target)
		{

		}

		public void SetServerTarget(string target)
		{

		}

		public bool HasError()
		{
			bool error = false;
			if(ClientDirectory == null)
			{
				Console.Write("No client directory provided. Please specify it with 'CLIENTDIR<path>'");
				error = true;
			}
			if(ServerDirectory == null)
			{
				Console.Write("No server directory provided. Please specify it with 'SERVERDIR<path>'");
				error = true;
			}
			return error;
		}

		#region Utility
		public string ConvertToClient(string dbType)
		{
			return clientTypeConverter.Convert(dbType);
		}
		public string ConvertToServer(string dbType)
		{
			return serverTypeConverter.Convert(dbType);
		}
		#endregion

		public void GenerateAll()
		{
			Console.Head("Generating code");
			AngularClientCodeCreator clientCreator = new AngularClientCodeCreator();
			PHPServerCodeCreator serverCreator = new PHPServerCodeCreator();



			foreach (FunctionDefinition function in Functions)
			{
				clientCreator.AddFunction(function);
				serverCreator.AddFunction(function);
			}

			foreach (Table table in Dependencies)
			{
				clientCreator.AddDependency(table);
				serverCreator.AddDependency(table);

			}

			clientCreator.GenerateAll();
			serverCreator.GenerateAll();

	
			ClientCodeFile.CloseAll();
			ServerCodeFile.CloseAll();

			Console.Write("Done generating code");
			Console.End();
		}

		#region Files
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
		public void AddFile(string path)
		{
			if (!Files.Contains(path))
				Files.Add(path);
		}
		#endregion

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
	}
}
