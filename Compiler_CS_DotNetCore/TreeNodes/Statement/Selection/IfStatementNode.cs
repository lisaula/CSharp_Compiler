using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class IfStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode expr;
        public EmbeddedStatementNode body;
        public ElseStatementNode elseStatement;

        public IfStatementNode()
        {

        }

        public IfStatementNode(ExpressionNode expr, EmbeddedStatementNode body, ElseStatementNode elseStatement)
        {
            this.expr = expr;
            this.body = body;
            this.elseStatement = elseStatement;
        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}