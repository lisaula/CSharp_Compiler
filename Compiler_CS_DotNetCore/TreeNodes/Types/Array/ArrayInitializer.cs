using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ArrayInitializer : VariableInitializer
    {
        public List<VariableInitializer> variables_list;

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode tdn = null;
            int arraysOfarrays = 0;
            VariableInitializer previous_expr = null;
            foreach (VariableInitializer vi in variables_list)
            {
                if (previous_expr != null)
                {
                    validateExpressions(previous_expr, vi);
                }
                previous_expr = vi;
                TypeDefinitionNode tdn2 = vi.evaluateType(api);
                if (tdn != null)
                {
                    if (!tdn.Equals(tdn2))
                    {
                        if ((tdn is NullTypeNode && tdn2 is PrimitiveType) || (tdn2 is NullTypeNode && tdn is PrimitiveType))
                            throw new SemanticException("Cannot use 'null' and primitive values in an array initialization.");
                        if (!(tdn is NullTypeNode && tdn2 is NullTypeNode))
                            throw new SemanticException("Values in array initialization are not equal. '" + tdn.ToString() + "' and '" + tdn2.ToString() + "'");
                    }

                }
                tdn = tdn2;
                if ((vi is InlineExpressionNode))
                {
                    if (((InlineExpressionNode)vi).getFirstExpressionType() is ArrayInstantiation)
                        arraysOfarrays++;
                }
            }
            if(tdn is ArrayTypeNode)
            {
                if (arraysOfarrays > 0)
                {
                    for (int i = 0; i < arraysOfarrays-1; i++)
                    {
                        ((ArrayTypeNode)tdn).indexes.Add(new ArrayNode());
                    }
                }
                else
                    ((ArrayTypeNode)tdn).indexes[0].dimensions++;
            }
            else
            {
                var a = new ArrayTypeNode(tdn);
                var i = new ArrayNode();
                a.indexes.Add(i);
                return a;
            }
            return tdn;
        }

        private void validateExpressions(VariableInitializer previous_expr, VariableInitializer vi)
        {
            if (previous_expr is ArrayInitializer && vi is ArrayInitializer)
                return;
            if ((previous_expr is InlineExpressionNode && vi is ArrayInitializer) || (vi is InlineExpressionNode && previous_expr is ArrayInitializer))
                throw new SemanticException("Invalid array initialization. Cannot interpolate expressions. InlineExpression and ArrayInitializer.");
            ExpressionNode ex1 = ((InlineExpressionNode)previous_expr).getFirstExpressionType();
            ExpressionNode ex2 = ((InlineExpressionNode)vi).getFirstExpressionType();
            if((ex1 is ArrayInstantiation && !(ex2 is ArrayInstantiation)) || (ex2 is ArrayInstantiation && !(ex1 is ArrayInstantiation)))
                throw new SemanticException("Invalid array initialization. Cannot interpolate expressions with ArrayInstantiation.");
        }
    }
}