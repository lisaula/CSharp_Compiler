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
            foreach (KeyValuePair<string, CompilationNode> tree in trees)
            {
                api.setClassesOnTableType(tree.Key, tree.Value);
                api.setNamespacesOnTableNms(tree.Key, tree.Value);
            }
            foreach (KeyValuePair<string, TypeDefinitionNode> entry in Singleton.tableTypes)
            {
                Console.Out.WriteLine(entry.Key + " - " + entry.Value.GetType());
            }

            foreach (KeyValuePair<string, string> entry in Singleton.tableNamespaces)
            {
                Console.Out.WriteLine(entry.Key + " - " + entry.Value);
            }

            /*foreach (KeyValuePair<string, CompilationNode> tree in trees)
            {
                tree.Value.Evaluate();
            }*/
        }
    }
}
