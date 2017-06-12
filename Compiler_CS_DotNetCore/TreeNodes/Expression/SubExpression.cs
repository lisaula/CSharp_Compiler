using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class SubExpression : BinaryExpression
    {
        public SubExpression()
        {

        }
        public SubExpression(ExpressionNode leftExpression, ExpressionNode rightExpression) : base (leftExpression, rightExpression)
        {
        }
    }
}