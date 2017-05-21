using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void interface_declaration()
        {
            DebugInfoMethod("interface_declaration");
            if (!pass(TokenType.RW_INTERFACE))
                throwError("interface");
            consumeToken();

            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();

            if (pass(TokenType.OP_COLON))
            {
                inheritance_base();
            }

            interface_body();
            optional_body_end();
            
        }

        private void interface_body()
        {
            DebugInfoMethod("interface_body");
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            interface_method_declaration_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
        }

        private void interface_method_declaration_list()
        {
            DebugInfoMethod("interface_method_declaration_list");
            TokenType[] nuevo = {TokenType.RW_VOID };
            if (pass(typesOptions.Concat(nuevo).ToArray())) {
                interface_method_header();

                if (!pass(TokenType.END_STATEMENT))
                    throwError(";");
                consumeToken();
                interface_method_declaration_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void interface_method_header()
        {
            DebugInfoMethod("interface_method_header");
            type_or_void();
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
        }

        private void type_or_void()
        {
            DebugInfoMethod("type_or_void");
            if (pass(TokenType.RW_VOID))
                consumeToken();
            else if(pass(typesOptions))
            {
                types();
            }
            else
            {
                throwError("a type");
            }
        }

        private void types()
        {
            DebugInfoMethod("type");
            if (!pass(typesOptions))
                throwError("a type");
            if (pass(TokenType.ID))
            {
                qualified_identifier();
                optional_rank_specifier_list();
            }
            else
            {
                built_in_type();
                optional_rank_specifier_list();
            }
        }

        private void built_in_type()
        {
            DebugInfoMethod("built_in_type");
            if (!pass(TokenType.RW_INT,
            TokenType.RW_CHAR,
            TokenType.RW_STRING,
            TokenType.RW_BOOL,
            TokenType.RW_FLOAT))
                throwError("a primitive type");
            consumeToken();
        }

        private void qualified_identifier()
        {
            DebugInfoMethod("qualified_identifier");
            if (pass(TokenType.ID))
            {
                consumeToken();
                if(pass(TokenType.OP_DOT))
                    identifier_attribute();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void fixed_parameters()
        {
            DebugInfoMethod("fixed_parameters");
            if (pass(typesOptions))
            {
                fixed_parameter();
                fixed_parameters_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void fixed_parameters_p()
        {
            DebugInfoMethod("fixed_parameters_p");
            if (pass(TokenType.OP_COMMA))
            { 
                consumeToken();
                fixed_parameter();
                fixed_parameters_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void fixed_parameter()
        {
            DebugInfoMethod("fixed-parameter");
            if (!pass(typesOptions))
                throwError("a type");
            types();
            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();
        }

        private void inheritance_base()
        {
            DebugInfoMethod("inheritance_base");
            if (pass(TokenType.OP_COLON))
            {
                consumeToken();
                identifiers_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void identifiers_list()
        {
            DebugInfoMethod("identifiers_list");
            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();
            identifiers_list_p();
        }

        private void identifiers_list_p()
        {
            DebugInfoMethod("identifiers_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                if (!pass(TokenType.ID))
                    throwError("identifier");
                consumeToken();
                identifiers_list_p();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }
    }
}
