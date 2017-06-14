using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayNode
    {
        public int dimensions;

        public List<ExpressionNode> expression_list;
        public ArrayNode(List<ExpressionNode> list)
        {
            expression_list = list;
            dimensions = expression_list.Count;
        }
        public ArrayNode()
        {
            dimensions = 1;
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

        public override bool Equals(object obj)
        {
            if(obj is ArrayNode)
            {
                return ((ArrayNode)obj).dimensions == dimensions;
            }
            return false;
        }
    }
}