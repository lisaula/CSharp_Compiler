using System.Collections.Generic;
using Compiler.Tree;

namespace Compiler.Tree
{
    public class InlineExpressionNode : UnaryExpressionNode
    {
        public List<ExpressionNode> list;

        public InlineExpressionNode(List<ExpressionNode> list)
        {
            this.list = list;
        }

        public InlineExpressionNode()
        {

        }
    }
}