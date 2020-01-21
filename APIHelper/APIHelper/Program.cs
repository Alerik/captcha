using System;
using System.Collections.Generic;
using Console = APIHelper.ConsoleHelper;

namespace APIHelper
{
	class Program
	{
		static void Main(string[] args)
		{
			if(args.Length > 0)
			{
				if(args[0] == "clean")
				{
					API api = new API("", "", "");
					api.Clean();
					api.SaveIndex();
					return;
				}
			}
			//string func = @"create/createdataset {name TEXT (The name of the dataset), entry_count INTEGER, completed* BOOLEAN} uses datasets as DatasetN, customers as Customer, users;";

			//API api = new API("http://127.0.0.1/captcha", @"C:\xampp\htdocs\captcha\customer-portal\src\app", @"C:\xampp\htdocs\captcha");		
			//APIFunction f = Parser.ParseFunction(func);
			//Table datasets = Parser.ParseDependencyDefinition("datasets {completion boolean, created boolean, reviewed boolean, query_count integer};");
			//f.GenerateCode(); 

			FileParser parser = new FileParser("api.desc");
			parser.Parse();
			API.Instance.GenerateAll();
			API.Instance.SaveIndex();
			Console.Write("Done!");
		}
	}
}
