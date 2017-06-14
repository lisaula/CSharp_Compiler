using System;
using System.Collections.Generic;
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
            api.contextManager = copy;
            foreach (ExpressionNode exp in list)
            {
                t = exp.evaluateType(api);
                List<Context> contexts = api.contextManager.buildEnvironment(t, ContextType.CLASS, api, t.onTableType);
                api.contextManager.contexts.Clear();
                api.pushContext(contexts.ToArray());
            }
            api.contextManager = ctx_mng;
            return t;
        }
    }
}