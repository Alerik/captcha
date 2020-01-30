using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser.CodeGeneration
{
	public class ClientCodeFile : CodeFile
	{
		internal ClientCodeFile(string _Path) : base(_Path)
		{
			this.Path = System.IO.Path.Combine(API.Instance.ClientDirectory, _Path);
			string dir = System.IO.Path.GetDirectoryName(this.Path);
			Directory.CreateDirectory(dir);
			API.Instance.AddFile(this.Path);
			writer = new StreamWriter(this.Path);
		}

		public void AddFunction(string functionCode)
		{

		}
		public void AddMember(string memberCode)
		{

		}
		public void AddImport(string importCode)
		{

		}
	}
}
