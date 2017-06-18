using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
using System.IO;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler_CS_DotNetCore.CodeGeneration
{
    public class CodeGenerator
    {
        private Dictionary<string, NamespaceNode> trees;
        public StringBuilder builder;
        const string directoryPath = @"./Javascript";
        public API api;
        public CodeGenerator()
        {
            builder = new StringBuilder();
            Directory.CreateDirectory(directoryPath);
        }

        public CodeGenerator(Dictionary<string, NamespaceNode> trees, API api):this()
        {
            this.api = api;
            this.trees = trees;
            generateCode();
            writeFile();
        }

        private void writeFile()
        {
            string fileName = "generation.js";
            string pathString = System.IO.Path.Combine(directoryPath, fileName);
            File.WriteAllText(pathString, builder.ToString());
        }

        private void generateCode()
        {
            builder.Append("let " + Utils.GlobalNamespace + " = {};");
            foreach(var nms in Singleton.tableNamespaces)
            {
                if(nms != Utils.GlobalNamespace)
                builder.Append(Utils.EndLine + nms + " = {};");
            }
            foreach (var tree in trees)
            {
                try
                {
                    tree.Value.generateCode(builder, api);
                }catch(NotImplementedException nie)
                {
                    Console.WriteLine("Not implemented.");
                }
            }
            builder.Append("\nmodule.exports = " + Utils.GlobalNamespace + ";");
        }
    }
}
