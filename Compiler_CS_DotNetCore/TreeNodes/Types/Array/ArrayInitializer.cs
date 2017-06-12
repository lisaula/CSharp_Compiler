using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayInitializer : VariableInitializer
    {
        public List<VariableInitializer> variables_list;

        public override TypeDefinitionNode evaluateType(API api)
        {
            throw new NotImplementedException();
        }
    }
}