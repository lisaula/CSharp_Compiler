using System.Collections.Generic;

namespace Compiler.Tree
{
    public class InterfaceNode : TypeDefinitionNode
    {
        internal InheritanceNode inheritance;
        internal List<MethodNode> methods;
        private Token token_identifier;
        private EncapsulationNode encapsulation;

        public InterfaceNode()
        {
        }

        public InterfaceNode(Token token)
        {
            this.token_identifier = token;
        }

        public InterfaceNode(EncapsulationNode encapsulation, Token token)
        {
            this.encapsulation = encapsulation;
            this.token_identifier = token;
        }
    }
}