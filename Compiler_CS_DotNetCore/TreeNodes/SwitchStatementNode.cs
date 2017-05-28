using System.Collections.Generic;

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
    }
}