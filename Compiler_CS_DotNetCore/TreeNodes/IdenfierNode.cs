﻿namespace Compiler.Tree
{
    public class IdentifierNode: PrimaryExpressionNode
    {
        public Token token;

        public IdentifierNode(Token token)
        {
            this.token = token;
        }
        public IdentifierNode()
        {

        }
    }
}