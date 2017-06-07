using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ClassDefinitionNode : TypeDefinitionNode
    {
        public Dictionary<string,FieldNode> fields;
        public Dictionary<string,MethodNode> methods;
        public List<ConstructorNode> constructors;
        public EncapsulationNode encapsulation;
        public bool isAbstract;
        public IdentifierNode id;
        public InheritanceNode inheritance;

        public ClassDefinitionNode(EncapsulationNode encapsulation, bool isAbstract, IdentifierNode id, InheritanceNode inheritance)
        {
            this.encapsulation = encapsulation;
            this.isAbstract = isAbstract;
            this.id = id;
            this.inheritance = inheritance;
            fields = new Dictionary<string, FieldNode>();
            methods = new Dictionary<string, MethodNode>();
        }
        public ClassDefinitionNode()
        {

        }
        public override string ToString()
        {
            return id.token.lexema;
        }

        public override void Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}