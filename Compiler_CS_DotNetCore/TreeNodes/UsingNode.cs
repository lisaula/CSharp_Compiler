using System.Collections.Generic;

namespace Compiler.Tree
{
    public class UsingNode
    {
        private List<IdentifierNode> identifierList;

        public UsingNode(List<IdentifierNode> identifier)
        {
            this.identifierList = identifier;
        }
    }
}