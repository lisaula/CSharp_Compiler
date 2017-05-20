using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public partial class Parser
    {
        public void parse()
        {
            DebugInfoMethod("parser");
            code();
            if (current_token.type != TokenType.EOF)
                throw new ParserException("Not all tokens were consumed. EOF Expected");
            Console.Out.WriteLine("Successful");
        }

        private void code()
        {
            DebugInfoMethod("code");
            compilation_unit();
        }

        private void compilation_unit()
        {
            TokenType[] nuevo ={ TokenType.RW_NAMEESPACE };
            DebugInfoMethod("compilation_unit");
            if (current_token.type == TokenType.RW_USING)
            {
                optional_using_directive();
            } else if (pass(encapsulationTypes.Concat(nuevo).Concat(typesDeclarationOptions).ToArray()))
            {
                optional_namespace_member_declaration();
            }
        }

        private void optional_namespace_member_declaration()
        {
            DebugInfoMethod("optional_namespace_declaration");

        }

        private void optional_using_directive()
        {
            DebugInfoMethod("optional_using_directive");
            if (pass(TokenType.RW_USING))
                using_directive();
            else
            {
                DebugInfoMethod("epsilon");
                //epsilon
            }

        }

        private void using_directive()
        {
            DebugInfoMethod("using_directive");
            if (!pass(TokenType.RW_USING))
                throwError("using");
            consumeToken();

            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();

            if (pass(TokenType.OP_DOT))
            {
                identifier_attribute();
            }

            if (!pass(TokenType.END_STATEMENT))
                throwError(";");
            consumeToken();
            optional_using_directive();
        }

        private void identifier_attribute()
        {
            DebugInfoMethod("identifier_attribute");
            if (pass(TokenType.OP_DOT))
            {
                consumeToken();
                if (!pass(TokenType.ID))
                    throwError("identifier");
                consumeToken();
                identifier_attribute();
            }
            else
            {
                DebugInfoMethod("epsilon");
                //epsilon
            }
        }
    }
}
