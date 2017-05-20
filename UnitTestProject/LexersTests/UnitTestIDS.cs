using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestIDs
    {
        [TestMethod]
        public void TestIDSWithNumbersAndUnderScore()
        {
            var inputString = new InputString("cont acd2 asd_das lojasd");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.ID);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestIDSUnderScores()
        {
            var inputString = new InputString("_cont_ ma_ma halo_ ");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.ID);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestIDCantHaveNumbersAtTheBeggining()
        {
            var inputString = new InputString("1cont");
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
        [ExpectedException(typeof(LexicalException))]
        public void TestIDCantHaveAtInTheMiddle()
        {
            var inputString = new InputString("cont@dor");
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
