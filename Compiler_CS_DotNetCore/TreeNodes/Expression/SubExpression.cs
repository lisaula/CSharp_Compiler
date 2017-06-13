using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class SubExpression : BinaryExpression
    {
        public SubExpression()
        {

        }
        public SubExpression(ExpressionNode leftExpression, ExpressionNode rightExpression,Token op) : base (leftExpression, rightExpression,op)
        {
            rules[Utils.Int + "," + Utils.Int] = new IntType();
            rules[Utils.Int + "," + Utils.Char] = new IntType();
            rules[Utils.Char + "," + Utils.Int] = new IntType();

            rules[Utils.Char + "," + Utils.Char] = new IntType();

            rules[Utils.Float + "," + Utils.Int] = new FloatType();
            rules[Utils.Int + "," + Utils.Float] = new FloatType();
            rules[Utils.Float + "," + Utils.Float] = new FloatType();
            rules[Utils.Float + "," + Utils.Char] = new FloatType();
            rules[Utils.Char + "," + Utils.Float] = new FloatType();
        }
    }
}