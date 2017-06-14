﻿using System;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;
using System.Collections.Generic;

namespace Compiler
{
    public class VarType : TypeDefinitionNode
    {
        public VarType(Token token)
        {
            this.identifier = new IdentifierNode(token);
        }
        public VarType()
        {

        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }
    }
}