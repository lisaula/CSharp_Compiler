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
            identifierList = new List<IdentifierNode>();
        }

        public UsingNode(string nms): this()
        {
            var token = new Token();
            token.lexema = nms;
            identifierList.Add(new IdentifierNode(token));
        }

        internal void evaluate(API api)
        {

            string name = Utils.GlobalNamespace+"."+api.getIdentifierListAsString(".", identifierList);
            if (!Singleton.tableNamespaces.Contains(name))
                throw new SemanticException("Could not be found a namespace with using "+name,identifierList[0].token);
        }
    }
}