using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
namespace Compiler
{
    public partial class Parser
    {
        private TypeDefinitionNode enum_declaration(EncapsulationNode encapsulation)
        {
            DebugInfoMethod("enum_declaration");
            if (!pass(TokenType.RW_ENUM))
                throwError("enum");
            consumeToken();
            if (!pass(TokenType.ID))
                throwError("identifier");
            var identifier = new IdentifierNode(current_token.lexema);
            consumeToken();
            var enumDefition = new EnumDefinitionNode(encapsulation, identifier);
            enumDefition.enumNodeList = enum_body();
            optional_body_end();
            return enumDefition;
        }

        private void optional_body_end()
        {
            DebugInfoMethod("optional_body_end");
            if (pass(TokenType.END_STATEMENT))
            {
                consumeToken();
            }
            else
            {
                DebugInfoMethod("epsilon");
            }
        }

        private List<EnumNode> enum_body()
        {
            DebugInfoMethod("enum_body");
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            var lista = optional_assignable_identifiers_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
            return lista;
        }

        private List<EnumNode> optional_assignable_identifiers_list()
        {
            DebugInfoMethod("optional_assignable_identifiers_list");
            if (pass(TokenType.ID))
            {
                var identifier = new IdentifierNode(current_token.lexema);
                consumeToken();
                return assignment_options(identifier);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private List<EnumNode> assignment_options(IdentifierNode identifier)
        {
            DebugInfoMethod("assignment_options");
                      
            if (pass(TokenType.OP_ASSIGN))
            {
                consumeToken();

                expression();
                return optional_assignable_identifiers_list_p(identifier, new ExpressionNode("7"));
            }else
                return optional_assignable_identifiers_list_p(identifier, null);
        }

        private List<EnumNode> optional_assignable_identifiers_list_p(IdentifierNode identifier, ExpressionNode expressionNode)
        {
            DebugInfoMethod("optional_assignable_identifiers_list_p");
            var enumNode = new EnumNode(identifier, expressionNode);
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                var lista = optional_assignable_identifiers_list();
                lista.Insert(0, enumNode);
                return lista;
            }
            else
            {
                DebugInfoMethod("epsilon");
                var lista = new List<EnumNode>();
                lista.Add(enumNode);
                return lista;
            }
            
        }
    }
}
