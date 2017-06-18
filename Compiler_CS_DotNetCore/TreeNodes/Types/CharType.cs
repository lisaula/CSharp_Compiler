using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class CharType : PrimitiveType
    {
        public CharType(Token token) : base(token)
        {
        }
        public CharType()
        {

        }
        public CharType(TypeDefinitionNode t) : base(t)
        {
                
        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is CharType || obj is ClassDefinitionNode && ((ClassDefinitionNode)obj).identifier.token.lexema == Utils.Char;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }

        public override string getComparativeType()
        {
            return GetType().Name;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            throw new NotImplementedException();
        }
    }
}