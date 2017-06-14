﻿using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public Token @operator;

        public PostAdditiveExpressionNode(PrimaryExpressionNode primary, Token @operator)
        {
            this.primary = primary;
            this.@operator = @operator;
        }
        public PostAdditiveExpressionNode()
        {

        }

        public PostAdditiveExpressionNode(Token @operator)
        {
            this.@operator = @operator;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode tdn = primary.evaluateType(api);
            if (!tdn.Equals(new IntType()) && !(tdn.Equals(new FloatType())) && !(tdn.Equals(new CharType())))
                throw new SemanticException("Invalid operation. Cant make post unary expression of a type '" + tdn.ToString() + "'.", @operator);
            return tdn;
        }
    }
}