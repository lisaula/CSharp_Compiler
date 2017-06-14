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
                TokenType.OPEN_PARENTHESIS, TokenType.RW_THIS, TokenType.RW_BASE, TokenType.RW_NULL
            };
            DebugInfoMethod("unary_expression");
            if (pass(unaryOperatorOptions))
            {
                var unaryOperator = current_token;
                consumeToken();

                var unaryExpression = unary_expression();
                return new PreExpressionNode(unaryOperator, unaryExpression);
            }else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                DebugInfoMethod("Empezo");
                //if(look_ahead.Count == 0)addLookAhead(lexer.getNextToken());
                int count = 0;
                Token placehold = getNextLookAhead(count);// look_ahead[look_ahead.Count() - 1];
                int first = look_ahead.IndexOf(placehold);//look_ahead.Count() - 1;
                bool accept = false;
                while (typesOptions.Contains(placehold.type) || placehold.type == TokenType.OP_DOT
                    || placehold.type == TokenType.OPEN_SQUARE_BRACKET || placehold.type == TokenType.CLOSE_SQUARE_BRACKET
                    || placehold.type == TokenType.OP_LESS_THAN || placehold.type == TokenType.OP_GREATER_THAN
                    || placehold.type == TokenType.OP_COMMA)
                {
                    count++;
                    placehold = getNextLookAhead(count);
                    accept = true;
                }
                Token after_closing = getNextLookAhead(count+1);
                DebugInfoMethod("AC " + after_closing.type + " " + look_ahead[first].type);
                DebugInfoMethod("PH " + placehold.type+ " "+look_ahead[first].type);
                if (typesOptions.Contains(look_ahead[first].type) && accept && 
                    (placehold.type == TokenType.CLOSE_PARENTHESIS) 
                    && (after_closing.type != TokenType.OP_INCREMENT
                    && after_closing.type != TokenType.OP_DECREMENT
                    && after_closing.type != TokenType.OP_DOT) )
                {
                    consumeToken();
                    if (!pass(typesOptions))
                        throwError("a type");
                    var targetType = types();

                    if (!pass(TokenType.CLOSE_PARENTHESIS))
                        throwError("close parenthesis ')'");
                    consumeToken();
                    var expression = primary_expression();
                    return new CastingExpressionNode(targetType,new InlineExpressionNode(expression));
                }
                else
                {
                    DebugInfoMethod("Aqui entro"); 
                    return new InlineExpressionNode(primary_expression());
                }
            }else if (pass(nuevo.Concat(literalOptions).Concat(primitiveTypes).ToArray()))
            {
                return new InlineExpressionNode(primary_expression());
            }
            else
            {
                throwError("unary-operator, casting or primary-expression");
                return null;
            }
        }

        private List<ExpressionNode> primary_expression()
        {
            DebugInfoMethod("primary_expression");
            if (pass(TokenType.RW_NEW))
            {
                consumeToken();
                PrimaryExpressionNode instance =  instance_expression();
                //if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT, 
                //    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                var right = primary_expression_p();
                right.Insert(0, instance);
                return right;
            }
            else if (pass(literalOptions))
            {
                var token = current_token;
                consumeToken();
                PrimaryExpressionNode literal = null;
                if (token.type == TokenType.LIT_CHAR)
                {
                    literal = new LiteralChar(token);
                }
                else if (token.type == TokenType.LIT_INT)
                {
                    literal = new LiteralInt(token);
                }
                else if (token.type == TokenType.LIT_FLOAT)
                {
                    literal = new LiteralFloat(token);
                }
                else if (token.type == TokenType.LIT_STRING)
                {
                    literal = new LiteralString(token);
                }
                else if (token.type == TokenType.LIT_BOOL)
                {
                    literal = new LiteralBool(token);
                }
                else
                {
                    literal = new LiteralString(token);
                }
                //if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                //    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                var list = primary_expression_p();
                list.Insert(0, literal);
                return list;
            }else if (pass(TokenType.ID))
            {
                PrimaryExpressionNode id = new IdentifierNode(current_token);
                consumeToken();
                //if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                //    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                var list = primary_expression_p();
                is_function_or_array(ref list, id);
                return list;
            }else if (pass(TokenType.RW_BASE))
            {
                PrimaryExpressionNode r_base = new ReferenceAccessNode(current_token);
                consumeToken();
                //if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                //    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                var list = primary_expression_p();
                list.Insert(0, r_base);
                return list;
            }
            else if (pass(TokenType.RW_NULL))
            {
                PrimaryExpressionNode r_base = new NullExpressionNode(new NullTypeNode(current_token));
                consumeToken();
                //if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                //    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                var list = primary_expression_p();
                list.Insert(0, r_base);
                return list;
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
                //if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                //    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                var list =primary_expression_p();
                //list.Insert(0, parenthisized);
                is_function_or_array(ref list, parenthisized);
                return list;
            }
            else if(pass(TokenType.RW_THIS))
            {
                PrimaryExpressionNode r_this = new ReferenceAccessNode(current_token);
                consumeToken();
                //if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                //    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                var list =primary_expression_p();
                list.Insert(0, r_this);
                return list;
            }
            else if (pass(primitiveTypes))
            {
                PrimaryExpressionNode primitive = new ReferenceAccessNode(current_token);
                consumeToken();
                List<ExpressionNode> list = null;
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    list = primary_expression_p();
                else
                    throwError("Function call");
                list.Insert(0, primitive);
                return list;
            }
            else
            {
                throwError("new, literal, identifier, '(' or \"this\"");
                return null;
            }
        }

        private void is_function_or_array(ref List<ExpressionNode> list, PrimaryExpressionNode id)
        {
            if(list.Count>0 && 
                list[0] is FunctionCallExpression && ((FunctionCallExpression)list[0]).primary == null)
            {
                if(id is AccessMemory)
                {
                    var r_id = ((AccessMemory)id).expression;
                    ((FunctionCallExpression)list[0]).primary = r_id;
                    ((AccessMemory)id).expression = list[0] as PrimaryExpressionNode;
                    list[0] =id;
                }
                else
                {
                    ((FunctionCallExpression)list[0]).primary = id;
                }

            }
            else if(list.Count > 0 &&
                list[0] is ArrayAccessNode && ((ArrayAccessNode)list[0]).primary == null)
            {
                if (id is AccessMemory)
                {
                    var r_id = ((AccessMemory)id).expression;
                    ((ArrayAccessNode)list[0]).primary = r_id;
                    ((AccessMemory)id).expression = list[0] as PrimaryExpressionNode;
                    list[0] = id;
                }
                else
                {
                    ((ArrayAccessNode)list[0]).primary = id;
                }
            }
            else if(list.Count > 0 &&
                list[0] is PostAdditiveExpressionNode && ((PostAdditiveExpressionNode)list[0]).primary == null)
            {
                if (id is AccessMemory)
                {
                    var r_id = ((AccessMemory)id).expression;
                    ((PostAdditiveExpressionNode)list[0]).primary = r_id;
                    ((AccessMemory)id).expression = list[0] as PrimaryExpressionNode;
                    list[0] = id;
                }
                else
                {
                    ((PostAdditiveExpressionNode)list[0]).primary = id;
                }
            }
            else
            {
                list.Insert(0, id);
            }
        }

        private List<ExpressionNode> primary_expression_p()
        {
            DebugInfoMethod("primary_expression_p");
            if (pass(TokenType.OP_DOT))
            {
                consumeToken();

                if (!pass(TokenType.ID))
                    throwError("identifier");
                var id = current_token;
                var left = new AccessMemory(new IdentifierNode(id));
                consumeToken();
                var list = primary_expression_p();
                is_function_or_array(ref list, left);
                return list;
            }else if(pass(TokenType.OPEN_PARENTHESIS, TokenType.OPEN_SQUARE_BRACKET))
            {
                var funcOrArray = optional_funct_or_array_call();
                var list = primary_expression_p() ;
                list.Insert(0, funcOrArray);
                return list;
            }
            else if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT))
            {
                var Operator = current_token;
                consumeToken();
                var left = new PostAdditiveExpressionNode(Operator);
                var list = primary_expression_p();
                list.Insert(0, left);
                return list;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<ExpressionNode>();
            }
        }

        private PrimaryExpressionNode optional_funct_or_array_call()
        {
            DebugInfoMethod("optional_funct_or_array_call");
            if (pass(TokenType.OPEN_PARENTHESIS))
            {
                consumeToken();
                var arguments = argument_list();

                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                consumeToken();
                return new FunctionCallExpression(arguments);
            }else if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                var lista = optional_array_access_list();
                return new ArrayAccessNode(lista);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private List<ArrayNode> optional_array_access_list()
        {
            DebugInfoMethod("optional_array_access_list");
            if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                consumeToken();
                var expressionList = expression_list();
                var arrayNode = new ArrayNode(expressionList);
                if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                    throwError("close square bracket ']'");
                consumeToken();

                var lista = optional_array_access_list();
                lista.Insert(0, arrayNode);
                return lista;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<ArrayNode>();
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
            else if (pass(TokenType.RW_NULL))
            {
                type = dictionary();
            }
            else
            {
                //var token = current_token;
                //consumeToken();
                type = built_in_type();
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
            ArrayNode arrayNode = new ArrayNode();
            DebugInfoMethod("instance_expression_factorized_p");
            TokenType[] nuevo = { TokenType.OP_TER_NULLABLE, TokenType.OP_COLON,
                TokenType.OP_NULLABLE, TokenType.OP_LOG_OR,
                TokenType.OP_LOG_AND, TokenType.OP_BIN_OR,
                TokenType.OP_BIN_XOR, TokenType.OP_BIN_AND,
                TokenType.OPEN_PARENTHESIS, TokenType.RW_NEW,
                TokenType.ID, TokenType.RW_THIS, TokenType.RW_BASE, TokenType.RW_NULL
            };
            if (pass(nuevo.Concat(equalityOperatorOptions).Concat(relationalOperatorOptions).
                Concat(Is_AsOperatorOptions).Concat(shiftOperatorOptions).Concat(additiveOperatorOptions).
                Concat(multiplicativeOperatorOptions).Concat(assignmentOperatorOptions).Concat(unaryOperatorOptions)
                .Concat(literalOptions).ToArray()))
            {                    
                arrayNode.expression_list = expression_list();
                arrayNode.dimensions = arrayNode.expression_list.Count;
                if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                    throwError("close square bracket ']'");
                consumeToken();
                type = new ArrayTypeNode(type);
                ((ArrayTypeNode)type).indexes.Add(arrayNode);
                type = optional_rank_specifier_list(type);
                var initialization = optional_array_initializer();
                return new ArrayInstantiation(type, initialization);

            }else if (pass(TokenType.OP_COMMA,TokenType.CLOSE_SQUARE_BRACKET))
            {
                type = new ArrayTypeNode(type);
                rank_specifier_list(ref type);
                var initialization = array_initializer();
                return new ArrayInstantiation(type, initialization);
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
                TokenType.ID, TokenType.RW_THIS,TokenType.OPEN_CURLY_BRACKET, TokenType.RW_BASE, TokenType.RW_NULL
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

        private void rank_specifier_list(ref TypeDefinitionNode atn)
        {
            DebugInfoMethod("rank_specifier_list");
            ArrayNode an = rank_specifier();
            ((ArrayTypeNode)atn).indexes.Add(an);
            atn = optional_rank_specifier_list(atn);
        }

        private ArrayNode rank_specifier()
        {
            DebugInfoMethod("rank_specifier");

            var an = new ArrayNode();
            optional_comma_list(ref an);

            if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                throwError("close square bracket ']'");
            consumeToken();
            return an;
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

        private TypeDefinitionNode optional_rank_specifier_list(TypeDefinitionNode type)
        {
            DebugInfoMethod("optional_rank_specifier_list");
            if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                consumeToken();
                if(!(type is ArrayTypeNode))
                    type = new ArrayTypeNode(type);

                rank_specifier_list(ref type );
                return type;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return type;
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
