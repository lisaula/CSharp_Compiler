using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

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
            if (tdn.getComparativeType() != Utils.Int && tdn.getComparativeType() != Utils.Float && tdn.getComparativeType() != Utils.Char)
                throw new SemanticException("Invalid operation. Cant make post unary expression of a type '" + tdn.ToString() + "'.", @operator);
            this.returnType = tdn;
            return tdn;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            primary.generateCode(builder, api);
            builder.Append(@operator.lexema);
        }
    }
}