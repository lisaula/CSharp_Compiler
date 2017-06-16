using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class ReferenceAccessNode : PrimaryExpressionNode
    {
        public  Token token;

        public ReferenceAccessNode(Token token)
        {
            this.token = token;
        }
        public ReferenceAccessNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            if (api.TokenPass(token, TokenType.RW_BASE))
            {
                return api.contextManager.getParent(api.working_type);
            }
            else if (api.TokenPass(token, TokenType.RW_THIS))
            {
                return api.contextManager.getThis(api.working_type);
            }
            TypeDefinitionNode t = null;
            string type = GetType(token);
            t = api.searchInTableType(type);
            if (t != null)
                t.onTableType = true;
            if (t == null)
                throw new SemanticException("Variable '" + token.lexema + "' could not be found in the current context.", token);
            return t;
        }

        private string GetType(Token toke)
        {
            if(toke.type == TokenType.RW_BOOL)
            {
                return Utils.Bool;
            }
            if (toke.type == TokenType.RW_INT)
            {
                return Utils.Int;
            }
            if (toke.type == TokenType.RW_CHAR)
            {
                return Utils.Char;
            }
            if (toke.type == TokenType.RW_STRING)
            {
                return Utils.String;
            }
            if (toke.type == TokenType.RW_FLOAT)
            {
                return Utils.Float;
            }
            throw new SemanticException("Could not found type '" + toke.lexema + "' in table types.", toke);
        }
    }
}