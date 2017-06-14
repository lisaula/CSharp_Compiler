﻿using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class LocalVariableDefinitionNode : Statement
    {
        public Dictionary<string,FieldNode> variable;

        public LocalVariableDefinitionNode()
        {
            this.variable = new Dictionary<string, FieldNode>();
        }

        public override void evaluate(API api)
        {
            evaluateFields(api);
            checkFieldsAssignment(api);
            api.addVariableToCurrentContext(variable);
        }
        private void checkFieldsAssignment(API api)
        {
            foreach (KeyValuePair<string, FieldNode> key in variable)
            {
                if (key.Value.assignment != null)
                {
                    FieldNode f = key.Value;
                    TypeDefinitionNode tdn = f.assignment.evaluateType(api);
                    if (!f.type.Equals(tdn))
                    {
                        if (f.type.getComparativeType() == Utils.Class && tdn.getComparativeType() == Utils.Class)
                        {
                            if (!api.checkRelationBetween(f.type, tdn))
                                throw new SemanticException("Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), key.Value.id.token);
                        }
                        else if ((!(f.type.getComparativeType() == Utils.Class || f.type.getComparativeType() == Utils.String) && tdn is NullTypeNode))
                        {
                            throw new SemanticException("Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), key.Value.id.token);
                        }
                        else
                            throw new SemanticException("Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.type.ToString(), key.Value.id.token);
                    }
                }
            }
        }
        private void evaluateFields(API api)
        {
            foreach (KeyValuePair<string, FieldNode> field in variable)
            {
                FieldNode f = field.Value;
                if (f.modifier != null)
                    if (!api.modifierPass(field.Value.modifier, TokenType.RW_STATIC))
                        throw new SemanticException("The modifier '" + field.Value.modifier.ToString() + "' is not valid for field " + field.Value.id.ToString() + " in class " + api.working_type.identifier.ToString() + ".", field.Value.modifier.token);

                if (f.type is VoidTypeNode)
                    throw new SemanticException("The type '" + f.type.GetType().Name + "' is not valid for field " + field.Value.id.ToString() + " in class " + api.working_type.identifier.ToString() + ".", f.type.getPrimaryToken());

                string name = f.type.ToString();
                if (f.type is ArrayTypeNode)
                {
                    name = ((ArrayTypeNode)f.type).getArrayType().ToString();
                }
                TypeDefinitionNode tdn = api.searchType(field.Value.type); 
                if (tdn == null)
                    throw new SemanticException("Could not find Type '" + name + "' in the current context. ", f.type.getPrimaryToken());
                if (tdn is InterfaceNode || tdn is VoidTypeNode)
                    throw new SemanticException("The type '" + tdn.ToString() + "' is not valid for field " + field.Value.id.ToString() + " in class " + api.working_type.identifier.ToString() + ".", f.type.getPrimaryToken());
                if (api.TokenPass(((ClassDefinitionNode)tdn).encapsulation.token, TokenType.RW_PRIVATE))
                    throw new SemanticException("The type '" + f.type.ToString() + "' can't be reached due to encapsulation level.", f.type.getPrimaryToken());
                f.type.typeNode = f.type;
                if (f.type is ArrayTypeNode)
                {
                    ((ArrayTypeNode)f.type).type = tdn;
                }
                else
                    f.type = tdn;
                //tdn.Evaluate(api);
            }
        }
    }
}