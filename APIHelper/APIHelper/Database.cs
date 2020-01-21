using System;
using System.Collections.Generic;
using System.Text;

namespace APIHelper
{
	public class Database
	{
		public string Name { get; set; }

		public Database(string _Name)
		{
			this.Name = _Name;
		}
	}
}
