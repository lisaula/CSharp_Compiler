using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1.ParsersTests
{
    [TestClass]
    public class UsingAndNamespaceTest
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

        [TestMethod]
        public void TestUsingNamespaceExpression()
        {
            var inputString = new InputString(@"using System;
using Otro;
using jamones;

namespace A{
    using prueba;
    namespace B
    {
        using prueba2.prueba3.prueba4;
    }
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestUsingWithNamespaceEmpty()
        {
            var inputString = new InputString(@"using System;
using Otro;
using jamones;

namespace A{
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestUsingAfterANamespaceExpression()
        {
            var inputString = new InputString(@"using System;
using Otro;
using jamones;

namespace A{
    using prueba;
    namespace B
    {
        using prueba2.prueba3.prueba4;
    }
    using system;
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
    }
}
