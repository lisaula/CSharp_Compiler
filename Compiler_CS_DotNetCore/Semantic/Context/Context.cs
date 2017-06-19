using Compiler;
using Compiler.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler_CS_DotNetCore.Semantic.Context
{
    public class Context
    {
        public Dictionary<string, FieldNode> variables;
        public Dictionary<string, MethodNode> methods;
        public Dictionary<string, ConstructorNode> constructors;
        public string name;
        public ContextType type;
        public List<TypeDefinitionNode> returnsFound;
        public TypeDefinitionNode owner;

        public Context()
        {
            name = null;
            variables = new Dictionary<string, FieldNode>();
            methods = new Dictionary<string, MethodNode>();
            constructors = new Dictionary<string, ConstructorNode>();
            returnsFound = new List<TypeDefinitionNode>();
        }

        public Context(String name, ContextType type, API api):this()
        {
            this.name = name;
            this.type = type;
        }

        public Context(string name,Dictionary<string, MethodNode> methods)
        {
            this.name = name;
            this.methods = methods;
        }

        internal void addReturnType(TypeDefinitionNode t)
        {
            returnsFound.Add(t);
        }

        public override string ToString()
        {
            return name;
        }
        private FieldNode convertToField(TypeDefinitionNode it, IdentifierNode id)
        {
            var token = new Token();
            token.type = TokenType.RW_PUBLIC;
            token.lexema = "public";
            FieldNode f = new FieldNode(new EncapsulationNode(token), null, it, id, null);
            return f;
        }
        public Context(string name,Dictionary<string, FieldNode> fields, Dictionary<string, MethodNode> methods, Dictionary<string, ConstructorNode> constructors)
        {
            this.name = name;
            this.variables = fields;
            this.methods = methods;
            this.constructors = constructors;
        }

        internal MethodNode findFunction(string name)
        {
            if (methods.ContainsKey(name))
                return methods[name];
            return null;
        }

        public bool variableExist(FieldNode f)
        {
            return variables.ContainsKey(f.id.ToString());
        }
        public Context(ContextType type, API api):this()
        {
            this.type = type;
        }
        public Context(TypeDefinitionNode node, ContextType type, API api) : this()
        {
            this.owner = node;
            this.type= type;
            if (node is ClassDefinitionNode)
            {
                buildEnvironment(((ClassDefinitionNode)node), api);
            }else if(node is InterfaceNode)
            {
                buildEnvironment(((InterfaceNode)node), api);
            }else if (node is EnumDefinitionNode)
            {
                buildEnvironment((EnumDefinitionNode)node, api);
            }
        }

        private void buildEnvironment(EnumDefinitionNode node, API api)
        {
            var token = new Token();
            token.type = TokenType.RW_STATIC;
            ModifierNode m = new ModifierNode(token);
            if (node.enumNodeList == null)
                return;
            foreach (EnumNode en in node.enumNodeList)
            {
                FieldNode f = convertToField(en, en.identifier);
                addVariable(f);
                f.modifier = m;
            }
        }

        internal FieldNode findVariable(Token id)
        {
            if (variables.ContainsKey(id.lexema))
            {
                return variables[id.lexema];
            }
            return null;
        }

        private void buildEnvironment(ClassDefinitionNode node, API api)
        {
            name = node.ToString();
            copyFields(node.fields);
            copyMethods(node.methods);
            constructors = node.constructors;
            checkType(type, api);
        }

        private void copyMethods(Dictionary<string, MethodNode> methods)
        {
            List<MethodNode> ms = new List<MethodNode>();
            foreach(var key in methods)
            {
                /*if (isStatic)
                {
                    if (key.Value.modifier != null)
                        if (!pass(key.Value.modifier.token, TokenType.RW_STATIC))
                            continue;
                }*/
                ms.Add(key.Value);
            }
            MethodNode[] mss = new MethodNode[ms.Count];
            ms.CopyTo(mss);
            addMethod(mss);
        }

        private void addMethod(params MethodNode[] mss)
        {
            foreach(MethodNode m in mss)
            {
                string key = Utils.getMethodName(m);
                if (methods.ContainsKey(key))
                    throw new SemanticException("Method '" + key + "' already exist in current context.", m.id.token);
                m.returnType.localy = false;
                m.returnType.globally = false;
                m.returnType.onTableType = false;
                methods[key] = m;
            }
        }

        private void copyFields(Dictionary<string, FieldNode> fields)
        {
            List<FieldNode> fs = new List<FieldNode>();
            foreach(var f in fields)
            {
                /*if (isStatic)
                {
                    if (f.Value.modifier != null)
                        if (!pass(f.Value.modifier.token, TokenType.RW_STATIC))
                            continue;
                }*/
                fs.Add(f.Value);
            }
            FieldNode[] fss = new FieldNode[fs.Count];
            fs.CopyTo(fss);
            addVariable(fss);
        }

        private void setFields(Dictionary<string, FieldNode> fields, ContextType cLASS)
        {
            foreach (KeyValuePair<string, FieldNode> key in fields)
            {
                if (type == ContextType.CLASS)
                {
                    if (!pass(key.Value.encapsulation.token, TokenType.RW_PRIVATE))
                    {
                        fields[key.Key] = key.Value;
                    }
                }
                else
                    fields[key.Key] = key.Value;
            }
        }

        private void setConstructors(Dictionary<string, ConstructorNode> constructors, ContextType cLASS)
        {
            foreach (KeyValuePair<string, ConstructorNode> key in constructors)
            {
                if (type == ContextType.CLASS)
                {
                    if (!pass(key.Value.encapsulation.token, TokenType.RW_PRIVATE))
                    {
                        constructors[key.Key] = key.Value;
                    }
                }
                else
                    constructors[key.Key] = key.Value;
            }
        }

        internal void addVariable(params FieldNode[] fields)
        {
            foreach (var f in fields)
            {
                if (variables.ContainsKey(f.id.ToString()))
                    throw new SemanticException("Variable '" + f.id.ToString() + "' already exist in the current context.", f.id.token);
                f.type.localy = false;
                f.type.onTableType = false;
                f.type.globally = false;
                variables[f.id.ToString()] = f;
            }
        }

        private void setMethods(Dictionary<string, MethodNode> methods, ContextType type)
        {
            foreach(KeyValuePair<string, MethodNode> key in methods)
            {
                if (type == ContextType.CLASS)
                {
                    if (!pass(key.Value.encapsulation.token, TokenType.RW_PRIVATE))
                    {
                        methods[key.Key] = key.Value;
                    }
                }else
                    methods[key.Key] = key.Value;
            }
        }

        private void checkType(ContextType contextType, API api)
        {
            if (type == ContextType.CLASS)
                return;
            if(type == ContextType.PARENT)
                removePrivates(TokenType.RW_PRIVATE);
            else if(type == ContextType.ATRIBUTE)
                removePrivates(TokenType.RW_PRIVATE, TokenType.RW_PROTECTED);
        }

        private void buildEnvironment(InterfaceNode node, API api)
        {
            name = node.ToString();
            methods = node.methods;
        }

        internal void removePrivates(params TokenType[] types)
        {
            Dictionary<string, FieldNode> fis = new Dictionary<string, FieldNode>();
            Dictionary<string, MethodNode> mts = new Dictionary<string, MethodNode>();
            Dictionary<string, ConstructorNode> ctrs = new Dictionary<string, ConstructorNode>();
            foreach(KeyValuePair<string, FieldNode> key in variables)
            {
                FieldNode f = key.Value;
                if(!pass(f.encapsulation.token, types)){
                    fis[key.Key] = f;
                }
            }

            foreach (KeyValuePair<string, MethodNode> key in methods)
            {
                MethodNode f = key.Value;
                if (!pass(f.encapsulation.token, types)){
                    mts[key.Key] = f;
                }
            }

            foreach (KeyValuePair<string, ConstructorNode> key in constructors)
            {
                ConstructorNode f = key.Value;
                if (!pass(f.encapsulation.token, types)){
                    ctrs[key.Key] = f;
                }
            }
            variables = fis;
            methods = mts;
            constructors = ctrs;
        }

        public bool pass(Token token, params TokenType[] types )
        {

            foreach(TokenType t in types)
            {
                if(t == token.type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
