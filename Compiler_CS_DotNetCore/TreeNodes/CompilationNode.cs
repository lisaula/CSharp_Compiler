using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class CompilationNode
    {
        public List<UsingNode> usingList;
        public List<NamespaceNode> namespaceList;
        public List<TypeDefinitionNode> typeList;

        public CompilationNode()
        {
            usingList = new List<UsingNode>();
        }
        internal void Evaluate(API api)
        {
            foreach(UsingNode us in usingList)
            {
                us.evaluate(api);
            }
            /*foreach(TypeDefinitionNode t in typeList)
            {
                t.Evaluate();
            }*/

        }
    }
}