using System;
using System.Collections.Generic;
using System.Text;
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

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(Utils.EndLine + "switch ( ");
            constantExpression.generateCode(builder, api);
            builder.Append("){");
            if(cases != null)
            {
                foreach(var c in cases)
                {
                    c.generateCode(builder, api);
                }
            }
            builder.Append(Utils.EndLine+"}");
        }
    }
}