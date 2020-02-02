using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser.CodeGeneration
{
	public abstract class ClientCodeCreator
	{
		public abstract void AddFunction(APIFunction function);
		public abstract void AddDependency(APITable table);
		public abstract void GenerateAll();
	}
}
