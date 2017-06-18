using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

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
            primary.returnType = tdn;
            TypeDefinitionNode t1 = api.searchType(targetType);

            string rule = t1.ToString() + "," + tdn.ToString();
            string rule2 = t1.getComparativeType() + "," + tdn.ToString();
            string rule3 = t1.getComparativeType() + "," + tdn.getComparativeType();
            if (rules.Contains(rule)
                || rules.Contains(rule2)
                || rules.Contains(rule3)
                || t1.Equals(tdn))
            {
                this.returnType = t1;
                return t1;
            }
            if(t1 is ClassDefinitionNode)
            {
                if (tdn is ClassDefinitionNode)
                {
                    if (api.checkRelationBetween(tdn, t1))
                    {
                        t1.onTableType = false;
                        this.returnType = t1;
                        return t1;
                    }
                }else if((tdn is NullTypeNode))
                {
                    this.returnType = t1;
                    return t1;
                }
            }
            throw new SemanticException("There is no relation between '" + tdn.ToString() + "' and '" + t1.ToString() + "'.", targetType.getPrimaryToken());
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            List<string> primitive = new List<string>(Utils.primitives);
            primitive.Add(Utils.String);
            if(api.pass(this.returnType.getComparativeType(), primitive.ToArray()))
            {
                api.checkExpression(this.returnType.getComparativeType(), primary.returnType.getComparativeType(), builder);
                builder.Append("(");
                primary.generateCode(builder, api);
                builder.Append(")");
            }
            else
            {
                if(returnType is NullTypeNode)
                {
                    primary.generateCode(builder, api);
                }
                else
                {
                    string name = Utils.GlobalNamespace + "." + api.getParentNamespace(returnType);
                    name += "." + returnType.identifier.ToString();
                    builder.Append("Object.create( ");
                    builder.Append(name);
                    builder.Append(" , ");
                    primary.generateCode(builder, api);
                    builder.Append(")");
                }
            }
        }
    }
}