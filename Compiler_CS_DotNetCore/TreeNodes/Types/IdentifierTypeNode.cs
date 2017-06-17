using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class IdentifierTypeNode : TypeDefinitionNode
    {
        public IdentifierTypeNode()
        {
            Identifiers = new List<IdentifierNode>();
        }

        public IdentifierTypeNode(List<IdentifierNode> list)
        {
            this.Identifiers = list;
        }
        public IdentifierTypeNode(IdentifierNode id): this()
        {
            this.Identifiers.Add(id);
        }
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

        public override bool Equals(object obj)
        {
            if(obj is IdentifierTypeNode)
            {
                var o = obj as IdentifierTypeNode;
                return o.typeNode.Equals(typeNode);
            }
            return false;
        }

        public override Token getPrimaryToken()
        {
            return Identifiers[0].token;
        }

        public override string getComparativeType()
        {
            throw new NotImplementedException();
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}