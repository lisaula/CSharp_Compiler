using Compiler_CS_DotNetCore.Semantic;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ConstructorInitializerNode
    {
        public List<ExpressionNode> argumentList;
        public Token reference;
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
            string ctr = t.identifier.ToString() + "(" +Utils.getArgumentsNameType(argumentList, api) + ")";
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
    }
}