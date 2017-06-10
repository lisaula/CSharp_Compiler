using System;

namespace Compiler.Tree
{
    public class LiteralInt : LiteralNode
    {
        public LiteralInt(Token token):base(token)
        { 
        }

        public override TypeDefinitionNode evaluateType()
        {
            return new IntType();
        }
    }
}