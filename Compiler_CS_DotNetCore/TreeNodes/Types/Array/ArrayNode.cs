using Compiler_CS_DotNetCore.Semantic;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayNode
    {
        public int dimensions;

        public List<ExpressionNode> expression_list;
        public ArrayNode(List<ExpressionNode> list) : this()
        {
            expression_list = list;
            dimensions = expression_list.Count;
        }
        public ArrayNode()
        {
            expression_list = new List<ExpressionNode>();
            dimensions = 1;
        }

        public void evaluate(API api)
        {
            foreach(var exp in expression_list)
            {
                TypeDefinitionNode t = exp.evaluateType(api);
                if (t.getComparativeType() != Utils.Int)
                    throw new SemanticException("Invalid expression. Expression inside index should return IntType, but actually return '" + t.ToString() + "'");
            }
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