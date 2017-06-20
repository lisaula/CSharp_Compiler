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
            builder.Append("let { itoa, atoi, toInt } = require(\"./Utils.js\");");
            builder.Append("let " + Utils.GlobalNamespace + " = {};");
            var array = Singleton.tableNamespaces.ToArray();
            IntArrayInsertionSort(array);
            foreach (var nms in array)
            {
                if(nms != Utils.GlobalNamespace)
                builder.Append(Utils.EndLine + nms + " = {};");
            }
            builder.Append(Utils.includesDefaultJS);
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
        public static void exchange(string[] data, int m, int n)
        {
            string temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
        public static void IntArrayInsertionSort(string[] data)
        {
            int i, j;
            int N = data.Length;

            for (j = 1; j < N; j++)
            {
                for (i = j; i > 0 && data[i].Length < data[i - 1].Length; i--)
                {
                    exchange(data, i, i - 1);
                }
            }
        }
    }
}
