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
            TokenType.ID
        };
        TokenType[] optionalModifiersOptions = {
            TokenType.RW_STATIC,
            TokenType.RW_VIRTUAL,
            TokenType.RW_OVERRIDE,
            TokenType.RW_ABSTRACT,
        };
        
        public void addLookAhead(Token token)
        {
            look_ahead.Add(token);
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
            throw new ParserException("Expected: "+expected, current_token.row, current_token.column);
        }

        void consumeToken()
        {
            if (look_ahead.Count > 0)
            {
                current_token = look_ahead[0];
                removeLookAhead(0);
            }
            else
            {
                current_token = lexer.getNextToken();
            }
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
