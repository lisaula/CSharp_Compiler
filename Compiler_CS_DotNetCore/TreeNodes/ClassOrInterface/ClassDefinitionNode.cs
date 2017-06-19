using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class ClassDefinitionNode : TypeDefinitionNode
    {
        public Dictionary<string,FieldNode> fields;
        public bool generated = false;
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
            if (identifier.ToString() == "sort")
                Console.WriteLine();
            checkInheritanceExistance(api);
            checkParents(api);
            checkMethodHeader(api);
            checkConstructors(api);
            evaluateFields(api);
            checkFieldsAssignment(api);
            checkConstructorsBody(api);
            checkMethodsBody(api);
            evaluated = true;
        }

        private void checkMethodsBody(API api)
        {
            if (methods == null || methods.Count == 0)
                return;
            api.contextManager.contexts.Clear();
            foreach (var key in methods)
            {
                if (api.modifierPass(key.Value.modifier, TokenType.RW_ABSTRACT))
                    continue;
               Debug.printMessage("Evaluando " + key.Key);
                List<Context> contexts = api.contextManager.buildEnvironment(this, ContextType.CLASS, api);
                api.pushContext(contexts.ToArray());
                var ctr_context = new Context(key.Value.id.ToString(), ContextType.METHOD, api);
                api.contextManager.pushFront(ctr_context);
                api.addParametersToCurrentContext(key.Value.parameters);
                api.setWorkingType(this);
                if (key.Value.bodyStatements == null
                    && !api.modifierPass(key.Value.modifier, TokenType.RW_ABSTRACT))
                    throw new SemanticException("Method '"+Utils.getMethodWithParentName(key.Key, this)+"'has no body. " + key.Value.id.token);

                key.Value.bodyStatements.evaluate(api);

                api.setWorkingType(null);
                List<TypeDefinitionNode> returns = api.getCurrentReturnType();
                if (returns.Count == 0 && key.Value.returnType.getComparativeType() != Utils.Void)
                    throw new SemanticException("Not all code in method '" + Utils.getMethodWithParentName(key.Key, this) + "' returns a value.", key.Value.id.token);
                foreach (TypeDefinitionNode t in returns)
                {
                    if(t.getComparativeType() == Utils.Null){
                        if (api.pass(key.Value.returnType.getComparativeType(), Utils.primitives))
                            throw new SemanticException("Method '" + Utils.getMethodWithParentName(key.Key, this) + "' can'r return null.", key.Value.id.token);
                    }else if (t.getComparativeType() != key.Value.returnType.getComparativeType())
                    {
                       throw new SemanticException("Method '" + Utils.getMethodWithParentName(key.Key, this) + "' does not return type '" + t.ToString() + "'", key.Value.id.token);
                    }
                }
                api.contextManager.contexts.Clear();
            }
        }

        private void checkConstructorsBody(API api)
        {
            api.contextManager.contexts.Clear();

            var token = new Token();
            token.type = TokenType.RW_BASE;
            ConstructorInitializerNode init = new ConstructorInitializerNode(token, new List<ExpressionNode>());

            foreach (var key in constructors)
            {
                if (key.Value.id.ToString() == "myClase")
                    Console.WriteLine();
                List<Context> contexts = api.contextManager.buildEnvironment(this, ContextType.CLASS, api);
                api.pushContext(contexts.ToArray());
                var ctr_context = new Context(key.Value.id.ToString(),ContextType.CONSTRUCTOR,api);
                api.contextManager.pushFront(ctr_context);
                api.addParametersToCurrentContext(key.Value.parameters);
                api.setWorkingType(this);
                if (key.Value.bodyStatements == null)
                    throw new SemanticException("Constructor has no body. "+key.Value.id.token);

                
                if (key.Value.base_init == null && parents.Count > 0)
                {
                    TypeDefinitionNode t = api.contextManager.getParent(api.working_type);
                    if (t is ClassDefinitionNode)
                    {
                        key.Value.base_init = init;
                    }
                    key.Value.base_init.evaluate(api);
                }else if(key.Value.base_init != null)
                    key.Value.base_init.evaluate(api);

                key.Value.bodyStatements.evaluate(api);

                api.setWorkingType(null);
                List<TypeDefinitionNode> returns =  api.getCurrentReturnType();
                foreach(TypeDefinitionNode t in returns)
                {
                    if (t.getComparativeType() != Utils.Void)
                        throw new SemanticException("Constructor '"+key.Key+"'does not return type '" + t.ToString() + "'");
                }
                api.contextManager.contexts.Clear();
            }
        }

        private void checkFieldsAssignment(API api)
        {
            api.contextManager.contexts.Clear();
            foreach(KeyValuePair<string,FieldNode> key in fields)
            {
                if(key.Value.assignment != null)
                {
                    List<Context> contexts = api.contextManager.buildEnvironment(this, ContextType.CLASS, api);
                    api.pushContext(contexts.ToArray());
                    api.setWorkingType(this);
                    api.contextManager.isStatic = true;
                    FieldNode f = key.Value;
                    if (f.id.ToString() == "t")
                        Console.WriteLine();
                    TypeDefinitionNode tdn = f.assignment.evaluateType(api);
                    string rule = f.type.ToString() + "," + tdn.ToString();
                    string rule2 = f.type.getComparativeType() + "," + tdn.ToString();
                    string rule3 = f.type.getComparativeType() + "," + tdn.getComparativeType();
                    if (!api.assignmentRules.Contains(rule)
                        && !api.assignmentRules.Contains(rule2)
                        && !api.assignmentRules.Contains(rule3)
                        && !f.type.Equals(tdn))
                    {
                        if(f.type.getComparativeType() == Utils.Class && tdn.getComparativeType() == Utils.Class)
                        {
                            if(!api.checkRelationBetween(f.type, tdn))
                                throw new SemanticException("1Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), key.Value.id.token);
                        }else if ((!(f.type.getComparativeType() == Utils.Class || f.type.getComparativeType() == Utils.String) && tdn is NullTypeNode))
                        {
                            throw new SemanticException("2Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), key.Value.id.token);
                        }
                        else
                            throw new SemanticException("4Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), key.Value.id.token);
                    }
                    api.setWorkingType(null);
                    api.contextManager.isStatic = false;
                    api.contextManager.Enums_or_Literal = false;
                    api.contextManager.contexts.Clear();
                }
            }
        }

        private void checkConstructors(API api)
        {
            string key = identifier.ToString() + "()";
            if (constructors.Count == 0 && !constructors.ContainsKey(key))
            {
                var token = new Token();
                token.type = TokenType.RW_PUBLIC;
                token.lexema = "public";
                var ctr = new ConstructorNode(new EncapsulationNode(token), identifier, null, null, new BodyStatement());
                constructors[key] = ctr;
            }

            foreach (KeyValuePair<string,ConstructorNode> ctr in constructors)
            {
                Debug.printMessage("Evaluando ctr " + ctr.Key);
                ConstructorNode c = ctr.Value;
                if (!c.id.Equals(identifier))
                    throw new SemanticException("Not a valid constructor. "+c.id.ToString()+" does not match class "+identifier.ToString()+".", c.id.token);
                api.checkParametersExistance(this,c.parameters);
                c.headerEvaluation = true;
            }
        }

        private void checkMethodHeader(API api)
        {
            foreach(KeyValuePair<string, MethodNode> method in methods)
            {
                if (method.Key == "metodo()")
                    Console.WriteLine();
                api.checkParametersExistance(this,method.Value.parameters);
                api.setWorkingType(this);
                api.checkReturnTypeExistance(ref method.Value.returnType);
                api.setWorkingType(null);

                if (api.modifierPass(method.Value.modifier, TokenType.RW_OVERRIDE, TokenType.RW_ABSTRACT, TokenType.RW_VIRTUAL))
                    if (api.TokenPass(method.Value.encapsulation.token, TokenType.RW_PRIVATE))
                        throw new SemanticException("Override, Abstract or Virtual member '" + method.Key + "' can't have private encapsulation.", method.Value.id.token);

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
                                    throw new SemanticException(Utils.getMethodWithParentName(method.Key, this) + " cannot declare a body because is marked as abstract.", method.Value.modifier.token);
                            }
                            else
                                throw new SemanticException("Abtract method " + Utils.getMethodName(method.Value) + " is contained in " + this.ToString() + " which is a non-abstract class.", method.Value.modifier.token);
                        }
                        else if (api.modifierPass(method.Value.modifier, TokenType.RW_OVERRIDE))
                            throw new SemanticException("Modifier 'override' can't be aplied to the method " + Utils.getMethodWithParentName(method.Value, this));
                        
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
            int count = 0;
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
                    if(count != 0)
                    {
                        throw new SemanticException("Class '" + api.getIdentifierListAsString(".",parent)+ "' must be in first position in inheritance list.", parent[0].token);
                    }
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
                count++;
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
                }else if((parent is InterfaceNode) || ((ClassDefinitionNode)parent).isAbstract && !isAbstract) { 
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
                if (f.id.ToString() == "t")
                    Console.WriteLine();
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
                if(tdn is ClassDefinitionNode)
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
            string[] primitives = { Utils.Bool, Utils.Char, Utils.Dict, Utils.Float, Utils.String, Utils.Int, Utils.Void, Utils.Var };
            foreach (string s in primitives)
            {
                if (identifier.token.lexema == s)
                    return s;
            }
            return Utils.Class;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            if (generated)
                return;

            generateParentsFirst(builder, api);

            string nms = Utils.EndLine + api.getFullNamespaceName(this);
            nms += "." + identifier.ToString();

            builder.Append(nms + " = class ");
            bool haParrent = false;
            if (parents != null)
            {
                foreach (var pKey in parents)
                {
                    if (pKey.Value is ClassDefinitionNode)
                    {
                        builder.Append("extends ");
                        string name = api.getFullNamespaceName(pKey.Value);
                        name += "." + pKey.Value.ToString();
                        builder.Append(name);
                        haParrent = true;
                    }
                }
            }
            builder.Append(" {");
            StringBuilder fieldsBuilder = new StringBuilder();
            foreach (var field in fields)
            {
                if (field.Value.id.ToString() == "field45")
                    Console.WriteLine();
                if (!api.modifierPass(field.Value.modifier, TokenType.RW_STATIC))
                {
                    field.Value.setIsThis();
                    field.Value.generateCode(fieldsBuilder, api);
                    fieldsBuilder.Append(";");
                }
            }

            foreach (var ctr in constructors)
            {
                ctr.Value.generateCode(builder, api);
            }

            builder.Append(Utils.EndLine + "constructor(){");
            if (haParrent)
                builder.Append(Utils.EndLine + "super()");
            builder.Append(fieldsBuilder.ToString());
            builder.Append(Utils.EndLine + "let argumentos = Array.from(arguments);");
            builder.Append(Utils.EndLine + "let argus = argumentos.slice(1);");
            builder.Append(Utils.EndLine + "if(argumentos.length>1)");
            builder.Append(Utils.EndLine + "\tthis[arguments[0]](...argus);");
            builder.Append(Utils.EndLine + "}");
            if (methods != null)
            {
                foreach (var method in methods)
                {
                    method.Value.generateCode(builder, api);
                }
            }
            builder.Append(Utils.EndLine + "}");
            foreach (var field in fields)
            {
                if (api.modifierPass(field.Value.modifier, TokenType.RW_STATIC))
                {
                    builder.Append(nms + ".");
                    field.Value.generateCode(builder, api);
                    builder.Append(";");
                }

            }

            generated = true;
        }

        private void generateParentsFirst(StringBuilder builder, API api)
        {
            if (parents != null)
            {
                foreach (var pKey in parents)
                {
                    if (pKey.Value is ClassDefinitionNode && ((ClassDefinitionNode)pKey.Value).generated==false)
                    {
                        pKey.Value.generateCode(builder, api);
                    }
                }
            }
        }
    }
}