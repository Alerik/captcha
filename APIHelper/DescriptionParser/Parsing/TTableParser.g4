typed_type
	: Identifier (LParam (Identifier Comma)* Identifier? RParam)
	;
basic_type
	: Identifier
	;

type
	:
	typed_type
	| basic_type
	;
conversion
	:
	 (type Comma)* type Arrow type
	;