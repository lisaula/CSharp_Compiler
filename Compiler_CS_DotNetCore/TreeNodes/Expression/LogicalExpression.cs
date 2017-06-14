using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class LogicalExpression : ConditionExpression
    {
        public LogicalExpression(ExpressionNode condition, Token Operator, ExpressionNode conditionExpression) : base(condition, conditionExpression, Operator)
        { 
            rules[Utils.Bool + "," + Utils.Bool] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
        }
        public LogicalExpression()
        {
        }
    }
}