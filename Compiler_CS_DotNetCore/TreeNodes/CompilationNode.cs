using System.Collections.Generic;


namespace Compiler.Tree
{
    public class CompilationNode
    {
        public List<UsingNode> usingList;
        public List<NamespaceNode> namespaceList;
        public List<TypeDefinitionNode> typeList;
    }
}