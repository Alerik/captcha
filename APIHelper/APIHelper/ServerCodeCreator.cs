using System;
using System.Collections.Generic;
using System.Text;

namespace APIHelper
{
	public abstract class ServerCodeCreator
	{
		public abstract void CreateCode(Table dependency, string functionName, List<Column> args, List<Column> internalColumns, List<Column> exposedColumns);
	}
}
