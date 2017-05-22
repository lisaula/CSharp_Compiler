using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void jump_statement()
        {
            DebugInfoMethod("jump_statement");
            if(pass(TokenType.RW_BREAK, TokenType.RW_CONTINUE))
            {
                consumeToken();
            }else if (pass(TokenType.RW_RETURN))
            {
                consumeToken();
                optional_expresion();
            }
        }

        private void optional_expresion()
        {
            DebugInfoMethod("optional_expresion");
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
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void iteration_statement()
        {
            DebugInfoMethod("iteration_statement");
            if (pass(TokenType.RW_WHILE))
            {
                while_statement();
            }else if (pass(TokenType.RW_DO))
            {
                do_statement();
            }else if (pass(TokenType.RW_FOR))
            {
                for_statement();
            }else if (pass(TokenType.RW_FOREACH))
            {
                foreach_statement();
            }else
            {
                throwError("while, do, for or foreach");
            }
        }

        private void foreach_statement()
        {
            DebugInfoMethod("foreach_statement");
            if (!pass(TokenType.RW_FOREACH))
                throwError("foreach");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            if (pass(TokenType.RW_VAR))
                consumeToken();
            else
                types();

            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();

            if (!pass(TokenType.RW_IN))
                throwError("in");
            consumeToken();

            expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            embedded_statement();
        }

        private void for_statement()
        {
            DebugInfoMethod("for_statement");
            if (!pass(TokenType.RW_FOR))
                throwError("for");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            optional_for_initializer();

            if (!pass(TokenType.END_STATEMENT))
                throwError("end statement ';'");
            consumeToken();

            optional_expresion();

            if (!pass(TokenType.END_STATEMENT))
                throwError("end statement ';'");
            consumeToken();

            optional_statement_expression_list();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            embedded_statement();
        }

        private void optional_statement_expression_list()
        {
            DebugInfoMethod("optional_statement_expression_list");
            if (pass(unaryExpressionOptions.Concat(unaryOperatorOptions).Concat(literalOptions).ToArray()))
            {
                statement_expression_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void statement_expression_list()
        {
            DebugInfoMethod("statement_expression_list");
            statement_expression();
            statement_expression_list_p();
        }

        private void statement_expression_list_p()
        {
            DebugInfoMethod("statement_expression_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                statement_expression();
                statement_expression_list_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void optional_for_initializer()
        {
            TokenType[] var = { TokenType.RW_VAR };
                DebugInfoMethod("optional_for_initializer");
            addLookAhead(lexer.getNextToken());
            int look_ahead_index = look_ahead.Count()-1;
            if (pass(typesOptions.Concat(var).ToArray()) && 
                (look_ahead[look_ahead_index].type == TokenType.ID
                || look_ahead[look_ahead_index].type == TokenType.OPEN_SQUARE_BRACKET
                || look_ahead[look_ahead_index].type == TokenType.OP_DOT))
            {
                local_variable_declaration();
            }else if (pass(unaryExpressionOptions.Concat(unaryOperatorOptions).Concat(literalOptions).ToArray()))
            {
                statement_expression_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void do_statement()
        {
            DebugInfoMethod("do_statement");
            if (!pass(TokenType.RW_DO))
                throwError("do");
            consumeToken();
            embedded_statement();

            if (!pass(TokenType.RW_WHILE))
                throwError("while");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            if (!pass(TokenType.END_STATEMENT))
                throwError("end statement ';'");
            consumeToken();
        }

        private void while_statement()
        {
            DebugInfoMethod("while_statement");
            if (!pass(TokenType.RW_WHILE))
                throwError("while");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();
            embedded_statement();
        }

        private void selection_statement()
        {
            DebugInfoMethod("selection_statement");
            if (pass(TokenType.RW_IF))
                if_statement();
            else if (pass(TokenType.RW_SWITCH))
                switch_statement();
            else
            {
                throwError("if, or switch");
            }
        }

        private void switch_statement()
        {
            DebugInfoMethod("switch_statement");
            if (!pass(TokenType.RW_SWITCH))
                throwError("switch");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            optional_switch_section_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
        }

        private void optional_switch_section_list()
        {
            DebugInfoMethod("optional_switch_section_list");
            if(pass(TokenType.RW_CASE, TokenType.RW_DEFAULT))
            {
                switch_label_list();
                statement_list();
                optional_switch_section_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void switch_label_list()
        {
            DebugInfoMethod("switch_label_list");
            switch_label();

            if (!pass(TokenType.OP_COLON))
                throwError("colon ':'");
            consumeToken();

            switch_label_list_p();
        }

        private void switch_label_list_p()
        {
            DebugInfoMethod("switch_label_list_p");
            if (pass(TokenType.RW_CASE, TokenType.RW_DEFAULT))
            {
                switch_label_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void switch_label()
        {
            DebugInfoMethod("switch_label");
            if (pass(TokenType.RW_CASE))
            {
                consumeToken();
                expression();
            }else if (pass(TokenType.RW_DEFAULT))
            {
                consumeToken();
            }
            else
            {
                throwError("case or default");
            }
        }

        private void if_statement()
        {
            DebugInfoMethod("if_statement");
            if (!pass(TokenType.RW_IF))
                throwError("if");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();
            embedded_statement();

            if (pass(TokenType.RW_ELSE))
            {
                optional_else_part();
            }
        }

        private void optional_else_part()
        {
            DebugInfoMethod("optional_else_part");
            if (pass(TokenType.RW_ELSE))
            {
                consumeToken();
                embedded_statement();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void statement_expression()
        {
            DebugInfoMethod("statement_expression");
            unary_expression();
            statement_expression_factorized();
        }

        private void statement_expression_factorized()
        {
            DebugInfoMethod("statement_expression_factorized");
            if (pass(assignmentOperatorOptions))
            {
                consumeToken();
                expression();
                statement_expression_p();
            }
            else
            {
                statement_expression_p();
            }
        }

        private void statement_expression_p()
        {
            DebugInfoMethod("statement_expression_p");
            if (pass(TokenType.OPEN_PARENTHESIS))
            {
                consumeToken();

                argument_list();

                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                consumeToken();
            }
            else if(pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT))
            {
                consumeToken();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }
    }
}
