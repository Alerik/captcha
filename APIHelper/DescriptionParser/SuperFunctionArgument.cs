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
}
