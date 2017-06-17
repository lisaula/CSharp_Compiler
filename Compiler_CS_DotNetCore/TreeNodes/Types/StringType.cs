using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class StringType : PrimitiveType
    {
        public StringType(Token token) : base(token)
        {
        }
        public StringType()
        {

        }
        public StringType(TypeDefinitionNode t):base(t)
        {
        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is StringType || obj is ClassDefinitionNode && ((ClassDefinitionNode)obj).identifier.token.lexema == Utils.String;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }

        public override string getComparativeType()
        {
            return GetType().Name;
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}