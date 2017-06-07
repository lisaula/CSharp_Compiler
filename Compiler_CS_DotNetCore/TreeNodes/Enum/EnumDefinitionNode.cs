using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class EnumDefinitionNode : TypeDefinitionNode
    {
        public List<EnumNode> enumNodeList;
        public EncapsulationNode encapsulation;
        public IdentifierNode identifier;

        public EnumDefinitionNode(EncapsulationNode encapsulation, IdentifierNode identifier)
        {
            this.encapsulation = encapsulation;
            this.identifier = identifier;
        }
        public EnumDefinitionNode()
        {

        }
        public override string ToString()
        {
            return identifier.token.lexema;
        }

        public override void Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}