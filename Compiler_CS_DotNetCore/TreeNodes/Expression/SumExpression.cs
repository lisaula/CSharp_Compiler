using System;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class SumExpression : BinaryExpression
    {

        public SumExpression(ExpressionNode leftExpression, ExpressionNode rightExpression, Token op) : base(leftExpression, rightExpression, op)
        {
            rules[Utils.Char + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];

            rules[Utils.Int + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Int + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Char + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];

            rules[Utils.Float + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Char + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Float + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];

            rules[Utils.Char + "," + Utils.String] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
            rules[Utils.String + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
            rules[Utils.String + "," + Utils.String] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
            rules[Utils.String + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
            rules[Utils.Int + "," + Utils.String] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
            rules[Utils.String + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
            rules[Utils.Float + "," + Utils.String] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
        }
        public SumExpression()
        {
        }
    }
}