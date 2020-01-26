parser grammar DescriptParser;

column
	: Identifier Identifier
	;
functionArg
	: Identifier Star? Identifier
	;

functionDefinition
	: Function Identifier Lparam (functionArg Comma)* functionArg? Rparam Semicolon
	;
tableDefinition
	: Function Identifier Lparam (column Comma)* column? Rparam Semicolon
	;
	
literalDefinition
	:
	Langle Literal Rangle
	;


definition
	: functionDefinition | tableDefinition | literalDefinition
	;

file
	: section+
	;

sectionHeader
	: Hash Identifier
	;
section
	: sectionHeader subsection*
	;

subsectionHeader
	: Identifier Colin
	;
subsection
	: subsectionHeader? definition+
	;