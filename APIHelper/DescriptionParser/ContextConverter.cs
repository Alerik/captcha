using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DescriptionParser
{
	public static class ContextConverter
	{
		public static APIFunction ContextToFunction(DescriptParser.FunctionDefinitionContext context, string parentPath)
		{
			return new APIFunction(context.Identifier().GetText(), HttpMethods.GET, parentPath, ContextToArgs(context.functionArg()),
			ContextToDependencies(context.usesClause()));
		}

		private static List<Table> ContextToDependencies(DescriptParser.UsesClauseContext context)
		{
			return new List<Table>();
		}

		private static List<APIArgument> ContextToArgs(DescriptParser.FunctionArgContext[] context)
		{
			return context.Select(c => new APIArgument(c.Identifier(0).GetText(), c.Identifier(1).GetText(), "", c.Star() is null ? false : true)).ToList();
		}

		public static Table ContextToTable(DescriptParser.TableDefinitionContext context)
		{
			return new Table(context.Identifier().GetText(), ContextToColumns(context.column()));
		}

		private static List<Column> ContextToColumns(DescriptParser.ColumnContext[] context) 
		{
			return context.Select(c => new Column(c.Identifier(0).GetText(), c.Identifier(1).GetText())).ToList();
		}
	}
}
