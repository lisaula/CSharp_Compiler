using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public abstract class TypeDefinitionNode
    {
        public bool evaluated = false;
        public NamespaceNode parent_namespace;
        public IdentifierNode identifier;
        public virtual void Evaluate(API api)
        {

        }

        public abstract Token getPrimaryToken();

        public virtual void verifiCycle(TypeDefinitionNode classDefinitionNode,API api) { }
    }
}