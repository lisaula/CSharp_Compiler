using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class VoidTypeNode : TypeDefinitionNode
    {
        public VoidTypeNode(Token token_type)
        {
            this.identifier =new IdentifierNode( token_type);
        }
        public VoidTypeNode()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is VoidTypeNode || obj is ClassDefinitionNode && ((ClassDefinitionNode)obj).identifier.token.lexema == Utils.Void;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }

        public override string getComparativeType()
        {
            return Utils.Void;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            throw new NotImplementedException();
        }
    }
}