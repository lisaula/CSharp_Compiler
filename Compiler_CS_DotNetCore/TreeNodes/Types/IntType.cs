using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class IntType : PrimitiveType
    {
        public IntType(Token token) : base(token)
        {
        }
        public IntType(TypeDefinitionNode t ): base(t)
        {

        }
        public IntType()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is IntType || obj is ClassDefinitionNode && ((ClassDefinitionNode)obj).identifier.token.lexema == Utils.Int ;
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