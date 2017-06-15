using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

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

            if (rules.Contains(t1.ToString() + "," + t2.ToString()) || t1.Equals(t2))
            {
                return t1;
            }
            throw new SemanticException("Rule not supported. '"+t1.ToString()+"' "+assigmentOperator.ToString()+" '"+t2.ToString()+"'.", assigmentOperator);
        }
    }
}