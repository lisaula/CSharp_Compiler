using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject.ParsersTests
{
    [TestClass]
    public class StatementTest
    {
        [TestMethod]
        public void TestClassWithLocalStamentsInBody()
        {
            var inputString = new InputString(@"
public class kevin : Nexer{
    public void method(int a){
        var hola = 5;
        int uno, dos, tres = 5;

    }
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithIfsNestedInBody()
        {
            var inputString = new InputString(@"
public class kevin {
    public void method(int a){
        if(a==true)
            if(b>10)
                if(j != a)
                    for(int a = 10; a >10; a--){}
        switch(hola){
            case 5:
            break;
            case 10:
                if(a>10){
                    System.Console.out.Writeline(""hola"");
                }
            break;
            default:
                continue;
        }
        
        Dictionary<int,float> hashmap = new Dictionary<int,float>();
        if(hashmap is Dictionary<int,float>){
            var nuevo = (Dictionary<int,float>)hashmap;
        }
    }
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithMultipleLocalStamentsInBody()
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\local_variable_test_file.txt");
            var inputString = new InputString(txt);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithStamentsInBody()
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\test_statements.txt");
            var inputString = new InputString(txt);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
        
        
        [TestMethod]
        public void TestParserCompiiiss()
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\compiiiss1.txt");
            var inputString = new InputString(txt);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithStamentsInBody2()
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\test_statements2.txt");
            var inputString = new InputString(txt);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithMaybeEmptyBlockInBody()
        {
            var txt = @"
public class kevin : Nexer{
    public method(int a){
    }

    public method(int a) : base(a){
        int[] a = new int[5];
        {
            if(a==10){
                return a;
                {
                    for(;;){}
                }
            }
        }
    }
}
";
            var inputString = new InputString(txt);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithStatementExpressionInBody()
        {
            var txt = @"
public class kevin : Nexer{
    public method(int a){
    }

    public method(int a) : base(a){
        this.local = a;
        x[5] = 10;
        ++x;
        x++;
        (Persona)new Persona();
        5;
        (5+A).CompareTo(10);
        this.prototype.jamon(a,a).value;
        this.prototype.jamon(a,a).value[5];
        Dictionary<int,float> hasmap = new Dictionary<int,float>();
        var nuevo = new Dictionary<int,float>();
    }
}
";
            var inputString = new InputString(txt);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
    }
}
