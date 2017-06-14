using System;
using System.Collections.Generic;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;

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
            list = new List<ExpressionNode>();
        }

        public ExpressionNode getFirstExpressionType()
        {
            if(list != null)
            {
                return list[0];
            }
            return null;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode t = null;
            foreach(ExpressionNode exp in list)
            {
                t = exp.evaluateType(api);
            }
            return t;
        }
    }
}