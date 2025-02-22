﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser.CodeGeneration
{
	public abstract class ServerCodeCreator
	{
		public abstract void AddFunction(FunctionDefinition function);

		public abstract void AddDependency(TableDefinition dependency);

		public abstract void GenerateAll();
	}
}
