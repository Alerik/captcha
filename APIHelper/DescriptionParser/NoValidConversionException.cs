using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public class NoValidConversionException : Exception
	{
		public NoValidConversionException(string datatype) : base("No valid conversion for " + datatype + " exists") { }
	}
}
