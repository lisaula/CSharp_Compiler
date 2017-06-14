﻿using System;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class ForeachStatementNode : EmbeddedStatementNode
    {
        public  TypeDefinitionNode type;
        public IdentifierNode id;
        public ExpressionNode collection;
        public EmbeddedStatementNode body;

        public ForeachStatementNode()
        {
                
        }

        public ForeachStatementNode(TypeDefinitionNode type, IdentifierNode id, ExpressionNode collection, EmbeddedStatementNode body)
        {
            this.type = type;
            this.id = id;
            this.collection = collection;
            this.body = body;
        }

        public override void evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}