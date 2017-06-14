using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class EqualityExpression : ConditionExpression
    {

        public EqualityExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression): base(leftExpression, rightExpression, @operator)
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

            rules[Utils.Bool + "," + Utils.Bool] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.String + "," + Utils.String] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Class + "," + Utils.Class] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Enum + "," + Utils.Enum] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Class + "," + Utils.Null] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            rules[Utils.Null + "," + Utils.Class] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
        }
        public EqualityExpression()
        {

        }
    }
}