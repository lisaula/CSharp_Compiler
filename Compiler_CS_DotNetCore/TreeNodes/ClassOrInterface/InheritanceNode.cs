using System.Collections.Generic;

namespace Compiler.Tree
{
    public class InheritanceNode
    {
        public List<List<IdentifierNode>> identifierList;
        public InheritanceNode()
        {
            identifierList = new List<List<IdentifierNode>>();
        }

        public void addObjectInheritance()
        {
            List<IdentifierNode> list = new List<IdentifierNode>();
            Token t = new Token();
            t.lexema = "Object";
            list.Add(new IdentifierNode(t));
            identifierList.Add(list);
        }
    }
}