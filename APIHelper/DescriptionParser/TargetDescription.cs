using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public class TargetDescription
	{
		public enum TargetTypes
		{
			Server,
			Client
		}
		public enum FileStructures
		{
			Aggregate,
			Clustered,
			Solitary
		}

		public TargetTypes TargetType;
		public FileStructures FileStructure;
		public string DefaultName;
		public string Extension;
		public string Template;
		public string[] Includes;
		public string Directory;

		public TargetDescription()
		{

		}
	}
}
