using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class IdentifierNode : PrimaryExpressionNode
    {
        public Token token;
        public bool first = false;
        public IdentifierNode(Token token) : this()
        {
            this.token = token;
        }

        public void setFirst()
        {
            first = true;
        }
        public IdentifierNode()
        {
        }

        public override string ToString()
        {
            return token.lexema;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode t = null;
            t = api.contextManager.findVariable(token);
            if (t == null)
            {
                t = api.searchInTableType(token.lexema);
                if(t!=null)
                    t.onTableType = true;
            }
            if(t == null)
                throw new SemanticException("Variable '" + token.lexema + "' could not be found in the current context.", token);
            return t;
        }

        public override bool Equals(object obj)
        {
            if(obj is IdentifierNode)
            {
                var t = obj as IdentifierNode;
                return t.token.Equals(token);
            }
            return false;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            if (first)
                builder.Append("this.");
            builder.Append(token.lexema);
        }
    }
}