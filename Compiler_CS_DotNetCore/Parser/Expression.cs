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
                .Concat(literalOptions).Concat(primitiveTypes).ToArray() ))
                throwError("Operator, identifier or literal in expression");
            return conditional_expression();
        }

        private ExpressionNode conditional_expression()
        {
            DebugInfoMethod("conditional_expression");
            ExpressionNode expr =  null_coalescing_expression();    
            return conditional_expression_p(expr);
        }

        private ExpressionNode conditional_expression_p(ExpressionNode expr)
        {
            DebugInfoMethod("conditional_expression_p");
            if (pass(TokenType.OP_TER_NULLABLE))
            {
                consumeToken();

                var true_expression = expression();

                if (!pass(TokenType.OP_COLON))
                    throwError("colon ':'");
                consumeToken();

                var false_expression = expression();
                return new TernaryExpressionNode(expr, true_expression, false_expression);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return expr;
            }
        }

        private ExpressionNode null_coalescing_expression()
        {
            DebugInfoMethod("null_coalescing_expression");
            ExpressionNode conditionalOr = conditional_or_expression();
            return null_coalescing_expression_p(conditionalOr);
        }

        private ExpressionNode null_coalescing_expression_p(ExpressionNode conditionalOr)
        {
            DebugInfoMethod("null_coalescing_expression_p");
            if (pass(TokenType.OP_NULLABLE))
            {
                consumeToken();
                ExpressionNode nullCoalescing = null_coalescing_expression();
                return new CoalescingExpressionNode(conditionalOr, nullCoalescing);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return conditionalOr;
            }
        }

        private ExpressionNode conditional_or_expression()
        {
            DebugInfoMethod("conditional_or_expression");
            ExpressionNode conditionExpression = conditional_and_expression();
            return conditional_or_expression_p(conditionExpression);
        }

        private ExpressionNode conditional_or_expression_p(ExpressionNode condition)
        {
            DebugInfoMethod("conditional_or_expression_p");
            if (pass(TokenType.OP_LOG_OR))
            {
                var conditionalOperator = current_token;
                consumeToken();
                var conditionExpression = conditional_and_expression();
                
                return conditional_or_expression_p(new ConditionExpression(condition, conditionalOperator, conditionExpression));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return condition;
            }
        }

        private ExpressionNode conditional_and_expression()
        {
            DebugInfoMethod("conditional_and_expression");
            ExpressionNode binaryExpression = inclusive_or_expression();
            return conditional_and_expression_p(binaryExpression);
        }

        private ExpressionNode conditional_and_expression_p(ExpressionNode leftExpression)
        {
            DebugInfoMethod("conditional_and_expression_p");
            if (pass(TokenType.OP_LOG_AND))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression =inclusive_or_expression();
                
                return conditional_and_expression_p(new ConditionExpression(leftExpression, Operator, rightExpression));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftExpression;
            }
        }

        private ExpressionNode inclusive_or_expression()
        {
            DebugInfoMethod("inclusive_or_expression");
            ExpressionNode binaryExpression = exclusive_or_expression();
            return inclusive_or_expression_p(binaryExpression);
        }

        private ExpressionNode inclusive_or_expression_p(ExpressionNode leftExpression)
        {
            DebugInfoMethod("inclusive_or_expression_p");
            if (pass(TokenType.OP_BIN_OR))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression = exclusive_or_expression();
                return inclusive_or_expression_p(new BinaryExpression(leftExpression, Operator, rightExpression));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftExpression;
            }
        }

        private ExpressionNode exclusive_or_expression()
        {
            DebugInfoMethod("exclusive_or_expression");
            ExpressionNode andExpresion = and_expression();
            return exclusive_or_expression_p(andExpresion);
        }

        private ExpressionNode exclusive_or_expression_p(ExpressionNode LeftExpression)
        {
            DebugInfoMethod("exclusive_or_expression_p");
            if (pass(TokenType.OP_BIN_XOR))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression = and_expression();
                return exclusive_or_expression_p(new BinaryExpression(LeftExpression, Operator, rightExpression));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return LeftExpression;
            }
        }

        private ExpressionNode and_expression()
        {
            DebugInfoMethod("and_expression");
            ExpressionNode equalityExpression = equality_expression();
            return and_expression_p(equalityExpression);
        }

        private ExpressionNode and_expression_p(ExpressionNode leftExpression)
        {
            DebugInfoMethod("and_expression_p");
            if (pass(TokenType.OP_BIN_AND))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression = equality_expression();
                return and_expression_p(new BinaryExpression(leftExpression, Operator, rightExpression));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftExpression;
            }
        }

        private ExpressionNode equality_expression()
        {
            DebugInfoMethod("equality_expression");
            ExpressionNode relationalExpression = relational_expression();
            return equality_expression_p(relationalExpression);
        }

        private ExpressionNode equality_expression_p(ExpressionNode leftExpression)
        {
            DebugInfoMethod("equality_expression_p");
            if (pass(equalityOperatorOptions))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression = relational_expression();
                return equality_expression_p(new ConditionExpression(leftExpression, Operator, rightExpression));

            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftExpression;
            }
        }

        private ExpressionNode relational_expression()
        {
            DebugInfoMethod("relational_expression");
            ExpressionNode shiftExpression = shift_expression();
            return relational_expression_p(shiftExpression);
        }

        private ExpressionNode relational_expression_p(ExpressionNode leftExpression)
        {
            DebugInfoMethod("relational_expression_p");
            if (pass(relationalOperatorOptions))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression = shift_expression();
                return relational_expression_p(new ConditionExpression(leftExpression, Operator, rightExpression));
            }else if (pass(Is_AsOperatorOptions))
            {
                var Operator = current_token;
                consumeToken();
                TypeDefinitionNode type = types();
                if (Operator.type == TokenType.RW_AS)
                    return relational_expression_p(new CastingExpressionNode(type, leftExpression));
                else
                    return relational_expression_p(new ConditionExpression(leftExpression, Operator, type));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftExpression;
            }
        }

        private ExpressionNode shift_expression()
        {
            DebugInfoMethod("shift_expression");
            ExpressionNode additiveExpression = additive_expression();
            return shift_expression_p(additiveExpression);
        }

        private ExpressionNode shift_expression_p(ExpressionNode leftExpression)
        {
            DebugInfoMethod("shift_expression_p");
            if (pass(shiftOperatorOptions))
            {
                var Operator = current_token;
                consumeToken();
                var rigtExpression = additive_expression();
                return shift_expression_p(new BinaryExpression(leftExpression, Operator, rigtExpression));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftExpression;
            }
        }

        private ExpressionNode additive_expression()
        {
            DebugInfoMethod("additive_expression");
            ExpressionNode multiplicative = multiplicative_expression();
            return additive_expression_p(multiplicative);
        }

        private ExpressionNode additive_expression_p(ExpressionNode leftExpression)
        {
            DebugInfoMethod("additive_expression_p");
            if (pass(additiveOperatorOptions))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression = multiplicative_expression();
                return additive_expression_p(new ArithmeticExpression(leftExpression, Operator, rightExpression));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftExpression;
            }
        }

        private ExpressionNode multiplicative_expression()
        {
            DebugInfoMethod("multiplicative_expression");
            ExpressionNode unary = unary_expression();
            return multiplicative_expression_factorized(unary);
        }

        private ExpressionNode multiplicative_expression_factorized(ExpressionNode unary)
        {
            DebugInfoMethod("multiplicative_expression_factorized");
            if (pass(assignmentOperatorOptions))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression = expression();
                return multiplicative_expression_p(new AssignmentNode(unary, Operator, rightExpression));
            }else
            {
                return multiplicative_expression_p(unary);
            }
        }

        private ExpressionNode multiplicative_expression_p(ExpressionNode leftExpression)
        {
            DebugInfoMethod("multiplicative_expression_p");
            if (pass(multiplicativeOperatorOptions))
            {
                var Operator = current_token;
                consumeToken();
                var rightExpression = unary_expression();
                return multiplicative_expression_p(new ArithmeticExpression(leftExpression, Operator, rightExpression));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return leftExpression;
            }
        }
    }
}
