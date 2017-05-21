using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void optional_statement_list()
        {
            DebugInfoMethod("optional_statement_list");
            TokenType[] nuevo = {
                TokenType.RW_VAR, TokenType.OPEN_CURLY_BRACKET,TokenType.END_STATEMENT,
                TokenType.RW_IF, TokenType.RW_SWITCH, TokenType.RW_WHILE, 
                TokenType.RW_DO, TokenType.RW_FOR, TokenType.RW_FOREACH,
                TokenType.RW_BREAK, TokenType.RW_CONTINUE, TokenType.RW_RETURN
            };
            if (pass(nuevo.Concat(typesOptions).Concat(unaryExpressionOptions).Concat(unaryOperatorOptions).
                Concat(literalOptions).ToArray()
                ))
            {
                statement_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }

        }

        private void statement_list()
        {
            DebugInfoMethod("statement_list");
            statement();
            optional_statement_list();
        }

        private void statement()
        {
            DebugInfoMethod("statement");
            TokenType[] nuevo = { TokenType.RW_VAR };
            TokenType[] embedded= {
                TokenType.OPEN_CURLY_BRACKET,TokenType.END_STATEMENT,
                TokenType.RW_IF, TokenType.RW_SWITCH, TokenType.RW_WHILE,
                TokenType.RW_DO, TokenType.RW_FOR, TokenType.RW_FOREACH,
                TokenType.RW_BREAK, TokenType.RW_CONTINUE, TokenType.RW_RETURN
            };
            addLookAhead(lexer.getNextToken());
            if (pass(nuevo.Concat(typesOptions).ToArray()) &&
                look_ahead[0].type == TokenType.ID)
            {
                local_variable_declaration();
                if (!pass(TokenType.END_STATEMENT))
                    throwError("end statement ';'");
                consumeToken();
            }
            else if(pass(embedded.Concat(unaryExpressionOptions).Concat(unaryOperatorOptions).
                Concat(literalOptions).ToArray()
                ))
            {
                embedded_statement();
            }
            else
            {
                throwError("local or embedded statement");
            }
        }

        private void embedded_statement()
        {
            DebugInfoMethod("embedded_statement");
            if (pass(TokenType.OPEN_CURLY_BRACKET, TokenType.END_STATEMENT))
            {
                maybe_empty_block();
            }else if (pass(unaryExpressionOptions.Concat(unaryOperatorOptions).Concat(literalOptions).ToArray())) 
            {
                statement_expression();
                if (!pass(TokenType.END_STATEMENT))
                    throwError("end statement ';'");
                consumeToken();
            }else if(pass(TokenType.RW_IF, TokenType.RW_SWITCH))
            {
                selection_statement();
            }else if(pass(TokenType.RW_WHILE, TokenType.RW_DO, TokenType.RW_FOR
                , TokenType.RW_FOREACH))
            {
                iteration_statement();
            }else if(pass(TokenType.RW_BREAK, TokenType.RW_CONTINUE, TokenType.RW_RETURN))
            {
                jump_statement();
                if (!pass(TokenType.END_STATEMENT))
                    throwError("end statement ';'");
                consumeToken();
            }
            else
            {
                throwError("block, statement, selection,iteration, or jump statement");
            }
        }
        private void local_variable_declaration()
        {
            DebugInfoMethod("local_variable_declaration");
            TokenType[] nuevo = { TokenType.RW_VAR };
            if (!pass(typesOptions.Concat(nuevo).ToArray()))
                throwError("a type");
            if (pass(TokenType.RW_VAR))
                consumeToken();
            else
                types();
            variable_declarator_list();
        } 
    }
}
