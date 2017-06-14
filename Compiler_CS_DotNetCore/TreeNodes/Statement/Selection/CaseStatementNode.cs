using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree     
{
    public class CaseStatementNode : EmbeddedStatementNode
    {
        public  List<CaseLabel> caseLabel;
        public List<Statement> body;

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
            throw new NotImplementedException();
        }
    }
}