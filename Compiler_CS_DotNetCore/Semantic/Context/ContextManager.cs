using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;

namespace Compiler_CS_DotNetCore.Semantic.Context
{
    public class ContextManager
    {
        public string name;
        public List<Context> contexts;

        public ContextManager()
        {
            contexts = new List<Context>();
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

        internal List<Context> buildEnvironment(TypeDefinitionNode node, ContextType type, API api)
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
            foreach(KeyValuePair<string, TypeDefinitionNode> key in parents)
            {
                contexts.AddRange(buildEnvironment(key.Value, ContextType.PARENT, api));
            }
            if(type == ContextType.CLASS)
            {
                contexts.Add(new Context(Singleton.tableTypes[Utils.GlobalNamespace+".Object"], ContextType.PARENT, api));
            }
            return contexts;
        }
    }
}
