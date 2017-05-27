using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compiler.Tree;
namespace Compiler
{
    public partial class Parser
    {
        private void field_or_method(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type, IdentifierNode id, ref ClassDefinitionNode clase)
        {
            DebugInfoMethod("field_or_method");
            if(pass(TokenType.OP_ASSIGN, TokenType.OP_COMMA, TokenType.END_STATEMENT))
            {
                var assignment = field_declaration(encapsulation, modifier, type, ref clase);
                var field = new FieldNode(encapsulation, modifier, type, id, assignment);
                clase.fields.Add(field);
            } else if (pass(TokenType.OPEN_PARENTHESIS))
            {
                method_declaration();
            }
            else
            {
                throwError("'=', ',', ';' or '('. Field_or_method expected");
            }
            
        }

        private void method_declaration()
        {
            DebugInfoMethod("method_declaration");
            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            fixed_parameters();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();

            maybe_empty_block();
        }

        private VariableInitializer field_declaration(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type,ref ClassDefinitionNode clase)
        {
            DebugInfoMethod("field_declaration");
            if (!pass(TokenType.OP_ASSIGN, TokenType.OP_COMMA, TokenType.END_STATEMENT))
                throwError("'=', ',' or ';' ");
            var assignmentExpression = variable_assigner();
            var list = variable_declarator_list_p(encapsulation, modifier, type);
            clase.fields.AddRange(list);
            if (!pass(TokenType.END_STATEMENT))
                throwError(" end statement ';'");
            consumeToken();
            return assignmentExpression;
        }

        private List<FieldNode> variable_declarator_list_p(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type)
        {
            DebugInfoMethod("variable_declarator_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                return variable_declarator_list(encapsulation,modifier, type);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<FieldNode>();
            }
        }

        private List<FieldNode> variable_declarator_list(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type)
        {
            DebugInfoMethod("variable_declarator_list");
            if (!pass(TokenType.ID))
                throwError("identifier");
            var id = new IdentifierNode(current_token);
            consumeToken();

            VariableInitializer init = null;
            if (pass(TokenType.OP_ASSIGN))
            {
                init = variable_assigner();
            }
            var field = new FieldNode(encapsulation, modifier, type, id, init);

            var lista = variable_declarator_list_p(encapsulation, modifier, type);
            lista.Insert(0, field);
            return lista;
        }

        private VariableInitializer variable_assigner()
        {
            DebugInfoMethod("variable_assigner");
            if (pass(TokenType.OP_ASSIGN))
            {
                consumeToken();
                var assignmentExpression = variable_initializer();
                return assignmentExpression;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private VariableInitializer variable_initializer()
        {
            DebugInfoMethod("variable_initializer");
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
                return expression();
            }else if (pass(TokenType.OPEN_CURLY_BRACKET))
            {
                return array_initializer();
            }
            else
            {
                throwError("expression or array initializer '{'");
                return null;
            }
        }
    }
}
