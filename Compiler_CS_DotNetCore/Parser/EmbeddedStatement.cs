using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compiler.Tree;
namespace Compiler
{
    public partial class Parser
    {
        private JumpStatementNode jump_statement()
        {
            DebugInfoMethod("jump_statement");
            if(pass(TokenType.RW_BREAK, TokenType.RW_CONTINUE))
            {
                var token = current_token;
                consumeToken();
                return new JumpStatementNode(token);
            }else if (pass(TokenType.RW_RETURN))
            {
                var token = current_token;
                consumeToken();
                var expression = optional_expresion();
                return new JumpStatementNode(token, expression);
            }
            else
            {
                throwError("break, continue or return statement");
                return null;
            }
        }

        private ExpressionNode optional_expresion()
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
                return expression();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private EmbeddedStatementNode iteration_statement()
        {
            DebugInfoMethod("iteration_statement");
            if (pass(TokenType.RW_WHILE))
            {
                return while_statement();
            }else if (pass(TokenType.RW_DO))
            {
                return do_statement();
            }else if (pass(TokenType.RW_FOR))
            {
                return for_statement();
            }else if (pass(TokenType.RW_FOREACH))
            {
                return foreach_statement();
            }else
            {
                throwError("while, do, for or foreach");
                return null;
            }
        }

        private ForeachStatementNode foreach_statement()
        {
            DebugInfoMethod("foreach_statement");
            if (!pass(TokenType.RW_FOREACH))
                throwError("foreach");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            TypeDefinitionNode type = null; 
            if (pass(TokenType.RW_VAR))
            {
                var token = current_token;
                consumeToken();
                type = new VarType(token);
            }
            else
            {
                type  = types();
            }

            if (!pass(TokenType.ID))
                throwError("identifier");
            var id = new IdentifierNode(current_token);
            consumeToken();

            if (!pass(TokenType.RW_IN))
                throwError("in");
            consumeToken();

            var collection = expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            var body = embedded_statement();
            return new ForeachStatementNode(type, id, collection, body);
        }

        private ForStatementNode for_statement()
        {
            DebugInfoMethod("for_statement");
            if (!pass(TokenType.RW_FOR))
                throwError("for");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            var initializer = optional_for_initializer();

            if (!pass(TokenType.END_STATEMENT))
                throwError("end statement ';'");
            consumeToken();

            var expresion = optional_expresion();

            if (!pass(TokenType.END_STATEMENT))
                throwError("end statement ';'");
            consumeToken();

            var iterative = optional_statement_expression_list();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            var body = embedded_statement();
            return new ForStatementNode(initializer, expresion, iterative, body);
        }

