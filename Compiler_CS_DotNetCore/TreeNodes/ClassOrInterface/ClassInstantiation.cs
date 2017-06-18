using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class ClassInstantiation : InstanceExpressionNode
    {
        public TypeDefinitionNode type;
        public List<ExpressionNode> arguments;
        List<TypeDefinitionNode> argumentsType;
        public ClassInstantiation(TypeDefinitionNode type, List<ExpressionNode> arguments)
        {
            this.type = type;
            this.arguments = arguments;
            argumentsType = new List<TypeDefinitionNode>();
        }
        public ClassInstantiation()
        {
            
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode tdn = api.searchType(type);
            argumentsType = api.getArgumentsType(arguments);
            if (api.findConstructor(tdn, Utils.getTypeName(argumentsType)))
            {
                this.returnType = tdn;
                return tdn;
            }
            throw new SemanticException("Error while evaluateType in ClassInstantiation.");
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            string name = returnType.ToString() +"(" +Utils.getTypeNameConcated(argumentsType)+")";
            string fullname = api.getFullNamespaceName(returnType);
            fullname += "." + returnType.ToString();
            builder.Append("new " + fullname+"(\""+name+"\"");
            if (arguments != null)
            {
                builder.Append(",");
                int argumentsLen = arguments.Count - 1;
                int count = 0;
                foreach (var expr in arguments)
                {
                    expr.generateCode(builder, api);
                    if(count< argumentsLen)
                        builder.Append(",");
                    count++;
                }
            }
            builder.Append(")");
        }
    }
}