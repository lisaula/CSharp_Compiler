using System.Collections.Generic;

namespace Compiler.Tree
{
    public class NamespaceNode
    {
        private IdenfierNode identifier;
        public List<UsingNode> usingList;
        public List<NamespaceNode> namespaceList;
        public NamespaceNode(IdenfierNode identifier)
        {
            this.identifier = identifier;
        }
    }
}