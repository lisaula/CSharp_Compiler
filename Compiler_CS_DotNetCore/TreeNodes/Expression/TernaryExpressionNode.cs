using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class TernaryExpressionNode : ExpressionNode
    {
        public ExpressionNode conditional_expression;
        public ExpressionNode true_expression;
        public ExpressionNode false_expression;

        public TernaryExpressionNode(ExpressionNode expr, ExpressionNode true_expression, ExpressionNode false_expression)
        {
            this.conditional_expression = expr;
            this.true_expression = true_expression;
            this.false_expression = false_expression;
        }
        public TernaryExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            if (conditional_expression is ConditionExpression)
            {
                TypeDefinitionNode t1 = conditional_expression.evaluateType(api);
                if (t1.Equals(new BoolType()))
                {
                    TypeDefinitionNode ttrue = true_expression.evaluateType(api);
                    TypeDefinitionNode tfalse = false_expression.evaluateType(api);
                    if (ttrue.Equals(tfalse))
                        return ttrue;
                    throw new SemanticException("Cannot explicitly convert " + ttrue.ToString() + " to " + tfalse.ToString() + " in ternary expression.", ttrue.identifier.token);
                }
                throw new SemanticException("Condition expression does not returns a bool in ternary expression.", t1.identifier.token);
            }
            TypeDefinitionNode t = conditional_expression.evaluateType(api);
            throw new SemanticException("Not a condition expression in ternary expression.",t.getPrimaryToken());
        }
    }
}