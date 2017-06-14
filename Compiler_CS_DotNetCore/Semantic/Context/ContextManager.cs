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

        internal TypeDefinitionNode findFunction(bool base_reference, Token id)
        {
            throw new NotImplementedException();
        }

        internal TypeDefinitionNode findVariable(bool base_reference, Token id)
        {
            TypeDefinitionNode t = null;
            for (int i = base_reference ? 1:0; i < contexts.Count;i++)
            {
                t = contexts[i].findVariable(id);
                if (t != null)
                    return t;
            }
            return null;
        }

        internal List<Context> buildEnvironment(TypeDefinitionNode node, ContextType type, API api, bool isStatic= false)
        {
            List<Context> contexts = new List<Context>();
            contexts.Add(new Context(node, type, api, isStatic));
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
                contexts.Add(new Context(Singleton.tableTypes[Utils.GlobalNamespace+".Object"], ContextType.PARENT, api, isStatic));
            }
            return contexts;
        }

        internal void pushFront(Context ctr_context)
        {
            contexts.Insert(0, ctr_context);
        }

        internal void addVariableToCurrentContext(FieldNode f)
        {
            var ctx = contexts[0];
            ctx.addVariable(f);
        }
    }
}
