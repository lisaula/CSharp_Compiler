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
        public Dictionary<string, TypeDefinitionNode> parents;

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
            parents = null;
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
            checkInheritanceExistance(api);
            //evaluateFields(api);

            evaluated = true;
        }

        private void checkInheritanceExistance(API api)
        {
            if(inheritance == null)
            {
                inheritance = new InheritanceNode();
            }
            inheritance.addObjectInheritance();
            parents = new Dictionary<string, TypeDefinitionNode>();
            foreach (List<IdentifierNode> parent in inheritance.identifierList)
            {
                string name = api.getIdentifierListAsString(".", parent);
                string nms = api.getParentNamespace(this);
                var usings = parent_namespace.usingList;
                usings.Add(new UsingNode(nms));
                TypeDefinitionNode tdn = api.findTypeInUsings(usings, name);
                if (tdn == null)
                    throw new SemanticException("Could not find Type '" + name + "' in the current context. ", parent[0].token);

                if (parents.ContainsKey(name))
                    throw new SemanticException("Redundant Inheritance. " + name + " was found twice as inheritance in " + identifier.token.lexema, parent[0].token);
                parents[name] = tdn;
            }
        }

        private void checkParents(API api)
        {
            if (parents == null)
                return;
            foreach (KeyValuePair<string, TypeDefinitionNode> parent in parents)
            {
                if (!(parent.Value is InterfaceNode))
                    throw new SemanticException("Type '" + parent.Key + "' in " + identifier.token.lexema + " is not an interface", identifier.token);
                ((InterfaceNode)parent.Value).Evaluate(api);
            }
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
                    string nms = api.getParentNamespace(this);
                    var usings = parent_namespace.usingList;
                    usings.Add(new UsingNode(nms));
                    TypeDefinitionNode tdn = api.findTypeInUsings(usings, name);
                    if (tdn == null)
                        throw new SemanticException("Could not find Type '" + name + "' in the current context. ", f.type.getPrimaryToken());
                    tdn.Evaluate(api);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if(obj is ClassDefinitionNode)
            {
                var t = obj as ClassDefinitionNode;
                return identifier.Equals(t.identifier);
            }
            return false;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }
    }
}