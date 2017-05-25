using System.Collections.Generic;

namespace Compiler.Tree
{
    public class EnumDefinitionNode : TypeDefinitionNode
    {
        public List<EnumNode> enumNodeList;
        private EncapsulationNode encapsulation;
        private IdentifierNode identifier;

        public EnumDefinitionNode(EncapsulationNode encapsulation, IdentifierNode identifier)
        {
            this.encapsulation = encapsulation;
            this.identifier = identifier;
        }
    }
}