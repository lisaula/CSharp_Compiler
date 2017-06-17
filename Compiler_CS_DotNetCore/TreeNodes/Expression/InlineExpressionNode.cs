using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;
using Compiler_CS_DotNetCore.Semantic.Context;

namespace Compiler.Tree
{
    public class InlineExpressionNode : UnaryExpressionNode
    {
        public List<ExpressionNode> list;

        public InlineExpressionNode(List<ExpressionNode> list)
        {
            this.list = list;
        }

        public InlineExpressionNode()
        {
            list = new List<ExpressionNode>();
        }

        public ExpressionNode getFirstExpressionType()
        {
            if(list != null)
            {
                return list[0];
            }
            return null;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode t = null;
            ContextManager ctx_mng = api.contextManager;
            Context[] copy_context = new Context[ctx_mng.contexts.Count];
            ctx_mng.contexts.CopyTo(copy_context);
            var copy = new ContextManager();
            copy.contexts = new List<Context>(copy_context);
            copy.isStatic = api.contextManager.isStatic;
            copy.Enums_or_Literal = api.contextManager.Enums_or_Literal;
            api.contextManager = copy;
            foreach (ExpressionNode exp in list)
            {
                api.class_contextManager = ctx_mng;
                t = exp.evaluateType(api);
                List<Context> contexts = api.contextManager.buildEnvironment(t, ContextType.ATRIBUTE, api, t.onTableType);
                api.contextManager.isStatic = t.onTableType;
                api.contextManager.contexts.Clear();
                api.pushContext(contexts.ToArray());
            }
            api.contextManager = ctx_mng;
            api.class_contextManager = null;
            return t;
        }

        public override string generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}