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
        public bool isStatic = false, foundLocally = false, foundGlobally = false;
        public TypeDefinitionNode firstFound = null;
        private bool onTableType = false;
        private bool funcionStatic = false;
        private TypeDefinitionNode functionOwner;
        

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
            isStatic = copy.isStatic;
            copy.Enums_or_Literal = api.contextManager.Enums_or_Literal;
            api.contextManager = copy;
            int count = 0;
            foreach (ExpressionNode exp in list)
            {
                api.class_contextManager = ctx_mng;
                t = exp.evaluateType(api);
                exp.returnType = t;
                if (count == 0)
                {
                    foundLocally = t.localy;
                    onTableType = t.onTableType;
                    foundGlobally = t.globally;
                    funcionStatic = t.functionStatic;
                    functionOwner = t.functionOwner;
                }
                count++;
                List<Context> contexts = api.contextManager.buildEnvironment(t, ContextType.ATRIBUTE, api, t.onTableType);
                api.contextManager.isStatic = t.onTableType;
                api.contextManager.contexts.Clear();
                api.pushContext(contexts.ToArray());
            }
            api.contextManager = ctx_mng;
            api.class_contextManager = null;
            this.returnType = t;
            return t;
        }

        public override void generateCode(StringBuilder builder, API api) {
            int count = 0;
            foreach (var element in list)
            {
                if (count == 0)
                {
                    if (element is IdentifierNode)
                    {
                        if (foundGlobally && !onTableType)
                        {
                            ((IdentifierNode)element).setFirst();
                        }
                        else
                        {
                            if (isStatic || (element.returnType is EnumDefinitionNode) || onTableType)
                            {
                                string name = api.getFullNamespaceName(element.returnType);
                                builder.Append(name + ".");
                            }
                        }
                        /*if (foundLocally && !(returnType is EnumDefinitionNode))
                            ((IdentifierNode)element).setFirst();
                        else
                        {
                            if (isStatic && foundLocally)
                            {
                                ((IdentifierNode)element).setFirst();
                            }
                            else if(isStatic)
                            {

                                string name = api.getFullNamespaceName(returnType);
                                builder.Append(name + ".");
                            }
                        }*/
                    }else if(element is ArrayAccessNode)
                    {
                        if (foundGlobally)
                        {
                            ((IdentifierNode)((ArrayAccessNode)element).primary).setFirst();
                        }
                        else
                        {
                            if (isStatic || onTableType)
                            {
                                string name = api.getFullNamespaceName(returnType);
                                builder.Append(name + ".");
                            }
                        }
                        /*if (foundLocally && !(returnType is EnumDefinitionNode))
                            ((IdentifierNode)((ArrayAccessNode)element).primary).setFirst();
                        else
                        {
                            if (isStatic && foundLocally)
                            {
                                ((IdentifierNode)((ArrayAccessNode)element).primary).setFirst();
                            }
                            else if(isStatic)
                            {
                                string name = api.getFullNamespaceName(returnType);
                                builder.Append(name + ".");
                            }
                        }*/
                    }
                    else if (element is FunctionCallExpression)
                    {
                        if (foundGlobally && !isStatic && !funcionStatic)
                        {
                            ((IdentifierNode)((FunctionCallExpression)element).primary).setFirst();
                        }
                        else
                        {
                            if (isStatic || onTableType)
                            {
                                if (funcionStatic)
                                    Console.WriteLine();
                                string name = api.getFullNamespaceName(functionOwner);
                                builder.Append(name + ".");
                            }else if(funcionStatic && foundGlobally)
                            {
                                string name = api.getFullNamespaceName(functionOwner)+"."+functionOwner.ToString();
                                builder.Append(name + ".");
                            }
                        }
                    }
                }
                count++;
                element.generateCode(builder,api);
            }
        }
    }
}