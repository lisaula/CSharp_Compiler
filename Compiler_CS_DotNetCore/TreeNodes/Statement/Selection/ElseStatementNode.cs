﻿using System;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;

namespace Compiler.Tree
{
    public class ElseStatementNode : EmbeddedStatementNode
    {
        public EmbeddedStatementNode body;

        public ElseStatementNode()
        {

        }

        public ElseStatementNode(EmbeddedStatementNode body)
        {
            this.body = body;
        }

        public override void evaluate(API api)
        {
            api.contextManager.pushFront(new Context(ContextType.ELSE, api));
            body.evaluate(api);
            api.popFrontContext();
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(Utils.EndLine + "else {");
            body.generateCode(builder, api);
            builder.Append(Utils.EndLine + "}");
        }
    }
}