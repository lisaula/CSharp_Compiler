using Compiler_CS_DotNetCore.Semantic;
using System.Text;

namespace Compiler.Tree
{
    public abstract class VariableInitializer
    {
        public abstract TypeDefinitionNode evaluateType(API api);
        public abstract string generateCode(StringBuilder builder);
    }
}