using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
using System.IO;
namespace Compiler_CS_DotNetCore.CodeGeneration
{
    public class CodeGenerator
    {
        private Dictionary<string, NamespaceNode> trees;
        public StringBuilder builder;
        const string directoryPath = @"./Javascript";
        public CodeGenerator()
        {
            builder = new StringBuilder();
            Directory.CreateDirectory(directoryPath);
        }

        public CodeGenerator(Dictionary<string, NamespaceNode> trees):this()
        {
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
            foreach (var tree in trees)
            {
                try
                {
                    tree.Value.generateCode(builder);
                }catch(NotImplementedException nie)
                {
                    Console.WriteLine("Not implemented.");
                }
            }
        }
    }
}
