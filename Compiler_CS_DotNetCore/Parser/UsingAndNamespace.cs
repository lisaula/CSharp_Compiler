using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Tree;
namespace Compiler
{
    public partial class Parser
    {
        public CompilationNode parse()
        {
            DebugInfoMethod("parser");
            var tree = code();
            if (current_token.type != TokenType.EOF)
                throwError("EOF. Not all tokens were consumed.");
            Console.Out.WriteLine("Successful");
            return tree;
        }

        private CompilationNode code()
        {
            DebugInfoMethod("code");
            return compilation_unit();
        }

        private CompilationNode compilation_unit()
        {
            var compilationNode = new CompilationNode();
            DebugInfoMethod("compilation_unit");
            var usingList = new List<UsingNode>();
            compilationNode.usingArray = optional_using_directive(ref usingList);
            var namespaceList = new List<NamespaceNode>();
            compilationNode.namespaceArray = optional_namespace_member_declaration(ref namespaceList);
            return compilationNode;
        }

        private List<NamespaceNode> optional_namespace_member_declaration(ref List<NamespaceNode> namespaceList)
        {
            DebugInfoMethod("optional_namespace_member_declaration");
            TokenType[] nuevo = { TokenType.RW_NAMEESPACE };
            if (pass(encapsulationTypes.Concat(nuevo).Concat(typesDeclarationOptions).ToArray()))
            {
                return namespace_member_declaration(ref namespaceList);
            }
            else
            {
                DebugInfoMethod("epsilon");
                return namespaceList;
            } 
        }

        private List<NamespaceNode> namespace_member_declaration(ref List<NamespaceNode> namespaceList)
        {
            DebugInfoMethod("namespace_member_declaration");
            if (pass(TokenType.RW_NAMEESPACE))
            {
                namespaceList.Add(namespace_declaration());
                return optional_namespace_member_declaration(ref namespaceList);
            }else if (pass(encapsulationTypes.Concat(typesDeclarationOptions).ToArray()))
            {
                type_declaration_list();
                return optional_namespace_member_declaration(ref namespaceList);
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
            var identifier = new IdenfierNode(current_token.lexema);
            consumeToken();
            if (pass(TokenType.OP_DOT))
            {
                identifier_attribute();
            }
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            var namespaceNode = new NamespaceNode(identifier);
            namespace_body(ref namespaceNode);
            return namespaceNode;
        }

        private void namespace_body(ref NamespaceNode namespaceNode)
        {
            if (!pass(TokenType.OPEN_CURLY_BRACKET))
                throwError("open curly bracket '{'");
            consumeToken();
            optional_using_directive(ref namespaceNode.usingList);
            optional_namespace_member_declaration(ref namespaceNode.namespaceList);
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

        private List<UsingNode> optional_using_directive(ref List<UsingNode> usingList)
        {
            DebugInfoMethod("optional_using_directive");
            if (pass(TokenType.RW_USING))
                return using_directive(usingList);
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
            var identifier = new IdenfierNode(current_token.lexema);
            consumeToken();

            if (pass(TokenType.OP_DOT))
            {
                identifier_attribute();
            }

            if (!pass(TokenType.END_STATEMENT))
                throwError(";");

            consumeToken();
            usignList.Add(new UsingNode(identifier));
            return optional_using_directive(ref usignList);
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
