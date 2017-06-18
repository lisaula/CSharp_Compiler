using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class EnumDefinitionNode : TypeDefinitionNode
    {
        public List<EnumNode> enumNodeList;
        public EncapsulationNode encapsulation;
        int initialization;

        public EnumDefinitionNode(EncapsulationNode encapsulation, IdentifierNode identifier): this()
        {
            this.encapsulation = encapsulation;
            this.identifier = identifier;
        }
        public EnumDefinitionNode()
        {
            initialization = -1;
            parent_namespace = null;
        }
        public override string ToString()
        {
            return identifier.token.lexema;
        }

        public override void Evaluate(API api)
        {
            if (this.evaluated)
                return;
            Debug.printMessage("Evaluatiog " +identifier.token.lexema);
            if (api.getEncapsulation(encapsulation) != TokenType.RW_PUBLIC && encapsulation.token.lexema !=null)
                throw new SemanticException("Enum " + identifier.token.lexema + " can't be accesible due to its encapsulation level.", identifier.token);
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
                    throw new SemanticException(n.identifier.token.lexema + " already exist in enum " + identifier.token.lexema + ". " ,n.identifier.token);
                names.Add(n.identifier.token.lexema);
                checkExpression(ref n);
            }
        }
        public override string getComparativeType()
        {
            return Utils.Enum;
        }
        private void checkExpression(ref EnumNode enum_)
        {
            if (enum_.expressionNode == null)
            {
                if (initialization + 1 >= 0)
                {
                    enum_.expressionNode = new InlineExpressionNode();
                    var token = new Token();
                    token.type = TokenType.LIT_INT;
                    int n = initialization + 1;
                    token.lexema = "" + n;
                    var l = new LiteralInt(token);
                    ((InlineExpressionNode)enum_.expressionNode).list.Add(l);
                }
                else
                {
                    var token = new Token();
                    token.type = TokenType.LIT_INT;
                    int n = initialization + 1;
                    n = Math.Abs(n);
                    token.lexema = "" + n;
                    var l = new LiteralInt(token);
                    var op = new Token();
                    op.type = TokenType.OP_SUBSTRACT;
                    op.lexema = "-";
                    var inline = new InlineExpressionNode();
                    inline.list.Add(l);
                    enum_.expressionNode = new PreExpressionNode(op,inline);
                }
                return;
            }

            if (enum_.expressionNode is InlineExpressionNode)
            {
                InlineExpressionNode ex = enum_.expressionNode as InlineExpressionNode;
                if (ex.list.Count == 1)
                {
                    if (!(ex.list[0] is LiteralInt))
                        throw new SemanticException("Not a constant expression.", enum_.identifier.token);
                    initialization = int.Parse(((LiteralInt)ex.list[0]).token.lexema);

                }else
                    throw new SemanticException("Not a constant expression." ,enum_.identifier.token);
            }
            else if(enum_.expressionNode is PreExpressionNode)
            {
                PreExpressionNode p = enum_.expressionNode as PreExpressionNode;
                if(p.Operator.type != TokenType.OP_SUBSTRACT)
                    throw new SemanticException("Not a constant expression. Can't use operator "+p.Operator.lexema+" " , enum_.identifier.token);
                if(!(p.expression is InlineExpressionNode))
                    throw new SemanticException("Not a constant expression." ,enum_.identifier.token);
                InlineExpressionNode ex = p.expression as InlineExpressionNode;
                if (ex.list.Count == 1)
                {
                    if (!(ex.list[0] is LiteralInt))
                        throw new SemanticException("Not a constant expression." , enum_.identifier.token);
                    initialization = - int.Parse(((LiteralInt)ex.list[0]).token.lexema);
                }
                else
                    throw new SemanticException("Not a constant expression." ,enum_.identifier.token);
            }
            else
                throw new SemanticException("Enum must have a constant expression. " ,enum_.identifier.token);
           
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            builder.Append("\nconst " + identifier.ToString() + " =  {");
            foreach(var enums in enumNodeList)
            {
                enums.generateCode(builder,api);
            }
            builder.Append("\n}");
        }
    }
}