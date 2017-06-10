using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayNode
    {
        public int arrayOfArrays;
        public int dimensions;
        public List<ExpressionNode> expression_list;

        public ArrayNode()
        { 
        }
    }
}