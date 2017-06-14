using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayAccessNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public List<ArrayNode> lista;
        public ArrayAccessNode()
        {

        }

        public ArrayAccessNode(List<ArrayNode> lista)
        {
            this.lista = lista;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return null;
        }
    }
}