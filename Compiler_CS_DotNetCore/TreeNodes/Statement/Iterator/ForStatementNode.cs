﻿using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;

namespace Compiler.Tree
{
    public class ForStatementNode : EmbeddedStatementNode 
    {
        public  List<Statement> initializer;
        public ExpressionNode expresion;
        public List<Statement> iterative;
        public EmbeddedStatementNode body;

        public ForStatementNode()
        {

        }

        public ForStatementNode(List<Statement> initializer, ExpressionNode expresion, List<Statement> iterative, EmbeddedStatementNode body)
        {
            this.initializer = initializer;
            this.expresion = expresion;
            this.iterative = iterative;
            this.body = body;
        }

        public override void evaluate(API api)
        {
            api.contextManager.pushFront(new Context(ContextType.ITERATIVE, api));
            foreach (var s in initializer)
            {
                s.evaluate(api);
            }
            TypeDefinitionNode t = expresion.evaluateType(api);
            if (t.getComparativeType() != Utils.Bool)
                throw new SemanticException("Bool expression expected in for but got '"+t.ToString()+"'.");

            foreach (var s in iterative)
            {
                s.evaluate(api);
            }
            body.evaluate(api);
            api.popFrontContext();
        }
    }
}