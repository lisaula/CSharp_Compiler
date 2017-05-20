using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestLit_Char
    {
        [TestMethod]
        public void TestCharShouldBeLengthOfOne()
        {
            var inputString = new InputString(@"'a' 'f' 'h' '1' 'r' '@' '#'  ' '");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_CHAR);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestCharWithEscapeCharacter()
        {
            var inputString = new InputString(@"'\n' '\a' '\b' '\t' '\'' '\""'");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_CHAR);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        [ExpectedException(typeof(LexicalException))]
        public void TestCharWrongEscapeCharacter()
        {
            var inputString = new InputString(@"'\' '\a' '\b' '\t' '\'' '\""'");
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
        public void TestCharWithOneSingleQuote()
        {
            var inputString = new InputString(@"'a");
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
