using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Console = DescriptionParser.ConsoleHelper;
using DescriptionParser.CodeGeneration;

namespace DescriptionParser
{
	public enum HttpMethods
	{
		PUT,
		POST,
		GET
	}
	public class APIFunction
	{
		public string Name { get; private set; }
		public string NameCamel => Formatter.LowerFirst(Name);
		public string Path { get; private set; }
		public string FullPath => API.Instance.BaseUrl + Path + '/' +  Name;

		public string Return = "Row1";//{ get; private set; }

		public HttpMethods Method { get; private set; }

		public bool IsGet => Method == HttpMethods.GET;
		public bool IsPut => Method == HttpMethods.PUT;
		public bool IsPost => Method == HttpMethods.POST;

		public List<APIArgument> Arguments = new List<APIArgument>();
		public List<Table> Dependencies = new List<Table>();


		internal APIFunction(string _Name, HttpMethods _Method, string _Path, List<APIArgument> _Arguments, List<Table> _Dependencies)
		{
			this.Name = _Name;
			this.Path = _Path;
			this.Method = _Method;

			this.Arguments = _Arguments ?? new List<APIArgument>();
			this.Dependencies = _Dependencies ?? new List<Table>();
			Console.Write("Creating function {0}", Name);
			API.Instance.Functions.Add(this);
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
	}
}
