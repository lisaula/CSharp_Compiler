using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
using Compiler;
using Compiler_CS_DotNetCore.Semantic.Context;

namespace Compiler_CS_DotNetCore.Semantic
{
    public class API
    {
        private List<string> paths;
        public Dictionary<string, NamespaceNode> trees;
        public ContextManager contextManager, class_contextManager;
        public TypeDefinitionNode working_type;

        public List<string> assignmentRules;

        public void setAssigmentRules()
        {
            assignmentRules = new List<string>();
            assignmentRules.Add(Utils.Bool + "," + Utils.Bool);
            assignmentRules.Add(Utils.String + "," + Utils.String);
            assignmentRules.Add(Utils.Float + "," + Utils.Int);
            assignmentRules.Add(Utils.Float + "," + Utils.Float);
            assignmentRules.Add(Utils.Float + "," + Utils.Char);
            assignmentRules.Add(Utils.Char + "," + Utils.Char);
            assignmentRules.Add(Utils.Int + "," + Utils.Char);
            assignmentRules.Add(Utils.Int + "," + Utils.Int);
            assignmentRules.Add(Utils.Class + "," + Utils.Null);
            assignmentRules.Add(Utils.String + "," + Utils.Null);
            assignmentRules.Add(Utils.Enum + "," + Utils.Enum);
        }

        internal string ValidateExpressionCode(VariableInitializer assignment)
        {
            if(assignment == null)
            {
                return "null";
            }
            StringBuilder builder = new StringBuilder();
            assignment.generateCode(builder, this);
            return builder.ToString();
        }

        internal bool pass(string v, params string[] primitives)
        {
            List<string> l = new List<string>(primitives);
            return l.Contains(v);
        }

        internal TypeDefinitionNode CopyType(TypeDefinitionNode t)
        {
            TypeDefinitionNode nuevo = new ClassDefinitionNode();
            nuevo.localy = t.localy;
            nuevo.onTableType = t.onTableType;
            return nuevo;
        }

        public void setWorkingType(TypeDefinitionNode type)
        {
            working_type = type;
        }

        internal void addVariableToCurrentContext(Dictionary<string, FieldNode> variable)
        {
            List<FieldNode> fields = new List<FieldNode>();
            foreach(var key in variable)
            {
                fields.Add(key.Value);
            }
            contextManager.addVariableToCurrentContext(fields.ToArray());
        }

        internal List<TypeDefinitionNode> getArgumentsType(List<ExpressionNode> arguments)
        {
            List<TypeDefinitionNode> type = new List<TypeDefinitionNode>();
            if(arguments != null)
            {
                var context_placeholder = contextManager;
                if(class_contextManager != null)
                {

                    contextManager = class_contextManager;
                }
                foreach(var a in arguments)
                {
                    type.Add(a.evaluateType(this));
                }
                contextManager = context_placeholder;
            }
            return type;
        }

        public TypeDefinitionNode searchType(TypeDefinitionNode type)
        {
            if (working_type == null)
                throw new SemanticException("Working directory has not been set.", type.getPrimaryToken());
            if (type is NullTypeNode)
                return type;
            string name = type.ToString();
            if (type is ArrayTypeNode)
            {
                name = ((ArrayTypeNode)type).getArrayType().ToString();
            }
            string nms = getParentNamespace(working_type);
            var usings = working_type.parent_namespace.usingList;
            usings.Add(new UsingNode(nms));
            TypeDefinitionNode tdn = findTypeInUsings(usings, name);
            if (tdn == null)
                throw new SemanticException("Could not find Type '" + name + "' in the current context. ", type.getPrimaryToken());
            return tdn;
        }

        internal List<TypeDefinitionNode> getCurrentReturnType()
        {
            if(contextManager.contexts.Count > 0)
            {
                return contextManager.contexts[0].returnsFound;
            }
            return null;
        }

