using Compiler.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler_CS_DotNetCore.Semantic
{

    public class Evaluator
    {
        public Dictionary<string, CompilationNode> trees;
        private API api;
        public Evaluator(List<string> paths)
        {
            api = new API(paths);
            trees = api.buildTrees();
            setTables();
            printTableTypes();
            printTableNamespaces();
            setUsingsInAllNamespaces();
            evaluateProject();
        }

        private void setUsingsInAllNamespaces()
        {
            foreach (KeyValuePair<string, CompilationNode> tree in trees)
            {
                api.setUsingsOnNamespace(tree.Value.usingList, tree.Value.namespaceList);
            }
        }

        private void evaluateProject()
        {
            foreach (KeyValuePair<string, CompilationNode> tree in trees)
            {
                try
                {
                    tree.Value.Evaluate(api);
                }catch(Exception ex)
                {
                    throw new Exception(tree.Key + ":" + ex.Message);
                }
            }
        }

        private static void printTableNamespaces()
        {
            Console.WriteLine("Table Namespaces");
            foreach (KeyValuePair<string, string> entry in Singleton.tableNamespaces)
            {
                Console.Out.WriteLine(entry.Key + " - " + entry.Value);
            }
        }

        private static void printTableTypes()
        {
            Console.WriteLine("Table Types");
            foreach (KeyValuePair<string, TypeDefinitionNode> entry in Singleton.tableTypes)
            {
                Console.Out.WriteLine(entry.Key + " - " + entry.Value.GetType());
            }
        }

        private void setTables()
        {
            foreach (KeyValuePair<string, CompilationNode> tree in trees)
            {
                api.setClassesOnTableType(tree.Key, tree.Value);
                api.setNamespacesOnTableNms(tree.Key, tree.Value);
            }
        }
    }
}
