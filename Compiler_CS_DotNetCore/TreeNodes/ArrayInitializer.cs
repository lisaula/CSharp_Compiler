using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayInitializer : VariableInitializer
    {
        public List<VariableInitializer> variables_list;
    }
}