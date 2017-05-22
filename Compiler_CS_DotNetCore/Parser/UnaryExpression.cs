using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void unary_expression()
        {
            TokenType[] nuevo = { TokenType.RW_NEW , TokenType.ID,
                TokenType.OPEN_PARENTHESIS, TokenType.RW_THIS
            };
            DebugInfoMethod("unary_expression");
            if (pass(unaryOperatorOptions))
            {
                consumeToken();
                unary_expression();
            }else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                addLookAhead(lexer.getNextToken());
                addLookAhead(lexer.getNextToken());
                if (typesOptions.Contains(look_ahead[0].type) && 
                    (look_ahead[1].type == TokenType.CLOSE_PARENTHESIS || look_ahead[1].type == TokenType.OP_DOT))
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
            }else if (pass(nuevo.Concat(literalOptions).ToArray()))
            {
                primary_expression();
            }
            else
            {
                throwError("unary-operator, casting or primary-expression");
            }
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
            }else if (pass(TokenType.ID))
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

                optional_rank_specifier_list();
                optional_array_initializer();

            }else if (pass(TokenType.OP_COMMA,TokenType.CLOSE_SQUARE_BRACKET))
            {
                rank_specifier_list();
                array_initializer();
            }
            else
            {
                throwError("expression or rank specifier ','");
            }
        }

        private void array_initializer()
        {
            DebugInfoMethod("array_initializer");
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            optional_variable_initializer_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '{'");
            consumeToken();
        }

        private void optional_variable_initializer_list()
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
                variable_initializer_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void variable_initializer_list()
        {
            DebugInfoMethod("variable_initializer_list");
            variable_initializer();
            variable_initializer_list_p();
        }

        private void variable_initializer_list_p()
        {
            DebugInfoMethod("variable_initializer_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                variable_initializer_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void rank_specifier_list()
        {
            DebugInfoMethod("rank_specifier_list");
            rank_specifier();
            optional_rank_specifier_list();
        }

        private void rank_specifier()
        {
            DebugInfoMethod("rank_specifier");
            optional_comma_list();

            if (!pass(TokenType.CLOSE_SQUARE_BRACKET))
                throwError("close square bracket ']'");
            consumeToken();
        }

        private void optional_comma_list()
        {
            DebugInfoMethod("optional_comma_list");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                optional_comma_list();
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

        private void optional_rank_specifier_list()
        {
            DebugInfoMethod("optional_rank_specifier_list");
            if (pass(TokenType.OPEN_SQUARE_BRACKET))
            {
                consumeToken();
                rank_specifier_list();
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
