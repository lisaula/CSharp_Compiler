using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class BinaryExpression : ExpressionNode
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
            leftExpression.returnType = t1;
            TypeDefinitionNode t2 = rightExpression.evaluateType(api);
            rightExpression.returnType = t2;
            string key = t1.getComparativeType() + "," + t2.getComparativeType();
            if (!rules.ContainsKey(key))
                throw new SemanticException("Invalid operation '"+@operator.ToString()+"'. Rule not supported '"+key+"'", @operator);
            this.returnType = rules[key];
            return rules[key];
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            api.checkExpression(this.returnType.getComparativeType(), leftExpression.returnType.getComparativeType(), builder);
            builder.Append("(");
            leftExpression.generateCode(builder, api);
            builder.Append(") ");

            builder.Append(@operator.lexema+" ");

            api.checkExpression(this.returnType.getComparativeType(), rightExpression.returnType.getComparativeType(), builder);
            builder.Append("(");
            rightExpression.generateCode(builder, api);
            builder.Append(")");
        }
    }
}