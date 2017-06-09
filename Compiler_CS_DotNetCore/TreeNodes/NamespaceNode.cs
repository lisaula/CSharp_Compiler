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
            identifierList = new List<IdentifierNode>();
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
            foreach (UsingNode us in usingList)
            {
                us.evaluate(api);
            }
            foreach (TypeDefinitionNode t in typeList)
            {
                try
                {
                    t.Evaluate(api);
                }
                catch (NotImplementedException nie) { }
            }

            foreach (NamespaceNode nms in namespaceList)
            {
                try
                {
                    nms.Evaluate(api);
                }
                catch (NotImplementedException nie) { }
            }
        }
    }
}