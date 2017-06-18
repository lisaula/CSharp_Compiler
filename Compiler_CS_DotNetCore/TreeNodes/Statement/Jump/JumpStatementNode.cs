using System;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class JumpStatementNode : EmbeddedStatementNode
    {
        public Token token;
        public ExpressionNode expression;
        public JumpStatementNode(Token token)
        {
            this.token = token;
        }

        public JumpStatementNode(Token token, ExpressionNode expression) : this(token)
        {
            this.expression = expression;
        }
        public JumpStatementNode()
        {

        }

        public override void evaluate(API api)
        {
            if (expression != null)
            {
                TypeDefinitionNode t = expression.evaluateType(api);
                if (api.TokenPass(token, TokenType.RW_RETURN) && t.getComparativeType() == Utils.Void)
                    throw new SemanticException("Cannot return VoidType.", token);
                api.contextManager.returnTypeFound(t);
            }
            else
            {
                if (api.TokenPass(token, TokenType.RW_RETURN))
                    api.contextManager.returnTypeFound(Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Void]);
            }

            api.contextManager.jumpValidation(token);
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            throw new NotImplementedException();
        }
    }
}