﻿using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class IdentifierNode : PrimaryExpressionNode
    {
        public Token token;
        public bool FunctionID;
        public IdentifierNode(Token token) : this()
        {
            this.token = token;
        }
        public IdentifierNode()
        {
            FunctionID = false;
        }

        public override string ToString()
        {
            return token.lexema;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode t = null;
            t = api.contextManager.findVariable(false, token);
            return t;
        }

        public override bool Equals(object obj)
        {
            if(obj is IdentifierNode)
            {
                var t = obj as IdentifierNode;
                return t.token.Equals(token);
            }
            return false;
        }
    }
}