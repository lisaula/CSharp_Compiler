using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject.ParsersTests
{
    [TestClass]
    public class ExpressionTest
    {
        [TestMethod]
        public void TestClassWithInlineFieldWithExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int prueba = (!(isVisible && iden) || noEsta);
    int LUNES = ((5 + 3) + (4 + 3));
    int MARTES = ((5 * 9 / 3) - 7 + (2 * 7 + 4) / ( (128 >> 5 * 5) - (1 << 7 * 46) / 3 )) + 15;
    int MIERCOLES = 0;
    int x = y = z = w = MARTES / 2;
    Kevin kevin = new Kevin();
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithLogicalOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES = (!(isVisible && isHere) || noEsta);
    bool boleano = isNull ?? true;
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithTernaryOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES = x==0? y=(5+3):z/2;
    bool boleano = y>5? k--: (k>4? y++:k++);
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithNullCoalescingOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES = x ?? ( y ?? (t??5) );
    bool boleano = j ?? (k>4? y++:k++);
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithBinaryOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  1 << 5;
    bool boleano = 5;
    int otro = 10; 
    float f = (1 | (0 & 5) & (100^100));
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithEqualityOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  (1 == 5);
    bool boleano = (x!=5);
    int otro = 10 != (otro==mama); 
    float f = (mama == (0!=5));
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithShiftOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  (1 << 5);
    bool boleano = (x>>5);
    int otro = 10 != (otro>>mama); 
    float f = (mama <= (0<<5));
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithAdittiveOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  (1 + 5);
    bool boleano = (x-5);
    int otro = 10 + (otro-mama); 
    float f = (mama + (0-5));
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithMultiplicativeOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  (1 * 5);
    bool boleano = (x/5);
    int otro = 10 * (otro%mama); 
    float f = (mama / (0*5));
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithUnaryOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES = (x += 5);
    bool boleano = (y >>= 5);
    int otro = ((x++));
    int nuevo = + ++y;
    float f = (~nada);
    int t = (int)nada;
    float mana = (float)(n.atributo);
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithAccesMemoryOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    float mana = (float)(n.atributo);
    public int funcion = persona.methodo(x=3).metodo;
    float f = persona.tryParse(x);
    Persona persona = this.atributo;
    Persona persona = this.method(x,y,r,nada);
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithArraysInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    float mana = new int[2][][][];
    private int arreglo =  new float[,,,,]; 
    int arreglo = new int[2]{ 5,7 };
    int arreglo = new int[2][]{ new int[5],new int[8]};
    int arreglo = { new int[5], new int[4], array };
    int value = new Persona(x,y,w).array[2];
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
    }
}
