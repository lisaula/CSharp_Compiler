using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class FunctionCallExpression : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public List<ExpressionNode> arguments;
        public List<TypeDefinitionNode> argumentsType;

        public FunctionCallExpression(PrimaryExpressionNode primary, List<ExpressionNode> arguments)
        {
            this.primary = primary;
            this.arguments = arguments;
            argumentsType = new List<TypeDefinitionNode>();
        }
        public FunctionCallExpression()
        {

        }

        public FunctionCallExpression(List<ExpressionNode> arguments)
        {
            this.arguments = arguments;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            
            argumentsType = api.getArgumentsType(arguments);
            string functionName = ((IdentifierNode)primary).ToString() +"("+Utils.getTypeName(argumentsType)+")";
            MethodNode m = api.contextManager.findFunction(functionName);
            if (m == null)
                throw new SemanticException("Function '" + functionName + "' could not be found in the current context.");
            this.returnType = m.returnType;
            return m.returnType;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            primary.generateCode(builder, api);
            string argus = Utils.getTypeNameConcated(argumentsType);
            builder.Append(argus);
            builder.Append("(");
            if(arguments != null)
            {
                int len = arguments.Count - 1;
                int count = 0;
                foreach (var arg in arguments)
                {
                    arg.generateCode(builder, api);
                    if (count < len)
                        builder.Append(",");
                    count++;
                }
            }
            builder.Append(")");
        }
    }
}