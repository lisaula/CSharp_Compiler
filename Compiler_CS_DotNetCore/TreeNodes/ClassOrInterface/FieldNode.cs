using System;
using System.Text;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler.Tree
{
    public class FieldNode
    {
        public EncapsulationNode encapsulation;
        public ModifierNode modifier;
        public TypeDefinitionNode type;
        public IdentifierNode id;
        public VariableInitializer assignment;
        private bool isThis = false;

        public FieldNode(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type, IdentifierNode id, VariableInitializer assignment)
        {
            this.encapsulation = encapsulation;
            this.modifier = modifier;
            this.type = type;
            this.id = id;
            this.assignment = assignment;
        }
        public FieldNode()
        {

        }

        internal void generateCode(StringBuilder fieldsBuilder, API api)
        {
            if (isThis)
            {
                fieldsBuilder.Append(Utils.This);
            }
            fieldsBuilder.Append(id.ToString() + " = " + api.ValidateExpressionCode(assignment)+";");
        }

        internal void setIsThis()
        {
            isThis = true;
        }
    }
}