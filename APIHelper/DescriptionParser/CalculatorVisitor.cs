namespace DescriptionParser
{
	using System;
	using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using static DescriptParser;
	using Console = ConsoleHelper;

    class CalculatorVisitor : DescriptParserBaseVisitor<double>
	{
		public override double VisitSection([NotNull] SectionContext context)
		{
			SectionHeaderContext header = context.sectionHeader();
			string head = header.Identifier().GetText();
			Console.Head("Visiting section {0}", head);
   
			SubsectionContext[] sections = context.subsection();

			foreach(SubsectionContext sectionContext in sections)
			{
				//Console.Write(sectionContext.GetText());
				Console.Head("Visiting sub-section {0}", sectionContext.subsectionHeader()?.Identifier().GetText() ?? "[NullSection]");
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

						Console.Write("Got literal {0} with value of {1}", identifier, literalValue);
					}
					if (commandContext.functionDefinition() != null)
					{
						if(head != "FUNCTION")
						{
							Console.Write("Please place functions under the FUNCTION header");
						}
						FunctionDefinitionContext functionContext = commandContext.functionDefinition();
						ContextConverter.ContextToFunction(functionContext, sectionContext.subsectionHeader()?.Identifier().GetText() ?? "[NullSection]");
					}
					if (commandContext.tableDefinition() != null)
					{
						if(head != "TABLE")
						{
							Console.Write("Please place tables under the TABLE header");
						}
						TableDefinitionContext tableContext = commandContext.tableDefinition();
						ContextConverter.ContextToTable(tableContext);
					}
				}
				Console.End();
			}
			Console.End();
			return base.VisitSection(context);
		}
	}
}
