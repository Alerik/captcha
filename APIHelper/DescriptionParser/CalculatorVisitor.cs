namespace DescriptionParser
{
	using System;
	using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using static DescriptParser;

    class CalculatorVisitor : DescriptParserBaseVisitor<double>
	{
		public override double VisitSection([NotNull] SectionContext context)
		{
			SectionHeaderContext header = context.sectionHeader();
			string head = header.Identifier().GetText();
			SubsectionContext[] sections = context.subsection();

			foreach(SubsectionContext sectionContext in sections)
			{
				foreach(DefinitionContext commandContext in sectionContext.definition())
				{
					if(commandContext.literalDefinition() != null)
					{
						string identifier = commandContext.literalDefinition().Identifier().GetText();
						string literalValue = commandContext.literalDefinition().Literal().GetText();

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
							default:
								Console.Write("No defined action for literal {0} with value {1}", identifier, literalValue);
								break;
						}

						Console.WriteLine("Got literal value of {0}", literalValue);
					}
					if (commandContext.functionDefinition() != null)
					{
						if(head != "FUNCTION")
						{
							Console.WriteLine("Please place functions under the FUNCTION header");
						}
						FunctionDefinitionContext functionContext = commandContext.functionDefinition();
						ContextConverter.ContextToFunction(functionContext, sectionContext.subsectionHeader().GetText());
					}
					if (commandContext.tableDefinition() != null)
					{
						if(head != "TABLE")
						{
							Console.WriteLine("Please place tables under the TABLE header");
						}
						TableDefinitionContext tableContext = commandContext.tableDefinition();
						ContextConverter.ContextToTable(tableContext);
					}
				}
			}

			return base.VisitSection(context);
		}
	}
}
