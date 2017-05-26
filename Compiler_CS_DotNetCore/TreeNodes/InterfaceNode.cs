using System.Collections.Generic;

namespace Compiler.Tree
{
    public class InterfaceNode : TypeDefinitionNode
    {
        internal InheritanceNode inheritance;
        internal List<MethodNode> methods;
        private string Identifier;
        private EncapsulationNode encapsulation;

        public InterfaceNode()
        {
        }

        public InterfaceNode(string lexema)
        {
            this.Identifier = lexema;
        }

        public InterfaceNode(EncapsulationNode encapsulation, string lexema)
        {
            this.encapsulation = encapsulation;
            this.Identifier = lexema;
        }
    }
}