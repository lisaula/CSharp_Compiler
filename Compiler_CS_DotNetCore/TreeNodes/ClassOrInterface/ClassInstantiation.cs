using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

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
            if (api.findConstructor(tdn, Utils.getArgumentsNameType(arguments, api)))
                return tdn;
            throw new SemanticException("Error while evaluateType in ClassInstantiation.");
        }
    }
}