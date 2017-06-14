using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class ElseStatementNode : EmbeddedStatementNode
    {
        public EmbeddedStatementNode body;

        public ElseStatementNode()
        {

        }

        public ElseStatementNode(EmbeddedStatementNode body)
        {
            this.body = body;
        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}