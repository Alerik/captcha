using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public class SuperFunctionCall
	{
		public string Identifier { get; private set; }

		public List<SuperFunctionArgument> Arguments = new List<SuperFunctionArgument>();

		public SuperFunctionCall(string _Identifier, List<SuperFunctionArgument> _Arguments)
		{
			this.Identifier = _Identifier;
			this.Arguments = _Arguments;
		}
	}
}
