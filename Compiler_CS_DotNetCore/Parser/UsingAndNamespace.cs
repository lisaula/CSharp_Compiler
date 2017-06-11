using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler
{
    public partial class Parser
    {
        public NamespaceNode parse()
        {
            DebugInfoMethod("parser");
            var tree = code();
            if (current_token.type != TokenType.EOF)
                throwError("EOF. Not all tokens were consumed. Current token: "+current_token.type);
            Console.Out.WriteLine("Successful");
            return tree;
        }

        private NamespaceNode code()
        {
            DebugInfoMethod("code");
            return compilation_unit();
        }

        private NamespaceNode compilation_unit()
        {
            DebugInfoMethod("compilation_unit");

            NamespaceNode compilationNode = new NamespaceNode();
            var token = new Token();
            token.lexema = Utils.GlobalNamespace;
            compilationNode.identifierList.Add(new IdentifierNode(token));

            var usingList = new List<UsingNode>();
            compilationNode.usingList = optional_using_directive(ref usingList);
            var namespaceList = new List<NamespaceNode>();
            compilationNode.typeList = new List<TypeDefinitionNode>();
            compilationNode.namespaceList = optional_namespace_member_declaration(ref namespaceList, ref compilationNode.typeList, compilationNode);
            return compilationNode;
        }

        private List<NamespaceNode> optional_namespace_member_declaration(ref List<NamespaceNode> namespaceList, ref List<TypeDefinitionNode> typeList, NamespaceNode parent)
        {
            DebugInfoMethod("optional_namespace_member_declaration");
            TokenType[] nuevo = { TokenType.RW_NAMEESPACE };
            if (pass(encapsulationTypes.Concat(nuevo).Concat(typesDeclarationOptions).Concat(optionalModifiersOptions).ToArray()))
            {
                if (namespaceList == null)
                    namespaceList = new List<NamespaceNode>();
                if (typeList == null)
                    typeList = new List<TypeDefinitionNode>();
                return namespace_member_declaration(ref namespaceList, ref typeList, parent);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return namespaceList;
            } 
        }

        private List<NamespaceNode> namespace_member_declaration(ref List<NamespaceNode> namespaceList, ref List<TypeDefinitionNode> typeList, NamespaceNode parent)
        {
            DebugInfoMethod("namespace_member_declaration");
            if (pass(TokenType.RW_NAMEESPACE))
            {
                var nms = namespace_declaration();
                nms.parent = parent;
                namespaceList.Add(nms);
                return optional_namespace_member_declaration(ref namespaceList, ref typeList, parent);
            }else if (pass(encapsulationTypes.Concat(typesDeclarationOptions).Concat(optionalModifiersOptions).ToArray()))
            {
                typeList.AddRange(type_declaration_list(parent));
                return optional_namespace_member_declaration(ref namespaceList, ref typeList, parent);
            }
            else
            {
                throwError("Namespace, or Type_declaration");
                return null;
            }
        }

        private NamespaceNode namespace_declaration()
        {
            DebugInfoMethod("namespace_declaration");
            if (!pass(TokenType.RW_NAMEESPACE))
            {
                throwError("namespace");
            }
            consumeToken();
            if (!pass(TokenType.ID))
                throwError("identifier");

            var lista = qualified_identifier();
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            NamespaceNode namespaceNode = new NamespaceNode(lista);
            namespace_body(ref namespaceNode);
            return namespaceNode;
        }

        private void namespace_body(ref NamespaceNode namespaceNode)
        {
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();
            namespaceNode.usingList = optional_using_directive(ref namespaceNode.usingList);
            namespaceNode.namespaceList = optional_namespace_member_declaration(ref namespaceNode.namespaceList, ref namespaceNode.typeList, namespaceNode);
            if (!pass(TokenType.CLOSE_CURLY_BRACKET))
                throwError("close curly bracket '}'");
            consumeToken();
        }

        private List<TypeDefinitionNode> type_declaration_list(NamespaceNode parent)
        {
            DebugInfoMethod("type_declaration_list");
            if (pass(encapsulationTypes.Concat(typesDeclarationOptions).Concat(optionalModifiersOptions).ToArray()))
            {
                var typeDefinition = type_declaration();
                typeDefinition.parent_namespace = parent;
                var typeList = type_declaration_list(parent);

                typeList.Insert(0, typeDefinition);
                return typeList;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<TypeDefinitionNode>();
            }
        }

        private TypeDefinitionNode type_declaration()
        {
            DebugInfoMethod("type_declaration");
            var encapsulationNode = encapsulation_modifier();
            if (!pass(typesDeclarationOptions.Concat(optionalModifiersOptions).ToArray()))
                throwError("Class, Interface or Enum");
            return group_declaration(encapsulationNode);
        }

        private TypeDefinitionNode group_declaration(EncapsulationNode encapsulation)
        {
            if (pass(TokenType.RW_ABSTRACT, TokenType.RW_STATIC, TokenType.RW_VIRTUAL, TokenType.RW_CLASS))
            {
                return class_declaration(encapsulation);
            }else if (pass(TokenType.RW_INTERFACE))
            {
                return interface_declaration(encapsulation);
            }
            else if(pass(TokenType.RW_ENUM))
            {
                return enum_declaration(encapsulation);
            }
            else
            {
                throwError("Class, Interface or Enum");
            }
            return null;
        }

        private EncapsulationNode encapsulation_modifier()
        {
            DebugInfoMethod("encapsulation_modifier");
            if (pass(encapsulationTypes))
            {
                var encapsulation = new EncapsulationNode(current_token);
                consumeToken();
                return encapsulation;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new EncapsulationNode();
            }
        }

        private List<UsingNode> optional_using_directive(ref List<UsingNode> usingList)
        {
            DebugInfoMethod("optional_using_directive");
            if (pass(TokenType.RW_USING))
                return using_directive(usingList?? new List<UsingNode>());
            else
            {
                DebugInfoMethod("epsilon");
                return usingList;
                //epsilon
            }
        }

        private List<UsingNode> using_directive(List<UsingNode> usignList)
        {
            DebugInfoMethod("using_directive");
            if (!pass(TokenType.RW_USING))
                throwError("using");
            consumeToken();

            if (!pass(TokenType.ID))
                throwError("identifier");

            var lista = qualified_identifier();

            if (!pass(TokenType.END_STATEMENT))
                throwError(";");

            consumeToken();
            usignList.Add(new UsingNode(lista));
            return optional_using_directive(ref usignList);
        }

        private List<IdentifierNode> identifier_attribute()
        {
            DebugInfoMethod("identifier_attribute");
            if (pass(TokenType.OP_DOT))
            {
                consumeToken();
                if (!pass(TokenType.ID))
                    throwError("identifier");
                var id = new IdentifierNode(current_token);
                consumeToken();
                var lista = identifier_attribute();
                lista.Insert(0, id);
                return lista;
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<IdentifierNode>();
                //epsilon
            }
        }
    }
}
