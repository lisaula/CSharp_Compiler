using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ClassDefinitionNode : TypeDefinitionNode
    {
        public Dictionary<string,FieldNode> fields;
        public Dictionary<string,MethodNode> methods;
        public Dictionary<string,ConstructorNode> constructors;
        public bool isAbstract;
        public InheritanceNode inheritance;
        public EncapsulationNode encapsulation;
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
            if (identifier.ToString() == "myClase")
                Console.WriteLine();
            checkInheritanceExistance(api);
            checkParents(api);
            checkMethodHeader(api);
            checkConstructors(api);
            evaluateFields(api);
            checkFieldsAssignment(api);
            checkConstructorsBody(api);
            evaluated = true;
        }

        private void checkConstructorsBody(API api)
        {
            foreach (var key in constructors)
            {
                List<Context> contexts = api.contextManager.buildEnvironment(this, ContextType.CLASS, api);
                api.pushContext(contexts.ToArray());
                api.setWorkingType(this);
                if (key.Value.bodyStatements == null)
                    throw new SemanticException("Constructor has no body. "+key.Value.id.token);
                foreach (var s in key.Value.bodyStatements.statements)
                {
                    
                }
                api.setWorkingType(null);
                api.popContext(contexts.ToArray());
            }
        }

        private void checkFieldsAssignment(API api)
        {
            List<Context> contexts = api.contextManager.buildEnvironment(this, ContextType.CLASS,api);
            api.pushContext(contexts.ToArray());
            api.setWorkingType(this);
            foreach(KeyValuePair<string,FieldNode> key in fields)
            {
                if(key.Value.assignment != null)
                {
                    FieldNode f = key.Value;
                    if (f.id.ToString() == "r2")
                        Console.WriteLine();
                    TypeDefinitionNode tdn = f.assignment.evaluateType(api);
                    if (!f.type.Equals(tdn))
                    {
                        if(f.type.getComparativeType() == Utils.Class && tdn.getComparativeType() == Utils.Class)
                        {
                            if(!api.checkRelationBetween(f.type, tdn))
                                throw new SemanticException("Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), tdn.getPrimaryToken());
                        }else if ((!(f.type.getComparativeType() == Utils.Class || f.type.getComparativeType() == Utils.String) && tdn is NullTypeNode))
                        {
                            throw new SemanticException("Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), tdn.getPrimaryToken());
                        }
                        else
                            throw new SemanticException("Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), tdn.getPrimaryToken());
                    }
                }
            }
            api.setWorkingType(null);
            api.popContext(contexts.ToArray());
        }

        private void checkConstructors(API api)
        {
            foreach(KeyValuePair<string,ConstructorNode> ctr in constructors)
            {
                Debug.printMessage("Evaluando ctr " + ctr.Key);
                ConstructorNode c = ctr.Value;
                if (!c.id.Equals(identifier))
                    throw new SemanticException("Not a valid constructor. "+c.id.ToString()+" does not match class "+identifier.ToString()+".", c.id.token);
                api.checkParametersExistance(this,c.parameters);
                c.headerEvaluation = true;
            }
            string key = identifier.ToString() + "()";
            if (!constructors.ContainsKey(key))
            {
                var token = new Token();
                token.type = TokenType.RW_PUBLIC;
                token.lexema = "public";
                var ctr = new ConstructorNode(new EncapsulationNode(token), identifier,null, null, new BodyStatement());
                constructors[key] = ctr;
            }
        }

        private void checkMethodHeader(API api)
        {
            foreach(KeyValuePair<string, MethodNode> method in methods)
            {
                api.checkParametersExistance(this,method.Value.parameters);
                if (method.Value.modifier != null)
                {
                    Debug.printMessage("Evaluando methodo " + Utils.getMethodWithParentName(method.Value, this));
                    if (!method.Value.modifier.evaluated)
                    {
                        if (api.modifierPass(method.Value.modifier, TokenType.RW_ABSTRACT))
                        {
                            if (isAbstract)
                            {
                                if (method.Value.bodyStatements != null)
                                    throw new SemanticException(Utils.getMethodName(method.Value) + " cannot declare a body because is marked as abstract.", method.Value.modifier.token);
                            }
                            else
                                throw new SemanticException("Abtract method " + Utils.getMethodName(method.Value) + " is contained in " + this.ToString() + " which is a non-abstract class.", method.Value.modifier.token);
                        }
                        else if (api.modifierPass(method.Value.modifier, TokenType.RW_OVERRIDE))
                            throw new SemanticException("Modifier 'override' can't be aplied to the method " + Utils.getMethodWithParentName(method.Value, this));
                        
                    }else
                    {
                        Debug.printMessage("Evaluado " + method.Value.id.ToString());
                    }
                }
            }
        }

        public void checkInheritanceExistance(API api)
        {
            if(inheritance == null)
            {
                inheritance = new InheritanceNode();
            }
            int class_count = 0;
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
                if (tdn is ClassDefinitionNode)
                {
                    class_count++;
                    if (api.TokenPass(((ClassDefinitionNode)tdn).encapsulation.token, TokenType.RW_PRIVATE))
                        throw new SemanticException("Parent '" + name + "' can't be reached due to its encapsulation level.", parent[0].token);
                }
                else
                {
                    if (api.TokenPass(((InterfaceNode)tdn).encapsulation.token, TokenType.RW_PRIVATE))
                        throw new SemanticException("Parent '" + name + "' can't be reached due to its encapsulation level.", parent[0].token);
                }
                if (class_count > 1)
                    throw new SemanticException("Object '" + identifier.ToString() + "' can't have multiple class inheritance.", identifier.token);
                if (parents.ContainsKey(name))
                    throw new SemanticException("Redundant Inheritance. " + name + " was found twice as inheritance in " + identifier.token.lexema, parent[0].token);
                parents[name] = tdn;
            }
        }
        public override void verifiCycle(TypeDefinitionNode type, API api)
        {
            if (parents == null)
                return;
            foreach (KeyValuePair<string, TypeDefinitionNode> p in parents)
            {
                if (type.identifier.Equals(p.Value.identifier) && api.getParentNamespace(type) == api.getParentNamespace(p.Value))
                    throw new SemanticException("Cycle inheritance detected in " + identifier.ToString() + ".", p.Value.identifier.token);
                p.Value.verifiCycle(type, api);
            }
        }

        private void checkParents(API api)
        {
            foreach (KeyValuePair<string, TypeDefinitionNode> parent in parents)
            {
                if (parent.Value is EnumDefinitionNode)
                    throw new SemanticException("Enum '" + parent.Key + "' in " + identifier.token.lexema + " can't be used as a inheritance.", identifier.token);
                checkParentMethods(parent.Value, api);
            }
            verifiCycle(this, api);
        }

        private void checkParentMethods(TypeDefinitionNode parent, API api)
        {
            Dictionary<string, MethodNode> parent_methods = api.getParentMethods(parent);
            foreach (KeyValuePair<string, MethodNode> method in parent_methods)
            {
                if (methods.ContainsKey(method.Key))
                {
                    api.checkParentMethodOnMe(methods[method.Key], method.Value, parent);
                }else if (!isAbstract) {
                    if((parent is InterfaceNode) || api.modifierPass(method.Value.modifier, TokenType.RW_ABSTRACT))
                        throw new SemanticException(identifier.ToString() + " does not implement " + parent.ToString() + "." + method.Key, identifier.token);
                }
            }
        }
        public bool checkRelationWith(TypeDefinitionNode type, API api)
        {
            if (!evaluated)
                checkInheritanceExistance(api);
            if (type.identifier.Equals(identifier) && api.getParentNamespace(type) == api.getParentNamespace(this))
                return true;
            if (parents == null)
                return false;
            foreach (KeyValuePair<string, TypeDefinitionNode> key in parents)
            {
                if(key.Value is ClassDefinitionNode)
                {
                    if (type.identifier.Equals(key.Value.identifier) && api.getParentNamespace(type) == api.getParentNamespace(key.Value))
                        return true;
                    bool found = ((ClassDefinitionNode)key.Value).checkRelationWith(type, api);
                    if (found)
                        return true;
                }
            }
            return false;
        }

        private void evaluateFields(API api)
        {
            foreach(KeyValuePair<string, FieldNode> field in fields)
            {
                FieldNode f = field.Value;
                if (f.modifier != null)
                    if (!api.modifierPass(field.Value.modifier, TokenType.RW_STATIC))
                        throw new SemanticException("The modifier '" + field.Value.modifier.ToString() + "' is not valid for field " + field.Value.id.ToString() + " in class "+identifier.ToString()+".", field.Value.modifier.token);

                if (f.type is VoidTypeNode)
                    throw new SemanticException("The type '" + f.type.GetType().Name + "' is not valid for field " + field.Value.id.ToString() + " in class " + identifier.ToString() + ".", f.type.getPrimaryToken());

                string name = f.type.ToString();
                if (f.type is ArrayTypeNode)
                {
                    name = ((ArrayTypeNode)f.type).getArrayType().ToString();
                }
                string nms = api.getParentNamespace(this);
                var usings = parent_namespace.usingList;
                usings.Add(new UsingNode(nms));
                TypeDefinitionNode tdn = api.findTypeInUsings(usings, name);
                if (tdn == null)
                    throw new SemanticException("Could not find Type '" + name + "' in the current context. ", f.type.getPrimaryToken());
                if(tdn is InterfaceNode || tdn is VoidTypeNode)
                    throw new SemanticException("The type '" + tdn.ToString()+ "' is not valid for field " + field.Value.id.ToString() + " in class " + identifier.ToString() + ".", f.type.getPrimaryToken());
                if (api.TokenPass(((ClassDefinitionNode)tdn).encapsulation.token, TokenType.RW_PRIVATE))
                    throw new SemanticException("The type '" + f.type.ToString() + "' can't be reached due to encapsulation level.", f.type.getPrimaryToken());
                f.type.typeNode = f.type;
                if (f.type is ArrayTypeNode)
                {
                    ((ArrayTypeNode)f.type).type = tdn;
                }
                else
                    f.type = tdn;
                //tdn.Evaluate(api);
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

        public override string getComparativeType()
        {
            string[] primitives = { Utils.Bool, Utils.Char, Utils.Dict, Utils.Float, Utils.String, Utils.Int, Utils.Void };
            foreach (string s in primitives)
            {
                if (identifier.token.lexema == s)
                    return s;
            }
            return Utils.Class;
        }
    }
}