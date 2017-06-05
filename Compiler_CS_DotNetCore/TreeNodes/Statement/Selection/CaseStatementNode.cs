using System.Collections.Generic;

namespace Compiler.Tree     
{
    public class CaseStatementNode
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
    }
}