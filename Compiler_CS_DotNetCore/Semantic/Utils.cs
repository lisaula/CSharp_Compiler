using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Tree;
namespace Compiler_CS_DotNetCore.Semantic
{
    public class Utils
    {
        public const string Bool = "BoolType";
        public const string Char = "CharType";
        public const string Dict = "DictionaryTypeNode";
        public const string Float = "FloatType";
        public const string Int = "IntType";
        public const string String = "StringType";
        public const string Class = "ClassDefinitionNode";
        public const string Interface = "InterfaceNode";
        public const string Enum = "EnumDefinitionNode";
        public const string Null = "NullTypeNode";
        public static string Void = "VoidTypeNode";
        public static string[] primitives = {Bool, Char, Float,Int};
        internal static string makeConstructorName(TypeDefinitionNode tdn, List<ExpressionNode> arguments, API api)
        {
            string name = tdn.ToString() + "(" + Utils.getArgumentsNameType(arguments, api) + ")";
            return name;
        }

        internal static string getTypeName(List<TypeDefinitionNode> argumentsType)
        {
            List<string> name = new List<string>();
            foreach (TypeDefinitionNode en in argumentsType)
            {
                name.Add(en.ToString());
            }
            return string.Join(",", name);
        }

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

        public static string getMethodWithParentName(MethodNode method, TypeDefinitionNode parent)
        {
            string name = parent.ToString()+"."+method.id.token.lexema + "(" + getParametersName(method.parameters) + ")";
            return name;
        }

        public static string getConstructorName(ConstructorNode cons)
        {
            string name = cons.ToString() + "("+ getParametersName(cons.parameters) + ")";
            return name;
        }

        public static string getArgumentsNameType(List<ExpressionNode> arguments, API api)
        {
            List<string> name = new List<string>();
            if(arguments != null)
            {
                foreach(ExpressionNode en in arguments)
                {
                    name.Add(en.evaluateType(api).ToString());
                }
            }
            return string.Join(",", name);
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
 
public class VoidTypeNode{
}

public class BoolType{
    public override string ToString(){}
}
";

        public const string GlobalNamespace = "GlobalNamespace";

        public const string Array = "ArrayTypeNode";

        public static string Var = "VarType"; 
    }
}
