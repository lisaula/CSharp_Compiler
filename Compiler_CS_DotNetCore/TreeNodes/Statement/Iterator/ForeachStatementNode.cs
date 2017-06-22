using System;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;

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
            TypeDefinitionNode t = collection.evaluateType(api);
            if (t.getComparativeType() != Utils.Array)
                throw new SemanticException("Invalid expression. '" + id.ToString() + "' is not iterable.",id.token);
            ArrayTypeNode a = t as ArrayTypeNode;
            TypeDefinitionNode it = api.searchType(type);
            if(it.getComparativeType() == Utils.Array)
            {
                if (((ArrayTypeNode)it).type.getComparativeType() != a.type.getComparativeType())
                    throw new SemanticException("Assignable variable in foreach is not compitible with Iterable. '"+ ((ArrayTypeNode)it).ToString() + "' and '"+a.ToString()+"'");
            }
            if(it.getComparativeType() == Utils.Var)
            {
                it = a.type;
                if (a.indexes.Count > 1)
                {

                    it = a.getFistLevelIndex();
                }
            }
            TypeDefinitionNode temp = it;
            if (a.getFistLevelIndex().ToString() != temp.ToString())
            {
                throw new SemanticException("Assignable variable in foreach is not compitible with Iterable. '" + it.ToString() + "' and '" + a.type.ToString() + "'");
            }

            api.contextManager.pushFront(new Context(ContextType.ITERATIVE, api));
            api.contextManager.pushFront(it, id);
            body.evaluate(api);
            api.popFrontContext();
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(Utils.EndLine + "for(let ");
            builder.Append(id.ToString() + " of ");
            collection.generateCode(builder, api);
            builder.Append("){");
            body.generateCode(builder, api);
            builder.Append(Utils.EndLine + "}");
        }
    }
}