using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser.CodeGeneration
{
	public abstract class ServerCodeCreator
	{
		public abstract void CreateCode(Table dependency, string parentPath, string functionName, List<Column> args, List<Column> internalColumns, List<Column> exposedColumns);
	}
}
