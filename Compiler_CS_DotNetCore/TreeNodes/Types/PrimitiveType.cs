using Compiler.Tree;

namespace Compiler.Tree
{
    public abstract class PrimitiveType : TypeDefinitionNode
    {
        public ArrayNode arrayNode;
        public PrimitiveType(Token token)
        {
            this.identifier = new IdentifierNode( token);
        }
        public PrimitiveType()
        {

        }
    }
}