using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class SwitchStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode constantExpression;
        public List<CaseStatementNode> cases;

        public SwitchStatementNode(ExpressionNode constantExpression, List<CaseStatementNode> cases)
        {
            this.constantExpression = constantExpression;
            this.cases = cases;
        }
        public SwitchStatementNode()
        {

        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}