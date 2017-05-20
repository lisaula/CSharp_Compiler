using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestLitNumerica
    {
        [TestMethod]
        public void TestLit_IntDecimal()
        {
            var inputString = new InputString("11 325  125");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_INT);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestLit_IntHex()
        {
            var inputString = new InputString("0x0012af 0x129bde");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_INT);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestLit_IntBinary()
        {
            var inputString = new InputString("0b00110 0b1101");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_INT);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestLit_IntHexWithBadCharacters()
        {
            var inputString = new InputString("0x00fsa110");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            Assert.AreEqual(token.type, TokenType.LIT_INT);
            token = lexer.getNextToken();
            Assert.AreEqual(token.type, TokenType.ID);
            token = lexer.getNextToken();
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestLit_IntBinWithBadCharacters()
        {
            var inputString = new InputString("0bads");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            Assert.AreEqual(token.type, TokenType.LIT_INT);
            token = lexer.getNextToken();
            Assert.AreEqual(token.type, TokenType.ID);
            token = lexer.getNextToken();
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestLit_float()
        {
            var inputString = new InputString("0.01f 1.2f 2f 562f 10.22664f");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_FLOAT);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        [ExpectedException(typeof(LexicalException))]
        public void TestLit_floatWithNoNumberAfterPoint()
        {
            var inputString = new InputString("0.f 1.");
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
