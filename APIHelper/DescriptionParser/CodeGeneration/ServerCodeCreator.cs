using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser.CodeGeneration
{
	public abstract class ServerCodeCreator
	{
		public abstract void AddFunction(APIFunction function);

		public abstract void AddDependency(Table dependency);

		public abstract void GenerateAll();
	}
}
