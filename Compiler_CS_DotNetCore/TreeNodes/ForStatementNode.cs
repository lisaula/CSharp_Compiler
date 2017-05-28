using System.Collections.Generic;

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
    }
}