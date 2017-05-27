using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compiler.Tree;
namespace Compiler
{
    public partial class Parser
    {
        private ClassDefinitionNode class_declaration(EncapsulationNode encapsulation)
        {
            DebugInfoMethod("class_declaration");
            bool isAbstract = false;
            if (pass(TokenType.RW_ABSTRACT))
            {
                isAbstract =  class_modifier();
            }
            if (!pass(TokenType.RW_CLASS))
                throwError("class");
            consumeToken();

            if (!pass(TokenType.ID))
                throwError("identifier");
            var id = new IdentifierNode(current_token);
            consumeToken();
            InheritanceNode inheritance = null;
            if (pass(TokenType.OP_COLON))
            {
                inheritance =  inheritance_base();
            }
            var clase = new ClassDefinitionNode(encapsulation, isAbstract, id, inheritance);
            class_body(ref clase);

            optional_body_end();
            return clase;
        }

        private void class_body(ref ClassDefinitionNode clase)
        {
            DebugInfoMethod("class_body");
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            optional_class_member_declaration_list(ref clase);

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
        }

        private void optional_class_member_declaration_list(ref ClassDefinitionNode clase)
        {
            DebugInfoMethod("optional_class_member_declaration_list");
            TokenType[] nuevo = { TokenType.RW_VOID };
            if (pass(encapsulationTypes.Concat(optionalModifiersOptions).Concat(typesOptions).Concat(nuevo).ToArray()))
            {
                class_member_declaration(ref clase);
                optional_class_member_declaration_list(ref clase);
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void class_member_declaration(ref ClassDefinitionNode clase)
        {
            DebugInfoMethod("class_member_declaration");
            EncapsulationNode encapsulation = null;
            if (pass(encapsulationTypes))
            {
                encapsulation = encapsulation_modifier();
            }
            class_member_declaration_options(encapsulation, ref clase);
        }

        private void class_member_declaration_options(EncapsulationNode encapsulation, ref ClassDefinitionNode clase)
        {
            DebugInfoMethod("class_member_declaration_options");
            TokenType[] nuevo = { TokenType.RW_VOID };
            if (pass(optionalModifiersOptions.Concat(typesOptions).Concat(nuevo).ToArray()))
            {
                addLookAhead(lexer.getNextToken());
                if (pass(TokenType.ID) && look_ahead[0].type == TokenType.OPEN_PARENTHESIS)
                {
                        constructor_declaration();
                }
                else
                {
                    var modifier = optional_modifier();
                    var type = type_or_void();
                    if (!pass(TokenType.ID))
                        throwError("identifier");
                    var id = new IdentifierNode(current_token);
                    consumeToken();
                    field_or_method(encapsulation, modifier, type, id, ref clase);
                }
            }
            else
            {
                throwError("optional-modifier, type-or-void");
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

        private List<Statement> maybe_empty_block()
        {
            DebugInfoMethod("maybe_empty_block");
            if (pass(TokenType.OPEN_CURLY_BRACKET))
            {
                consumeToken();
                var lista =  optional_statement_list();
                if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                    throwError("close curly bracket '}'");
                consumeToken();
                return lista;
            }else if (pass(TokenType.END_STATEMENT)){
                consumeToken();
                return null;
            }
            else
            {
                throwError("block of code, or end of statement");
                return null;
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

        private void argument_list()
        {
            DebugInfoMethod("argument_list");
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
                expression();
                if (pass(TokenType.OP_COMMA))
                    argument_list_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
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

        private ModifierNode optional_modifier()
        {
            DebugInfoMethod("optional_modifier");
            if (pass(optionalModifiersOptions))
            {
                var token = current_token;
                consumeToken();
                return new ModifierNode(token);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private bool class_modifier()
        {
            DebugInfoMethod("class_modifier");
            if (pass(TokenType.RW_ABSTRACT))
            {
                consumeToken();
                return true;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return false;
            }
            
        }
    }
}
