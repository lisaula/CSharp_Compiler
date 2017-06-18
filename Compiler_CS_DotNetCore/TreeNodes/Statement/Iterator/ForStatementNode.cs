using System;
using System.Collections.Generic;
using System.Text;
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
            if (initializer != null)
            {
                foreach (var s in initializer)
                {
                    if (s is StatementExpressionNode)
                    {
                        StatementExpressionNode temp = s as StatementExpressionNode;
                        if (!(temp.expression is AssignmentNode))
                        {
                            throw new SemanticException("Not an assignable expression for initializer.");
                        }
                    }
                    else if (!(s is LocalVariableDefinitionNode))
                        throw new SemanticException("Not a valid for initializer expression.");
                    s.evaluate(api);
                }
            }
            if (expresion != null)
            {
                TypeDefinitionNode t = expresion.evaluateType(api);
                if (t.getComparativeType() != Utils.Bool)
                    throw new SemanticException("Bool expression expected in for but got '" + t.ToString() + "'.");
            }
            if (iterative != null)
            {
                foreach (var s in iterative)
                {
                    s.evaluate(api);
                }
            }
            body.evaluate(api);
            api.popFrontContext();
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(Utils.EndLine + "for(");
            if (initializer != null)
            {
                int len = initializer.Count - 1;
                int count = 0;
                foreach (var s in initializer)
                {
                    s.generateCode(builder, api);
                    if (count < len)
                        builder.Append(",");
                }
            }
            builder.Append(";");
            if(expresion != null)
                expresion.generateCode(builder, api);
            builder.Append(";");
            if(iterative != null)
            {
                int len = iterative.Count-1;
                int count = 0;
                foreach(var s in iterative)
                {
                    s.generateCode(builder, api);
                    if (count < len)
                        builder.Append(",");
                    count++;
                }
            }
            builder.Append(") {");
            if(body != null)
            {
                body.generateCode(builder, api);
            }
            builder.Append(Utils.EndLine + "}");
        }
    }
}