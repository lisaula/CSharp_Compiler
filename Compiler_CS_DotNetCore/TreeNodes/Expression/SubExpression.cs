using System;
using System.Text;
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
            rules[Utils.Int + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Int + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Char + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];

            rules[Utils.Char + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];

            rules[Utils.Float + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Int + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Float + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Float + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Char + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}