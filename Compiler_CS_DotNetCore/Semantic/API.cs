using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
using Compiler;

namespace Compiler_CS_DotNetCore.Semantic
{
    public class API
    {
        private List<string> paths;
        public Dictionary<string, NamespaceNode> trees;
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

        internal bool modifierPass(ModifierNode modifier, TokenType tokentype)
        {
            return modifier.token.type == tokentype;
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
