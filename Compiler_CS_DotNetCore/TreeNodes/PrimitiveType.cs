﻿using Compiler.Tree;

namespace Compiler.Tree
{
    public class PrimitiveType : TypeDefinitionNode
    {
        public Token token_type;
        public ArrayNode arrayNode;
        public PrimitiveType(Token token)
        {
            this.token_type = token;
        }
        public PrimitiveType()
        {

        }
    }
}