using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler_CS_DotNetCore.Semantic
{
    public class ContextManager
    {
        public string name;
        public List<Context> contexts;

        public ContextManager()
        {
            contexts = new List<Context>();
        }

        public ContextManager(string name):this()
        {
            this.name = name;
        }
    }
}
