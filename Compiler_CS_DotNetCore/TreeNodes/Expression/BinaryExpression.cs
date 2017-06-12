using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public abstract class BinaryExpression : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode rightExpression;
        protected Dictionary<string, TypeDefinitionNode> rules = new Dictionary<string, TypeDefinitionNode>();
        public BinaryExpression(ExpressionNode leftExpression, ExpressionNode rightExpression)
        {
            this.leftExpression = leftExpression;
            this.rightExpression = rightExpression;
        }
        public BinaryExpression()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            throw new NotImplementedException();
        }
    }
}