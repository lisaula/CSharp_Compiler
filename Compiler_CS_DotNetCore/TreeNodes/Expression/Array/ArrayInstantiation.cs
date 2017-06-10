using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayInstantiation : InstanceExpressionNode
    {
        public ArrayInitializer initialization;
        public TypeDefinitionNode type;
        public ArrayInstantiation()
        {

        }

        public ArrayInstantiation(TypeDefinitionNode type, ArrayInitializer initialization)
        {
            this.type = type;
            this.initialization = initialization;
        }
    }
}