using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class IsASExpression : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public TypeDefinitionNode type;
        public Token @operator;
        public List<string> rules;
        public IsASExpression(ExpressionNode leftExpression, TypeDefinitionNode type, Token op) : this()
        {
            this.leftExpression = leftExpression;
            this.type = type;
            @operator = op;
            if (@operator.type == TokenType.RW_AS)
            {
                rules = new List<string>();
                rules.Add(Utils.String + "," + Utils.String);
                rules.Add(Utils.Class + "," + Utils.Null);
            }
        }
        public IsASExpression()
        {
        }
        
        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode tdn = leftExpression.evaluateType(api);
            if (@operator.type == TokenType.RW_AS)
            {
                if (tdn.getComparativeType() == Utils.Dict || type is DictionaryTypeNode)
                    throw new SemanticException("Cannot implicitly conver Dictionary.", type.getPrimaryToken());

                if (rules.Contains(tdn.getComparativeType() + "," + type.ToString()))
                    return type;
                else
                {
                    TypeDefinitionNode targetType = null;
                    if(type is ArrayTypeNode)
                    {
                        targetType = api.searchType(((ArrayTypeNode)type).type);
                    }
                    if (type is NullTypeNode)
                        throw new SemanticException("Cannot implicitly convert null to '"+tdn.ToString()+"'.", type.getPrimaryToken());

                    if (targetType is PrimitiveType || tdn is PrimitiveType || type is PrimitiveType)
                        throw new SemanticException("Cannot use a primity type with this operation.", type.getPrimaryToken());

                    if (type is InterfaceNode)
                    {
                        throw new SemanticException("Cannot implicitly convert type '" + tdn.ToString() + "' to interface '" + targetType.ToString() + "'.", type.getPrimaryToken());
                    }else if(tdn.getComparativeType() == Utils.Interface)
                    {
                        throw new SemanticException("Cannot implicitly convert interface '" + tdn.ToString() + "' to object '" + targetType.ToString() + "'.", type.getPrimaryToken());
                    }
                    targetType = api.searchType(type);
                    if(api.checkRelationBetween(tdn, targetType))
                    {
                        return targetType;
                    }
                }
                throw new SemanticException("There is no relation between '"+tdn.ToString()+"' and '" +type.ToString() + "'.", type.getPrimaryToken());
            }
            else
            {
                if (tdn.getComparativeType() == Utils.Void)
                    throw new SemanticException("Invalid is Expression. Left expression shold not be 'void'.", @operator);
                TypeDefinitionNode t = null;
                if (type is ArrayTypeNode)
                {
                    t = api.searchType(((ArrayTypeNode)type).type);
                }
                if(!(type is NullTypeNode))
                    t = api.searchType(type);

                if (t.getComparativeType() == Utils.Interface)
                    throw new SemanticException("Cannot compare an object with interface '" + t.ToString() + "'", type.getPrimaryToken());
                return Singleton.tableTypes[Utils.GlobalNamespace+"."+Utils.Bool];
            }
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}