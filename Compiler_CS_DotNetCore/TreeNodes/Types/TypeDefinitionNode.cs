using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

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
        internal bool localy = false;
        internal bool globally;

        public virtual void Evaluate(API api)
        {

        }
        public abstract string getComparativeType();
        public abstract Token getPrimaryToken();
        public virtual void verifiCycle(TypeDefinitionNode classDefinitionNode,API api) { }

        public abstract void generateCode(StringBuilder builder, API api);
    }
}