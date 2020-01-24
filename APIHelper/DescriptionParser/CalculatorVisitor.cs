namespace DescriptionParser
{
	using System;
	using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using static descriptionParser;

    class CalculatorVisitor : descriptionBaseVisitor<double>
	{
		public override double VisitTop_section([NotNull] descriptionParser.Top_sectionContext context)
		{
			HeaderContext header = context.header();
			string head = header.IDENTIFIER().GetText();
			Category_sectionContext[] sections = context.category_section();

			foreach(Category_sectionContext sectionContext in sections)
			{
				foreach(CommandContext commandContext in sectionContext.command())
				{
					if(commandContext.literal() != null)
					{
						string literalValue = commandContext.literal().GetText();
						Console.WriteLine("Got literal value of {0}", literalValue);
					}
					if (commandContext.function() != null)
					{
						if(head != "FUNCTION")
						{
							Console.WriteLine("Please place functions under the FUNCTION header");
						}
						FunctionContext functionContext = commandContext.function();
						ITerminalNode functionName = functionContext.IDENTIFIER();
						ArgContext[] functionArgs = functionContext.arg();
					}
					if (commandContext.table() != null)
					{
						if(head != "TABLE")
						{
							Console.WriteLine("Please place tables under the TABLE header");
						}
						TableContext tableContext = commandContext.table();
						ITerminalNode tableName = tableContext.IDENTIFIER();
						ColumnContext[] tableColumns = tableContext.column();
					}
				}
			}

			return base.VisitTop_section(context);
		}
	}
}
