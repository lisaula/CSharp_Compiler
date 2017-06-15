using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
using Compiler;

namespace Compiler_CS_DotNetCore.Semantic.Context
{
    public class ContextManager
    {
        public string name;
        public List<Context> contexts;
        public bool isStatic { get; set; }
        public ContextManager()
        {
            contexts = new List<Context>();
            isStatic = false;
        }

        public ContextManager(string name):this()
        {
            this.name = name;
        }

        internal ConstructorNode findConstructor(TypeDefinitionNode type,string ctr)
        {
            int last = contexts.Count - 1;
            return findCtrInContext(contexts[last], type,ctr);
        }

        private ConstructorNode findCtrInContext(Context context, TypeDefinitionNode type, string ctr)
        {
            string key = type.ToString() + "(" + ctr + ")";
            if (type.ToString() == context.name)
            {
                if (context.constructors.ContainsKey(key))
                    return context.constructors[key];
            }
            throw new SemanticException("Constructor '" + key + "' could not be found in current context '"+context.name+"'");
        }
        internal MethodNode findFunction(string name)
        {
            MethodNode t = null;
            for (int i = 0; i < contexts.Count; i++)
            {
                t = contexts[i].findFunction(name);
                if (t != null)
                {
                    if (isStatic) {
                        if (t.modifier == null)
                            throw new SemanticException("Cannot reference a non-static method '"+Utils.getMethodName(t)+"'");
                        if(t.modifier.token.type != TokenType.RW_STATIC)
                            throw new SemanticException("Cannot reference a non-static method '" + Utils.getMethodName(t) + "'");
                    }
                    return t;
                }
            }
            return null;
        }

        internal TypeDefinitionNode findVariable(Token id)
        {
            FieldNode t = null;
            for (int i = 0; i < contexts.Count;i++)
            {
                t = contexts[i].findVariable(id);
                if (t != null)
                {
                    if (isStatic)
                    {
                        if (t.modifier == null)
                            throw new SemanticException("Cannot reference a non-static field '" + t.id.ToString() + "'");
                        if(t.modifier.token.type != TokenType.RW_STATIC)
                            throw new SemanticException("Cannot reference a non-static field '" + t.id.ToString() + "'");
                    }
                    return t.type;
                }
            }
            return null;
        }

        internal List<Context> buildEnvironment(TypeDefinitionNode node, ContextType type, API api, bool isStatic= false)
        {
            List<Context> contexts = new List<Context>();
            contexts.Add(new Context(node, type, api));
            Dictionary<string, TypeDefinitionNode> parents = null;
            if (node is ClassDefinitionNode) {
                if(!(((ClassDefinitionNode)node).evaluated))
                    ((ClassDefinitionNode)node).checkInheritanceExistance(api);
                parents = ((ClassDefinitionNode)node).parents;
            }else if (node is InterfaceNode)
            {
                if(!(((InterfaceNode)node).evaluated))
                    ((InterfaceNode)node).checkInheritanceExistance(api);
                parents = ((InterfaceNode)node).parents;
            }
            if (parents != null)
            {
                foreach (KeyValuePair<string, TypeDefinitionNode> key in parents)
                {
                    contexts.AddRange(buildEnvironment(key.Value, ContextType.PARENT, api, isStatic));
                }
            }
            if(type == ContextType.CLASS)
            {
                contexts.Add(new Context(Singleton.tableTypes[Utils.GlobalNamespace+".Object"], ContextType.PARENT, api));
            }
            return contexts;
        }

        internal void pushFront(Context ctr_context)
        {
            contexts.Insert(0, ctr_context);
        }

        internal void addVariableToCurrentContext(params FieldNode[] fields)
        {
            foreach (var f in fields)
            {
                var ctx = contexts[0];
                ctx.addVariable(f);
            }
        }
    }
}
