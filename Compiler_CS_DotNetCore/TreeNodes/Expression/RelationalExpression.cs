using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class RelationalExpression : ConditionExpression
    {

        public RelationalExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression):base(leftExpression, rightExpression, @operator)
        {
            rules[Utils.Char + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Char + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Int + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Char + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Float + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];

            rules[Utils.Int + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Int + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Float + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];

            rules[Utils.Float + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
        }
        public RelationalExpression()
        {

        }
    }
}