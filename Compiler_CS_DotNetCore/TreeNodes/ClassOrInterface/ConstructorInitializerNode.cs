using Compiler_CS_DotNetCore.Semantic;
using System.Collections.Generic;
using System;
using System.Text;

namespace Compiler.Tree
{
    public class ConstructorInitializerNode
    {
        public List<ExpressionNode> argumentList;
        public Token reference;
        public TypeDefinitionNode I = null;
        public List<TypeDefinitionNode> argumentsType;
        public ConstructorInitializerNode()
        {

        }

        public ConstructorInitializerNode(Token reference, List<ExpressionNode> argumentList)
        {
            this.reference = reference;
            this.argumentList = argumentList;
        }

        public void evaluate(API api)
        {
            TypeDefinitionNode t = api.working_type;
            if (reference.type == TokenType.RW_BASE)
            {
                t = api.contextManager.getParent(api.working_type);
            }
            I = t;
            argumentsType = api.getArgumentsType(argumentList);
            string ctr = t.identifier.ToString() + "(" +Utils.getTypeName(argumentsType) + ")";
            ClassDefinitionNode c = t as ClassDefinitionNode;
            if (!c.constructors.ContainsKey(ctr))
            {
                throw new SemanticException("Reference constructor '" + ctr + "' does not exist.", reference);
            }
            else
            {
                if(reference.type == TokenType.RW_BASE)
                {
                    if (api.TokenPass(c.constructors[ctr].encapsulation.token, TokenType.RW_PRIVATE))
                        throw new SemanticException("Reference constructor '"+ctr+"' cannot be reach due to its encapsulation level.", reference);
                }
            }
        }

        internal void generateCode(StringBuilder builder, API api)
        {
            builder.Append(Utils.EndLine);
            if (api.TokenPass(reference, TokenType.RW_BASE)) {
                builder.Append("super.");
            }else
            {
                builder.Append("this.");
            }
            string ctr = I.ToString() + Utils.getTypeNameConcated(argumentsType);
            builder.Append(ctr+"(");
            if(argumentList != null)
            {
                int count = 0;
                int len = argumentList.Count - 1;
                foreach(var argu in argumentList)
                {
                    argu.SetNotThis();
                    argu.generateCode(builder, api);
                    if (count < len)
                        builder.Append(",");
                    count++;
                }
            }
            builder.Append(");");
        }
    }
}