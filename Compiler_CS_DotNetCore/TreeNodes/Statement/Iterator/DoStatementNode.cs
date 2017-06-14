using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public  class DoStatementNode : EmbeddedStatementNode
    {
        public EmbeddedStatementNode body;
        public ExpressionNode conditionExpression;

        public DoStatementNode()
        {

        }

        public DoStatementNode(EmbeddedStatementNode body, ExpressionNode conditionExpression)
        {
            this.body = body;
            this.conditionExpression = conditionExpression;
        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }

}