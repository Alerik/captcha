parser grammar DescriptParser;

options {
    tokenVocab=DescriptLexer;
}

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
	: Table Identifier Lparam (column Comma)* column? Rparam Semicolon
	;
	
literalDefinition
	:
	Identifier Langle Literal Rangle Semicolin
	;


definition
	: literalDefinition | functionDefinition | tableDefinition
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