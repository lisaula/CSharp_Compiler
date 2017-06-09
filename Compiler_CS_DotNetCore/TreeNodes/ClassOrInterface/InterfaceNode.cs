﻿using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class InterfaceNode : TypeDefinitionNode
    {
        public InheritanceNode inheritance;
        public Dictionary<string, MethodNode> methods;
        public EncapsulationNode encapsulation;
        public Dictionary<string, TypeDefinitionNode> parents;

        public InterfaceNode()
        {
            parent_namespace = null;
            methods = new Dictionary<string, MethodNode>();
        }

        public InterfaceNode(Token token):this()
        {
            this.identifier = new IdentifierNode(token);
        }

        public InterfaceNode(EncapsulationNode encapsulation, Token token):this(token)
        {
            this.encapsulation = encapsulation;
        }

        public override void Evaluate(API api)
        {
            if (evaluated)
                return;
            Debug.printMessage("Evaluating " + identifier.token.lexema);
            if(encapsulation.token.type != TokenType.RW_PUBLIC)
            {
                throw new SemanticException("Interface "+identifier.token.lexema+"naccessible due to its encapsulation level. ",identifier.token);
            }
            checkInheritanceExistance(api);
            evaluated = true;
        }

        private void checkInheritanceExistance(API api)
        {
            parents = new Dictionary<string, TypeDefinitionNode>();
            if (inheritance == null || inheritance.identifierList == null)
                return;
            foreach (List<IdentifierNode> parent in inheritance.identifierList)
            {
                string name = api.getIdentifierListAsString(".",parent);
                NamespaceNode nms = api.getParentNamespace(this);
                TypeDefinitionNode tdn =  api.findTypeInList(nms.typeList, name);
                if(tdn == null)
                {
                    tdn = api.findTypeInUsings(nms.usingList, name);
                }
                if (parents.ContainsKey(name))
                    throw new SemanticException("Redundant Inheritance. " + name + " was found twice as inheritance in " + identifier.token.lexema, identifier.token);
                parents[name] = tdn;
            }
        }

        public override string ToString()
        {
            return identifier.token.lexema;
        }
    }
}