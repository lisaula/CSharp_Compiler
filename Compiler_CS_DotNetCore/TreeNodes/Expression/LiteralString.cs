using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class LiteralString : LiteralNode
    {
        public LiteralString(Token token) : base(token)
        {
        }
        public LiteralString()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return new StringType(token);
        }
    }
}