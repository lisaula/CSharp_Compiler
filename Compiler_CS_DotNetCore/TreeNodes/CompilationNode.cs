using System;
using System.Collections.Generic;


namespace Compiler.Tree
{
    public class CompilationNode
    {
        public static Dictionary<string, string> namespaces = new Dictionary<string, string>();
        public List<UsingNode> usingList;
        public List<NamespaceNode> namespaceList;
        public List<TypeDefinitionNode> typeList;

        internal void Evaluate()
        {
            /*foreach(TypeDefinitionNode t in typeList)
            {
                t.Evaluate();
            }*/
        }
    }
}