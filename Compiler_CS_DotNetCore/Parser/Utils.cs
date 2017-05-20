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
            current_token = lexer.getNextToken();
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
                Console.WriteLine(message+" token: "+current_token.type);
            }
        }
#endif
    }
}
