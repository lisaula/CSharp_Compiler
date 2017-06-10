using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ClassDefinitionNode : TypeDefinitionNode
    {
        public Dictionary<string,FieldNode> fields;
        public Dictionary<string,MethodNode> methods;
        public Dictionary<string,ConstructorNode> constructors;
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
            constructors = new Dictionary<string, ConstructorNode>();
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
            if (evaluated)
                return;
            Debug.printMessage("Evaluating class " + identifier.ToString());
            if (identifier.ToString() == "Circulo")
                Console.WriteLine();
            evaluateFields(api);

            evaluated = true;
        }

        private void evaluateFields(API api)
        {
            foreach(KeyValuePair<string, FieldNode> field in fields)
            {
                FieldNode f = field.Value;
                if (f.id.ToString() == "field5")
                    Console.WriteLine();
                if (f.modifier != null)
                    if (!api.modifierPass(field.Value.modifier, TokenType.RW_STATIC))
                        throw new SemanticException("The modifier '" + field.Value.modifier.ToString() + "' is not valid for field " + field.Value.id.ToString() + " in class "+identifier.ToString()+".", field.Value.modifier.token);
                
                if(f.type is VoidTypeNode)
                    throw new SemanticException("The type '"+f.type.GetType().Name+"' is not valid for field " + field.Value.id.ToString() + " in class "+identifier.ToString()+".", f.type.identifier.token);
                else if(f.type is IdentifierTypeNode)
                {
                    string name = f.type.ToString();
                    NamespaceNode nms = api.getParentNamespace(this);
                    TypeDefinitionNode tdn = api.findTypeInList(nms.typeList, name);
                    if (tdn == null)
                    {
                        tdn = api.findTypeInUsings(nms.usingList, name);
                        tdn.Evaluate(api);
                    }
                }


            }
        }


    }
}