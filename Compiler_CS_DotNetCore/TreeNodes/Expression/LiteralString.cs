using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class LiteralString : LiteralNode
    {
        public LiteralString(Token token) : base(token)
        {
        }
        public LiteralString()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
        }

        public override string generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}