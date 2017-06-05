using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayInstantiation : InstanceExpressionNode
    {
        public  List<ExpressionNode> primaryExpressionBracket;
        public ArrayNode arrayNode;
        public ArrayInitializer initialization;
        public TypeDefinitionNode type;
        public ArrayInstantiation()
        {

        }

        public ArrayInstantiation(TypeDefinitionNode type,ArrayNode arrayNode, ArrayInitializer initialization)
        {
            this.type = type;
            this.arrayNode = arrayNode;
            this.initialization = initialization;
        }

        public ArrayInstantiation(TypeDefinitionNode type,List<ExpressionNode> primaryExpressionBracket, ArrayNode arrayNode, ArrayInitializer initialization)
        {
            this.type = type;
            this.primaryExpressionBracket = primaryExpressionBracket;
            this.arrayNode = arrayNode;
            this.initialization = initialization;
        }
    }
}