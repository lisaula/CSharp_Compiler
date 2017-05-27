using Compiler.Tree;

namespace Compiler
{
    public class VarType : TypeDefinitionNode
    {
        private Token token;

        public VarType(Token token)
        {
            this.token = token;
        }
    }
}