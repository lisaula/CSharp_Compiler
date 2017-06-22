using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class LiteralInt : LiteralNode
    {
        public LiteralInt(Token token):base(token)
        { 
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            this.returnType = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Int];
            return returnType;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(token.lexema);
        }
    }
}