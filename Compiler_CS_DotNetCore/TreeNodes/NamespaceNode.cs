using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class NamespaceNode
    {
        public List<IdentifierNode> identifierList;
        public List<UsingNode> usingList;
        public List<NamespaceNode> namespaceList;
        public List<TypeDefinitionNode> typeList;
        public NamespaceNode(List<IdentifierNode> identifier) : this()
        {
            this.identifierList = identifier;
        }
        public NamespaceNode()
        {
            usingList = new List<UsingNode>();
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

        internal void Evaluate(API api)
        {
            
        }
    }
}