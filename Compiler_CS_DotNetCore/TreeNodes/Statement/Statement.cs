using Compiler_CS_DotNetCore.Semantic;
using System.Text;

namespace Compiler.Tree
{
    public abstract class Statement
    {
        public abstract void evaluate(API api);
        public abstract void generateCode(StringBuilder builder, API api);
    }
}