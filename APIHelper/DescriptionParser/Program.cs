using Antlr4.Runtime;
using System;
using System.Linq;
using Antlr4.StringTemplate;
using System.Collections.Generic;
using System.Diagnostics;

namespace DescriptionParser
{
	public class Program
	{
		static void Main(string[] args)
		{
			string path = @"C:\xampp\htdocs\captcha\APIHelper\DescriptionParser\bin\Debug\netcoreapp3.1\api.desc";

			if (args.Length > 0)
			{
				if (args[0] == "clean")
				{
					API api = new API("", "", "");
					api.Clean();
					api.SaveIndex();
					return;
				}
			}
			FileParser parser = new FileParser(path);
			parser.Parse();

			if (!API.Instance.HasError())
			{
				API.Instance.GenerateAll();
				API.Instance.SaveIndex();
				Process.Start("explorer.exe", API.Instance.ClientDirectory);
				Console.Write("Done!");
			}
			else
			{
				Console.Write("Error occured. No code generated");
			}
			
		}
	}
}
