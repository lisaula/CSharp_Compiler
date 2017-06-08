using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class EnumDefinitionNode : TypeDefinitionNode
    {
        public List<EnumNode> enumNodeList;
        public EncapsulationNode encapsulation;
        public IdentifierNode identifier;

        public EnumDefinitionNode(EncapsulationNode encapsulation, IdentifierNode identifier)
        {
            this.encapsulation = encapsulation;
            this.identifier = identifier;
        }
        public EnumDefinitionNode()
        {

        }
        public override string ToString()
        {
            return identifier.token.lexema;
        }

        public override void Evaluate(API api)
        {
            if (this.evaluated)
                return;
            Console.WriteLine("Evaluatiog " +identifier.token.lexema);
            if (api.getEncapsulation(encapsulation) != TokenType.RW_PUBLIC && encapsulation.token.lexema !=null)
                throw new SemanticException("Enum " + identifier.token.lexema + " can't be accesible due to its encapsulation level." + identifier.token.ToString());
            enumNodes();
            this.evaluated = true;
        }

        private void enumNodes()
        {
            List<string> names = new List<string>();
            for (int i = 0; i < enumNodeList.Count; i++)
            {
                EnumNode n = enumNodeList[i];
                if(names.Contains(n.identifier.token.lexema))
                    throw new SemanticException(n.identifier.token.lexema + " already exist in enum " + identifier.token.lexema + ". " + n.identifier.token.ToString());
                names.Add(n.identifier.token.lexema);
                checkExpression(n);
            }
        }

        private void checkExpression(EnumNode enum_)
        {
            if (enum_.expressionNode == null)
                return;
            if (enum_.expressionNode is InlineExpressionNode)
            {
                InlineExpressionNode ex = enum_.expressionNode as InlineExpressionNode;
                if (ex.list.Count == 1)
                {
                    if (!(ex.list[0] is LiteralInt))
                        throw new SemanticException("Not a constant expression." + enum_.identifier.token.ToString());
                }else
                    throw new SemanticException("Not a constant expression." + enum_.identifier.token.ToString());
            }
            else if(enum_.expressionNode is PreExpressionNode)
            {
                PreExpressionNode p = enum_.expressionNode as PreExpressionNode;
                if(p.Operator.type != TokenType.OP_SUBSTRACT)
                    throw new SemanticException("Not a constant expression. Can't use operator "+p.Operator.lexema+" " + enum_.identifier.token.ToString());
                if(!(p.expression is InlineExpressionNode))
                    throw new SemanticException("Not a constant expression." + enum_.identifier.token.ToString());
                InlineExpressionNode ex = p.expression as InlineExpressionNode;
                if (ex.list.Count == 1)
                {
                    if (!(ex.list[0] is LiteralInt))
                        throw new SemanticException("Not a constant expression." + enum_.identifier.token.ToString());
                }
                else
                    throw new SemanticException("Not a constant expression." + enum_.identifier.token.ToString());
            }
            else
                throw new SemanticException("Enum must have a constant expression. " + enum_.identifier.token.ToString());
           
        }
    }
}