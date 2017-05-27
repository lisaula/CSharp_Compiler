using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ClassDefinitionNode : TypeDefinitionNode
    {
        public  List<FieldNode> fields;
        private EncapsulationNode encapsulation;
        private bool isAbstract;
        private IdentifierNode id;
        private InheritanceNode inheritance;

        public ClassDefinitionNode(EncapsulationNode encapsulation, bool isAbstract, IdentifierNode id, InheritanceNode inheritance)
        {
            this.encapsulation = encapsulation;
            this.isAbstract = isAbstract;
            this.id = id;
            this.inheritance = inheritance;
            fields = new List<FieldNode>();
        }
    }
}