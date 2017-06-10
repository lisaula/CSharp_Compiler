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

        public override TypeDefinitionNode evaluateType()
        {
            return new FloatType();
        }
    }
}