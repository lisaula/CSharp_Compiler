using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class StatementExpressionNode: EmbeddedStatementNode
    {
        public ExpressionNode expression;
        public StatementExpressionNode()
        {

        }
        public StatementExpressionNode(ExpressionNode expression)
        {
            this.expression = expression;
        }

        public override void evaluate(API api)
        {
            expression.evaluateType(api);
        }
    }
}