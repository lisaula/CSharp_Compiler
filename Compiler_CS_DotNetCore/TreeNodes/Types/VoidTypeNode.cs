using System;

namespace Compiler.Tree
{
    public class VoidTypeNode : TypeDefinitionNode
    {
        public VoidTypeNode(Token token_type)
        {
            this.identifier =new IdentifierNode( token_type);
        }
        public VoidTypeNode()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}