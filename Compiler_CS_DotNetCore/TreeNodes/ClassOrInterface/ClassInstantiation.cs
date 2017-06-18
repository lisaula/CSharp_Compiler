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

        public ClassInstantiation(TypeDefinitionNode type, List<ExpressionNode> arguments)
        {
            this.type = type;
            this.arguments = arguments;
        }
        public ClassInstantiation()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode tdn = api.searchType(type);
            List<TypeDefinitionNode> argumentsType = api.getArgumentsType(arguments);
            if (api.findConstructor(tdn, Utils.getTypeName(argumentsType)))
            {
                this.returnType = tdn;
                return tdn;
            }
            throw new SemanticException("Error while evaluateType in ClassInstantiation.");
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            List<TypeDefinitionNode> argumentsType = api.getArgumentsType(arguments);
            string name = returnType.ToString() + Utils.getTypeNameConcated(argumentsType);
            string fullname = Utils.GlobalNamespace+"."+api.getParentNamespace(returnType);
            fullname += "." + returnType.ToString();
            builder.Append("new " + fullname+"(\""+name+"\",");
            foreach(var expr in arguments)
            {
                expr.generateCode(builder, api);
                builder.Append(",");
            }
            builder.Append(")");
        }
    }
}