﻿using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class AssignmentNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public Token assigmentOperator;
        public ExpressionNode rightExpression;

        public AssignmentNode(ExpressionNode leftExpression, Token assigmentOperator, ExpressionNode rightExpression)
        {
            this.leftExpression = leftExpression;
            this.assigmentOperator = assigmentOperator;
            this.rightExpression = rightExpression;
        }
        public AssignmentNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            throw new NotImplementedException();
        }
    }
}