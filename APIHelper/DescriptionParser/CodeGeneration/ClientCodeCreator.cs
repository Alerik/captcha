using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser.CodeGeneration
{
	public abstract class ClientCodeCreator
	{
		public abstract void AddFunction(FunctionDefinition function);
		public abstract void AddDependency(Table table);
		public abstract void GenerateAll();
	}
}
