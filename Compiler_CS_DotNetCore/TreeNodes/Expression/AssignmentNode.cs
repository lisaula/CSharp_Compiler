using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class AssignmentNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public Token assigmentOperator;
        public ExpressionNode rightExpression;
        public List<string> rules;
        public AssignmentNode(ExpressionNode leftExpression, Token assigmentOperator, ExpressionNode rightExpression)
        {
            this.leftExpression = leftExpression;
            this.assigmentOperator = assigmentOperator;
            this.rightExpression = rightExpression;
            rules = new List<string>();

            setRules();
        }
        private void setRules()
        {
            switch (assigmentOperator.type)
            {
                case TokenType.OP_ASSIGN:
                    rules.Add(Utils.Bool + "," + Utils.Bool);
                    rules.Add(Utils.String + "," + Utils.String);
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    rules.Add(Utils.String + "," + Utils.Null);
                    rules.Add(Utils.Class + "," + Utils.Null);
                    rules.Add(Utils.Enum + "," + Utils.Enum);
                    break;
                case TokenType.OP_SUM_ONE_OPERND:
                    rules.Add(Utils.String + "," + Utils.Int);
                    rules.Add(Utils.String + "," + Utils.Float);
                    rules.Add(Utils.String + "," + Utils.Char);
                    rules.Add(Utils.String + "," + Utils.String);
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_SUBSTRACT_ONE_OPERND:
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_MULTIPLICATION_ASSIGN:
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_DIVISION_ASSIGN:
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_MOD_ASSIGN:
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_BIN_LS_ASSIGN:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Int);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_BIN_RS_ASSIGN:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Int);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_AND_ASSIGN:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_OR_ASSIGN:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_XOR_ASSIGN:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
            }
        }

        public AssignmentNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode t1 = leftExpression.evaluateType(api);
            TypeDefinitionNode t2 = rightExpression.evaluateType(api);
            string rule = t1.ToString() + "," + t2.ToString();
            string rule2 = t1.getComparativeType() + "," + t2.ToString();
            string rule3 = t1.getComparativeType() + "," + t2.getComparativeType();
            if (!rule.Contains(rule)
                        && !rule.Contains(rule2)
                        && !rule.Contains(rule3)
                        && !t1.Equals(t2))
            {
                if (t1.getComparativeType() == Utils.Class && t2.getComparativeType() == Utils.Class)
                {
                    if (!api.checkRelationBetween(t1, t2))
                        throw new SemanticException("Not a valid assignment. Trying to assign " + t2.ToString() + " to field with type " + t1.ToString());
                }
                else if ((!(t1.getComparativeType() == Utils.Class || t1.getComparativeType() == Utils.String) && t2 is NullTypeNode))
                {
                    throw new SemanticException("Not a valid assignment. Trying to assign " + t2.ToString() + " to field with type " + t1.ToString());
                }
                else if (t1.getComparativeType() == Utils.Var)
                {
                    t1 = t2;
                }
                else if (t1.getComparativeType() == Utils.Array && t2.getComparativeType() == Utils.Array)
                {
                    var token = new Token();
                    token.row = -1;
                    token.column = -1;
                    api.checkArrays(t1, t2, token);
                }
                else
                    throw new SemanticException("Not a valid assignment. Trying to assign " + t2.ToString() + " to field with type " + t1.ToString());
            }
            t1.onTableType = false;
            return t1;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            leftExpression.generateCode(builder, api);

            builder.Append(" " + assigmentOperator.lexema + " ");

            rightExpression.generateCode(builder, api);
        }
    }
}