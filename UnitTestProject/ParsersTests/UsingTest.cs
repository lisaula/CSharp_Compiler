using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1.ParsersTests
{
    [TestClass]
    public class UsingTest
    {
        [TestMethod]
        public void TestUsingRegular()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestUsingExpressionWithoutEndStatement()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada
using jamones;");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestUsingExpressionWithoutIdentifier()
        {
            var inputString = new InputString(@"using System;
using
using jamones;");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
    }
}
