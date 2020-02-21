namespace DescriptionParser
{
	using System;
	using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using static DescriptParser;
	using Console = ConsoleHelper;

    class DescriptVisitor : DescriptParserBaseVisitor<double>
	{
		private Environment environment;
		public DescriptVisitor(Environment _environment)
		{
			this.environment = _environment;
		}

		public override double VisitSection([NotNull] SectionContext context)
		{
			SectionHeaderContext header = context.sectionHeader();
			string head = header.Identifier().GetText();
			Console.Head("Visiting section {0}", head);
   
			SubsectionContext[] sections = context.subsection();

			foreach(DefinitionContext definition in context.definition())
			{
				HandleDefinition(definition, null);
			}

			foreach(SubsectionContext sectionContext in sections)
			{
				Console.Head("Visiting sub-section {0}", sectionContext.subsectionHeader()?.Identifier().GetText() ?? "[NullSection]");
				foreach(DefinitionContext definition in sectionContext.definition())
				{
					HandleDefinition(definition, sectionContext.subsectionHeader());
				}
				Console.End();
			}
			Console.End();
			return base.VisitSection(context);
		}

		private void HandleDefinition(DefinitionContext context, SubsectionHeaderContext header)
		{
			if (context.literalDefinition() != null)
			{
				HandleLiteral(context.literalDefinition());
			}
			if (context.functionDefinition() != null)
			{
				HandleFunction(context.functionDefinition(), header);
			}
			if (context.tableDefinition() != null)
			{
				HandleTable(context.tableDefinition());
			}
		}

		private void HandleLiteral(LiteralDefinitionContext context)
		{
			string identifier = context.Identifier().GetText();
			//We need to remove quotes from the literal

			DescriptParser.LiteralContext literal = context.literal();

			object literalValue = null;
			if(literal.StringLiteral() != null)
			{
				string strValue = literal.StringLiteral().GetText().Trim('\'');

				//switch (identifier)
				//{
				//	case API.BASE_URL:
				//		API.Instance.BaseUrl = strValue;
				//		break;
				//	case API.CLIENTDIR:
				//		API.Instance.ClientDirectory = strValue;
				//		break;
				//	case API.SERVERDIR:
				//		API.Instance.ServerDirectory = strValue;
				//		break;
				//	case API.CLIENT_TARGET:
				//		API.Instance.SetClientTarget(strValue);
				//		break;
				//	case API.SERVER_TARGET:
				//		API.Instance.SetServerTarget(strValue);
				//		break;
				//	default:
				//		Console.Write("No defined action for literal {0} with value {1}", identifier, literalValue);
				//		break;
				//}

				literalValue = strValue;
			}

			if(literal.IntegerLiteral() != null)
			{
				string intStr = literal.IntegerLiteral().GetText();
				Console.Write("Currently unable to parse integer literals");
			}

			if(literal.DecimalLiteral() != null)
			{
				string realStr = literal.DecimalLiteral().GetText();
				Console.Write("Currently unable to parse decimal literals");
			}

			environment.RegisterEnvironmentVariable(identifier, literalValue);

			Console.Write("Got literal {0} with value of {1}", identifier, literalValue);
		}
		private void HandleFunction(FunctionDefinitionContext context, SubsectionHeaderContext header)
		{
			environment.RegisterFunctionDefinition(ContextConverter.ContextToFunction(context, header?.Identifier().GetText() ?? "[NullSection]"));
		}

		private void HandleTable(TableDefinitionContext context)
		{
			environment.RegisterTableDefinition(ContextConverter.ContextToTable(context));
		}
	}
}
