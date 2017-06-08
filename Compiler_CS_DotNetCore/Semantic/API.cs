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

        public Dictionary<string, CompilationNode> buildTrees()
        {
            Dictionary<string, CompilationNode> trees = new Dictionary<string, CompilationNode>();
            foreach (string s in paths)
            {
                var txt = System.IO.File.ReadAllText(s);
                var inputString = new InputString(txt);
                var lexer = new LexicalAnalyzer(inputString);
                var parser = new Parser(lexer, s);
                CompilationNode tree;   
                tree = parser.parse();
                trees[s] =tree;
            }
            return trees;
        }

        internal TokenType getEncapsulation(EncapsulationNode encapsulation)
        {
            return encapsulation.token.type;
        }

        internal void setUsingsOnNamespace(List<UsingNode> usingList, List<NamespaceNode> namespaceList)
        {
            if (namespaceList == null)
                return;
            foreach(NamespaceNode nms in namespaceList)
            {
                nms.usingList.AddRange(usingList);
                setUsingsOnNamespace(nms.usingList, nms.namespaceList);
            }
        }

        internal void setNamespacesOnTableNms(string filename, CompilationNode tree)
        {
            this.file = filename;
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
                    Singleton.tableNamespaces[key] = value ;
                if(!Singleton.tableNamespaces.ContainsKey(value))
                    Singleton.tableNamespaces[value] = value;

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

        public void setClassesOnTableType( string filename,CompilationNode tree)
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
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (Singleton.tableTypeContains(fullname))
                        throwSemanticErrot(fullname + " of type "+ t.GetType() + "already exist on " + t.id.token.ToString());
                    Singleton.tableTypes[fullname] = t;
                }else if(type is EnumDefinitionNode)
                {
                    var t = type as EnumDefinitionNode;
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
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (Singleton.tableTypeContains(fullname))
                        throwSemanticErrot(fullname + " of type "+t.GetType()+"already exist on " + t.token_identifier.ToString());
                    Singleton.tableTypes[fullname] = t;
                }
            }
        }
    }
}
