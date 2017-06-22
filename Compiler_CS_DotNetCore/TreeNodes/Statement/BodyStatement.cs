using System;
using System.Collections.Generic;
using System.Text;
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
            if(statements != null)
            {
                foreach(var s in statements)
                {
                    s.evaluate(api);
                }
            }
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            foreach(var s in statements)
            {

                if(s is LocalVariableDefinitionNode)
                {
                    foreach(var key in ((LocalVariableDefinitionNode)s).variable)
                    {
                        ((LocalVariableDefinitionNode)s).endLine = true;
                        key.Value.localEstatement = true;
                    }
                }
                s.generateCode(builder, api);
                builder.Append(";");
            }
        }
    }
}