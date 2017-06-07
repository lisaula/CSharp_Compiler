using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;
namespace Compiler.Tree
{
    public class UsingNode
    {
        public List<IdentifierNode> identifierList;

        public UsingNode(List<IdentifierNode> identifier)
        {
            this.identifierList = identifier;
        }
        public UsingNode()
        {
        }

        internal void evaluate(API api)
        {
            string name = api.getIdentifierListAsString(".", identifierList);
            if (!Singleton.tableNamespaces.ContainsKey(name))
                throw new SemanticException("Could not be found a namespace with using " + name+identifierList[0].token.ToString());
        }
    }
}