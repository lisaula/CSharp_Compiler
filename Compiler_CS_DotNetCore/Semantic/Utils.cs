using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
namespace Compiler_CS_DotNetCore.Semantic
{
    public class Utils
    {
        public static string getParametersName(List<Parameter> parameter)
        {
            List<string> name = new List<string>();
            foreach(Parameter p in parameter)
            {
                name.Add(p.type.ToString());
            }
            return string.Join(",",name);
        }

        public static string getMethodName(MethodNode method)
        {
            string name = method.id.token.lexema + "(" + getParametersName(method.parameters) + ")";
            return name;
        }
    }
}
