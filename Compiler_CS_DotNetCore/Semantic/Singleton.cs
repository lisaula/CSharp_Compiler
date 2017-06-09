using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
namespace Compiler_CS_DotNetCore.Semantic
{
    public static class Singleton
    {
        public static Dictionary<string, TypeDefinitionNode> tableTypes = new Dictionary<string, TypeDefinitionNode>();
        public static Dictionary<string, NamespaceNode> tableNamespaces = new Dictionary<string, NamespaceNode>();

        public static bool tableTypeContains(string key)
        {
            return tableTypes.ContainsKey(key);
        }
    }
}
