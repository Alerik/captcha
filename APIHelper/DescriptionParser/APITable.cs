using System;
using System.Collections.Generic;
using System.Text;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser
{
	public class APITable
	{
		public string Name { get; private set; }
		public string RowName { get; set; }
		public List<APIColumn> ExposedColumns = new List<APIColumn>();
		public List<APIColumn> InternalColumns = new List<APIColumn>();

		private APIDatabase database;
		public bool Defined => ExposedColumns.Count + InternalColumns.Count != 0;

		public APITable(string _Name)
		{
			this.Name = _Name;
			this.RowName = _Name;
		}

		public APITable(string _Name, string _RowName)
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;
		}

		internal APITable(string _Name, List<APIColumn> _ExposedColumns, List<APIColumn> _InternalColumns, string _RowName = "")
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;
			this.ExposedColumns = _ExposedColumns;
			this.InternalColumns = _InternalColumns;

			API.Instance.Dependencies.Add(this);
		}
		internal APITable(string _Name, List<APIColumn> _ExposedColumns, string _RowName = "")
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;

			Console.Write("Got table {0} as {1}", Name, RowName);

			this.ExposedColumns = _ExposedColumns;
			this.InternalColumns = _ExposedColumns;

			API.Instance.Dependencies.Add(this);
		}
	}
}
