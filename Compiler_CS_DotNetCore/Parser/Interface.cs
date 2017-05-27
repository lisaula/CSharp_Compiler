using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compiler.Tree;
namespace Compiler
{
    public partial class Parser
    {
        private InterfaceNode interface_declaration(EncapsulationNode encapsulation)
        {
            DebugInfoMethod("interface_declaration");
            if (!pass(TokenType.RW_INTERFACE))
                throwError("interface");
            consumeToken();

            if (!pass(TokenType.ID))
                throwError("identifier");
            var interfaceNode = new InterfaceNode(encapsulation, current_token);
            consumeToken();

            if (pass(TokenType.OP_COLON))
            {
                interfaceNode.inheritance =  inheritance_base();
            }

            interfaceNode.methods = interface_body();
            optional_body_end();
            return interfaceNode;
        }

        private List<MethodNode> interface_body()
        {
            DebugInfoMethod("interface_body");
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();

            var lista =  interface_method_declaration_list();

            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();

            return lista;
        }

        private List<MethodNode> interface_method_declaration_list()
        {
            DebugInfoMethod("interface_method_declaration_list");
            TokenType[] nuevo = {TokenType.RW_VOID };
            if (pass(typesOptions.Concat(nuevo).ToArray())) {
                var method = interface_method_header();

                if (!pass(TokenType.END_STATEMENT))
                    throwError(";");
                consumeToken();

                var lista = interface_method_declaration_list();
                lista.Insert(0, method);
                return lista;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<MethodNode>();
            }
        }

        private MethodNode interface_method_header()
        {
            DebugInfoMethod("interface_method_header");
            var type = type_or_void();
            if (!pass(TokenType.ID))
                throwError("identifier");
            var id = new IdentifierNode(current_token);
            consumeToken();

            if (!pass(TokenType.OPEN_PARENTHESIS))
                throwError("open parenthesis '('");
            consumeToken();

            var parameters = fixed_parameters();

            if (!pass(TokenType.CLOSE_PARENTHESIS))
                throwError("close parenthesis ')'");
            consumeToken();
            return new MethodNode(type, id, parameters);
        }

        private TypeDefinitionNode type_or_void()
        {
            DebugInfoMethod("type_or_void");
            if (pass(TokenType.RW_VOID))
            {
                var t = new VoidTypeNode(current_token, null);
                consumeToken();
                return t;
            }
            else if (pass(typesOptions))
            {
                return types();
            }
            else
            {
                throwError("a type");
                return null;
            }
        }

        private TypeDefinitionNode types()
        {
            DebugInfoMethod("type");
            if (!pass(typesOptions))
                throwError("a type");
            if (pass(TokenType.ID))
            {
                var id = new IdentifierTypeNode();
                id.Identifiers = qualified_identifier();
                optional_rank_specifier_list(ref id.arrayNode);
                return id;
            }else if (pass(TokenType.RW_DICTIONARY))
            {
                var dict = dictionary();
                optional_rank_specifier_list(ref dict.arrayNode);
                return dict;
            }
            else
            {
                var primitive = built_in_type();
                optional_rank_specifier_list(ref primitive.arrayNode);
                return primitive;
            }
        }

        private DictionaryTypeNode dictionary()
        {
            DebugInfoMethod("dictionary");
            if (!pass(TokenType.RW_DICTIONARY))
                throwError("resered word \"dictionary\"");
            consumeToken();

            if (!pass(TokenType.OP_LESS_THAN))
                throwError("<");
            consumeToken();

            var t1 = types();

            if (!pass(TokenType.OP_COMMA))
                throwError("comma ','");
            consumeToken();

            var t2 = types();

            if (!pass(TokenType.OP_GREATER_THAN))
                throwError(">");
            consumeToken();
            return new DictionaryTypeNode(t1, t2);
        }

        private PrimitiveType built_in_type()
        {
            DebugInfoMethod("built_in_type");
            if (!pass(TokenType.RW_INT,
            TokenType.RW_CHAR,
            TokenType.RW_STRING,
            TokenType.RW_BOOL,
            TokenType.RW_FLOAT))
                throwError("a primitive type");
            var token = current_token;
            consumeToken();
            return new PrimitiveType(token);
        }

        private List<IdentifierNode> qualified_identifier()
        {
            DebugInfoMethod("qualified_identifier");
            if (!pass(TokenType.ID))
            {
                throwError("identifier");
            }
            var id = new IdentifierNode(current_token);
            consumeToken();
            var lista = identifier_attribute();
            lista.Insert(0, id);
            return lista;
        }

        private List<Parameter> fixed_parameters()
        {
            DebugInfoMethod("fixed_parameters");
            if (pass(typesOptions))
            {
                var parameter = fixed_parameter();
                var list = fixed_parameters_p();
                list.Insert(0, parameter);
                return list;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<Parameter>();
            }
        }

        private List<Parameter> fixed_parameters_p()
        {
            DebugInfoMethod("fixed_parameters_p");
            if (pass(TokenType.OP_COMMA))
            { 
                consumeToken();
                var parameter = fixed_parameter();
                var list = fixed_parameters_p();
                list.Insert(0, parameter);
                return list;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<Parameter>();
            }
        }

        private Parameter fixed_parameter()
        {
            DebugInfoMethod("fixed-parameter");
            if (!pass(typesOptions))
                throwError("a type");
            var t = types();
            if (!pass(TokenType.ID))
                throwError("identifier");
            var id = new IdentifierNode(current_token);
            consumeToken();
            return new Parameter(t, id);
        }

        private InheritanceNode inheritance_base()
        {
            DebugInfoMethod("inheritance_base");
            if (pass(TokenType.OP_COLON))
            {
                consumeToken();
                var inheritance = new InheritanceNode();
                inheritance.identifierList = identifiers_list();
                return inheritance;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return null;
            }
        }

        private List<List<IdentifierNode>> identifiers_list()
        {
            DebugInfoMethod("identifiers_list");
            if (!pass(TokenType.ID))
                throwError("identifier");
            //consumeToken();
            var lista = qualified_identifier();
            var LIST = identifiers_list_p();
            LIST.Insert(0, lista);
            return LIST;
        }

        private List<List<IdentifierNode>> identifiers_list_p()
        {
            DebugInfoMethod("identifiers_list_p");
            if (pass(TokenType.OP_COMMA))
            {
                consumeToken();
                if (!pass(TokenType.ID))
                    throwError("identifier");
                //consumeToken();
                var lista = qualified_identifier();
                var LIST = identifiers_list_p();
                LIST.Insert(0, lista);
                return LIST;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<List<IdentifierNode>>();
            }
        }
    }
}
