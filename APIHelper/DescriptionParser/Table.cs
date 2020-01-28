using System;
using System.Collections.Generic;
using System.Text;

namespace DescriptionParser
{
	public class Table
	{
		public string Name { get; private set; }
		public string RowName { get; set; }
		public List<Column> ExposedColumns = new List<Column>();
		public List<Column> InternalColumns = new List<Column>();

		private Database database;
		public bool Defined => ExposedColumns.Count + InternalColumns.Count != 0;

		public Table(string _Name)
		{
			this.Name = _Name;
			this.RowName = _Name;
		}

		public Table(string _Name, string _RowName)
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;
		}

		internal Table(string _Name, List<Column> _ExposedColumns, List<Column> _InternalColumns, string _RowName = "")
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;
			this.ExposedColumns = _ExposedColumns;
			this.InternalColumns = _InternalColumns;

			API.Instance.Dependencies.Add(this);
		}
		internal Table(string _Name, List<Column> _ExposedColumns, string _RowName = "")
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;
			this.ExposedColumns = _ExposedColumns;
			this.InternalColumns = _ExposedColumns;

			API.Instance.Dependencies.Add(this);
		}
	}
}
