﻿using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class CoalescingExpressionNode : ExpressionNode
    {
        public ExpressionNode conditionalExpression;
        public ExpressionNode nullCoalescing;

        public CoalescingExpressionNode(ExpressionNode conditionalExpression, ExpressionNode nullCoalescing)
        {
            this.conditionalExpression = conditionalExpression;
            this.nullCoalescing = nullCoalescing;
        }
        public CoalescingExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            throw new NotImplementedException();
        }
    }
}