        private List<Statement> optional_statement_expression_list()
        {
            DebugInfoMethod("optional_statement_expression_list");
            if (pass(unaryExpressionOptions.Concat(unaryOperatorOptions).Concat(literalOptions).ToArray()))
            {
                return statement_expression_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private List<Statement> statement_expression_list()
        {
            DebugInfoMethod("statement_expression_list");
            var stmt = statement_expression();
            var lista = statement_expression_list_p();
            lista.Insert(0, stmt);
            return lista;
        }

        private List<Statement> statement_expression_list_p()
        {
            DebugInfoMethod("statement_expression_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                return statement_expression_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<Statement>();
            }
        }

        private List<Statement> optional_for_initializer()
        {
            TokenType[] var = { TokenType.RW_VAR };
                DebugInfoMethod("optional_for_initializer");
            /*addLookAhead(lexer.getNextToken());
            int look_ahead_index = look_ahead.Count()-1;
            addLookAhead(lexer.getNextToken());
            int look_ahead_index2 = look_ahead.Count() - 1;
            if (pass(var.Concat(typesOptions).ToArray()) &&
                (look_ahead[look_ahead_index].type == TokenType.ID
                || look_ahead[look_ahead_index].type == TokenType.OPEN_SQUARE_BRACKET
                || look_ahead[look_ahead_index].type == TokenType.OP_DOT
                || look_ahead[look_ahead_index].type == TokenType.OP_LESS_THAN) && !literalOptions.Contains(look_ahead[look_ahead_index2].type))
            {*/
            //addLookAhead(lexer.getNextToken());
            //int first = look_ahead.Count() - 1;
            while (pass(typesOptions.Concat(var).ToArray()))
            {
                addLookAhead(lexer.getNextToken());
                if (look_ahead[look_ahead.Count() - 1].type == TokenType.OP_DOT)
                {
                    addLookAhead(lexer.getNextToken());
                }
                else
                    break;
            }
            int index;
            int index2 = 0;
            Token placeholder = current_token;
            if (pass(typesOptions.Concat(var).ToArray()))
            {
                index = look_ahead.Count() - 1;
                placeholder = look_ahead[index];
                addLookAhead(lexer.getNextToken());
                index2 = look_ahead.Count() - 1;
                DebugInfoMethod("PH: " + placeholder.type + " " + look_ahead[index2].type);
            }
            if (
                (pass(typesOptions.Concat(var).ToArray()) &&
                (placeholder.type == TokenType.ID
                || placeholder.type == TokenType.OP_LESS_THAN
                ||
                (placeholder.type == TokenType.OPEN_SQUARE_BRACKET
                && (look_ahead[index2].type == TokenType.CLOSE_SQUARE_BRACKET
                || look_ahead[index2].type == TokenType.OP_COMMA))))
               ) {
                var localVariable = local_variable_declaration();
                var lista = new List<Statement>();
                lista.Add(localVariable);
                return lista;
            }else if (pass(unaryExpressionOptions.Concat(unaryOperatorOptions).Concat(literalOptions).ToArray()))
            {
                return statement_expression_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private DoStatementNode do_statement()
        {
            DebugInfoMethod("do_statement");
            if (!pass(TokenType.RW_DO))
                throwError("do");
            consumeToken();
            var body = embedded_statement();

            if (!pass(TokenType.RW_WHILE))
                throwError("while");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            var conditionExpression = expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            if (!pass(TokenType.END_STATEMENT))
                throwError("end statement ';'");
            consumeToken();
            return new DoStatementNode(body, conditionExpression);
        }

        private WhileStatementNode while_statement()
        {
            DebugInfoMethod("while_statement");
            if (!pass(TokenType.RW_WHILE))
                throwError("while");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            var conditionExpression = expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();
            var body = embedded_statement();
            return new WhileStatementNode(conditionExpression, body);
        }

        private EmbeddedStatementNode selection_statement()
        {
            DebugInfoMethod("selection_statement");
            if (pass(TokenType.RW_IF))
                return if_statement();
            else if (pass(TokenType.RW_SWITCH))
                return switch_statement();
            else
            {
                throwError("if, or switch");
                return null;
            }
        }

        private SwitchStatementNode switch_statement()
        {
            DebugInfoMethod("switch_statement");
            if (!pass(TokenType.RW_SWITCH))
                throwError("switch");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            var constantExpression =expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            var cases = optional_switch_section_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
            return new SwitchStatementNode(constantExpression, cases);
        }

        private List<CaseStatementNode> optional_switch_section_list()
        {
            DebugInfoMethod("optional_switch_section_list");
            if(pass(TokenType.RW_CASE, TokenType.RW_DEFAULT))
            {
                var caseLabel =  switch_label_list();
                var body = statement_list();
                var lista = optional_switch_section_list();
                lista.Insert(0, new CaseStatementNode(caseLabel, body));
                return lista;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<CaseStatementNode>();
            }
        }

        private List<CaseLabel> switch_label_list()
        {
            DebugInfoMethod("switch_label_list");
             var caseLabel = switch_label();

            if (!pass(TokenType.OP_COLON))
                throwError("colon ':'");
            consumeToken();

            var lista = switch_label_list_p();
            lista.Insert(0, caseLabel);
            return lista;
        }

        private List<CaseLabel> switch_label_list_p()
        {
            DebugInfoMethod("switch_label_list_p");
            if (pass(TokenType.RW_CASE, TokenType.RW_DEFAULT))
            {
                return switch_label_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<CaseLabel>();
            }
        }

        private CaseLabel switch_label()
        {
            DebugInfoMethod("switch_label");
            if (pass(TokenType.RW_CASE))
            {
                var token = current_token;
                consumeToken();
                var expr= expression();
                return new CaseLabel(token, expr);
            }else if (pass(TokenType.RW_DEFAULT))
            {
                var token = current_token;
                consumeToken();
                return new CaseLabel(token);
            }
            else
            {
                throwError("case or default");
                return null;
            }
        }

        private IfStatementNode if_statement()
        {
            DebugInfoMethod("if_statement");
            if (!pass(TokenType.RW_IF))
                throwError("if");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            var expr = expression();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();
            var body = embedded_statement();
            ElseStatementNode elseStatement = null;
            if (pass(TokenType.RW_ELSE))
            {
                elseStatement = optional_else_part();
            }
            return new IfStatementNode(expr, body, elseStatement);
        }

        private ElseStatementNode optional_else_part()
        {
            DebugInfoMethod("optional_else_part");
            if (pass(TokenType.RW_ELSE))
            {
                consumeToken();
                var Body = embedded_statement();
                return new ElseStatementNode(Body);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private StatementExpressionNode statement_expression()
        {
            DebugInfoMethod("statement_expression");
            var unary = unary_expression();
            return new StatementExpressionNode(statement_expression_factorized(unary));
        }

        private ExpressionNode statement_expression_factorized(UnaryExpressionNode leftValue)
        {
            DebugInfoMethod("statement_expression_factorized");
            if (pass(assignmentOperatorOptions))
            {
                var assigmentOperator = current_token;
                consumeToken();
                var expr = expression();
                return new AssignmentNode(leftValue, assigmentOperator, expr);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftValue;
            }
        }
    }
}
