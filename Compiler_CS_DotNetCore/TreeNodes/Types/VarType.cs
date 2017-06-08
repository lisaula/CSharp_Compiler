using System;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler
{
    public class VarType : TypeDefinitionNode
    {
        public Token token;

        public VarType(Token token)
        {
            this.token = token;
        }
        public VarType()
        {

        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}