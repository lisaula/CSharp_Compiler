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
            this.returnType = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Bool];
            return returnType;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(token.lexema);
        }
    }
}