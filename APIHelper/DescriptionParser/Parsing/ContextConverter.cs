using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DescriptionParser
{
	public static class ContextConverter
	{
		public static FunctionDefinition ContextToFunction(DescriptParser.FunctionDefinitionContext context, string parentPath)
		{
			string identifier = context.Identifier().GetText();
			HttpMethods httpMethod = GetMethod(context.HttpMethod().GetText());
			List<FunctionParameter> functionParameters = ContextToArgs(context.functionParameter());
			List<SuperFunctionCall> superFunctions = ContextToSuperFunctions(context.superFunctionClause());
			List<TableDefinition> dependencies = new List<TableDefinition>();//ContextToDependencies(context.uses());
			return new FunctionDefinition(identifier, httpMethod, parentPath,functionParameters, superFunctions, dependencies);
		}

		private static HttpMethods GetMethod(string str)
		{
			if (str.ToLower() == "post")
				return HttpMethods.POST;
			if (str.ToLower() == "put")
				return HttpMethods.PUT;
			return HttpMethods.GET;
		}

		private static List<SuperFunctionCall> ContextToSuperFunctions(DescriptParser.SuperFunctionClauseContext context)
		{
			return context?.superFunction().Select(c => ContextToSuperFunction(c)).ToList();
		}
		private static SuperFunctionCall ContextToSuperFunction(DescriptParser.SuperFunctionContext context)
		{
			string identifier = context.Identifier().GetText();
			List<SuperFunctionArgument> arguments = ContextToSuperFunctionArguments(context.superFunctionArgument());

			return new SuperFunctionCall(identifier, arguments);
		}
		private static SuperFunctionArgument ContextToSuperFunctionArgument(DescriptParser.SuperFunctionArgumentContext context)
		{
			if (context.reference() != null)
			{
				if (context.reference().TableReference() != null)
					return new SuperFunctionArgument(SuperFunctionArgumentTypes.TableReference);
				if (context.reference().ArgumentReference() != null)
					return new SuperFunctionArgument(SuperFunctionArgumentTypes.ArgumentReference);
			}
			if (context.Literal() != null)
				return new SuperFunctionArgument(SuperFunctionArgumentTypes.StringLiteral);
			return new SuperFunctionArgument(SuperFunctionArgumentTypes.StringLiteral);
		}
		private static List<SuperFunctionArgument> ContextToSuperFunctionArguments(DescriptParser.SuperFunctionArgumentContext[] context)
		{
			return context?.Select(c => ContextToSuperFunctionArgument(c)).ToList();
		}


		private static List<TableDefinition> ContextToDependencies(DescriptParser.UsesClauseContext context)
		{
			return new List<TableDefinition>();
		}

		private static FunctionParameter ContextToFunctionParameter(DescriptParser.FunctionParameterContext context)
		{
			if(context.typedParameter() != null)
			{
				DescriptParser.TypedParameterContext c = context.typedParameter();
				return ContextToFunctionParameter(c);
			}
			else
			{
				DescriptParser.ColumnParameterContext c = context.columnParameter();

				return new ColumnFunctionParameter(c.Identifier().GetText(), ContextToFunctionParameters(c.typedParameter()));
			}
		}

		private static FunctionParameter ContextToFunctionParameter(DescriptParser.TypedParameterContext context)
		{
			return new FunctionParameter(context.Identifier(0).GetText(), context.Identifier(1).GetText(), "", context.Star() is null ? false : true);
		}

		private static List<FunctionParameter> ContextToFunctionParameters(DescriptParser.TypedParameterContext[] context)
		{
			return context?.Select(c => ContextToFunctionParameter(c)).ToList();
  }


		private static List<FunctionParameter> ContextToArgs(DescriptParser.FunctionParameterContext[] context)
		{
			return context?.Select(c => ContextToFunctionParameter(c)).ToList();
		}

		public static TableDefinition ContextToTable(DescriptParser.TableDefinitionContext context)
		{
			return new TableDefinition(context.Identifier().GetText(), ContextToColumns(context.column()));
		}

		private static List<Column> ContextToColumns(DescriptParser.ColumnContext[] context) 
		{
			return context?.Select(c => new Column(c.Identifier(0).GetText(), c.Identifier(1).GetText())).ToList();
		}
	}
}
