using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public  class ParenthesizedExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode expr;

        public ParenthesizedExpressionNode(ExpressionNode expr)
        {
            this.expr = expr;
        }
        public ParenthesizedExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            this.returnType = expr.evaluateType(api);
            return returnType;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append("(");
            expr.generateCode(builder, api);
            builder.Append(")");
        }
    }
}