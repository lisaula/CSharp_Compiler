using System;
using System.Collections.Generic;

namespace Compiler
{
    internal class OneSymbolOperator : State
    {

        private Dictionary<string, TokenType> operators;
        public OneSymbolOperator(string name, bool isInital, bool isFinal) : base(name, isInital, isFinal)
        {
            operators = new Dictionary<string, TokenType>();
            initReservedWords();
        }
        private void initReservedWords()
        {
            operators["~"] = TokenType.OP_BIN_ONES_COMPLMTS;
            operators[";"] = TokenType.END_STATEMENT;
            operators[":"] = TokenType.OP_COLON;
            operators["("] = TokenType.OPEN_PARENTHESIS;
            operators[")"] = TokenType.CLOSE_PARENTHESIS;
            operators["{"] = TokenType.OPEN_CURLY_BRACKET;
            operators["}"] = TokenType.CLOSE_CURLY_BRACKET;
            operators["["] = TokenType.OPEN_SQUARE_BRACKET;
            operators["]"] = TokenType.CLOSE_SQUARE_BRACKET;
            operators["."] = TokenType.OP_DOT;
            operators[","] = TokenType.OP_COMMA;
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
                throw new LexicalException("Error: Lexema: " + lexema + " row: " + lexemaRow + " col: " + lexemaColumn);
            }
        }
    }
}