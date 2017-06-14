using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class LiteralBool : LiteralNode
    {
        public LiteralBool(Token token) : base (token)
        {
        }
        public LiteralBool()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
        }
    }
}