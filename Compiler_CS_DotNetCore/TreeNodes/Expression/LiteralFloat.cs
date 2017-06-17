using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class LiteralFloat : LiteralNode
    { 

        public LiteralFloat(Token token) : base(token)
        {
        }
        public LiteralFloat()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}