using System.Collections.Generic;

namespace Compiler.Tree
{
    public class IdentifierTypeNode : TypeDefinitionNode
    {
        public IdentifierTypeNode()
        {
        }
        public ArrayNode arrayNode;
        public List<IdentifierNode> Identifiers { get; internal set; }
    }
}