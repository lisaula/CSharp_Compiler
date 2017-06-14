using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class LocalVariableDefinitionNode : Statement
    {
        public Dictionary<string,FieldNode> variable;

        public LocalVariableDefinitionNode()
        {
            this.variable = new Dictionary<string, FieldNode>();
        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}