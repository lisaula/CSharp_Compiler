using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestLit_Bool
    {
        [TestMethod]
        public void TestLit_Bool()
        {
            var inputString = new InputString(@"true false true true");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.LIT_BOOL);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestLit_BoolBadTyped()
        {
            var inputString = new InputString(@"treu");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            Assert.AreNotEqual(token.type, TokenType.LIT_BOOL);
        }
    }
}
