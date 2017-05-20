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
            operators["+"] = TokenType.OP_SUM;
            operators["-"] = TokenType.OP_SUBSTRACT;
            operators["*"] = TokenType.OP_MULTIPLICATION;
            operators["/"] = TokenType.OP_DIVISION;
            operators["%"] = TokenType.OP_MODULO;
            operators["!"] = TokenType.OP_DENIAL;
            operators["&"] = TokenType.OP_BIN_AND;
            operators["|"] = TokenType.OP_BIN_OR;
            operators["^"] = TokenType.OP_BIN_XOR;
            operators["~"] = TokenType.OP_BIN_ONES_COMPLMTS;
            operators["="] = TokenType.OP_ASSIGN;
            operators[";"] = TokenType.END_STATEMENT;
            operators["?"] = TokenType.OP_TER_NULLABLE;
            operators[":"] = TokenType.OP_COLON;
            operators["("] = TokenType.OPEN_PARENTHESIS;
            operators[")"] = TokenType.CLOSE_PARENTHESIS;
            operators["{"] = TokenType.KEY_OPEN;
            operators["}"] = TokenType.KEY_CLOSE;
            operators["["] = TokenType.KEY_BAR_OPEN;
            operators["]"] = TokenType.KEY_BAR_CLOSE;
            operators["<"] = TokenType.OP_LESS_THAN;
            operators[">"] = TokenType.OP_GREATER_THAN;
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