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
