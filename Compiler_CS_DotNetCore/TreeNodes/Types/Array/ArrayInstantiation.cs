using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class ArrayInstantiation : InstanceExpressionNode
    {
        public ArrayInitializer initialization;
        public TypeDefinitionNode type;
        public ArrayInstantiation()
        {

        }

        public ArrayInstantiation(TypeDefinitionNode type, ArrayInitializer initialization)
        {
            this.type = type;
            this.initialization = initialization;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            ArrayTypeNode atn = type as ArrayTypeNode;
            TypeDefinitionNode arrayType = api.searchType(atn.getArrayType());
            if (arrayType is VoidTypeNode || arrayType is InterfaceNode)
            {
                throw new SemanticException("Cant instantiate an array with type '" + arrayType.ToString() + "'", type.getPrimaryToken());
            }
            ((ArrayTypeNode)type).type = arrayType;
            if(initialization != null)
            {
                ArrayTypeNode t = (ArrayTypeNode)initialization.evaluateType(api);
                if (!t.type.Equals(arrayType) && !api.compareIndexes(atn.indexes, t.indexes))
                    throw new SemanticException("Array initialization invalid. '" + initialization.ToString() + "' and '" + type.ToString() + "'", type.getPrimaryToken());
            }
            this.returnType = type;
            return type;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            if(initialization == null)
            {
                builder.Append("[]");
            }
            else
            {
                initialization.generateCode(builder, api);
            }
        }
    }
}