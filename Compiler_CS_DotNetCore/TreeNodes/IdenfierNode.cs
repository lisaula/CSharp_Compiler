using Compiler_CS_DotNetCore.Semantic;
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

        public override TypeDefinitionNode evaluateType(API api)
        {
            var id = new IdentifierTypeNode(new IdentifierNode(token));
            return api.searchType(id);
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