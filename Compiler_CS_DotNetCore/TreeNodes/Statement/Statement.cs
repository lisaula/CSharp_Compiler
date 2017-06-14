using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public abstract class Statement
    {
        public abstract void evaluate(API api);
    }
}