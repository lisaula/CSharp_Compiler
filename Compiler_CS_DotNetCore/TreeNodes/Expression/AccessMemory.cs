using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

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
            this.returnType = expression.evaluateType(api);
            return returnType;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(".");
            expression.generateCode(builder, api);
        }
    }
}