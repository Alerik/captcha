using System;
using System.Collections.Generic;
using System.Text;
using Console = DescriptionParser.ConsoleHelper;

namespace DescriptionParser
{
	public class Column
	{

		public string Identifier { get; private set; }
		public string DBType { get; private set; }

		public string ClientType => API.Instance.ConvertToClient(DBType);
		public string ServerType => API.Instance.ConvertToServer(DBType);

		public Column(string _Identifier, string _DBType)
		{
			this.Identifier = _Identifier;
			this.DBType = _DBType;
		}

		public override bool Equals(object obj)
		{
			if (obj is Column col)
				return col.Identifier.Equals(this.Identifier) && col.DBType.Equals(this.DBType);
			return false;
		}

		public override int GetHashCode()
		{
			return Identifier.GetHashCode() + DBType.GetHashCode();
		}
	}

	public class TableDefinition
	{
		public string Name { get; private set; }
		public string RowName { get; set; }
		public List<Column> ExposedColumns = new List<Column>();
		public List<Column> InternalColumns = new List<Column>();

		private APIDatabase database;
		public bool Defined => ExposedColumns.Count + InternalColumns.Count != 0;

		public TableDefinition(string _Name)
		{
			this.Name = _Name;
			this.RowName = _Name;
		}

		public TableDefinition(string _Name, string _RowName)
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;
		}

		internal TableDefinition(string _Name, List<Column> _ExposedColumns, List<Column> _InternalColumns, string _RowName = "")
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;
			this.ExposedColumns = _ExposedColumns;
			this.InternalColumns = _InternalColumns;
		}
		internal TableDefinition(string _Name, List<Column> _ExposedColumns, string _RowName = "")
		{
			this.Name = _Name;
			this.RowName = _RowName == "" ? _Name : _RowName;

			Console.Write("Got table {0} as {1}", Name, RowName);

			this.ExposedColumns = _ExposedColumns;
			this.InternalColumns = _ExposedColumns;
		}
	}
}
