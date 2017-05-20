using System;
using System.Collections.Generic;

namespace Compiler
{
    internal class TwoSymbolOperator : State
    {
        private Dictionary<string, TokenType> operators;
        public TwoSymbolOperator(string name, bool isInital, bool isFinal) : base(name, isInital, isFinal)
        {
            operators = new Dictionary<string, TokenType>();
            initOperatorsDict();
        }

        private void initOperatorsDict()
        {
            operators["++"] = TokenType.OP_INCREMENT;
            operators["--"] = TokenType.OP_DECREMENT;
            operators["-="] = TokenType.OP_SUBSTRACT_ONE_OPERND;
            operators["+="] = TokenType.OP_SUM_ONE_OPERND;
            operators["!="] = TokenType.OP_NOT_EQUAL;
            operators["&&"] = TokenType.OP_LOG_AND;
            operators["||"] = TokenType.OP_LOG_OR;
            operators["<<"] = TokenType.OP_BIN_LS;
            operators[">>"] = TokenType.OP_BIN_RS;
            operators["??"] = TokenType.OP_NULLABLE;
            operators["<<="] = TokenType.OP_BIN_LS_ASSIGN;
            operators[">>="] = TokenType.OP_BIN_RS_ASSIGN;
            operators[">="] = TokenType.OP_GREATER_OR_EQUAL;
            operators["<="] = TokenType.OP_LESS_OR_EQUAL;
            operators["=="] = TokenType.OP_EQUAL;
            operators["*="] = TokenType.OP_MULTIPLICATION_ASSIGN;
            operators["/="] = TokenType.OP_DIVISION_ASSIGN;
            operators["%="] = TokenType.OP_MOD_ASSIGN;
            operators["&="] = TokenType.OP_AND_ASSIGN;
            operators["|="] = TokenType.OP_OR_ASSIGN;
            operators["^="] = TokenType.OP_XOR_ASSIGN;
            operators["->"] = TokenType.OP_FLECHA;
        }

        public override Token makeToken(string lexema, int lexemaRow, int lexemaColumn)
        {
            try
            {
                TokenType token_type = operators[lexema];
                return new Token(
                        token_type,
                        lexema,
                        lexemaRow,
                        lexemaColumn
                        );
            }
            catch (KeyNotFoundException e)
            {
                throw new LexicalException("Error: Lexema: "+lexema+" row: "+lexemaRow+" col: "+lexemaColumn);
            }
        }
    }
}