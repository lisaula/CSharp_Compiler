using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayNode
    {
        public int dimensions;

        public List<ExpressionNode> expression_list;

        public ArrayNode()
        {
        }

        public override string ToString()
        {
            string name = "[";
            for(int i =0; i < dimensions-1; i++)
            {
                name += ",";
            }
            name += "]";
            return name;
        }
    }
}