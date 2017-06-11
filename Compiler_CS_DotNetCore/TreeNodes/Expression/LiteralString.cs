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

        public override TypeDefinitionNode evaluateType()
        {
            return new StringType(token);
        }
    }
}