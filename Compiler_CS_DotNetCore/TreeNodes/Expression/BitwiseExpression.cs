using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class BitwiseExpression : BinaryExpression
    {

        public BitwiseExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression): base (leftExpression, rightExpression, @operator)
        {
            rules[Utils.Char + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Char + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Int + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Int + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
        }
        public BitwiseExpression()
        {

        }
    }
}