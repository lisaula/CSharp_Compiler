using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class LiteralBool : LiteralNode
    {
        public LiteralBool(Token token) : base (token)
        {
        }
        public LiteralBool()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
        }

        public override string generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}