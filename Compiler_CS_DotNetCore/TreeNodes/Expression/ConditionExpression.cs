using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public abstract class ConditionExpression : BinaryExpression
    {

        public ConditionExpression(ExpressionNode leftExpression, ExpressionNode rightExpression, Token op) : base (leftExpression, rightExpression, op)
        {
        }

        public ConditionExpression()
        {

        }
    }
}