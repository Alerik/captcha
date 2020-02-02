namespace DescriptionParser
{
	using System;
	using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using static DescriptParser;
	using Console = ConsoleHelper;

    class DescriptVisitor : DescriptParserBaseVisitor<double>
	{
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
			string literalValue = context.Literal().GetText().Trim('\'');

			switch (identifier)
			{
				case API.BASE_URL:
					API.Instance.BaseUrl = literalValue;
					break;
				case API.CLIENTDIR:
					API.Instance.ClientDirectory = literalValue;
					break;
				case API.SERVERDIR:
					API.Instance.ServerDirectory = literalValue;
					break;
				case API.CLIENT_TARGET:
					API.Instance.SetClientTarget(literalValue);
					break;
				case API.SERVER_TARGET:
					API.Instance.SetServerTarget(literalValue);
					break;
				default:
					Console.Write("No defined action for literal {0} with value {1}", identifier, literalValue);
					break;
			}

			Console.Write("Got literal {0} with value of {1}", identifier, literalValue);

		}
		private void HandleFunction(FunctionDefinitionContext context, SubsectionHeaderContext header)
		{
			ContextConverter.ContextToFunction(context, header?.Identifier().GetText() ?? "[NullSection]");
		}

		private void HandleTable(TableDefinitionContext context)
		{
			ContextConverter.ContextToTable(context);
		}
	}
}
