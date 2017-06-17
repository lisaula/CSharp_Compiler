﻿using System;
using System.Text;

namespace Compiler.Tree
{
    public class ConditionExpression : BinaryExpression
    {
        public TypeDefinitionNode type;

        public ConditionExpression(ExpressionNode leftExpression, ExpressionNode rightExpression, Token op) : base (leftExpression, rightExpression, op)
        {
        }

        public ConditionExpression(ExpressionNode leftExpression, TypeDefinitionNode rightExpression)
        {
            this.leftExpression = leftExpression;
            this.type = rightExpression;
        }
        public ConditionExpression()
        {

        }

        public override string generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}