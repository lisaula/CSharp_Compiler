using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;

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
            throw new NotImplementedException();
        }
    }
}