using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void enum_declaration()
        {
            DebugInfoMethod("enum_declaration");
            if (!pass(TokenType.RW_ENUM))
                throwError("enum");
            consumeToken();
            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();

            enum_body();
            optional_body_end();

        }

        private void optional_body_end()
        {
            DebugInfoMethod("optional_body_end");
            if (pass(TokenType.END_STATEMENT))
            {
                consumeToken();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void enum_body()
        {
            DebugInfoMethod("enum_body");
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            optional_assignable_identifiers_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
        }

        private void optional_assignable_identifiers_list()
        {
            DebugInfoMethod("optional_assignable_identifiers_list");
            if (pass(TokenType.ID))
            {
                consumeToken();
                assignment_options();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void assignment_options()
        {
            DebugInfoMethod("assignment_options");

            if(pass(TokenType.OP_COMMA))
                optional_assignable_identifiers_list_p();        
            else if (pass(TokenType.OP_ASSIGN))
            {
                consumeToken();

                expression();

                if (pass(TokenType.OP_COMMA))
                    optional_assignable_identifiers_list_p();
            }
        }

        private void optional_assignable_identifiers_list_p()
        {
            DebugInfoMethod("optional_assignable_identifiers_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                optional_assignable_identifiers_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
            
        }
    }
}
