using Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject.LexersTests
{
    [TestClass]
    public class UnitTestOperators
    {
        [TestMethod]
        public void TestOperators()
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\LexersTests\FileTest\lexer_test.txt");
            var inputString = new InputString(txt);
            var dfa = new LexicalAnalyzer(inputString);
            Token token = dfa.getNextToken();
            while (token.type != TokenType.EOF)
            {
                token = dfa.getNextToken();
            }
        }
    }
}
