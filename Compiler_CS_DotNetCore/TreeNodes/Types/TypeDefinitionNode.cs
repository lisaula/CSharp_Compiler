using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public abstract class TypeDefinitionNode
    {
        public bool evaluated = false;
        public virtual void Evaluate(API api)
        {

        }
    }
}