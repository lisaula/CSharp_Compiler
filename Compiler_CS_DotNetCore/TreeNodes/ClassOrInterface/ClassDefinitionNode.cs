using Compiler_CS_DotNetCore.Semantic;
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
        public InheritanceNode inheritance;

        public ClassDefinitionNode(EncapsulationNode encapsulation, bool isAbstract, IdentifierNode id, InheritanceNode inheritance):this()
        {
            this.encapsulation = encapsulation;
            this.isAbstract = isAbstract;
            this.identifier = id;
            this.inheritance = inheritance;
            fields = new Dictionary<string, FieldNode>();
            methods = new Dictionary<string, MethodNode>();
        }
        public ClassDefinitionNode()
        {
            parent_namespace = null;
        }
        public override string ToString()
        {
            return identifier.token.lexema;
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}