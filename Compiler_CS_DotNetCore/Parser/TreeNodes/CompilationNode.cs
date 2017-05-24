using System.Collections.Generic;

namespace Compiler.Tree
{
    public class CompilationNode
    {
        public List<UsingNode> usingArray;
        public List<NamespaceNode> namespaceArray;
    }
}