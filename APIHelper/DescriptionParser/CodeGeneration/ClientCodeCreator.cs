using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser.CodeGeneration
{
	public abstract class ClientCodeCreator
	{
		public abstract void GenerateFunction(APIFunction function);
		public abstract void GenerateDependency(Table table);
	}
}
