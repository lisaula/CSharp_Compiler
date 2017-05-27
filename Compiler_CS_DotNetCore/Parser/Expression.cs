using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compiler.Tree;
namespace Compiler
{
    public partial class Parser
    {
        private ExpressionNode expression()
        {
            TokenType[] nuevo = { TokenType.OP_TER_NULLABLE, TokenType.OP_COLON,
                TokenType.OP_NULLABLE, TokenType.OP_LOG_OR,
                TokenType.OP_LOG_AND, TokenType.OP_BIN_OR,
                TokenType.OP_BIN_XOR, TokenType.OP_BIN_AND,
                TokenType.OPEN_PARENTHESIS, TokenType.RW_NEW,
                TokenType.ID, TokenType.RW_THIS
            };
            DebugInfoMethod("expression");
            if (!pass(nuevo.Concat(equalityOperatorOptions).Concat(relationalOperatorOptions).
                Concat(Is_AsOperatorOptions).Concat(shiftOperatorOptions).Concat(additiveOperatorOptions).
                Concat(multiplicativeOperatorOptions).Concat(assignmentOperatorOptions).Concat(unaryOperatorOptions)
                .Concat(literalOptions).ToArray() ))
                throwError("Operator, identifier or literal in expression");
            conditional_expression();
            return new ExpressionNode("expresion");
        }

        private void conditional_expression()
        {
            DebugInfoMethod("conditional_expression");
            null_coalescing_expression();
            if (pass(TokenType.OP_TER_NULLABLE))
            {
                conditional_expression_p();
            }
        }

        private void conditional_expression_p()
        {
            DebugInfoMethod("conditional_expression_p");
            if (pass(TokenType.OP_TER_NULLABLE))
            {
                consumeToken();

                expression();

                if (!pass(TokenType.OP_COLON))
                    throwError("colon ':'");
                consumeToken();

                expression();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void null_coalescing_expression()
        {
            DebugInfoMethod("null_coalescing_expression");
            conditional_or_expression();
            if (pass(TokenType.OP_NULLABLE))
                null_coalescing_expression_p();
        }

        private void null_coalescing_expression_p()
        {
            DebugInfoMethod("null_coalescing_expression_p");
            if (pass(TokenType.OP_NULLABLE))
            {
                consumeToken();
                null_coalescing_expression();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void conditional_or_expression()
        {
            DebugInfoMethod("conditional_or_expression");
            conditional_and_expression();
            if(pass(TokenType.OP_LOG_OR))
               conditional_or_expression_p();
        }

        private void conditional_or_expression_p()
        {
            DebugInfoMethod("conditional_or_expression_p");
            if (pass(TokenType.OP_LOG_OR))
            {
                consumeToken();
                conditional_and_expression();
                if (pass(TokenType.OP_LOG_OR))
                    conditional_or_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void conditional_and_expression()
        {
            DebugInfoMethod("conditional_and_expression");
            inclusive_or_expression();
            if(pass(TokenType.OP_LOG_AND))
                conditional_and_expression_p();
        }

        private void conditional_and_expression_p()
        {
            DebugInfoMethod("conditional_and_expression_p");
            if (pass(TokenType.OP_LOG_AND))
            {
                consumeToken();
                inclusive_or_expression();
                if (pass(TokenType.OP_LOG_AND))
                    conditional_and_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void inclusive_or_expression()
        {
            DebugInfoMethod("inclusive_or_expression");
            exclusive_or_expression();
            if(pass(TokenType.OP_BIN_OR))
                inclusive_or_expression_p();
        }

        private void inclusive_or_expression_p()
        {
            DebugInfoMethod("inclusive_or_expression_p");
            if (pass(TokenType.OP_BIN_OR))
            {
                consumeToken();
                exclusive_or_expression();
                if (pass(TokenType.OP_BIN_OR))
                    inclusive_or_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void exclusive_or_expression()
        {
            DebugInfoMethod("exclusive_or_expression");
            and_expression();
            if (pass(TokenType.OP_BIN_XOR))
                exclusive_or_expression_p();
        }

        private void exclusive_or_expression_p()
        {
            DebugInfoMethod("exclusive_or_expression_p");
            if (pass(TokenType.OP_BIN_XOR))
            {
                consumeToken();
                and_expression();
                if (pass(TokenType.OP_BIN_XOR))
                    exclusive_or_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void and_expression()
        {
            DebugInfoMethod("and_expression");
            equality_expression();
            if(pass(TokenType.OP_BIN_AND))
                and_expression_p();
        }

        private void and_expression_p()
        {
            DebugInfoMethod("and_expression_p");
            if (pass(TokenType.OP_BIN_AND))
            {
                consumeToken();
                equality_expression();
                if (pass(TokenType.OP_BIN_AND))
                    and_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void equality_expression()
        {
            DebugInfoMethod("equality_expression");
            relational_expression();
            if (pass(equalityOperatorOptions))
                equality_expression_p();
        }

        private void equality_expression_p()
        {
            DebugInfoMethod("equality_expression_p");
            if (pass(equalityOperatorOptions))
            {
                consumeToken();
                relational_expression();
                if (pass(equalityOperatorOptions))
                    equality_expression_p();

            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void relational_expression()
        {
            DebugInfoMethod("relational_expression");
            shift_expression();
            if (pass(relationalOperatorOptions.Concat(Is_AsOperatorOptions).ToArray()))
                relational_expression_p();
        }

        private void relational_expression_p()
        {
            DebugInfoMethod("relational_expression_p");
            if (pass(relationalOperatorOptions))
            {
                consumeToken();
                shift_expression();
                if (pass(relationalOperatorOptions.Concat(Is_AsOperatorOptions).ToArray()))
                    relational_expression_p();
            }else if (pass(Is_AsOperatorOptions))
            {
                consumeToken();
                types();
                if (pass(relationalOperatorOptions.Concat(Is_AsOperatorOptions).ToArray()))
                    relational_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void shift_expression()
        {
            DebugInfoMethod("shift_expression");
            additive_expression();
            if (pass(shiftOperatorOptions))
                shift_expression_p();
        }

        private void shift_expression_p()
        {
            DebugInfoMethod("shift_expression_p");
            if (pass(shiftOperatorOptions))
            {
                consumeToken();
                additive_expression();
                if (pass(shiftOperatorOptions))
                    shift_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void additive_expression()
        {
            DebugInfoMethod("additive_expression");
            multiplicative_expression();
            if (pass(additiveOperatorOptions))
                additive_expression_p();
        }

        private void additive_expression_p()
        {
            DebugInfoMethod("additive_expression_p");
            if (pass(additiveOperatorOptions))
            {
                consumeToken();
                multiplicative_expression();
                if (pass(additiveOperatorOptions))
                    additive_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void multiplicative_expression()
        {
            DebugInfoMethod("multiplicative_expression");
            unary_expression();
            multiplicative_expression_factorized();
        }

        private void multiplicative_expression_factorized()
        {
            DebugInfoMethod("multiplicative_expression_factorized");
            if (pass(assignmentOperatorOptions))
            {
                consumeToken();
                expression();
                if (pass(multiplicativeOperatorOptions))
                    multiplicative_expression_p();
            }else
            {
                multiplicative_expression_p();
            }
        }

        private void multiplicative_expression_p()
        {
            DebugInfoMethod("multiplicative_expression_p");
            if (pass(multiplicativeOperatorOptions))
            {
                consumeToken();
                unary_expression();
                if (pass(multiplicativeOperatorOptions))
                    multiplicative_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }
    }
}
