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
        private ContextType type;

        public Context()
        {
            name = null;
            variables = new Dictionary<string, FieldNode>();
            methods = new Dictionary<string, MethodNode>();
            constructors = new Dictionary<string, ConstructorNode>();
        }

        public Context(string name,Dictionary<string, MethodNode> methods)
        {
            this.name = name;
            this.methods = methods;
        }
        public override string ToString()
        {
            return name;
        }

        public Context(string name,Dictionary<string, FieldNode> fields, Dictionary<string, MethodNode> methods, Dictionary<string, ConstructorNode> constructors)
        {
            this.name = name;
            this.variables = fields;
            this.methods = methods;
            this.constructors = constructors;
        }

        public Context(TypeDefinitionNode node, ContextType type, API api, bool isStatic = false) : this()
        {
            this.type= type;
            if(node is ClassDefinitionNode)
            {
                buildEnvironment(((ClassDefinitionNode)node), api);
            }else if(node is InterfaceNode)
            {
                buildEnvironment(((InterfaceNode)node), api);
            }
        }

        internal TypeDefinitionNode findVariable(Token id)
        {
            if (variables.ContainsKey(id.lexema))
            {
                    return variables[id.lexema].type;
            }
            return null;
        }

        private void buildEnvironment(ClassDefinitionNode node, API api)
        {
            name = node.ToString();
            variables = node.fields;
            methods = node.methods;
            constructors = node.constructors;
            checkType(type, api);
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
            removePrivates(true);
        }

        private void buildEnvironment(InterfaceNode node, API api)
        {
            name = node.ToString();
            methods = node.methods;
        }

        internal void removePrivates(bool remove)
        {
            if (!remove)
                return;
            Dictionary<string, FieldNode> fis = new Dictionary<string, FieldNode>();
            Dictionary<string, MethodNode> mts = new Dictionary<string, MethodNode>();
            Dictionary<string, ConstructorNode> ctrs = new Dictionary<string, ConstructorNode>();
            foreach(KeyValuePair<string, FieldNode> key in variables)
            {
                FieldNode f = key.Value;
                if(!pass(f.encapsulation.token, TokenType.RW_PRIVATE)){
                    fis[key.Key] = f;
                }
            }

            foreach (KeyValuePair<string, MethodNode> key in methods)
            {
                MethodNode f = key.Value;
                if (!pass(f.encapsulation.token, TokenType.RW_PRIVATE)){
                    mts[key.Key] = f;
                }
            }

            foreach (KeyValuePair<string, ConstructorNode> key in constructors)
            {
                ConstructorNode f = key.Value;
                if (!pass(f.encapsulation.token, TokenType.RW_PRIVATE)){
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
