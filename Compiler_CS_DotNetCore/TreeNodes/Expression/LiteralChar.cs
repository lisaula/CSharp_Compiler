﻿using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class LiteralChar : LiteralNode
    {

        public LiteralChar(Token token):base(token)
        {
        }
        public LiteralChar()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {

            this.returnType = Singleton.tableTypes[Utils.GlobalNamespace + "." + Utils.Char];
            return returnType;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(token.lexema);
        }
    }
}