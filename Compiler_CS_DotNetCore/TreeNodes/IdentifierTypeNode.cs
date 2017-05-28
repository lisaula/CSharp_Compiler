using System.Collections.Generic;

namespace Compiler.Tree
{
    public class IdentifierTypeNode : TypeDefinitionNode
    {
        public IdentifierTypeNode()
        {
        }

        public IdentifierTypeNode(List<IdentifierNode> list)
        {
            this.Identifiers = list;
        }

        public ArrayNode arrayNode;
        public List<IdentifierNode> Identifiers { get; internal set; }
    }
}