using Compiler.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler_CS_DotNetCore.Semantic
{

    public class Evaluator
    {
        public Dictionary<string, NamespaceNode> trees;
        private API api;

        public Evaluator()
        {
            Debug.print = true;
        }
        public Evaluator(List<string> paths, API api, Compiler.InputString input) : this()
        {
            this.api = api;
            trees = api.buildTreesFromInput(input);
            setTables();
            printTableTypes();
            printTableNamespaces();
            setUsingsInAllNamespaces();
            evaluateProject();
        }

        public Evaluator(List<string> paths) : this()
        {
            this.api = new API(paths);
            trees = api.buildTrees();
            setTables();
            printTableTypes();
            printTableNamespaces();
            setUsingsInAllNamespaces();
            evaluateProject();
        }

        private void setUsingsInAllNamespaces()
        {
            foreach (KeyValuePair<string, NamespaceNode> tree in trees)
            {
                api.setUsingsOnNamespace(tree.Value.usingList, tree.Value.namespaceList, new List<string>());
            }
        }

        private void evaluateProject()
        {
            foreach (KeyValuePair<string, NamespaceNode> tree in trees)
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

        private void printTableNamespaces()
        {
            Debug.printMessage("Table Namespaces");
            foreach (string nms in Singleton.tableNamespaces)
            {
                Debug.printMessage(nms);
            }
        }

        private void printTableTypes()
        {
            Debug.printMessage("Table Types");
            foreach (KeyValuePair<string, TypeDefinitionNode> entry in Singleton.tableTypes)
            {
                Debug.printMessage(entry.Key + " - " + entry.Value.GetType());
            }
        }

        private void setTables()
        {
            foreach (KeyValuePair<string, NamespaceNode> tree in trees)
            {
                try
                {
                    api.setClassesOnTableType(tree.Value);
                    api.setNamespacesOnTableNms(tree.Value);
                }
                catch (SemanticException se)
                {
                    throw new SemanticException(tree.Key + ": " + se.Message);
                }
            }
        }
    }
}
