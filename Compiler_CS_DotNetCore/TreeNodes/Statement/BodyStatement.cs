using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class BodyStatement : EmbeddedStatementNode
    {
        public List<Statement> statements;

        public BodyStatement()
        {
            statements = new List<Statement>();
        }

        public BodyStatement(List<Statement> statements)
        {
            this.statements = statements;
        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}