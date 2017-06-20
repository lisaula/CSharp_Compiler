using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
using Compiler;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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
                    {
                        t.returnType.globally = true;
                        t.returnType.functionOwner = contexts[i].owner;
                    }
                    else
                        t.returnType.localy = true;

                    if (t.modifier != null && t.modifier.token.type == TokenType.RW_STATIC)
                        t.returnType.functionStatic = true;
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
                    TypeDefinitionNode n = copy(t.type);
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
                        n.globally = true;
                    else
                        n.localy = true;
                    return n;
                }
            }
            return null;
        }

        private TypeDefinitionNode copy(TypeDefinitionNode t)
        {
            if (t is ClassDefinitionNode) {
                var c = t as ClassDefinitionNode;
                var n  = new ClassDefinitionNode();
                n.constructors = c.constructors;
                n.encapsulation = c.encapsulation;
                n.evaluated = c.evaluated;
                n.fields = c.fields;
                n.generated = c.generated;
                n.globally = false ;
                n.identifier = c.identifier;
                n.inheritance = c.inheritance;
                n.isAbstract= c.isAbstract;
                n.isStatic = c.isStatic;
                n.localy = false;
                n.methods = c.methods;
                n.onTableType = false;
                n.parents = c.parents;
                n.parent_namespace = c.parent_namespace;
                n.typeNode = c.typeNode;
                return n;
            }
            else if(t is InterfaceNode)
            {
                var c = t as InterfaceNode;
                var n = new InterfaceNode();
                n.encapsulation = c.encapsulation;
                n.evaluated = c.evaluated;
                n.globally = false;
                n.identifier = c.identifier;
                n.inheritance = c.inheritance;
                n.isStatic = c.isStatic;
                n.localy = false;
                n.methods = c.methods;
                n.onTableType = false;
                n.parents = c.parents;
                n.parent_namespace = c.parent_namespace;
                n.typeNode = c.typeNode;
                return n;
            }
            else if(t is EnumNode)
            {
                var c = t as EnumNode;
                var n = new EnumNode();
                n.encapsulation = c.encapsulation;
                n.evaluated = c.evaluated;
                n.globally = false;
                n.identifier = c.identifier;
                n.isStatic = c.isStatic;
                n.localy = false;
                n.onTableType = false;
                n.parent_namespace = c.parent_namespace;
                n.typeNode = c.typeNode;

                n.enumNodeList = c.enumNodeList;
                n.expressionNode = c.expressionNode;
                return n;
            }
            else if (t is EnumDefinitionNode)
            {
                var c = t as EnumDefinitionNode;
                var n = new EnumDefinitionNode();
                n.encapsulation = c.encapsulation;
                n.evaluated = c.evaluated;
                n.globally = false;
                n.identifier = c.identifier;
                n.isStatic = c.isStatic;
                n.localy = false;
                n.onTableType = false;
                n.parent_namespace = c.parent_namespace;
                n.typeNode = c.typeNode;

                n.enumNodeList = c.enumNodeList;
                return n;
            }
            return t;
        }

        public TypeDefinitionNode clone(TypeDefinitionNode t)
        {

            System.Type[] types = { typeof(UsingNode), typeof(NamespaceNode), typeof(EnumDefinitionNode)
            , typeof(EnumNode), typeof(InterfaceNode), typeof(ClassDefinitionNode), typeof(FieldNode)
            , typeof(MethodNode), typeof(ConstructorNode),typeof(ConstructorInitializerNode), typeof(IdentifierNode), typeof(Token)
            , typeof(ExpressionNode), typeof(Parameter), typeof(ModifierNode), typeof(PrimitiveType), typeof(DictionaryTypeNode)
            , typeof(LiteralNode), typeof(StatementExpressionNode), typeof(FunctionCallExpression), typeof(AccessMemory)
            , typeof(ReferenceAccessNode), typeof(ForStatementNode), typeof(ForeachStatementNode), typeof(WhileStatementNode)
            , typeof(DoStatementNode), typeof(IfStatementNode),typeof(SwitchStatementNode) , typeof(BodyStatement), typeof(LocalVariableDefinitionNode)
            , typeof(EmbeddedStatementNode), typeof(IdentifierTypeNode), typeof(ClassInstantiation), typeof(ArrayInstantiation), typeof(ConditionExpression)
            , typeof(AssignmentNode), typeof(PostAdditiveExpressionNode), typeof(UnaryExpressionNode), typeof(PreExpressionNode),
            typeof(VoidTypeNode), typeof(ArrayAccessNode), typeof(ParenthesizedExpressionNode), typeof(ArithmeticExpression), typeof(VarType)
            , typeof(BinaryExpression), typeof(TernaryExpressionNode), typeof(JumpStatementNode), typeof(CastingExpressionNode)
            , typeof(InlineExpressionNode),typeof(SubExpression), typeof(SumExpression), typeof(MultExpression), typeof(DivExpression), typeof(LogicalExpression),
            typeof(RelationalExpression), typeof(EqualityExpression), typeof(ModExpression), typeof(IsASExpression)};

            var serializer = new XmlSerializer(typeof(TypeDefinitionNode), types);
            byte[] byteArray = Encoding.UTF8.GetBytes("cloning.xml");
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new MemoryStream(byteArray);

            using (var writer = XmlWriter.Create(stream))
            {
                serializer.Serialize(writer, t);
            }

            var des = new XmlSerializer(typeof(TypeDefinitionNode), types);
            TypeDefinitionNode nuevo = null;
            using (var reader = XmlReader.Create("cloning.xml"))
            {
                nuevo = (TypeDefinitionNode)serializer.Deserialize(reader);
            }
            return nuevo;
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
                f.type.localy = false;
                f.type.globally = false;
                f.type.onTableType = false;
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
