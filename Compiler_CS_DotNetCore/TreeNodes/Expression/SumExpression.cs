using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class SumExpression : BinaryExpression
    {

        public SumExpression(ExpressionNode leftExpression, ExpressionNode rightExpression, Token op) : base(leftExpression, rightExpression, op)
        {
            rules[Utils.Char + "," + Utils.Char] = new IntType();

            rules[Utils.Int + "," + Utils.Int] = new IntType();
            rules[Utils.Int + "," + Utils.Char] = new IntType();
            rules[Utils.Char + "," + Utils.Int] = new IntType();

            rules[Utils.Float + "," + Utils.Float] = new FloatType();
            rules[Utils.Char + "," + Utils.Float] = new FloatType();
            rules[Utils.Float + "," + Utils.Char] = new FloatType();

            rules[Utils.Char + "," + Utils.String] = new StringType();
            rules[Utils.String + "," + Utils.Char] = new StringType();
            rules[Utils.String + "," + Utils.String] = new StringType();
            rules[Utils.String + "," + Utils.Int] = new StringType();
            rules[Utils.Int + "," + Utils.String] = new StringType();
            rules[Utils.String + "," + Utils.Float] = new StringType();
            rules[Utils.Float + "," + Utils.String] = new StringType();
        }
        public SumExpression()
        {

        }
    }
}