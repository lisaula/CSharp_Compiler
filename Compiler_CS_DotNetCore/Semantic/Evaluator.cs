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
        protected Dictionary<string, TypeDefinitionNode> tableTypes;
        public Evaluator(List<string> paths)
        {
            api = new API(paths);
            trees = api.buildTrees();
            tableTypes = new Dictionary<string, TypeDefinitionNode>();
            foreach (KeyValuePair<string, CompilationNode> tree in trees) {
                api.setClassesOnTableType(ref tableTypes, tree.Key, tree.Value);
            }
            foreach (KeyValuePair<string, TypeDefinitionNode> entry in tableTypes)
            {
                Console.Out.WriteLine(entry.Key + " - " + entry.Value.GetType());
            }
        }
    }
}
