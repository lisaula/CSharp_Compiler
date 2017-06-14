using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class WhileStatementNode: EmbeddedStatementNode
    {
        public ExpressionNode conditionExpression;
        public EmbeddedStatementNode body;

        public WhileStatementNode()
        {

        }

        public WhileStatementNode(ExpressionNode conditionExpression, EmbeddedStatementNode body)
        {
            this.conditionExpression = conditionExpression;
            this.body = body;
        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}