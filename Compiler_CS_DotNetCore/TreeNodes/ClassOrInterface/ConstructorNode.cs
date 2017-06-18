using Compiler_CS_DotNetCore.Semantic;
using System.Collections.Generic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class ConstructorNode
    {
        public EncapsulationNode encapsulation;
        public IdentifierNode id;
        public List<Parameter> parameters;
        public ConstructorInitializerNode base_init;
        public BodyStatement bodyStatements;

        public bool headerEvaluation { get; set; }
        public ConstructorNode(EncapsulationNode encapsulation, IdentifierNode id, List<Parameter> parameters, ConstructorInitializerNode base_init, BodyStatement bodyStatements):this()
        {
            this.encapsulation = encapsulation;
            this.id = id;
            this.parameters = parameters;
            this.base_init = base_init;
            this.bodyStatements = bodyStatements;
        }
        public ConstructorNode()
        {
            headerEvaluation = false;
        }
        public override string ToString()
        {
            return id.token.lexema;
        }

        internal void generateCode(StringBuilder builder, API api)
        {
            List<TypeDefinitionNode> parameterList = new List<TypeDefinitionNode>();
            if (parameters != null) {
                foreach (var par in parameters)
                {
                    parameterList.Add(par.type);
                }
            }
            string name = id.ToString() + Utils.getTypeNameConcated(parameterList);
            builder.Append(Utils.EndLine +name + "(");
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
            if (base_init != null)
            {
                base_init.generateCode(builder, api);
            }
            if(bodyStatements!= null)
            {
                //bodyStatements.generateCode(builder, api);
            }
            builder.Append(Utils.EndLine + "}");
        }
    }
}