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
						string literalValue = commandContext.literalDefinition().GetText();
						Console.WriteLine("Got literal value of {0}", literalValue);
					}
					if (commandContext.functionDefinition() != null)
					{
						if(head != "FUNCTION")
						{
							Console.WriteLine("Please place functions under the FUNCTION header");
						}
						FunctionDefinitionContext functionContext = commandContext.functionDefinition();
						ITerminalNode functionName = functionContext.Identifier();
						FunctionArgContext[] functionArgs = functionContext.functionArg();
					}
					if (commandContext.tableDefinition() != null)
					{
						if(head != "TABLE")
						{
							Console.WriteLine("Please place tables under the TABLE header");
						}
						TableDefinitionContext tableContext = commandContext.tableDefinition();
						ITerminalNode tableName = tableContext.Identifier();
						ColumnContext[] tableColumns = tableContext.column();
					}
				}
			}

			return base.VisitSection(context);
		}
	}
}
