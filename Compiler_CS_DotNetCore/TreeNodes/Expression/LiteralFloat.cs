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
            this.returnType = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Float];
            return returnType;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            string s = token.lexema.TrimEnd('F');
            s = s.TrimEnd('f');
            builder.Append(s);
        }
    }
}