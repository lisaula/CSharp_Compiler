using System;
using System.Text;
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

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(Utils.EndLine);
            expression.generateCode(builder, api);
        }
    }
}