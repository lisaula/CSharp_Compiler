using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestSentence
    {
        [TestMethod]
        public void TestAssignOperator()
        {
            var inputString = new InputString("var cont = 1;");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.RW_VAR, TokenType.ID, TokenType.OP_ASSIGN , TokenType.LIT_INT, TokenType.END_STATEMENT};
            var cont = 0;
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, tokensTypes[cont++]);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestAssignOperatorWithComplexArithmeticExpression()
        {
            var inputString = new InputString("int cont = 1 + (( cont2 + 3) * 5);");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.RW_INT, TokenType.ID, TokenType.OP_ASSIGN,
                                        TokenType.LIT_INT, TokenType.OP_SUM, TokenType.OPEN_PARENTHESIS,
                                        TokenType.OPEN_PARENTHESIS, TokenType.ID, TokenType.OP_SUM,
                                        TokenType.LIT_INT, TokenType.CLOSE_PARENTHESIS, TokenType.OP_MULTIPLICATION,
                                        TokenType.LIT_INT, TokenType.CLOSE_PARENTHESIS, TokenType.END_STATEMENT
                                        };
            var cont = 0;
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, tokensTypes[cont++]);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestWhileSenteces()
        {
            var inputString = new InputString(@"while(true){
cont++;
}");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.RW_WHILE, TokenType.OPEN_PARENTHESIS, TokenType.LIT_BOOL,
                                        TokenType.CLOSE_PARENTHESIS, TokenType.OPEN_CURLY_BRACKET, TokenType.ID,
                                        TokenType.OP_INCREMENT, TokenType.END_STATEMENT, TokenType.CLOSE_CURLY_BRACKET
                                        };
            var cont = 0;
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, tokensTypes[cont++]);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestForAndForeachLoopSenteces()
        {
            var inputString = new InputString(@"for(int i = 0; i <= ext; i--){
foreach(string s in Ranger){}}");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.RW_FOR, TokenType.OPEN_PARENTHESIS, TokenType.RW_INT,
                                        TokenType.ID, TokenType.OP_ASSIGN, TokenType.LIT_INT,
                                        TokenType.END_STATEMENT, TokenType.ID, TokenType.OP_LESS_OR_EQUAL,
                                        TokenType.ID, TokenType.END_STATEMENT, TokenType.ID,
                                        TokenType.OP_DECREMENT, TokenType.CLOSE_PARENTHESIS, TokenType.OPEN_CURLY_BRACKET,
                                        TokenType.RW_FOREACH, TokenType.OPEN_PARENTHESIS, TokenType.RW_STRING,
                                        TokenType.ID, TokenType.RW_IN, TokenType.ID,
                                        TokenType.CLOSE_PARENTHESIS, TokenType.OPEN_CURLY_BRACKET, TokenType.CLOSE_CURLY_BRACKET,
                                        TokenType.CLOSE_CURLY_BRACKET
                                        };
            var cont = 0;
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, tokensTypes[cont++]);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        public void TestIfElseSentence()
        {
            var inputString = new InputString(@"if(jaime == javier && tommy != manu){
if(man >= 8){man<<=1|0xffff}}else");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.RW_IF, TokenType.OPEN_PARENTHESIS, TokenType.ID,
                                        TokenType.OP_EQUAL, TokenType.ID, TokenType.OP_LOG_AND,
                                        TokenType.ID, TokenType.OP_NOT_EQUAL, TokenType.ID,
                                        TokenType.CLOSE_PARENTHESIS, TokenType.OPEN_CURLY_BRACKET, TokenType.RW_IF,
                                        TokenType.OPEN_PARENTHESIS, TokenType.ID, TokenType.OP_GREATER_OR_EQUAL,
                                        TokenType.LIT_INT, TokenType.CLOSE_PARENTHESIS, TokenType.OPEN_CURLY_BRACKET,
                                        TokenType.ID, TokenType.OP_BIN_LS_ASSIGN, TokenType.LIT_INT,
                                        TokenType.OP_BIN_OR, TokenType.LIT_INT, TokenType.CLOSE_CURLY_BRACKET,
                                        TokenType.CLOSE_CURLY_BRACKET, TokenType.RW_ELSE
                                        };
            var cont = 0;
            while (token.type != TokenType.EOF)
            {
                Assert.AreEqual(token.type, tokensTypes[cont++]);
                token = lexer.getNextToken();
            }
            Assert.AreEqual(token.type, TokenType.EOF);
        }

        [TestMethod]
        [ExpectedException(typeof(LexicalException))]
        public void TestNonExistentOperators()
        {
            var inputString = new InputString(@"++ -- += == << >> & | ^ . , *=  $");
            var lexer = new LexicalAnalyzer(inputString);
            lexer.init();
            Token token = lexer.getNextToken();
            TokenType[] tokensTypes = { TokenType.OP_INCREMENT, TokenType.OP_DECREMENT, TokenType.OP_SUM_ONE_OPERND,
                                        TokenType.OP_EQUAL, TokenType.OP_BIN_LS, TokenType.OP_BIN_RS,
                                        TokenType.OP_BIN_AND, TokenType.OP_BIN_OR, TokenType.OP_BIN_XOR,
                                        TokenType.OP_DOT, TokenType.OP_COMMA,TokenType.OP_MULTIPLICATION_ASSIGN,TokenType.OP_SUM
                                        };
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
