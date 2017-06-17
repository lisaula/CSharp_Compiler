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

        public FunctionCallExpression(PrimaryExpressionNode primary, List<ExpressionNode> arguments)
        {
            this.primary = primary;
            this.arguments = arguments;
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
            List<TypeDefinitionNode> argumentsType = api.getArgumentsType(arguments);
            string functionName = ((IdentifierNode)primary).ToString() +"("+Utils.getTypeName(argumentsType)+")";
            MethodNode m = api.contextManager.findFunction(functionName);
            if (m == null)
                throw new SemanticException("Function '" + functionName + "' could not be found in the current context.");
            return m.returnType;
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}