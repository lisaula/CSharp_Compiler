using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void field_or_method()
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
                throwError(" end statement ';'");
            consumeToken();
        }

        private void variable_declarator_list_p()
        {
            DebugInfoMethod("variable_declarator_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                variable_declarator_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
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

        private void variable_initializer()
        {
            DebugInfoMethod("variable_initializer");
            TokenType[] nuevo = { TokenType.OP_TER_NULLABLE, TokenType.OP_COLON,
                TokenType.OP_NULLABLE, TokenType.OP_LOG_OR,
                TokenType.OP_LOG_AND, TokenType.OP_BIN_OR,
                TokenType.OP_BIN_XOR, TokenType.OP_BIN_AND,
                TokenType.OPEN_PARENTHESIS, TokenType.RW_NEW,
                TokenType.ID, TokenType.RW_THIS
            };
            if (pass(nuevo.Concat(equalityOperatorOptions).Concat(relationalOperatorOptions).
                Concat(Is_AsOperatorOptions).Concat(shiftOperatorOptions).Concat(additiveOperatorOptions).
                Concat(multiplicativeOperatorOptions).Concat(assignmentOperatorOptions).Concat(unaryOperatorOptions)
                .Concat(literalOptions).ToArray()))
            {
                expression();
            }else if (pass(TokenType.OPEN_CURLY_BRACKET))
            {
                array_initializer();
            }
            else
            {
                throwError("expression or array initializer '{'");
            }
        }
    }
}
