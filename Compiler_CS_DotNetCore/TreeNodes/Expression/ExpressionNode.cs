using System;
using System.Text;
using Compiler_CS_DotNetCore.Semantic;
namespace Compiler.Tree
{
    public abstract class ExpressionNode : VariableInitializer
    {
        public bool notThis = true;
        public ExpressionNode()
        {

        }

        internal void SetNotThis()
        {
            notThis = false ;
        }
    }
}