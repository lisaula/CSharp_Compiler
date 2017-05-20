using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void field_or_method() //TODO: Not implemented
        {
            DebugInfoMethod("field_or_method");
            if(pass(TokenType.OP_ASSIGN, TokenType.OP_COMMA, TokenType.END_STATEMENT))
            {
                field_declaration();
            }else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                method_declaration();
            }
            else
            {
                throwError("'=', ',', ';' or '('. Field_or_method expected");
            }
            
        }

        private void method_declaration()
        {
            DebugInfoMethod("method_declaration");
            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            fixed_parameters();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            maybe_empty_block();
        }

        private void field_declaration()
        {
            DebugInfoMethod("field_declaration");
            if (!pass(TokenType.OP_ASSIGN, TokenType.OP_COMMA, TokenType.END_STATEMENT))
                throwError("'=', ',' or ';' ");
            variable_assigner();
            variable_declarator_list_p();

            if (!pass(TokenType.END_STATEMENT))
                throwError(";");
            consumeToken();
        }

        private void variable_declarator_list_p()
        {
            DebugInfoMethod("variable_declarator_list_p");
            if (!pass(TokenType.OP_COMMA))
                throwError("comma ','");
            consumeToken();
            variable_declarator_list();
        }

        private void variable_declarator_list()
        {
            DebugInfoMethod("variable_declarator_list");
            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();

            if (pass(TokenType.OP_ASSIGN))
            {
                variable_assigner();
            }
            variable_declarator_list_p();
        }

        private void variable_assigner()
        {
            DebugInfoMethod("variable_assigner");
            if (pass(TokenType.OP_ASSIGN))
            {
                consumeToken();
                variable_initializer();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void variable_initializer() //TODO
        {
            expression();
        }
    }
}
