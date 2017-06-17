using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class ArrayTypeNode : TypeDefinitionNode
    {
        public List<ArrayNode> indexes;
        public TypeDefinitionNode type;
        public ArrayTypeNode()
        {
            indexes = new List<ArrayNode>();
        }
        public TypeDefinitionNode getFistLevelIndex()
        {
            TypeDefinitionNode t = type;
            if (indexes.Count > 1)
            {
                var array = new ArrayTypeNode();
                array.type = type;
                for (int i = 1; i < indexes.Count; i++)
                {
                    array.indexes.Add(indexes[i]);
                }
                t = array;
            }
            return t;
        }
        public ArrayTypeNode(TypeDefinitionNode type, List<ArrayNode> indexes)
        {
            this.type = type;
            this.indexes = indexes;
        }

        public ArrayTypeNode(TypeDefinitionNode type): this()
        {
            this.type = type;
        }



        public override string ToString()
        {
            return type.ToString() + Utils.indexesToString(indexes);
        }

        public override bool Equals(object obj)
        {
            if(obj is ArrayTypeNode)
            {
                ArrayTypeNode atn = obj as ArrayTypeNode;
                if (type.Equals(atn.type) && indexes.Count == atn.indexes.Count)
                {
                    for(int i = 0; i < indexes.Count; i++)
                    {
                        if (indexes[i].dimensions != atn.indexes[i].dimensions)
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }
        public override void Evaluate(API api)
        {
            base.Evaluate(api);
        }

        public override Token getPrimaryToken()
        {
            return type.getPrimaryToken();
        }

        internal TypeDefinitionNode getArrayType()
        {
            return this.type;
        }

        public override string getComparativeType()
        {
            return Utils.Array;
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
