using Compiler.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler_CS_DotNetCore.Semantic
{
    public class Context
    {
        Dictionary<string, FieldNode> variables;
        Dictionary<string, MethodNode> methods;
        Dictionary<string, ConstructorNode> constructors;
        string name;
        public Context()
        {
            name = null;
            variables = new Dictionary<string, FieldNode>();
            methods = new Dictionary<string, MethodNode>();
            constructors = new Dictionary<string, ConstructorNode>();
        }

        public Context(string name,Dictionary<string, MethodNode> methods)
        {
            this.name = name;
            this.methods = methods;
        }
        public override string ToString()
        {
            return name;
        }

        public Context(string name,Dictionary<string, FieldNode> fields, Dictionary<string, MethodNode> methods, Dictionary<string, ConstructorNode> constructors)
        {
            this.name = name;
            this.variables = fields;
            this.methods = methods;
            this.constructors = constructors;
        }
    }
}
