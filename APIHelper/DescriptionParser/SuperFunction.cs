using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public enum SuperFunctionArgumentTypes
	{
		TableReference,
		ArgumentReference,
		NullLiteral,
		DecimalLiteral,
		IntegerLiteral,
		StringLiteral
	}
	public class SuperFunctionArgument
	{
		public SuperFunctionArgumentTypes ArgumentType { get; private set; }
		public SuperFunctionArgument(SuperFunctionArgumentTypes _ArgumentType)
		{
			this.ArgumentType = _ArgumentType;
		}
	}

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
