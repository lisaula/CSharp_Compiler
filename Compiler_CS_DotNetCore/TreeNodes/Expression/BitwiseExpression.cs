using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class BitwiseExpression : BinaryExpression
    {

        public BitwiseExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression): base (leftExpression, rightExpression, @operator)
        {
            rules[Utils.Char + "," + Utils.Char] = new IntType();
            rules[Utils.Char + "," + Utils.Int] = new IntType();
            rules[Utils.Int + "," + Utils.Char] = new IntType();
            rules[Utils.Int + "," + Utils.Int] = new IntType();
        }
        public BitwiseExpression()
        {

        }
    }
}