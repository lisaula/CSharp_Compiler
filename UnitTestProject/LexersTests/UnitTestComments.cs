using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestComments
    {
        [TestMethod]
        public void TestMethodComentarioLinea()
        {
            var inputString = new InputString("//lhlalksdjjflaskfdj54655112323");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestMethodComentarioLineaAndCondigo()
        {
            var inputString = new InputString(@"//lh
int x;      //variable x
int j= 10;");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.RW_INT, TokenType.ID, TokenType.END_STATEMENT,
            TokenType.RW_INT, TokenType.ID, TokenType.OP_ASSIGN, TokenType.LIT_INT,
            TokenType.END_STATEMENT};
            var cont = 0;
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, tokensTypes[cont++]);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestMethodCodeAndComment()
        {
            var inputString = new InputString(@"
int x; // hola 
//holasd int j= 10;
bool javi = false;
//// lklajsjd //");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.RW_INT, TokenType.ID, TokenType.END_STATEMENT,
            TokenType.RW_BOOL, TokenType.ID, TokenType.OP_ASSIGN, TokenType.LIT_BOOL,
            TokenType.END_STATEMENT};
            var cont = 0;
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, tokensTypes[cont++]);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestMethodBlockComment()
        {
            var inputString = new InputString(@"/*halaksjjhflak*/");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestMethodBlockCommentWithAsterisks()
        {
            var inputString = new InputString(@"/*halaksjj*hf***lak**//");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, TokenType.OP_DIVISION);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestMethodBlockCommentWithCode()
        {
            var inputString = new InputString(@"/*halaksjj*hf***lak**/
int x = a + c /b /*comentario*/
bool aqui;
/*termino*/");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.RW_INT, TokenType.ID, TokenType.OP_ASSIGN,
            TokenType.ID, TokenType.OP_SUM, TokenType.ID, TokenType.OP_DIVISION,
            TokenType.ID ,TokenType.RW_BOOL ,TokenType.ID,TokenType.END_STATEMENT};
            var cont = 0;
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, tokensTypes[cont++]);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }
    }
}
