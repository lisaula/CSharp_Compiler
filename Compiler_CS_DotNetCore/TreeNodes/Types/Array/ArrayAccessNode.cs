﻿using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Tree
{
    public class ArrayAccessNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public List<ArrayNode> lista;
        public ArrayAccessNode()
        {

        }

        public ArrayAccessNode(List<ArrayNode> lista)
        {
            this.lista = lista;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            TypeDefinitionNode t = primary.evaluateType(api);
            if (!(t is ArrayTypeNode))
                throw new SemanticException("Invalid operation. You are trying to access variable "+primary.ToString() + " as index, and is type '" + t.ToString() + "'");
            var origin = t as ArrayTypeNode;
            var array = new ArrayTypeNode();
            array.type = origin.type;
            array.type.localy = origin.localy;
            array.type.globally = origin.globally;
            array.type.onTableType = origin.onTableType;
            array.localy = origin.localy;
            array.globally = origin.globally;
            array.onTableType= origin.onTableType;
            if(lista != null)
            {
                if(lista.Count > origin.indexes.Count)
                {
                    throw new SemanticException("Invalid operation. Overloaded indexes in variable " + primary.ToString());
                }
                int count = 0;
                for(int i =0; i< lista.Count;i++)
                {
                    if (!lista[i].Equals(origin.indexes[i]))
                        throw new SemanticException("Invalid operation in access to variable " + primary.ToString() + ". Index integrity.");
                    lista[i].evaluate(api);
                    count++;
                }
                for (int i = count; i < origin.indexes.Count; i++)
                    array.indexes.Add(origin.indexes[i]);
            }
            if (array.indexes.Count == 0)
            {
                this.returnType = array.type;
                return array.type;
            }
            this.returnType = array;
            return array;
        }

        public override void generateCode(StringBuilder builder, API api)
        {
            primary.generateCode(builder, api);
            foreach(var array in lista)
            {
                array.generateCode(builder, api);
            }
        }
    }
}