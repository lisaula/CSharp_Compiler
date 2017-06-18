using System;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;

namespace Compiler.Tree
{
    public class IfStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode expr;
        public EmbeddedStatementNode body;
        public ElseStatementNode elseStatement;

        public IfStatementNode()
        {

        }

        public IfStatementNode(ExpressionNode expr, EmbeddedStatementNode body, ElseStatementNode elseStatement)
        {
            this.expr = expr;
            this.body = body;
            this.elseStatement = elseStatement;
        }

        public override void evaluate(API api)
        {
            TypeDefinitionNode t = expr.evaluateType(api);
            if (t.getComparativeType() != Utils.Bool)
                throw new SemanticException("If expression must return a bool type, but it actually return '" + t.ToString() + "'");
            api.contextManager.pushFront(new Context(ContextType.IF, api));
            body.evaluate(api);
            api.popFrontContext();
            if(elseStatement != null)
                elseStatement.evaluate(api);
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            throw new NotImplementedException();
        }
    }
}