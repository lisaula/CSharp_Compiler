using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestLit_String
    {
        [TestMethod]
        public void TestLitStringRegularEntrance()
        {
            var inputString = new InputString(@""" hola como estan 3653 asdjks """);
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_STRING);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestLitStringWithEscapeCharacter()
        {
            var inputString = new InputString(@"""\b hola como estan \n 3653 \tasdjks \r\t """);
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_STRING);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        [ExpectedException(typeof(LexicalException))]
        public void TestLitStringWrongEscapeCharacter()
        {
            var inputString = new InputString(@"""hola como estan \ 3653 \tasdjks \r\t """);
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            { 
                token = lexer.getNextToken();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LexicalException))]
        public void TestLitStringOneDoubleQuote()
        {
            var inputString = new InputString(@"""hola como estan ");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                token = lexer.getNextToken();
            }
        }

        [TestMethod]
        public void TestLitVerbatinRegularEntrance()
        {
            var inputString = new InputString(@"@""hola \n \t \p \\c \' \"""" asdf como estan """);
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_VERBATIN);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        [ExpectedException(typeof(LexicalException))]
        public void TestLitVerbatinBadDoubleQuotesEntrance()
        {
            var inputString = new InputString(@"@"" asdad "" askdas""");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();

            while (token.type != TokenType.EOF)
            {
                token = lexer.getNextToken();
            }
        }
    }
}
