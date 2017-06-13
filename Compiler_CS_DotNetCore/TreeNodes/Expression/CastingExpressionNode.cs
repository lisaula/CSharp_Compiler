using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeDefinitionNode targetType;
        public List<ExpressionNode> primary;
        public List<string> rules;
        public CastingExpressionNode(TypeDefinitionNode targetType, List<ExpressionNode> primary)
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
        }
        public CastingExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {

            return targetType;
        }
    }
}