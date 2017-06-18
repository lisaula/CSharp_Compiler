using System;
using System.Collections.Generic;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;

namespace Compiler.Tree     
{
    public class CaseStatementNode : EmbeddedStatementNode
    {
        public  List<CaseLabel> caseLabel;
        public List<Statement> body;
        internal TypeDefinitionNode primaryType;

        public CaseStatementNode()
        {

        }

        public CaseStatementNode(List<CaseLabel> caseLabel, List<Statement> body)
        {
            this.caseLabel = caseLabel;
            this.body = body;
        }
        public override void evaluate(API api)
        {
            if (caseLabel == null || caseLabel.Count == 0)
                return;
            foreach (CaseLabel c in caseLabel)
            {
                api.contextManager.Enums_or_Literal = true;
                if (c.expr == null)
                    continue;
                if(checkExpression(c.expr))
                {
                    api.contextManager.Enums_or_Literal = false;
                }
                TypeDefinitionNode t = c.expr.evaluateType(api);
                if (t.getComparativeType() != primaryType.getComparativeType())
                    throw new SemanticException("Expression in case should retorn same type as in switch expression. Switch type '" + primaryType.ToString() + "' and case type '" + t.ToString() + "'", c.token);
            }
            api.contextManager.Enums_or_Literal = false;
            if (body == null || body.Count == 0)
                throw new SemanticException("Cases in switch should have body.", caseLabel[caseLabel.Count-1].token);
            api.pushContext(new Context(ContextType.SWITCH, api));
            foreach (Statement s in body)
            {
                s.evaluate(api);
            }
            api.popFrontContext();
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            if (caseLabel != null)
            {
                foreach (var c in caseLabel)
                {
                    c.generateCode(builder, api);
                }
            }
            if(body != null)
            {
                foreach(var s in body)
                {
                    s.generateCode(builder, api);
                }
            }

        }

        private bool checkExpression(ExpressionNode expr)
        {
            if (expr is InlineExpressionNode)
            {
                InlineExpressionNode i = (InlineExpressionNode)expr;
                ExpressionNode e = i.list[0];
                return e is LiteralNode;
            }
            return false;
        }
    }
}