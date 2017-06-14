using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeDefinitionNode targetType;
        public ExpressionNode primary;
        public List<string> rules;
        public CastingExpressionNode(TypeDefinitionNode targetType, ExpressionNode primary)
        {
            this.targetType = targetType;
            this.primary = primary;
            rules = new List<string>();
            rules.Add(Utils.Int + "," + Utils.Int);
            rules.Add(Utils.Int + "," + Utils.Float);
            rules.Add(Utils.Float + "," + Utils.Int);
            rules.Add(Utils.Int + "," + Utils.Char);
            rules.Add(Utils.Char + "," + Utils.Int);

            rules.Add(Utils.Char + "," + Utils.Char);
            rules.Add(Utils.Char + "," + Utils.Float);

            rules.Add(Utils.Float + "," + Utils.Float);
            rules.Add(Utils.Float + "," + Utils.Char);

            rules.Add(Utils.String + "," + Utils.String);
            rules.Add(Utils.Class + "," + Utils.Null);

        }
        public CastingExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode tdn = primary.evaluateType(api);
            TypeDefinitionNode t1 = api.searchType(targetType);

            string key = tdn.ToString() + "," + t1.ToString();
            if (rules.Contains(key))
                return t1;
            if(t1 is ClassDefinitionNode)
            {
                if (tdn is ClassDefinitionNode)
                {
                    if (api.checkRelationBetween(tdn, t1))
                    {
                        return t1;
                    }
                }else if((tdn is NullTypeNode))
                {
                    return t1;
                }
            }
            throw new SemanticException("There is no relation between '" + tdn.ToString() + "' and '" + t1.ToString() + "'.", targetType.getPrimaryToken());
        }
    }
}