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

        public override string ToString()
        {
            List<string> name = new List<string>();
            foreach(IdentifierNode id in identifierList)
            {
                name.Add(id.token.lexema);
            }
            return string.Join(".",name);
        }
    }
}