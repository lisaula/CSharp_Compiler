using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class LiteralChar : LiteralNode
    {

        public LiteralChar(Token token):base(token)
        {
        }
        public LiteralChar()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Char];
        }
    }
}