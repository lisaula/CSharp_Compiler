using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public partial class Parser
    {
        TokenType[] encapsulationTypes = { TokenType.RW_PUBLIC, TokenType.RW_PRIVATE, TokenType.RW_PROTECTED };
        TokenType[] typesDeclarationOptions = { TokenType.RW_ABSTRACT, TokenType.RW_CLASS, TokenType.RW_ENUM, TokenType.RW_INTERFACE };
        TokenType[] typesOptions = {
            TokenType.RW_INT,
            TokenType.RW_CHAR,
            TokenType.RW_STRING,
            TokenType.RW_BOOL,
            TokenType.RW_FLOAT,
            TokenType.ID,
            TokenType.RW_DICTIONARY
        };
        TokenType[] typesOptionsWithNoID = {
            TokenType.RW_INT,
            TokenType.RW_CHAR,
            TokenType.RW_STRING,
            TokenType.RW_BOOL,
            TokenType.RW_FLOAT,
            TokenType.RW_DICTIONARY
        };
        TokenType[] primitiveTypes =
        {
            TokenType.RW_INT,
            TokenType.RW_CHAR,
            TokenType.RW_STRING,
            TokenType.RW_BOOL,
            TokenType.RW_FLOAT
        };
        TokenType[] optionalModifiersOptions = {
            TokenType.RW_STATIC,
            TokenType.RW_VIRTUAL,
            TokenType.RW_OVERRIDE,
            TokenType.RW_ABSTRACT,
        };
        TokenType[] literalOptions = {
            TokenType.LIT_INT,
            TokenType.LIT_CHAR,
            TokenType.LIT_FLOAT,
            TokenType.LIT_STRING,
            TokenType.LIT_BOOL,
            TokenType.LIT_VERBATIN
        };
        TokenType[] unaryOperatorOptions = {
            TokenType.OP_SUM,
            TokenType.OP_SUBSTRACT,
            TokenType.OP_INCREMENT,
            TokenType.OP_DECREMENT,
            TokenType.OP_DENIAL,
            TokenType.OP_BIN_ONES_COMPLMTS,
            TokenType.OP_MULTIPLICATION
        };
        TokenType[] assignmentOperatorOptions = {
            TokenType.OP_ASSIGN,
            TokenType.OP_SUM_ONE_OPERND,
            TokenType.OP_SUBSTRACT_ONE_OPERND,
            TokenType.OP_MULTIPLICATION_ASSIGN,
            TokenType.OP_DIVISION_ASSIGN,
            TokenType.OP_MOD_ASSIGN,
            TokenType.OP_AND_ASSIGN,
            TokenType.OP_OR_ASSIGN,
            TokenType.OP_XOR_ASSIGN,
            TokenType.OP_BIN_LS_ASSIGN,
            TokenType.OP_BIN_RS_ASSIGN,
        };
        TokenType[] relationalOperatorOptions = {
            TokenType.OP_LESS_THAN,
            TokenType.OP_GREATER_THAN,
            TokenType.OP_LESS_OR_EQUAL,
            TokenType.OP_GREATER_OR_EQUAL
        };

        TokenType[] equalityOperatorOptions = {
            TokenType.OP_EQUAL,
            TokenType.OP_NOT_EQUAL
        };

        TokenType[] shiftOperatorOptions = {
            TokenType.OP_BIN_LS,
            TokenType.OP_BIN_RS
        };

        TokenType[] additiveOperatorOptions = {
            TokenType.OP_SUM,
            TokenType.OP_SUBSTRACT
        };

        TokenType[] Is_AsOperatorOptions = {
            TokenType.RW_IS,
            TokenType.RW_AS
        };

        TokenType[] multiplicativeOperatorOptions = {
            TokenType.OP_MULTIPLICATION,
            TokenType.OP_DIVISION,
            TokenType.OP_MODULO
        };

        TokenType[] unaryExpressionOptions = {
            TokenType.OPEN_PARENTHESIS,TokenType.RW_NEW, TokenType.ID,
            TokenType.RW_THIS, TokenType.RW_BASE
        };

        public void addLookAhead(Token token)
        {
            look_ahead.Add(token);
            DebugInfoMethod("->agrego " + look_ahead[look_ahead.Count() - 1]);
        }
        public void removeLookAhead(int index)
        {
            if(look_ahead.Count >0)
                look_ahead.RemoveAt(index);
        }
        public bool pass(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (type == current_token.type)
                    return true;
            }
            return false;
        }

        void throwError(string expected)
        {
            throw new ParserException(expected, current_token.row, current_token.column);
        }

        void consumeToken()
        {
            DebugInfoMethod("\t->consumio " + current_token.type+" lexema: "+current_token.lexema);
            if (look_ahead.Count > 0)
            {
                current_token = look_ahead[0];
                removeLookAhead(0);
            }
            else
            {
                current_token = lexer.getNextToken();
            }
            DebugInfoMethod("\t->nuevo token " + current_token.type + " lexema: " + current_token.lexema);
        }
#if DEBUG
        private bool doDebugOnlyCode = false;
#endif
        [System.Diagnostics.Conditional("DEBUG")]
        public void DebugInfoMethod(string message)
        {
#if DEBUG
            if (doDebugOnlyCode)
            {
                Console.WriteLine(message+" - token: "+current_token.type+" lexema: "+current_token.lexema);
            }
        }
#endif
    }
}
