using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public abstract class TypeDefinitionNode
    {
        public bool evaluated = false;
        public bool onTableType= false;
        public bool isStatic = false;
        public NamespaceNode parent_namespace;
        public IdentifierNode identifier;
        public TypeDefinitionNode typeNode;
        public virtual void Evaluate(API api)
        {

        }
        public abstract string getComparativeType();
        public abstract Token getPrimaryToken();
        public virtual void verifiCycle(TypeDefinitionNode classDefinitionNode,API api) { }
    }
}