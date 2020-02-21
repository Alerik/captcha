parser grammar DescriptParser;

options {
    tokenVocab=DescriptLexer;
}

literal
	: IntegerLiteral
	| BooleanLiteral
	| NullLiteral
	| DecimalLiteral
	| StringLiteral
	;

tableReference: TablePrefix Identifier;
argumentReference: ArgumentPrefix Identifier;
reference : TableReference ArgumentReference;

column
	: Identifier Identifier
	;

typedParameter
	: Identifier Star* Identifier
	;
columnParameter
	: Identifier LCurly (typedParameter Comma)* typedParameter? RCurly
	;

functionParameter
	: typedParameter | columnParameter
	;

superFunctionArgument
	: reference | Literal
	;
superFunction
	: Identifier LParam (superFunctionArgument Comma)* superFunctionArgument? RParam
	;

superFunctionClause
	: Colon (superFunction Comma)* superFunction?
	;
usesClause
	: Uses (usesFrag Comma)* usesFrag
	;
usesFrag
	: Identifier As Identifier
	;

functionDefinition
	: HttpMethod Function Identifier Lparam (functionParameter Comma)* functionParameter? Rparam superFunctionClause? Semicolon
	;

tableDefinition
	: Table Identifier Lparam (column Comma)* column? Rparam Semicolon
	;
	
literalDefinition
	:
	Identifier Langle literal Rangle Semicolon
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