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
			return new APIFunction(context.Identifier().GetText(), GetMethod(context.HttpMethod().GetText()), parentPath, ContextToArgs(context.functionArg()),
			ContextToDependencies(context.usesClause()));
		}

		private static HttpMethods GetMethod(string str)
		{
			if (str.ToLower() == "post")
				return HttpMethods.POST;
			if (str.ToLower() == "put")
				return HttpMethods.PUT;
			return HttpMethods.GET;
		}

		private static List<APITable> ContextToDependencies(DescriptParser.UsesClauseContext context)
		{
			return new List<APITable>();
		}

		private static List<APIArgument> ContextToArgs(DescriptParser.FunctionArgContext[] context)
		{
			return context.Select(c => new APIArgument(c.Identifier(0).GetText(), c.Identifier(1).GetText(), "", c.Star() is null ? false : true)).ToList();
		}

		public static APITable ContextToTable(DescriptParser.TableDefinitionContext context)
		{
			return new APITable(context.Identifier().GetText(), ContextToColumns(context.column()));
		}

		private static List<APIColumn> ContextToColumns(DescriptParser.ColumnContext[] context) 
		{
			return context.Select(c => new APIColumn(c.Identifier(0).GetText(), c.Identifier(1).GetText())).ToList();
		}
	}
}
