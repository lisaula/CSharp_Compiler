using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayAccessNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public List<List<ExpressionNode>> lista;

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