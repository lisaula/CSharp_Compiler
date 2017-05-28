using System.Collections.Generic;

namespace Compiler.Tree
{
    public class NamespaceNode
    {
        public List<IdentifierNode> identifierList;
        public List<UsingNode> usingList;
        public List<NamespaceNode> namespaceList;
        public List<TypeDefinitionNode> typeList;
        public NamespaceNode(List<IdentifierNode> identifier)
        {
            this.identifierList = identifier;
        }
        public NamespaceNode()
        {

        }
    }
}