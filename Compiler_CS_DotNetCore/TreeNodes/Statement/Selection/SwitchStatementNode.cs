using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;

namespace Compiler.Tree
{
    public class SwitchStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode constantExpression;
        public List<CaseStatementNode> cases;

        public SwitchStatementNode(ExpressionNode constantExpression, List<CaseStatementNode> cases)
        {
            this.constantExpression = constantExpression;
            this.cases = cases;
        }
        public SwitchStatementNode()
        {

        }

        public override void evaluate(API api)
        {
            TypeDefinitionNode t = constantExpression.evaluateType(api);
            foreach(CaseStatementNode c in cases)
            {
                api.pushContext(new Context(ContextType.SWITCH, api));
                c.primaryType = t;
                c.evaluate(api);
                api.popFrontContext();
            }

        }
    }
}