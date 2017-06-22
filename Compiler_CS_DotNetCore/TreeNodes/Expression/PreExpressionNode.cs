using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class PreExpressionNode : UnaryExpressionNode
    {
        public Token Operator;
        public UnaryExpressionNode expression;

        public PreExpressionNode()
        {

        }
        /*
            TokenType.OP_SUM,
            TokenType.OP_SUBSTRACT,
            TokenType.OP_INCREMENT,
            TokenType.OP_DECREMENT,
            TokenType.OP_DENIAL,
            TokenType.OP_BIN_ONES_COMPLMTS,
            TokenType.OP_MULTIPLICATION 
        */
        public PreExpressionNode(Token unaryOperator, UnaryExpressionNode unary_expression)
        {
            this.Operator = unaryOperator;
            this.expression = unary_expression;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode t1 = expression.evaluateType(api);
            if(api.TokenPass(Operator, TokenType.OP_SUM, TokenType.OP_SUBSTRACT, TokenType.OP_INCREMENT, TokenType.OP_DECREMENT))
            {
                if (t1.ToString() != Utils.Int && t1.ToString() != Utils.Float && t1.ToString() != Utils.Char)
                    throw new SemanticException("Invalid pre unary expression. Cant apply " + Operator.ToString() + " to " + t1.ToString(), Operator);
            }
            if(api.TokenPass(Operator, TokenType.OP_BIN_ONES_COMPLMTS))
            {
                if (t1.ToString() != Utils.Char && t1.ToString() != Utils.Int)
                    throw new SemanticException("Invalid pre unary expression. Cant apply " + Operator.ToString() + " to " + t1.ToString(), Operator);
                if (t1.ToString() == Utils.Char)
                    return new IntType();
            }
            if(api.TokenPass(Operator, TokenType.OP_DENIAL))
            {
                if(t1.ToString() != Utils.Bool)
                    throw new SemanticException("Invalid pre unary expression. Cant apply " + Operator.ToString() + " to " + t1.ToString(), Operator);
            }
            this.returnType = t1;
            return t1;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append(Operator.lexema);
            expression.generateCode(builder, api);
        }
    }
}