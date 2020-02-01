using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DescriptionParser.CodeGeneration
{
	public static class Formatter
	{
		public static string ToCamelCase(string path, char sep ='.')
		{
			return LowerFirst(ToPascalCase(path, sep));
		}

		public static string ToPascalCase(string path, char sep = '/')
		{
			return string.Join("", path.Split().Select(s => CapFirst(s)));
		}

		public static string CapFirst(string s)
		{
			return char.ToUpper(s[0]) + s.Substring(1);
		}
		public static string LowerFirst(string s)
		{
			return char.ToLower(s[0]) + s.Substring(1);
		}
	}
}
