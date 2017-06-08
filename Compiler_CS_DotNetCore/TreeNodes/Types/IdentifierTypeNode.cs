using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class IdentifierTypeNode : TypeDefinitionNode
    {
        public IdentifierTypeNode()
        {
        }

        public IdentifierTypeNode(List<IdentifierNode> list)
        {
            this.Identifiers = list;
        }

        public ArrayNode arrayNode;
        public List<IdentifierNode> Identifiers { get; internal set; }
        public override string ToString()
        {
            List<string> names = new List<string>();
            foreach(IdentifierNode id in Identifiers)
            {
                names.Add(id.token.lexema);
            }
            return string.Join(".", names);
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}