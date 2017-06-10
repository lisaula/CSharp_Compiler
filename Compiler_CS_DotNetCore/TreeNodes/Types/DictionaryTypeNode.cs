using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class DictionaryTypeNode : TypeDefinitionNode
    {
        public TypeDefinitionNode t1;
        public TypeDefinitionNode t2;
        public DictionaryTypeNode(TypeDefinitionNode t1, TypeDefinitionNode t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }
        public DictionaryTypeNode()
        {

        }

        public override string ToString()
        {
            return "Dictionary<" + t1.ToString() + "," + t2.ToString() + ">";
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if(obj is DictionaryTypeNode)
            {
                DictionaryTypeNode d = obj as DictionaryTypeNode;
                return  (d.t1.Equals(t1) && d.t2.Equals(t2));
            }
            return false;
        }
    }
}