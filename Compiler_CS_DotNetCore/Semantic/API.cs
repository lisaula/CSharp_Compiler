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
        private string file;
        public API(List<string> paths)
        {
            this.paths = paths;
        }

        public void throwSemanticErrot(string msg)
        {
            throw new SemanticException(msg, file);
        }

        public Dictionary<string, NamespaceNode> buildTrees()
        {
            Dictionary<string, NamespaceNode> trees = new Dictionary<string, NamespaceNode>();
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

        internal NamespaceNode getParentNamespace(TypeDefinitionNode t)
        {
            if (t.parent_namespace == "blank")
            {
                return Singleton.tableNamespaces[t.file];
            }
            return Singleton.tableNamespaces[t.parent_namespace];
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
                string fname = getIdentifierListAsString(".",us.identifierList);
                fname += "." + name;
                if (Singleton.tableTypes.ContainsKey(fname))
                    return Singleton.tableTypes[fname];
            }
            string fullname = "blank." + name;
            if (Singleton.tableTypes.ContainsKey(fullname))
                return Singleton.tableTypes[fullname];

            throw new SemanticException("Could not find Type " + name + " in the current context");
        }

        internal TypeDefinitionNode findTypeInList(List<TypeDefinitionNode> typeList, string name)
        {
            foreach(TypeDefinitionNode tdn in typeList)
            {
                if (tdn.identifier.token.lexema == name)
                    return tdn;
            }
            return null;
        }

        internal void setNamespacesOnTableNms(string filename, NamespaceNode tree)
        {
            this.file = filename;
            Singleton.tableNamespaces[file] = tree;
            setNamespacesHerarchyOnTableNms(new List<string>(), tree.namespaceList);
        }

        private void setNamespacesHerarchyOnTableNms(List<string> namespace_, List<NamespaceNode> namespaceList)
        {
            foreach(NamespaceNode nms in namespaceList)
            {
                string key = getIdentifierListAsString(".", nms.identifierList);
                namespace_.Add(key);
                string value = string.Join(".", namespace_);
                if (!Singleton.tableNamespaces.ContainsKey(key))
                    Singleton.tableNamespaces[key] = nms ;
                if(!Singleton.tableNamespaces.ContainsKey(value))
                    Singleton.tableNamespaces[value] = nms;

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

        public void setClassesOnTableType( string filename,NamespaceNode tree)
        {
            this.file = filename;
            var name = new List<string>();
            name.Add("blank");
            setTypeListOnTableType(name, tree.typeList);
            setNamespacesOnTableType(new List<string>(), tree.namespaceList);
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
                    t.parent_namespace = string.Join(".", namespaces_);
                    t.file = this.file;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (Singleton.tableTypeContains(fullname))
                        throwSemanticErrot(fullname + " of type "+ t.GetType() + "already exist on " + t.identifier.token.ToString());
                    Singleton.tableTypes[fullname] = t;
                }else if(type is EnumDefinitionNode)
                {
                    var t = type as EnumDefinitionNode;
                    t.parent_namespace = string.Join(".", namespaces_);
                    t.file = this.file;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if ((Singleton.tableTypeContains(fullname)))
                        throwSemanticErrot(fullname + " of type "+ t.GetType() + " already exist on " + t.identifier.token.ToString());
                    Singleton.tableTypes[fullname] = t;
                }
                else if(type is InterfaceNode)
                {
                    var t = type as InterfaceNode;
                    t.parent_namespace = string.Join(".", namespaces_);
                    t.file = this.file;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (Singleton.tableTypeContains(fullname))
                        throwSemanticErrot(fullname + " of type "+t.GetType()+"already exist on " + t.identifier.token.ToString());
                    Singleton.tableTypes[fullname] = t;
                }
            }
        }
    }
}
