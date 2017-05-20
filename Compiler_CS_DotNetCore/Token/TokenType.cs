﻿namespace Compiler
{
    public enum TokenType
    {
        ID,
        EOF,
        OP_SUM,
        RW_NEW,
        READ_CALL,
        OP_SUBSTRACT,
        OP_DIVISION,
        OP_MULTIPLICATION,
        OP_MODULO,
        OP_ASSIGN,
        LIT_INT,
        OPEN_PARENTHESIS,
        CLOSE_PARENTHESIS,
        END_STATEMENT,
        LIT_DECIMAL,
        LIT_FLOAT,
        LIT_DOUBLE,
        LIT_HEX,
        LIT_BIN,
        LIT_BOOL,
        OP_DENIAL,
        OP_BIN_AND,
        OP_BIN_OR,
        OP_BIN_XOR,
        OP_BIN_ONES_COMPLMTS,
        OP_COLON,
        OP_TER_NULLABLE,
        KEY_OPEN,
        KEY_CLOSE,
        KEY_BAR_OPEN,
        KEY_BAR_CLOSE,
        RW_IS,
        RW_AS,
        OP_INCREMENT,
        OP_DECREMENT,
        OP_SUBSTRACT_ONE_OPERND,
        OP_SUM_ONE_OPERND,
        OP_LOG_AND,
        OP_LOG_OR,
        OP_BIN_LS,
        OP_BIN_RS,
        OP_NULLABLE,
        OP_LESS_THAN,
        OP_GREATER_THAN,
        LIT_CHAR,
        LIT_STRING,
        OP_NOT_EQUAL,
        RW_PUBLIC,
        RW_PROTECTED,
        RW_PRIVATE,
        RW_ABSTRACT,
        RW_BOOL,
        RW_INT,
        RW_DOUBLE,
        RW_VAR,
        RW_FLOAT,
        RW_DECIMAL,
        RW_CHAR,
        RW_STRING,
        RW_OVERRIDE,
        RW_VOID,
        RW_WHILE,
        RW_FOR,
        RW_FOREACH,
        RW_IN,
        RW_USING,
        RW_NAMEESPACE,
        OP_BIN_LS_ASSIGN,
        OP_BIN_RS_ASSIGN,
        OP_COMMA,
        OP_DOT,
        LIT_VERBATIN,
        OP_LESS_OR_EQUAL,
        OP_GREATER_OR_EQUAL,
        RW_IF,
        OP_EQUAL,
        RW_ELSE,
        RW_SWITCH,
        RW_CASE,
        RW_CLASS,
        RW_STATIC,
        OP_MULTIPLICATION_ASSIGN,
        OP_DIVISION_ASSIGN,
        OP_MOD_ASSIGN,
        OP_AND_ASSIGN,
        OP_XOR_ASSIGN,
        OP_OR_ASSIGN,
        OP_FLECHA,
        RW_SIZEOF,
        RW_TYPE,
        RW_ENUM,
        RW_INTERFACE
    }
}