using Compiler.Tree;

namespace Compiler.Tree
{
    public abstract class PrimitiveType : TypeDefinitionNode
    {
        public PrimitiveType(Token token)
        {
            this.identifier = new IdentifierNode( token);
        }
        public PrimitiveType()
        {

        }
        public PrimitiveType(TypeDefinitionNode t)
        {
            this.typeNode = t;
        }
    }
}