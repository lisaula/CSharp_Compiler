using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayAccessNode : PrimaryExpressionNode
    {
        private PrimaryExpressionNode primary;
        private List<List<ExpressionNode>> lista;

        public ArrayAccessNode()
        {

        }

        public ArrayAccessNode(PrimaryExpressionNode primary, List<List<ExpressionNode>> lista)
        {
            this.primary = primary;
            this.lista = lista;
        }
    }
}