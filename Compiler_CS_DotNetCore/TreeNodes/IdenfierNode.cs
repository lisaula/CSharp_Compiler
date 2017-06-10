using System;

namespace Compiler.Tree
{
    public class IdentifierNode : PrimaryExpressionNode
    {
        public Token token;

        public IdentifierNode(Token token)
        {
            this.token = token;
        }
        public IdentifierNode()
        {
            
        }

        public override string ToString()
        {
            return token.lexema;
        }

        public override TypeDefinitionNode evaluateType()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if(obj is IdentifierNode)
            {
                var t = obj as IdentifierNode;
                return t.token.Equals(token);
            }
            return false;
        }
    }
}