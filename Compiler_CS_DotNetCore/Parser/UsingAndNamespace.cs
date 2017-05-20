using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public partial class Parser
    {
        public void parse()
        {
            DebugInfoMethod("parser");
            code();
            if (current_token.type != TokenType.EOF)
                throwError("EOF. Not all tokens were consumed.");
            Console.Out.WriteLine("Successful");
        }

        private void code()
        {
            DebugInfoMethod("code");
            compilation_unit();
        }

        private void compilation_unit()
        {
            DebugInfoMethod("compilation_unit");
            optional_using_directive();
            optional_namespace_member_declaration();
        }

        private void optional_namespace_member_declaration()
        {
            DebugInfoMethod("optional_namespace_member_declaration");
            TokenType[] nuevo = { TokenType.RW_NAMEESPACE };
            if (pass(encapsulationTypes.Concat(nuevo).Concat(typesDeclarationOptions).ToArray()))
            {
                namespace_member_declaration();
            }
            else
            {
                DebugInfoMethod("epsilon");
            } 
        }

        private void namespace_member_declaration()
        {
            DebugInfoMethod("namespace_member_declaration");
            if (pass(TokenType.RW_NAMEESPACE))
            {
                namespace_declaration();
                optional_namespace_member_declaration();
            }else if (pass(encapsulationTypes.Concat(typesDeclarationOptions).ToArray()))
            {
                type_declaration_list();
                optional_namespace_member_declaration();
            }
            else
            {
                throwError("Namespace, or Type_declaration");
            }
        }

        private void namespace_declaration()
        {
            DebugInfoMethod("namespace_declaration");
            if (!pass(TokenType.RW_NAMEESPACE))
            {
                throwError("namespace");
            }
            consumeToken();
            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();
            if (pass(TokenType.OP_DOT))
            {
                identifier_attribute();
            }
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            namespace_body();
        }

        private void namespace_body()
        {
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();
            optional_using_directive();
            optional_namespace_member_declaration();
            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
        }

        private void type_declaration_list()
        {
            DebugInfoMethod("type_declaration_list");
            if (pass(encapsulationTypes.Concat(typesDeclarationOptions).ToArray()))
            {
                type_declaration();
                type_declaration_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void type_declaration()
        {
            DebugInfoMethod("type_declaration");
            encapsulation_modifier();
            if (!pass(typesDeclarationOptions))
                throwError("Class, Interface or Enum");
            group_declaration();
        }

        private void group_declaration()
        {
            if (pass(TokenType.RW_ABSTRACT, TokenType.RW_CLASS))
            {
                class_declaration();
            }else if (pass(TokenType.RW_INTERFACE))
            {
                interface_declaration();
            }
            else if(pass(TokenType.RW_ENUM))
            {
                enum_declaration();
            }
            else
            {
                throwError("Class, Interface or Enum");
            }

        }

        private void encapsulation_modifier()
        {
            DebugInfoMethod("encapsulation_modifier");
            if (pass(encapsulationTypes))
            {
                consumeToken();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private void optional_using_directive()
        {
            DebugInfoMethod("optional_using_directive");
            if (pass(TokenType.RW_USING))
                using_directive();
            else
            {
                DebugInfoMethod("epsilon");
                //epsilon
            }

        }

        private void using_directive()
        {
            DebugInfoMethod("using_directive");
            if (!pass(TokenType.RW_USING))
                throwError("using");
            consumeToken();

            if (!pass(TokenType.ID))
                throwError("identifier");
            consumeToken();

            if (pass(TokenType.OP_DOT))
            {
                identifier_attribute();
            }

            if (!pass(TokenType.END_STATEMENT))
                throwError(";");
            consumeToken();
            optional_using_directive();
        }

        private void identifier_attribute()
        {
            DebugInfoMethod("identifier_attribute");
            if (pass(TokenType.OP_DOT))
            {
                consumeToken();
                if (!pass(TokenType.ID))
                    throwError("identifier");
                consumeToken();
                identifier_attribute();
            }
            else
            {
                DebugInfoMethod("epsilon");
                //epsilon
            }
        }
    }
}
