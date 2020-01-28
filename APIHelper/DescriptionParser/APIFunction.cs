using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Console = DescriptionParser.ConsoleHelper;
using DescriptionParser.CodeGeneration;

namespace DescriptionParser
{
	public class APIFunction
	{
		public string Name { get; private set; }
		public string Path { get; private set; }
		public string ParentPath { get; private set; }
		public List<APIArgument> Arguments = new List<APIArgument>();
		public List<Table> Dependencies = new List<Table>();

		internal API api;

		internal APIFunction(API _api, string _ParentPath, string _Path, List<APIArgument> _Arguments, List<Table> _Dependencies)
		{
			this.api = _api;
			this.ParentPath = _ParentPath;
			this.Path = _Path;
			this.Name = Path.Split('/').Last();
			this.Arguments = _Arguments;
			this.Dependencies = _Dependencies;
			API.Instance.Functions.Add(this);
		}

		public void GenerateCode()
		{
			Console.Head("Generating {0}", Name);
			ResolveDependencies();
			GenerateClientCode();
			GenerateServerCode();
			Console.End();
		}

		//Checks for declarations in API
		void ResolveDependencies()
		{
			Console.Head("Resolving dependencies");
			int successes = 0;
			for(int i = 0; i < Dependencies.Count; i++)
			{
				if (!Dependencies[i].Defined)
				{
					Table match = API.Instance.Dependencies.Find(d => d.Name == Dependencies[i].Name);

					if (match != null)
					{
						string rowName = Dependencies[i].RowName;
						Dependencies[i] = match;
						Dependencies[i].RowName = rowName;
						successes++;
						Console.Write("Successfuly resolved {0}", Dependencies[i].Name);
					}
					else
					{
						Console.Warn("Could not resolve {0}", Dependencies[i].Name);
					}
				}
			}
			Console.Write("Resolved {0} out of {1} dependencies", successes, Dependencies.Count);
			Console.End();
		}

		void GenerateClientCode()
		{
			Console.Head("Generating client code");
			AngularClientCodeCreator creator = new AngularClientCodeCreator();
			if (!Dependencies[0].Defined)
				ConsoleHelper.Warn("The dependency {0} is undefined", Dependencies[0].Name);

			creator.GenerateCode(Dependencies[0], Name, ParentPath, Path + ".php", Arguments.Select(a => new Column(a.Identifier, a.DBType)).ToList(), Dependencies[0].ExposedColumns);
			Console.End();	
  }

		void GenerateServerCode()
		{
			Console.Head("Generating server code");
			PHPServerCodeCreator creator = new PHPServerCodeCreator();
			if (!Dependencies[0].Defined)
				ConsoleHelper.Warn("The dependency {0} is undefined", Dependencies[0].Name);
			creator.CreateCode(Dependencies[0], ParentPath, Name, Arguments.Select(a => new Column(a.Identifier, a.DBType)).ToList(), Dependencies[0].InternalColumns, Dependencies[0].ExposedColumns);
			Console.End();	
  }
	}
}
