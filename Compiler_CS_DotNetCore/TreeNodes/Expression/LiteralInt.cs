using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class LiteralInt : LiteralNode
    {
        public LiteralInt(Token token):base(token)
        { 
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return new IntType(Singleton.tableTypes[Utils.GlobalNamespace+"."+Utils.Int]);
        }
    }
}