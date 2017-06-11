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

        public override TypeDefinitionNode evaluateType()
        {
            return new CharType(token);
        }
    }
}