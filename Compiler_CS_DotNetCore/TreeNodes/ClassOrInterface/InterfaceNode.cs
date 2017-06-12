using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class InterfaceNode : TypeDefinitionNode
    {
        public InheritanceNode inheritance;
        public Dictionary<string, MethodNode> methods;
        public EncapsulationNode encapsulation;
        public Dictionary<string, TypeDefinitionNode> parents;

        public InterfaceNode()
        {
            parent_namespace = null;
            methods = new Dictionary<string, MethodNode>();
            parents = null;
        }

        public InterfaceNode(Token token):this()
        {
            this.identifier = new IdentifierNode(token);
        }

        public InterfaceNode(EncapsulationNode encapsulation, Token token):this(token)
        {
            this.encapsulation = encapsulation;
        }

        public override void Evaluate(API api)
        {
            if (evaluated)
                return;
            Debug.printMessage("Evaluating " + identifier.token.lexema);
            if(encapsulation.token.type != TokenType.RW_PUBLIC)
            {
                throw new SemanticException("Interface "+identifier.token.lexema+" unreachable due to its encapsulation level. ",identifier.token);
            }
            checkInheritanceExistance(api);
            checkParents(api);
            checkMethods(api);
            evaluated = true;
        }

        private void checkMethods(API api)
        {
            foreach (KeyValuePair<string, MethodNode> key in methods) {
                api.checkParametersExistance(this, key.Value.parameters);
            }
        }

        public override void verifiCycle(TypeDefinitionNode type, API api)
        {
            if (parents == null)
                return;
            foreach (KeyValuePair<string, TypeDefinitionNode> p in parents)
            {
                if (type.Equals(p.Value) && api.getParentNamespace(type) == api.getParentNamespace(p.Value))
                    throw new SemanticException("Cycle inheritance detected in " + identifier.ToString() + ".", p.Value.identifier.token);
                p.Value.verifiCycle(type, api);
            }
        }

        public override bool Equals(object obj)
        {
            if(obj is InterfaceNode)
            {
                var o = obj as InterfaceNode;
                if (o.identifier.Equals(identifier))
                    return true;
            }
            return false;
        }
        private void checkParents(API api)
        {
            if (parents == null)
                return;
            foreach(KeyValuePair<string, TypeDefinitionNode> parent in parents)
            {
                if (!(parent.Value is InterfaceNode))
                    throw new SemanticException("Type '" + parent.Key + "' in " + identifier.token.lexema + " is not an interface",identifier.token);
                ((InterfaceNode)parent.Value).Evaluate(api);
            }
            verifiCycle(this, api);
        }

        internal List<Context> buildEnvironment()
        {
            List<Context> contexts = new List<Context>();
            if (parents != null)
            {
                foreach (KeyValuePair<string, TypeDefinitionNode> key in parents)
                {
                    contexts.AddRange(((InterfaceNode)key.Value).buildEnvironment());
                }
            }
            contexts.Add(buildContext());
            return contexts;
        }

        private Context buildContext()
        {
            return new Context(identifier.ToString(), methods);
        }

        private void checkInheritanceExistance(API api)
        {
            if (inheritance == null || inheritance.identifierList == null)
                return;
            parents = new Dictionary<string, TypeDefinitionNode>();
            foreach (List<IdentifierNode> parent in inheritance.identifierList)
            {
                string name = api.getIdentifierListAsString(".",parent);
                string nms = api.getParentNamespace(this);
                var usings = parent_namespace.usingList;
                usings.Add(new UsingNode(nms));
                TypeDefinitionNode tdn = api.findTypeInUsings(usings, name);
                if (tdn == null)
                    throw new SemanticException("Could not find Type '" + name + "' in the current context. ", parent[0].token);
                if (parents.ContainsKey(name))
                    throw new SemanticException("Redundant Inheritance. " + name + " was found twice as inheritance in " + identifier.token.lexema, identifier.token);
                parents[name] = tdn;
            }
        }

        public override string ToString()
        {
            return identifier.token.lexema;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }
    }
}