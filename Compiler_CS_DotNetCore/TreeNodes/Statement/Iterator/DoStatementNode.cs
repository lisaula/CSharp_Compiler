using System;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;

namespace Compiler.Tree
{
    public  class DoStatementNode : EmbeddedStatementNode
    {
        public EmbeddedStatementNode body;
        public ExpressionNode conditionExpression;

        public DoStatementNode()
        {

        }

        public DoStatementNode(EmbeddedStatementNode body, ExpressionNode conditionExpression)
        {
            this.body = body;
            this.conditionExpression = conditionExpression;
        }

        public override void evaluate(API api)
        {
            TypeDefinitionNode t = conditionExpression.evaluateType(api);
            if (t.getComparativeType() != Utils.Bool)
            {
                throw new SemanticException("Condition expression in do while have to return a 'BoolType'");
            }
            api.contextManager.pushFront(new Context(ContextType.ITERATIVE, api));
            body.evaluate(api);
            api.popFrontContext();
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            throw new NotImplementedException();
        }
    }

}