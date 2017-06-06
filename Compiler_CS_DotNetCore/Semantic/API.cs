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

        public void setClassesOnTableType(ref Dictionary<string, TypeDefinitionNode> tableTypes, string filename,CompilationNode tree)
        {
            this.file = filename;
            var name = new List<string>();
            name.Add("blank");
            setTypeListOnTableType(ref tableTypes,name, tree.typeList);
            setNamespacesOnTableType(ref tableTypes, new List<string>(), tree.namespaceList);
        }

        private void setNamespacesOnTableType(ref Dictionary<string, TypeDefinitionNode> tableTypes, List<string> namespace_,List<NamespaceNode> namespaceList)
        {
            foreach (NamespaceNode nms in namespaceList)
            {
                namespace_.Add(nms.ToString());
                setTypeListOnTableType(ref tableTypes, namespace_, nms.typeList);
                setNamespacesOnTableType(ref tableTypes, namespace_, nms.namespaceList);
                namespace_.Remove(nms.ToString());
            }
        }

        private void setTypeListOnTableType(ref Dictionary<string, TypeDefinitionNode> tableTypes,List<string> namespaces_, List<TypeDefinitionNode> typeList)
        {
            foreach (TypeDefinitionNode type in typeList)
            {
                if(type is ClassDefinitionNode)
                {
                    var t = type as ClassDefinitionNode;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (tableTypes.ContainsKey(fullname))
                        throwSemanticErrot(fullname + " of type "+ t.GetType() + "already exist on " + t.id.token.ToString());
                    tableTypes[fullname] = t;
                }else if(type is EnumDefinitionNode)
                {
                    var t = type as EnumDefinitionNode;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (tableTypes.ContainsKey(fullname))
                        throwSemanticErrot(fullname + " of type "+ t.GetType() + " already exist on " + t.identifier.token.ToString());
                    tableTypes[fullname] = t;
                }
                else if(type is InterfaceNode)
                {
                    var t = type as InterfaceNode;
                    namespaces_.Add(t.ToString());
                    string fullname = String.Join(".", namespaces_);
                    namespaces_.Remove(t.ToString());
                    if (tableTypes.ContainsKey(fullname))
                        throwSemanticErrot(fullname + " of type "+t.GetType()+"already exist on " + t.token_identifier.ToString());
                    tableTypes[fullname] = t;
                }
            }
        }
    }
}