        internal TypeDefinitionNode searchInTableType(string typeName)
        {
            if (working_type == null)
            {
                throw new SemanticException("Working directory has not been set.");
            }
            if (typeName == Utils.Null)
                return new NullTypeNode();
            string name = typeName;
            string nms = getParentNamespace(working_type);
            var usings = working_type.parent_namespace.usingList;
            usings.Add(new UsingNode(nms));
            TypeDefinitionNode tdn = findTypeInUsings(usings, name);
            return tdn;
        }

        internal void checkExpression(string returnType, string expression, StringBuilder builder)
        {

            if (returnType == Utils.Int)
            {
                if (expression != Utils.Int)
                {
                    if (expression == Utils.Char)
                        builder.Append("charCodeAt");
                    else
                        builder.Append("+");
                }
            }
            else if (returnType == Utils.Float)
            {
                if (expression != Utils.Float)
                {
                    if (expression == Utils.Char)
                        builder.Append("charCodeAt");
                    else
                        builder.Append("+");
                }
            }
            else if (returnType == Utils.String)
            {
                if (expression != Utils.String)
                {
                    builder.Append("itoa");
                }
            }else if(returnType == Utils.Char)
            {
                if(expression != Utils.Char)
                {
                    if (expression == Utils.Int || expression == Utils.Float)
                        builder.Append("itoa");
                }
            }
        }

