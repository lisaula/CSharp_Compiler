using System;
using System.Collections.Generic;
using Compiler_CS_DotNetCore.Semantic;
using System.Text;

namespace Compiler.Tree
{
    public class NamespaceNode
    {
        public List<IdentifierNode> identifierList;
        public List<UsingNode> usingList;
        public List<NamespaceNode> namespaceList;
        public List<TypeDefinitionNode> typeList;
        public NamespaceNode parent;

        public NamespaceNode(List<IdentifierNode> identifier) : this()
        {
            this.identifierList = identifier;
        }
        public NamespaceNode()
        {
            usingList = new List<UsingNode>();
            identifierList = new List<IdentifierNode>();
        }

        internal void generateCode(StringBuilder builder, API api)
        {
            Debug.printMessage("Generando " + identifierList[0].ToString());
            foreach (TypeDefinitionNode t in typeList)
            {
                if (t is EnumDefinitionNode || t is ClassDefinitionNode)
                {
                    if(t.ToString() == "Circulo")
                    Debug.printMessage("Generando " + t.ToString());
                    t.generateCode(builder, api);
                }
            }

            foreach (NamespaceNode t in namespaceList)
            {
                Debug.printMessage("Generando " + t.identifierList[0].ToString());
                t.generateCode(builder, api);
            }
        }

        public override string ToString()
        {
            List<string> name = new List<string>();
            foreach(IdentifierNode id in identifierList)
            {
                name.Add(id.token.lexema);
            }
            return string.Join(".",name);
        }

        internal void Evaluate(API api)
        {
            foreach (UsingNode us in usingList)
            {
                us.evaluate(api);
            }
            foreach (TypeDefinitionNode t in typeList)
            {
                t.Evaluate(api);
            }

            foreach (NamespaceNode nms in namespaceList)
            {
                nms.Evaluate(api);
            }
        }
    }
}