using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class CoalescingExpressionNode : ExpressionNode
    {
        public ExpressionNode conditionalExpression;
        public ExpressionNode nullCoalescing;

        public CoalescingExpressionNode(ExpressionNode conditionalExpression, ExpressionNode nullCoalescing)
        {
            this.conditionalExpression = conditionalExpression;
            this.nullCoalescing = nullCoalescing;
        }
        public CoalescingExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode tcondition = conditionalExpression.evaluateType(api);
            conditionalExpression.returnType = tcondition;
            TypeDefinitionNode tnullCoalescing = nullCoalescing.evaluateType(api);
            nullCoalescing.returnType = tnullCoalescing;
            string t1 = tcondition.getComparativeType();
            string t2 = tnullCoalescing.getComparativeType();
            if (api.pass(t1, Utils.primitives) || api.pass(t2, Utils.primitives))
                throw new SemanticException("Invalid coalescing operation. Cant utilize a primitive type in null coalescing expression.");
            string rule = t1 + "," + t2;
            if (!tcondition.Equals(tnullCoalescing) && !api.assignmentRules.Contains(rule))
                throw new SemanticException("Cannot make null coalescing of type '" + tcondition.ToString() + "' and '" + tnullCoalescing.ToString() + "'.", tcondition.getPrimaryToken());
            this.returnType = tcondition;
            return tcondition;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append("(");
            conditionalExpression.generateCode(builder, api);
            builder.Append(")");
            builder.Append("||");
            builder.Append("(");
            nullCoalescing.generateCode(builder, api);
            builder.Append(")");
        }
    }
}