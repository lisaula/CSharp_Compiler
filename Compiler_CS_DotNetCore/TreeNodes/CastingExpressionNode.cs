namespace Compiler.Tree
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeDefinitionNode targetType;
        public ExpressionNode expression;

        public CastingExpressionNode(TypeDefinitionNode targetType, ExpressionNode expression)
        {
            this.targetType = targetType;
            this.expression = expression;
        }
        public CastingExpressionNode()
        {

        }
    }
}