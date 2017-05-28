using System.Collections.Generic;

namespace Compiler.Tree
{
    public class BodyStatement : EmbeddedStatementNode
    {
        public List<Statement> statements;

        public BodyStatement()
        {

        }

        public BodyStatement(List<Statement> statements)
        {
            this.statements = statements;
        }
    }
}