using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class AccessMemory : PrimaryExpressionNode
    {
        public PrimaryExpressionNode expression;

        public AccessMemory()
        {
            
        }

        public AccessMemory(PrimaryExpressionNode id)
        {
            this.expression = id;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return expression.evaluateType(api);
        }
    }
}