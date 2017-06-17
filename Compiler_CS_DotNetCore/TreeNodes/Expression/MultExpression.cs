﻿using System;
using System.Text;
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
            rules[Utils.Int + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Char + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Int + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            rules[Utils.Int + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Float + "," + Utils.Int] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];

            rules[Utils.Float + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Float + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            rules[Utils.Char + "," + Utils.Float] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];

            rules[Utils.Char + "," + Utils.Char] = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}