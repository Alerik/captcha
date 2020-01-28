using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public class ConsoleHelper
	{
		private static int level = 0;

		public static void Warn(string message, params object[] args)
		{
			ConsoleColor start = Console.ForegroundColor;

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(new string('\t', level) + "Warning! " + message, args);

			Console.ForegroundColor = start;
		}

		public static void Error(string message, params object[] args)
		{
			ConsoleColor start = Console.ForegroundColor;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(new string('\t', level) + "ERROR " + message, args);

			Console.ForegroundColor = start;
		}

		public static void Head(string message, params object[] args)
		{
			Console.WriteLine(new string('\t', level) + message, args);
			level++;
		}

		public static void Write(string message, params object[] args)
		{
			Console.WriteLine(new string('\t', level) + message, args);
		}

		public static void End()
		{
			level--;
		}
	}
}
