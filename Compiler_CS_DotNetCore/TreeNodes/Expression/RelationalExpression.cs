using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class RelationalExpression : ConditionExpression
    {

        public RelationalExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression):base(leftExpression, rightExpression, @operator)
        {
            rules[Utils.Char + "," + Utils.Char] = new BoolType();
            rules[Utils.Char + "," + Utils.Int] = new BoolType();
            rules[Utils.Int + "," + Utils.Char] = new BoolType();
            rules[Utils.Char + "," + Utils.Float] = new BoolType();
            rules[Utils.Float + "," + Utils.Char] = new BoolType();

            rules[Utils.Int + "," + Utils.Int] = new BoolType();
            rules[Utils.Int + "," + Utils.Float] = new BoolType();
            rules[Utils.Float + "," + Utils.Int] = new BoolType();

            rules[Utils.Float + "," + Utils.Float] = new BoolType();
        }
        public RelationalExpression()
        {

        }
    }
}