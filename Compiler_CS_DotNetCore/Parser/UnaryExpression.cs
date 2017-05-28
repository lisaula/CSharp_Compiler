﻿using Compiler.Tree;
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
                consumeToken();
                unary_expression();
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
                    types();

                    if (!pass(TokenType.CLOSE_PARENTHESIS))
                        throwError("close parenthesis ')'");
                    consumeToken();
                    primary_expression();
                }
                else
                {
                    primary_expression();
                }
            }else if (pass(nuevo.Concat(literalOptions).Concat(primitiveTypes).ToArray()))
            {
                primary_expression();
            }
            else
            {
                throwError("unary-operator, casting or primary-expression");
            }
            return null;
        }

        private void primary_expression()
        {
            DebugInfoMethod("primary_expression");
            if (pass(TokenType.RW_NEW))
            {
                consumeToken();
                instance_expression();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT, 
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    primary_expression_p();
            }else if (pass(literalOptions))
            {
                consumeToken();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    primary_expression_p();
            }else if (pass(TokenType.ID, TokenType.RW_BASE))
            {
                consumeToken();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    primary_expression_p();
            }else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                DebugInfoMethod("Entro parenthesized expression");
                consumeToken();
                expression();
                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                DebugInfoMethod("consumiendo close parenthesis");
                consumeToken();

                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    primary_expression_p();
            }
            else if(pass(TokenType.RW_THIS))
            {
                consumeToken();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    primary_expression_p();
            }else if (pass(primitiveTypes))
            {
                consumeToken();
                if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_DOT,
                    TokenType.OPEN_SQUARE_BRACKET, TokenType.OPEN_PARENTHESIS))
                    primary_expression_p();
                else
                    throwError("Function call");
            }
            else
            {
                throwError("new, literal, identifier, '(' or \"this\"");
            }
        }

        private void primary_expression_p()
        {
            DebugInfoMethod("primary_expression_p");
            if (pass(TokenType.OP_DOT))
            {
                consumeToken();

                if (!pass(TokenType.ID))
                    throwError("identifier");
                consumeToken();
                primary_expression_p();
            }else if(pass(TokenType.OPEN_PARENTHESIS, TokenType.OPEN_SQUARE_BRACKET))
            {
                optional_funct_or_array_call();
                primary_expression_p();
            }
            else if (pass(TokenType.OP_INCREMENT, TokenType.OP_DECREMENT))
            {
                consumeToken();
                primary_expression_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void optional_funct_or_array_call()
        {
            DebugInfoMethod("optional_funct_or_array_call");
            if (pass(TokenType.OPEN_PARENTHESIS))
            {
                consumeToken();
                argument_list();

                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                consumeToken();

            }else if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                optional_array_access_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void optional_array_access_list()
        {
            DebugInfoMethod("optional_array_access_list");
            if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                consumeToken();
                expression_list();

                if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                    throwError("close square bracket ']'");
                consumeToken();

                optional_array_access_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void instance_expression()
        {
            DebugInfoMethod("instance_expression");
            if (!pass(typesOptions))
                throwError("a type");
            //types();
            if (pass(TokenType.ID))
                qualified_identifier();
            else if (pass(TokenType.RW_DICTIONARY))
                dictionary();
            else
                consumeToken();

            instance_expression_factorized();
        }

        private void instance_expression_factorized()
        {
            DebugInfoMethod("instance_expression_factorized");
            if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                consumeToken();
                instance_expression_factorized_p();
            }else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                consumeToken();
                argument_list();

                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                consumeToken();
            }
            else
            {
                throwError("Square bracket '[' or Parenthesis '('");
            }
        }

        private void instance_expression_factorized_p()
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
                
                expression_list();

                if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                    throwError("close square bracket ']'");
                consumeToken();

                optional_rank_specifier_list(ref arrayNode);
                optional_array_initializer();

            }else if (pass(TokenType.OP_COMMA,TokenType.CLOSE_SQUARE_BRACKET))
            {
                rank_specifier_list(ref arrayNode);
                array_initializer();
            }
            else
            {
                throwError("expression or rank specifier ','");
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

        private void optional_array_initializer()
        {
            DebugInfoMethod("optional_array_initializer");
            if (pass(TokenType.OPEN_CURLY_BRACKET))
            {
                array_initializer();
            }
            else
            {
                DebugInfoMethod("epsilon");
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

        private void expression_list()
        {
            DebugInfoMethod("expression_list");
            expression();
            expression_list_p();
        }

        private void expression_list_p()
        {
            DebugInfoMethod("expression_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                expression_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void optional_expression_list()
        {
            DebugInfoMethod("optional_expression_list");
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
                expression_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }

        }
    }
}
