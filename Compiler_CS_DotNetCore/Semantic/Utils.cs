﻿using System;
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

        internal static string indexesToString(List<ArrayNode> indexes)
        {
            string name = "";
            foreach (ArrayNode an in indexes)
            {
                name += an.ToString();
            }
            return name;
        }

        public static string getMethodName(MethodNode method)
        {
            string name = method.id.token.lexema + "(" + getParametersName(method.parameters) + ")";
            return name;
        }

        public static string getConstructorName(ConstructorNode cons)
        {
            string name = cons.ToString() + "("+ getParametersName(cons.parameters) + ")";
            return name;
        }

        private static string getArgumentsNameType(List<Parameter> parameters)
        {
            throw new NotImplementedException();
        }

        public static string txtIncludes = @"
namespace System {
    namespace IO{
        public class TextWriter{
            public static void WriteLine(string message){}
        }
 
        public class TextReader{
            public static string ReadLine(){}
        }
    }
    public class Console{
        public static System.IO.TextWriter Out;
        public static System.IO.TextReader In;
        public static void WriteLine(string message){}
        public static string ReadLine(){}
    }
}
public class Object{
    public virtual string ToString(){}
}
 
public class IntType{
    public override string ToString(){}
    public static int Parse(string s){}
	public static int TryParse(string s, int out){}
 
}
    
public class CharType{
    public override string ToString(){}
	public static int Parse(string s){}
	public static int TryParse(string s, char out){}
}
 
public class DictionaryTypeNode{
    public override string ToString(){}
}
public class FloatType{
    public override string ToString(){}
	public static int Parse(string s){}
	public static int TryParse(string s, float out){}
}
public class StringType{
    public override string ToString(){}
}
public class VarType{
    public override string ToString(){}
}
 
public class VoidType{
}

public class BoolType{
    public override string ToString(){}
}
";

        public const string GlobalNamespace = "GlobalNamespace";
    }
}
