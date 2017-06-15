using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class BoolType : PrimitiveType
    {
        public BoolType(Token token) : base(token)
        {
        }
        public BoolType()
        {

        }

        public BoolType(TypeDefinitionNode t) : base(t)
        {
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is BoolType || obj is ClassDefinitionNode && ((ClassDefinitionNode)obj).identifier.token.lexema == Utils.Bool;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }

        public override string getComparativeType()
        {
            return GetType().Name;
        }
    }
}