using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void class_declaration()
        {
            DebugInfoMethod("class_declaration");
            if (pass(TokenType.RW_ABSTRACT))
            {
                class_modifier();
            }
            if (!pass(TokenType.RW_CLASS))
                throwError("class");
            consumeToken();

            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();

            if (pass(TokenType.OP_COLON))
            {
                inheritance_base();
            }

            class_body();
            optional_body_end();

        }

        private void class_body()
        {
            DebugInfoMethod("class_body");
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            optional_class_member_declaration_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
        }

        private void optional_class_member_declaration_list()
        {
            DebugInfoMethod("optional_class_member_declaration_list");
            TokenType[] nuevo = { TokenType.RW_VOID };
            if (pass(encapsulationTypes.Concat(optionalModifiersOptions).Concat(typesOptions).Concat(nuevo).ToArray()))
            {
                class_member_declaration();
                optional_class_member_declaration_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void class_member_declaration()
        {
            DebugInfoMethod("class_member_declaration");
            if (pass(encapsulationTypes))
            {
                encapsulation_modifier();
            }
            class_member_declaration_options();
        }

        private void class_member_declaration_options()
        {
            DebugInfoMethod("class_member_declaration_options");
            TokenType[] nuevo = { TokenType.RW_VOID };
            if (pass(optionalModifiersOptions.Concat(typesOptions).Concat(nuevo).ToArray()))
            {
                if (pass(TokenType.ID))
                {
                    addLookAhead(lexer.getNextToken());
                    if (look_ahead[0].type == TokenType.OPEN_PARENTHESIS)
                        constructor_declaration();
                }
                else
                {
                    optional_modifier();
                    type_or_void();
                    if (!pass(TokenType.ID))
                        throwError("identifier");
                    consumeToken();
                    field_or_method();
                }
            }
        }

        private void constructor_declaration()
        {
            DebugInfoMethod("constructor_declaration");
            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            fixed_parameters();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            if (pass(TokenType.OP_COLON))
            {
                constructor_initializer();
            }
            maybe_empty_block();
        }

        private void maybe_empty_block()
        {
            DebugInfoMethod("maybe_empty_block");
            if (pass(TokenType.OPEN_CURLY_BRACKET))
            {
                consumeToken();
                optional_statement_list();
                if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                    throwError("close curly bracket '}'");
                consumeToken();                    
            }else if (pass(TokenType.END_STATEMENT)){
                consumeToken();
            }
            else
            {
                throwError("block of code, or end of statement");
            }
        }

        private void constructor_initializer()
        {
            DebugInfoMethod("constructor_initializer");
            if (pass(TokenType.OP_COLON))
            {
                consumeToken();

                if (!pass(TokenType.RW_BASE))
                    throwError("reserved word \"base\"");
                consumeToken();

                if (!pass(TokenType.OPEN_PARENTHESIS))
                    throwError("open parenthesis '('");
                consumeToken();

                argument_list();

                if (!pass(TokenType.CLOSE_PARENTHESIS))
                    throwError("close parenthesis ')'");
                consumeToken();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void argument_list()//TODO: EXPRESSION NO ESTA IMPLEMENTADO
        {
            DebugInfoMethod("argument_list");
            expression();
            if(pass(TokenType.OP_COMMA))
                argument_list_p();
        }

        private void argument_list_p()
        {
            DebugInfoMethod("argument_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                expression();
                argument_list_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void optional_modifier()
        {
            DebugInfoMethod("optional_modifier");
            if (pass(optionalModifiersOptions))
            {
                consumeToken();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void class_modifier()
        {
            DebugInfoMethod("class_modifier");
            if (pass(TokenType.RW_ABSTRACT))
            {
                consumeToken();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
            
        }
    }
}
