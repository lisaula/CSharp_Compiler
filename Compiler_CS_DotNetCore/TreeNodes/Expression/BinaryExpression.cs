using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public abstract class BinaryExpression : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode rightExpression;
        public Token @operator;
        protected Dictionary<string, TypeDefinitionNode> rules = new Dictionary<string, TypeDefinitionNode>();
        public BinaryExpression(ExpressionNode leftExpression, ExpressionNode rightExpression, Token Operator)
        {
            this.leftExpression = leftExpression;
            this.rightExpression = rightExpression;
            this.@operator = Operator;
        }
        public BinaryExpression()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode t1 = leftExpression.evaluateType(api);
            TypeDefinitionNode t2 = rightExpression.evaluateType(api);
            string key = t1.getComparativeType() + "," + t2.getComparativeType();
            if (!rules.ContainsKey(key))
                throw new SemanticException("Invalid operation '"+@operator.ToString()+"'. Rule not supported '"+key+"'", @operator);
            return rules[key];
        }
    }
}