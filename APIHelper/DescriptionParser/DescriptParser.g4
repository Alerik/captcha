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

usesClause
	: Uses (usesFrag Comma)* usesFrag
	;
usesFrag
	: Identifier As Identifier
	;
functionDefinition
	: HttpMethod Function Identifier Lparam (functionArg Comma)* functionArg? Rparam usesClause? Semicolon
	;
tableDefinition
	: Table Identifier Lparam (column Comma)* column? Rparam Semicolon
	;
	
literalDefinition
	:
	Identifier Langle Literal Rangle Semicolon
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
	: sectionHeader definition* subsection*
	;

subsectionHeader
	: Identifier Colin
	;
	//this is matching the definition as single each and every single time instead of trying to be greedy
subsection
	: subsectionHeader definition+
	;