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
        public int initialSearch;

        public bool isStatic { get; set; }
        public bool Enums_or_Literal { get; internal set; }

        public ContextManager()
        {
            contexts = new List<Context>();
            isStatic = false;
            Enums_or_Literal = false;
            initialSearch = 0;
        }

        internal TypeDefinitionNode getParent(TypeDefinitionNode owner)
        {
            int count = 0;
            foreach(Context c in contexts)
            {
                if (c.owner != null)
                    count++;
                if (count == 2)
                    return c.owner;
            }
            throw new SemanticException("Object '" + owner.ToString() + "' has no parent.", owner.identifier.token);
        }

        internal TypeDefinitionNode getThis(TypeDefinitionNode owner)
        {
            return owner;
        }

        public ContextManager(string name):this()
        {
            this.name = name;
        }

        internal void returnTypeFound(TypeDefinitionNode t)
        {
            contexts[0].addReturnType(t);
        }

        internal void jumpValidation(Token token)
        {
            if(token.type == TokenType.RW_CONTINUE)
            {
                if (!IamInsideContext(ContextType.ITERATIVE))
                    throw new SemanticException("Jump statement '"+token.lexema+"' not in an enclosing loop.", token);
            }else if(token.type == TokenType.RW_BREAK)
            {
                if (!IamInsideContext(ContextType.SWITCH, ContextType.ITERATIVE))
                    throw new SemanticException("Jump statement '" + token.lexema + "' not in an enclosing loop or switch.", token);
            }else if(token.type == TokenType.RW_RETURN)
            {
                if (!IamInsideContext(ContextType.ITERATIVE, ContextType.METHOD, ContextType.CONSTRUCTOR))
                    throw new SemanticException("Jump statement '" + token.lexema + "' not in an enclosing loop or switch.", token);
            }
        }

        private bool IamInsideContext(params ContextType[] iTERATIVE)
        {
            List<ContextType> contextTypes = new List<ContextType>(iTERATIVE); 
            foreach (Context c in contexts)
            {
                if (contextTypes.Contains(c.type))
                    return true;
            }
            return false;
        }
        internal MethodNode findFunction(string name)
        {
            MethodNode t = null;
            for (int i = initialSearch; i < contexts.Count; i++)
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
                    if (contexts[i].type == ContextType.CLASS || contexts[i].type == ContextType.PARENT)
                        t.returnType.globally= true;
                    else
                        t.returnType.localy= true;
                    return t;
                }
            }
            return null;
        }

        internal void pushFront(TypeDefinitionNode it, IdentifierNode id)
        {

            FieldNode f = convertToField(it, id);
            addVariableToCurrentContext(f);
        }

        private FieldNode convertToField(TypeDefinitionNode it, IdentifierNode id)
        {
            var token = new Token();
            token.type = TokenType.RW_PUBLIC;
            token.lexema = "public";
            FieldNode f = new FieldNode(new EncapsulationNode(token), null, it, id, null);
            return f;
        }
        
        internal TypeDefinitionNode findVariable(Token id)
        {
            FieldNode t = null;
            for (int i = initialSearch; i < contexts.Count;i++)
            {
                t = contexts[i].findVariable(id);
                if (t != null)
                {
                    if (isStatic)
                    {
                        if (t.modifier == null)
                            throw new SemanticException("Cannot reference a non-static field '" + t.id.ToString() + "'");
                        if (t.modifier.token.type != TokenType.RW_STATIC)
                            throw new SemanticException("Cannot reference a non-static field '" + t.id.ToString() + "'");
                    }
                    if (Enums_or_Literal)
                    {
                        if (!(t.type is EnumDefinitionNode))
                            throw new SemanticException("Field '"+t.id.ToString()+"' of type '"+t.type.getComparativeType()+"'is not a enum or literal. ");
                    }
                    if (contexts[i].type == ContextType.CLASS || contexts[i].type == ContextType.PARENT)
                        t.type.globally = true;
                    else
                        t.type.localy = true;
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
            if(type == ContextType.CLASS || type == ContextType.ATRIBUTE)
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
            //checkVariableExistance(fields);
            foreach (var f in fields)
            {
                var ctx = contexts[0];
                ctx.addVariable(f);
            }
        }

        private void checkVariableExistance(params FieldNode[] fields)
        {
            foreach (FieldNode f in fields) {
                foreach (Context c in contexts)
                {
                    if (c.variableExist(f))
                        throw new SemanticException("Variable '" + f.id.ToString() + "' already exist in context.", f.id.token);
                }
            }
        }
    }
}
