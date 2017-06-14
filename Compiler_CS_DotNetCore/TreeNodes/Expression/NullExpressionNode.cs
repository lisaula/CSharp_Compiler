using System;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler
{
    internal class NullExpressionNode : PrimaryExpressionNode
    {
        public TypeDefinitionNode type;

        public NullExpressionNode(TypeDefinitionNode type)
        {
            this.type = type;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return type;
        }
    }
}