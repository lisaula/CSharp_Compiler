namespace Compiler.Tree
{
    public abstract class ExpressionNode : VariableInitializer
    {
        public abstract TypeDefinitionNode evaluateType();
        public ExpressionNode()
        {

        }
    }
}