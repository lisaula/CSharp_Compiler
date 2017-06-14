using System;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;
namespace Compiler
{
    public class VarType : TypeDefinitionNode
    {
        public VarType(Token token)
        {
            this.identifier = new IdentifierNode(token);
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

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }

        public override string getComparativeType()
        {
            throw new NotImplementedException();
        }
    }
}