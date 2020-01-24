using System;
using System.Collections.Generic;
using System.Text;

namespace APIHelper.CodeGeneration
{
	public abstract class ClientCodeCreator
	{
		public abstract void GenerateCode(Table dependency, string functionName, string parentPath, string path,List<Column> args, List<Column> exposed);
		public abstract string GenerateCall(string typeName, string functionName, string path, List<Column> args, List<Column> exposedColumns);
		public abstract string GenerateDataType(string typeName, List<Column> exposedColumns);
	}
}
