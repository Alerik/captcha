using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DescriptionParser
{
	public class DBTypeConverter
	{
		private Dictionary<string, string> lookupTable = new Dictionary<string, string>();
		private string def = "";

		public DBTypeConverter(Dictionary<string, string> _lookupTable, string _def = "")
		{
			this.lookupTable = _lookupTable;
			this.def = _def;
		}
		public DBTypeConverter(string path)
		{
			StreamReader reader = new StreamReader(path);

			string line = "";

			while((line = reader.ReadLine()) != "")
			{
				if(line.Contains(' '))
				{
					string[] split = line.Split(' ');
					lookupTable.Add(split[0], split[1]);
				}
				else
				{
					def = line;
				}
			}
			reader.Close();
		}

		public string Convert(string dbType)
		{
			if (lookupTable.ContainsKey(dbType.ToLower()))
				return lookupTable[dbType.ToLower()];
			else
				return def;
		}
	}
}
