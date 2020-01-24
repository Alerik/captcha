grammar description;

column
	: IDENTIFIER IDENTIFIER
	;
arg
	: IDENTIFIER STAR? IDENTIFIER
	;

function
	: 'function' IDENTIFIER '(' (arg ',')* arg? ')' ';'
	;
table
	: 'table' IDENTIFIER '(' (column ',')* column? ')' ';'
	;
	
literal_inside
	: ( options {greedy=false;} : .)*
	;
literal
	:
	'<' literal_inside '>'
	;


command
	: function | table | literal
	;


header
	: '#' IDENTIFIER
	;

category
	: IDENTIFIER ':'
	;

category_section
	: category? command+
	;

top_section
	: header category_section*
	;
file
	: top_section+
	;

STAR
	: '*'
	;

IDENTIFIER
	: [a-zA-Z0-9_]+
	;

WS
   : [ \r\n\t] + -> skip
   ;