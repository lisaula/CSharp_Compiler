using System;
using System.Collections.Generic;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class MethodNode
    {
        public TypeDefinitionNode returnType;
        public IdentifierNode id;
        public List<Parameter> parameters;
        public EncapsulationNode encapsulation;
        public ModifierNode modifier;
        public BodyStatement bodyStatements;
        public TypeDefinitionNode owner;
        public MethodNode(TypeDefinitionNode returnType, IdentifierNode id, List<Parameter> parameters)
        {
            this.returnType = returnType;
            this.id = id;
            this.parameters = parameters;
        }
        public MethodNode()
        {

        }

        public MethodNode(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type, IdentifierNode id, List<Parameter> parameters, BodyStatement bodyStatements)
        {
            this.encapsulation = encapsulation;
            this.modifier = modifier;
            this.returnType = type;
            this.id = id;
            this.parameters = parameters;
            this.bodyStatements = bodyStatements;
        }

        internal void generateCode(StringBuilder builder, API api)
        {
            builder.Append(Utils.EndLine);
            if (api.modifierPass(modifier, TokenType.RW_STATIC))
                builder.Append(modifier.token.lexema+" ");

            List<TypeDefinitionNode> parameterList = new List<TypeDefinitionNode>();
            if (parameters != null)
            {
                foreach (var par in parameters)
                {
                    parameterList.Add(par.type);
                }
            }
            string name = id.ToString() + Utils.getTypeNameConcated(parameterList);
            builder.Append(name + "(");

            if (parameters != null)
            {
                int len = parameters.Count - 1;
                int count = 0;
                foreach (var p in parameters)
                {
                    p.generateCode(builder, api);
                    if (count < len)
                        builder.Append(",");
                    count++;
                }
            }
            builder.Append(") {");
            if (bodyStatements != null)
            {
                bodyStatements.generateCode(builder, api);
            }
            builder.Append(Utils.EndLine + "}");
        }
    }
}