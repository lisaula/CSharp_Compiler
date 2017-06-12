using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public abstract class VariableInitializer
    {
        public abstract TypeDefinitionNode evaluateType(API api);
    }
}