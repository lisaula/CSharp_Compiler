using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class CaseLabel : Statement
    {
        public Token token;
        public ExpressionNode expr;

        public CaseLabel(Token token, ExpressionNode expr)
        {
            this.token = token;
            this.expr = expr;
        }
        public CaseLabel()
        {

        }

        public CaseLabel(Token token)
        {
            this.token = token;
        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}