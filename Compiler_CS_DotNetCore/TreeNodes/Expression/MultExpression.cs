using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class MultExpression : BinaryExpression
    {
        public MultExpression()
        {

        }
        public MultExpression(ExpressionNode unary, ExpressionNode rightExpression, Token op) : base (unary, rightExpression, op)
        {
            rules[Utils.Int + "," + Utils.Int] = new IntType();
            rules[Utils.Char + "," + Utils.Int] = new IntType();
            rules[Utils.Int + "," + Utils.Char] = new IntType();
            rules[Utils.Int + "," + Utils.Float] = new FloatType();
            rules[Utils.Float + "," + Utils.Int] = new FloatType();

            rules[Utils.Float + "," + Utils.Float] = new FloatType();
            rules[Utils.Float + "," + Utils.Char] = new FloatType();
            rules[Utils.Char + "," + Utils.Float] = new FloatType();

            rules[Utils.Char + "," + Utils.Char] = new IntType();
        }
    }
}