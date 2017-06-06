using System.Collections.Generic;

namespace Compiler.Tree
{
    public class InterfaceNode : TypeDefinitionNode
    {
        public InheritanceNode inheritance;
        public List<MethodNode> methods;
        public Token token_identifier;
        public EncapsulationNode encapsulation;

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
        public override string ToString()
        {
            return token_identifier.lexema;
        }
    }
}