using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeDefinitionNode targetType;
        public List<ExpressionNode> primary;

        public CastingExpressionNode(TypeDefinitionNode targetType, List<ExpressionNode> primary)
        {
            this.targetType = targetType;
            this.primary = primary;
        }
        public CastingExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            throw new NotImplementedException();
        }
    }
}