using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class LiteralFloat : LiteralNode
    { 

        public LiteralFloat(Token token) : base(token)
        {
        }
        public LiteralFloat()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return new FloatType(Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float]);
        }
    }
}