        internal bool compareIndexes(List<ArrayNode> indexes1, List<ArrayNode> indexes2)
        {
            if(indexes1.Count == indexes2.Count)
            {
                for(int i =0; i< indexes2.Count; i++)
                {
                    if (!indexes1[i].Equals(indexes2[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        internal bool findConstructor(TypeDefinitionNode type, string arguments)
        {

            if (!(type is ClassDefinitionNode))
                throw new SemanticException("Cannot instanciate '" + type.ToString() + "' due to is not a Class.");
            ClassDefinitionNode cdn = ((ClassDefinitionNode)type);
            if (!TokenPass(cdn.encapsulation.token, TokenType.RW_PUBLIC))
                throw new SemanticException("Type '" + type.ToString() + "' can't be reached due to encapsulation level.");
            if (cdn.isAbstract)
                throw new SemanticException("Abstract class '"+cdn.ToString()+"'can't be instantiated.");
            string key = cdn.ToString() + "(" + arguments + ")";
            if (!cdn.constructors.ContainsKey(key))
                throw new SemanticException("Constructor '" + key + "' could not be found class '" + cdn+ "'");
            if (!TokenPass(cdn.constructors[key].encapsulation.token, TokenType.RW_PUBLIC))
                    throw new SemanticException("Constructor '" + key + "' can't be reached due to encapsulation level.");
            return true;
        }

        internal void addParametersToCurrentContext(List<Parameter> parameters)
        {
            if (parameters == null)
                return;
            foreach(var parameter in parameters)
            {
                FieldNode f = convertToField(parameter);
                contextManager.addVariableToCurrentContext(f);
            }
        }

        public FieldNode convertToField(Parameter parameter)
        {
            var token = new Token();
            token.type = TokenType.RW_PUBLIC;
            token.lexema = "public";
            FieldNode f = new FieldNode(new EncapsulationNode(token), null, parameter.type, parameter.id, null);
            return f;
        }

        internal bool TokenPass(Token @operator, params TokenType[] types)
        {
            foreach(TokenType t in types)
            {
                if (t == @operator.type)
                    return true;
            }
            return false;
        }

        public API()
        {
            trees = new Dictionary<string, NamespaceNode>();

            //geting include tree
            var inputString = new InputString(Utils.txtIncludes);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer, "IncludesDefault");
            NamespaceNode tree;
            tree = parser.parse();
            setAllEvaluatesTrue(tree);
            trees["IncludesDefault"] = tree;
            setClassesOnTableType(tree);
            contextManager = new ContextManager();
            class_contextManager = null;
            setAssigmentRules();
        }
        public void checkParametersExistance(TypeDefinitionNode obj,List<Parameter> parameters)
        {
            if (parameters == null)
                return;
            foreach (Parameter p in parameters)
            {
                string name = p.type.ToString();
                if (p.type is ArrayTypeNode)
                    name = ((ArrayTypeNode)p.type).getArrayType().ToString();
                string nms = getParentNamespace(obj);
                var usings = obj.parent_namespace.usingList;
                usings.Add(new UsingNode(nms));
                TypeDefinitionNode tdn = findTypeInUsings(usings, name);
                if (tdn == null)
                    throw new SemanticException("Could not find Type '" + name + "' in the current context. ", p.id.token);
                if(tdn is InterfaceNode || tdn is VoidTypeNode)
                    throw new SemanticException("The type '" + tdn.ToString() + "' is not valid for parameter " + p.id.ToString(), p.type.getPrimaryToken());
                p.type.typeNode = p.type;
                if (p.type is ArrayTypeNode)
                {
                    ((ArrayTypeNode)p.type).type = tdn;
                }
                else
                    p.type = tdn;
            }
        }

        internal void checkReturnTypeExistance(ref TypeDefinitionNode returnType)
        {
            TypeDefinitionNode t = null;
            if (returnType is ArrayTypeNode)
            {
                t = searchType(((ArrayTypeNode)returnType).type);
                ((ArrayTypeNode)returnType).type = returnType;
            }
            else
            {
                returnType = searchType(returnType);
            }

        }

        private void setAllEvaluatesTrue(NamespaceNode tree)
        {
            foreach(TypeDefinitionNode td in tree.typeList)
            {
                td.evaluated = true;
            }
            foreach(NamespaceNode nms in tree.namespaceList)
            {
                setAllEvaluatesTrue(nms);
            }
        }

        internal bool checkRelationBetween(TypeDefinitionNode tdn, TypeDefinitionNode type)
        {
            TypeDefinitionNode placeHolder = working_type;
            bool found = ((ClassDefinitionNode)tdn).checkRelationWith(type, this);
            bool found1 = ((ClassDefinitionNode)type).checkRelationWith(type, this);
            working_type = placeHolder;
            return found || found1;
        }

        public API(List<string> paths): this()
        {
            this.paths = paths;
        }

        public static void throwSemanticErrot(string msg, string file)
        {
            throw new SemanticException(msg, file);
        }

        public Dictionary<string, NamespaceNode> buildTrees()
        {
            foreach (string s in paths)
            {
                var txt = System.IO.File.ReadAllText(s);
                var inputString = new InputString(txt);
                var lexer = new LexicalAnalyzer(inputString);
                var parser = new Parser(lexer, s);
                NamespaceNode tree;   
                tree = parser.parse();
                trees[s] =tree;
            }
            return trees;
        }

        internal void popContext(params Context.Context[] context)
        {
            foreach (Context.Context c in context)
            {
                contextManager.contexts.Remove(c);
            }

        }
        public void popFrontContext()
        {
            if (contextManager.contexts.Count > 0)
            {
                Context.Context c = contextManager.contexts[0];
                if(contextManager.contexts.Count > 1)
                {
                    contextManager.contexts[1].returnsFound.AddRange(c.returnsFound);
                }
                contextManager.contexts.RemoveAt(0);
            }
        }
        internal void pushContext(params Context.Context[] context)
        {
            contextManager.contexts.AddRange(context);
        }

        internal Dictionary<string, NamespaceNode> buildTreesFromInput(InputString input)
        {
            Dictionary<string, NamespaceNode> trees = new Dictionary<string, NamespaceNode>();
            var inputString = input;
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer, "Prueba");
            NamespaceNode tree;
            tree = parser.parse();
            trees["Prueba"] = tree;
            return trees;
        }

        internal bool modifierPass(ModifierNode modifier, params TokenType[] types)
        {
            if (modifier == null)
                return false;
            foreach (TokenType t in types) {
                if (modifier.token.type == t)
                    return true;
            }
            return false;
        }

        internal TokenType getEncapsulation(EncapsulationNode encapsulation)
        {
            return encapsulation.token.type;
        }

        internal void setUsingsOnNamespace(List<UsingNode> usingList, List<NamespaceNode> namespaceList, List<string> namespaces)
        {
            if (namespaceList == null)
                return;

            foreach(NamespaceNode nms in namespaceList)
            {
                setNamespacesInUsingList(ref nms.usingList, namespaces);
                string nms_name = getIdentifierListAsString(".", nms.identifierList);
                namespaces.Add(nms_name);
                nms.usingList.AddRange(usingList);
                setUsingsOnNamespace(nms.usingList, nms.namespaceList, namespaces);
                namespaces.Remove(nms_name);
            }
        }

        public string getParentNamespace(TypeDefinitionNode t)
        {
            List<string> parents_name = getParentsName(t.parent_namespace);
            return string.Join(".", parents_name);
        }

        public string getFullNamespaceName(TypeDefinitionNode t)
        {
            List<string> parents_name = new List<string>();
            parents_name.Add(Utils.GlobalNamespace);
            parents_name.AddRange(getParentsName(t.parent_namespace));
            return string.Join(".", parents_name);
        }
        private List<string> getParentsName(NamespaceNode parent_namespace)
        {
            if(parent_namespace == null)
            {
                return new List<string>();
            }
            string name = getIdentifierListAsString(".", parent_namespace.identifierList);
            var lista = getParentsName(parent_namespace.parent);
            if(name != Utils.GlobalNamespace)
                lista.Add(name);
            return lista;
        }

        internal void checkParentMethodOnMe(MethodNode mymethodNode, MethodNode parent_method, TypeDefinitionNode parent)
        {
            if (mymethodNode.id.ToString() == "getNombre")
                Console.WriteLine();
            if(parent is InterfaceNode)
            {
                if (mymethodNode.modifier != null) {
                    if (!modifierPass(mymethodNode.modifier, TokenType.RW_VIRTUAL, TokenType.RW_ABSTRACT))
                        throw new SemanticException("Modifier '" + mymethodNode.modifier.ToString() + "' can't be applied to method "+Utils.getMethodName(mymethodNode)+".", mymethodNode.modifier.token);

                    if (!modifierPass(mymethodNode.modifier, TokenType.RW_ABSTRACT))
                        mymethodNode.modifier.evaluated = true;
                }
            }
            else if(parent is ClassDefinitionNode)
            {
                if(modifierPass(parent_method.modifier, TokenType.RW_ABSTRACT, TokenType.RW_OVERRIDE, TokenType.RW_VIRTUAL)){
                    if (modifierPass(mymethodNode.modifier, TokenType.RW_OVERRIDE)){
                        if (mymethodNode.encapsulation.token.type == parent_method.encapsulation.token.type)
                            mymethodNode.modifier.evaluated = true;
                    } else
                        throw new SemanticException("Method " + Utils.getMethodName(mymethodNode) + " hide method " + parent.ToString() + "." + Utils.getMethodName(parent_method) + ".", mymethodNode.returnType.getPrimaryToken());
                }
                else
                {
                    if(parent_method.modifier == null)
                        throw new SemanticException("Method" + mymethodNode.id.ToString() + " cannot override inherit member " + parent.ToString() + "." + Utils.getMethodName(parent_method) + " becase is not marked as abstract, virtual or override.", mymethodNode.modifier.token);
                    throw new SemanticException("Method " + Utils.getMethodName(mymethodNode) + " hide method " + parent.ToString() +"."+ Utils.getMethodName(parent_method) + ".", mymethodNode.returnType.getPrimaryToken());
                }
            }
            if (!mymethodNode.returnType.Equals(parent_method.returnType))
                throw new SemanticException("Method " + Utils.getMethodName(mymethodNode) + " hide method " + parent.ToString() +"."+ Utils.getMethodName(parent_method) + ". Not the same return type.", mymethodNode.returnType.getPrimaryToken());
        }

        internal Dictionary<string, MethodNode> getParentMethods(TypeDefinitionNode parent)
        {
            if(parent is InterfaceNode)
            {
                return ((InterfaceNode)parent).methods;
            }else if(parent is ClassDefinitionNode)
            {
                return ((ClassDefinitionNode)parent).methods;
            }
            throw new Exception("Not a Class or interface type. Methods couldn't be extracted ");
        }

        internal bool methodIsAbstract(MethodNode value)
        {
            if(value.modifier != null)
            {
                return value.modifier.token.type == TokenType.RW_ABSTRACT;
            }
            return false;
        }

        private void setNamespacesInUsingList(ref List<UsingNode> usingList, List<string> namespaces)
        {
            foreach(string nms in namespaces)
            {
                var token = new Token();
                token.lexema = nms;
                var id = new IdentifierNode(token);
                var un = new UsingNode();
                un.identifierList.Add(id);
                usingList.Insert(0, un);
            }
        }

        internal TypeDefinitionNode findTypeInUsings(List<UsingNode> usingList, string name)
        {
            foreach(UsingNode us in usingList)
            {
                string fname = Utils.GlobalNamespace + "." + getIdentifierListAsString(".",us.identifierList);
                fname += "." + name;
                if (Singleton.tableTypes.ContainsKey(fname))
                    return Singleton.tableTypes[fname];
            }
            if(Singleton.tableTypes.ContainsKey(name))
                return Singleton.tableTypes[name];

            string fullname = Utils.GlobalNamespace+"." + name;
            if (Singleton.tableTypes.ContainsKey(fullname))
                return Singleton.tableTypes[fullname];

            return null;
        }

        internal void setNamespacesOnTableNms(NamespaceNode tree)
        {
            var l = new List<string>();
            l.Add(getIdentifierListAsString(".",tree.identifierList));
            setNamespacesHerarchyOnTableNms(l, tree.namespaceList);
        }

        private void setNamespacesHerarchyOnTableNms(List<string> namespace_, List<NamespaceNode> namespaceList)
        {
            foreach(NamespaceNode nms in namespaceList)
            {
                string key = getIdentifierListAsString(".", nms.identifierList);
                namespace_.Add(key);
                string value = string.Join(".", namespace_);
                Singleton.tableNamespaces.Add(value);
                setNamespacesHerarchyOnTableNms(namespace_, nms.namespaceList);
                namespace_.Remove(key);
            }
        }

        public string getIdentifierListAsString(string sep, List<IdentifierNode> identifiers)
        {
            List<string> name = new List<string>();
            foreach(IdentifierNode id in identifiers)
            {
                name.Add(id.token.lexema);
            }
            return string.Join(sep, name);
        }

        public void setClassesOnTableType(NamespaceNode tree)
        {
            var name = new List<string>();
            name.Add(Utils.GlobalNamespace);
            setTypeListOnTableType(name, tree.typeList);
            setNamespacesOnTableType(name, tree.namespaceList);
        }

        private void setNamespacesOnTableType(List<string> namespace_,List<NamespaceNode> namespaceList)
        {
            foreach (NamespaceNode nms in namespaceList)
            {
                namespace_.Add(nms.ToString());
                setTypeListOnTableType(namespace_, nms.typeList);
                setNamespacesOnTableType(namespace_, nms.namespaceList);
                namespace_.Remove(nms.ToString());
            }
        }

        private void setTypeListOnTableType(List<string> namespaces_, List<TypeDefinitionNode> typeList)
        {
            foreach (TypeDefinitionNode type in typeList)
            {
                if(type is ClassDefinitionNode)
                {
                    var t = type as ClassDefinitionNode;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (Singleton.tableTypeContains(fullname))
                        throw new SemanticException(fullname + " of type "+ t.GetType() + "already exist. At ", t.identifier.token);
                    Singleton.tableTypes[fullname] = t;
                }else if(type is EnumDefinitionNode)
                {
                    var t = type as EnumDefinitionNode;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if ((Singleton.tableTypeContains(fullname)))
                        throw new SemanticException(fullname + " of type " + t.GetType() + "already exist. At ", t.identifier.token);
                    Singleton.tableTypes[fullname] = t;
                }
                else if(type is InterfaceNode)
                {
                    var t = type as InterfaceNode;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (Singleton.tableTypeContains(fullname))
                        throw new SemanticException(fullname + " of type " + t.GetType() + "already exist. At ", t.identifier.token);
                    Singleton.tableTypes[fullname] = t;
                }
            }
        }
    }
}
