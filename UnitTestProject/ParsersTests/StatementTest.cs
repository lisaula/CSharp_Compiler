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
    }
}
