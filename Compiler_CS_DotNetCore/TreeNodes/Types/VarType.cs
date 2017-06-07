using Compiler.Tree;

namespace Compiler
{
    public class VarType : TypeDefinitionNode
    {
        public Token token;

        public VarType(Token token)
        {
            this.token = token;
        }
        public VarType()
        {

        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}