﻿using System;
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
        public static string This = "\nthis.";
        public static string EndLine = "\n";
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

        internal static string getTypeNameConcated(List<TypeDefinitionNode> argumentsType)
        {
            StringBuilder builder = new StringBuilder();
            foreach(TypeDefinitionNode t in argumentsType)
            {
                if(t is ArrayTypeNode)
                {
                    ((ArrayTypeNode)t).generated = true;
                }
                builder.Append(t.ToString());
            }
            return builder.ToString();
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

        internal static string getMethodWithParentName(string key, TypeDefinitionNode classDefinitionNode)
        {
            return classDefinitionNode.identifier.ToString() + "." + key;
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
    public Object(){}
}
 
public class IntType{
    public override string ToString(){}
    public static int Parse(string s){}
 
}
    
public class CharType{
    public override string ToString(){}
	public static int Parse(string s){}
}
 
public class DictionaryTypeNode{
    public override string ToString(){}
}
public class FloatType{
    public override string ToString(){}
	public static int Parse(string s){}
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

public static string includesDefaultJS = @"
GlobalNamespace.Object = class {
    Object() {}
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.IntType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return +(s);
    }
}
GlobalNamespace.CharType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return s;
    }
}
GlobalNamespace.FloatType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return +(s);
    }
}
GlobalNamespace.StringType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.BoolType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GlobalNamespace.System.Console = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {
        console.log(message);
     }
    static ReadLine() {

    }
}
GlobalNamespace.System.IO.TextWriter = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {
        console.log(message);
    }
}
GlobalNamespace.System.Console.Out = new GlobalNamespace.System.IO.TextWriter();
GlobalNamespace.System.IO.TextReader = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static ReadLine() {}
}";

        public const string GlobalNamespace = "GlobalNamespace";

        public const string Array = "ArrayTypeNode";

        public static string Var = "VarType"; 
    }
}
