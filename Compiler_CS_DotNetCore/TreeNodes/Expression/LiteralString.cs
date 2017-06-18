using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class LiteralString : LiteralNode
    {
        public bool verbatin = false;
        public LiteralString(Token token) : base(token)
        {
        }
        public LiteralString()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            this.returnType = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.String];
            return returnType;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            if (verbatin)
            {
                builder.Append(token.lexema);
            }else
                builder.Append(token.lexema);
        }
        internal void setVerbatin()
        {
            verbatin = true;
        }
    }
}