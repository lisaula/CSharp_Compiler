using Compiler.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private UnaryExpressionNode unary_expression()
        {
            TokenType[] nuevo = { TokenType.RW_NEW , TokenType.ID,
                TokenType.OPEN_PARENTHESIS, TokenType.RW_THIS, TokenType.RW_BASE
            };
            DebugInfoMethod("unary_expression");
            if (pass(unaryOperatorOptions))
            {
                var unaryOperator = current_token;
                consumeToken();

                var unaryExpression = unary_expression();
                return new ExpressionUnaryNode(unaryOperator, unaryExpression);
            }else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                addLookAhead(lexer.getNextToken());
                int first = look_ahead.Count() - 1;
                Token placehold = look_ahead[look_ahead.Count() - 1];
                bool accept = false;
                while (typesOptions.Contains(placehold.type) || placehold.type == TokenType.OP_DOT
                    || placehold.type == TokenType.OPEN_SQUARE_BRACKET || placehold.type == TokenType.CLOSE_SQUARE_BRACKET
                    || placehold.type == TokenType.OP_LESS_THAN || placehold.type == TokenType.OP_GREATER_THAN
                    || placehold.type == TokenType.OP_COMMA)
                {
                    addLookAhead(lexer.getNextToken());
                    placehold = look_ahead[look_ahead.Count() - 1];
                    accept = true;
                }
                DebugInfoMethod("PH" + placehold.type+ " "+look_ahead[first].type);
                if (typesOptions.Contains(look_ahead[first].type) && accept && 
                    (placehold.type == TokenType.CLOSE_PARENTHESIS))
                {
                    consumeToken();
                    if (!pass(typesOptions))
                        throwError("a type");
                    var targetType = types();

                    if (!pass(TokenType.CLOSE_PARENTHESIS))
                        throwError("close parenthesis ')'");
                    consumeToken();
                    var expression = primary_expression();
                    return new CastingExpressionNode(targetType, expression);
                }
                else
                {
                    return primary_expression();
                }
            }else if (pass(nuevo.Concat(literalOptions).Concat(primitiveTypes).ToArray()))
            {
                return primary_expression();
            }
            else
            {
                throwError("unary-operator, casting or primary-expression");
                return null;
            }
        }

        private PrimaryExpressionNode primary_expression()
        {
            DebugInfoMethod("primary_expression");
            if (pass(TokenType.RW_NEW))
            {
                consumeToken();
                PrimaryExpressionNode instance =  instance_expression();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT, 
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    instance = primary_expression_p(instance);
                return instance;
            }
            else if (pass(literalOptions))
            {
                var token = current_token;
                consumeToken();
                PrimaryExpressionNode literal = new LiteralNode(token);
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    literal = primary_expression_p(literal);
                return literal;
            }else if (pass(TokenType.ID))
            {
                PrimaryExpressionNode id = new IdentifierNode(current_token);
                consumeToken();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    id = primary_expression_p(id);
                return id;
            }else if (pass(TokenType.RW_BASE))
            {
                PrimaryExpressionNode r_base = new ReferenceAccessNode(current_token);
                consumeToken();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    r_base = primary_expression_p(r_base);
                return r_base;
            }
            else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                DebugInfoMethod("Entro parenthesized expression");
                consumeToken();

                var expr= expression();

                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                DebugInfoMethod("consumiendo close parenthesis");
                consumeToken();
                PrimaryExpressionNode parenthisized = new ParenthesizedExpressionNode(expr);
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    parenthisized=primary_expression_p(parenthisized);
                return parenthisized;
            }
            else if(pass(TokenType.RW_THIS))
            {
                PrimaryExpressionNode r_this = new ReferenceAccessNode(current_token);
                consumeToken();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    r_this=primary_expression_p(r_this);
                return r_this;
            }
            else if (pass(primitiveTypes))
            {
                PrimaryExpressionNode primitive = new ReferenceAccessNode(current_token);
                consumeToken();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    primitive=primary_expression_p(primitive);
                else
                    throwError("Function call");
                return primitive;
            }
            else
            {
                throwError("new, literal, identifier, '(' or \"this\"");
                return null;
            }
        }

        private PrimaryExpressionNode primary_expression_p(PrimaryExpressionNode primary)
        {
            DebugInfoMethod("primary_expression_p");
            if (pass(TokenType.OP_DOT))
            {
                consumeToken();

                if (!pass(TokenType.ID))
                    throwError("identifier");
                var id = current_token;
                consumeToken();
                return primary_expression_p(new AccessMemory(primary, id));
            }else if(pass(TokenType.OPEN_PARENTHESIS, TokenType.OPEN_SQUARE_BRACKET))
            {
                var funcOrArray = optional_funct_or_array_call(primary);
                return primary_expression_p(funcOrArray);
            }
            else if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT))
            {
                var Operator = current_token;
                consumeToken();
                return primary_expression_p(new PostAdditiveExpressionNode(primary, Operator));
            }
            else
            {
                DebugInfoMethod("epsilon");
                return primary;
            }
        }

        private PrimaryExpressionNode optional_funct_or_array_call(PrimaryExpressionNode primary)
        {
            DebugInfoMethod("optional_funct_or_array_call");
            if (pass(TokenType.OPEN_PARENTHESIS))
            {
                consumeToken();
                var arguments = argument_list();

                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                consumeToken();
                return new FunctionCallExpression(primary, arguments);
            }else if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                var lista = optional_array_access_list();
                return new ArrayAccessNode(primary, lista);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private List<List<ExpressionNode>> optional_array_access_list()
        {
            DebugInfoMethod("optional_array_access_list");
            if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                consumeToken();
                var expressionList = expression_list();

                if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                    throwError("close square bracket ']'");
                consumeToken();

                var lista = optional_array_access_list();
                lista.Insert(0, expressionList);
                return lista;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<List<ExpressionNode>>();
            }
        }

        private InstanceExpressionNode instance_expression()
        {
            DebugInfoMethod("instance_expression");
            if (!pass(typesOptions))
                throwError("a type");
            //types();
            TypeDefinitionNode type = null;
            if (pass(TokenType.ID))
            {
                var list = qualified_identifier();
                type = new IdentifierTypeNode(list);
            }
            else if (pass(TokenType.RW_DICTIONARY))
            {
                type = dictionary();
            }
            else
            {
                var token = current_token;
                consumeToken();
                type = new PrimitiveType(token);
            }
            return instance_expression_factorized(type);
        }

        private InstanceExpressionNode instance_expression_factorized(TypeDefinitionNode type)
        {
            DebugInfoMethod("instance_expression_factorized");
            if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                consumeToken();
                return instance_expression_factorized_p(type);
            }else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                consumeToken();
                var arguments = argument_list();

                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                consumeToken();
                return new ClassInstantiation(type, arguments);
            }
            else
            {
                throwError("Square bracket '[' or Parenthesis '('");
                return null;
            }
        }

        private ArrayInstantiation instance_expression_factorized_p(TypeDefinitionNode type)
        {
            var arrayNode = new ArrayNode();
            DebugInfoMethod("instance_expression_factorized_p");
            TokenType[] nuevo = { TokenType.OP_TER_NULLABLE, TokenType.OP_COLON,
                TokenType.OP_NULLABLE, TokenType.OP_LOG_OR,
                TokenType.OP_LOG_AND, TokenType.OP_BIN_OR,
                TokenType.OP_BIN_XOR, TokenType.OP_BIN_AND,
                TokenType.OPEN_PARENTHESIS, TokenType.RW_NEW,
                TokenType.ID, TokenType.RW_THIS
            };
            if (pass(nuevo.Concat(equalityOperatorOptions).Concat(relationalOperatorOptions).
                Concat(Is_AsOperatorOptions).Concat(shiftOperatorOptions).Concat(additiveOperatorOptions).
                Concat(multiplicativeOperatorOptions).Concat(assignmentOperatorOptions).Concat(unaryOperatorOptions)
                .Concat(literalOptions).ToArray()))
            {
                
                var primaryExpressionBracket = expression_list();

                if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                    throwError("close square bracket ']'");
                consumeToken();
                
                optional_rank_specifier_list(ref arrayNode);
                var initialization = optional_array_initializer();
                return new ArrayInstantiation(type, primaryExpressionBracket, arrayNode, initialization);
            }else if (pass(TokenType.OP_COMMA,TokenType.CLOSE_SQUARE_BRACKET))
            {
                rank_specifier_list(ref arrayNode);
                var initialization = array_initializer();
                return new ArrayInstantiation(type, arrayNode, initialization);
            }
            else
            {
                throwError("expression or rank specifier ','");
                return null;
            }
        }

        private ArrayInitializer array_initializer()
        {
            DebugInfoMethod("array_initializer");
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();
            var array_init = new ArrayInitializer();
            array_init.variables_list = optional_variable_initializer_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '{'");
            consumeToken();
            return array_init;
        }

        private List<VariableInitializer> optional_variable_initializer_list()
        {
            DebugInfoMethod("optional_variable_initializer_list");
            TokenType[] nuevo = { TokenType.OP_TER_NULLABLE, TokenType.OP_COLON,
                TokenType.OP_NULLABLE, TokenType.OP_LOG_OR,
                TokenType.OP_LOG_AND, TokenType.OP_BIN_OR,
                TokenType.OP_BIN_XOR, TokenType.OP_BIN_AND,
                TokenType.OPEN_PARENTHESIS, TokenType.RW_NEW,
                TokenType.ID, TokenType.RW_THIS,TokenType.OPEN_CURLY_BRACKET
            };
            if (pass(nuevo.Concat(equalityOperatorOptions).Concat(relationalOperatorOptions).
                Concat(Is_AsOperatorOptions).Concat(shiftOperatorOptions).Concat(additiveOperatorOptions).
                Concat(multiplicativeOperatorOptions).Concat(assignmentOperatorOptions).Concat(unaryOperatorOptions)
                .Concat(literalOptions).ToArray()))
            {
                return variable_initializer_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private List<VariableInitializer> variable_initializer_list()
        {
            DebugInfoMethod("variable_initializer_list");
            var var_init = variable_initializer();
            var lista = variable_initializer_list_p();
            lista.Insert(0, var_init);
            return lista;
        }

        private List<VariableInitializer> variable_initializer_list_p()
        {
            DebugInfoMethod("variable_initializer_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                return variable_initializer_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<VariableInitializer>();
            }
        }

        private void rank_specifier_list(ref ArrayNode arrayNode)
        {
            DebugInfoMethod("rank_specifier_list");
            rank_specifier(ref arrayNode);
            optional_rank_specifier_list(ref arrayNode);
        }

        private void rank_specifier(ref ArrayNode arrayNode)
        {
            DebugInfoMethod("rank_specifier");
            optional_comma_list(ref arrayNode);

            if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                throwError("close square bracket ']'");
            consumeToken();
            arrayNode.arrayOfArrays++;
        }

        private void optional_comma_list(ref ArrayNode arrayNode)
        {
            DebugInfoMethod("optional_comma_list");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                arrayNode.dimensions++;
                optional_comma_list(ref arrayNode);
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private ArrayInitializer optional_array_initializer()
        {
            DebugInfoMethod("optional_array_initializer");
            if (pass(TokenType.OPEN_CURLY_BRACKET))
            {
                return array_initializer();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private void optional_rank_specifier_list(ref ArrayNode arrayNode)
        {
            if (arrayNode == null)
                arrayNode = new ArrayNode();
            DebugInfoMethod("optional_rank_specifier_list");
            if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                consumeToken();
                rank_specifier_list(ref arrayNode);
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private List<ExpressionNode> expression_list()
        {
            DebugInfoMethod("expression_list");
            var expr = expression();
            var lista = expression_list_p();
            lista.Insert(0, expr);
            return lista;
        }

        private List<ExpressionNode> expression_list_p()
        {
            DebugInfoMethod("expression_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                return expression_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<ExpressionNode>();
            }
        }
    }
}
