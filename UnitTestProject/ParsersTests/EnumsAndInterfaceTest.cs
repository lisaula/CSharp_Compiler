using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

namespace UnitTestProject.ParsersTests
{
    [TestClass]
    public class EnumsAndInterfaceTest
    {
        [TestMethod]
        public void TestEnumInsideANamespace()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
namespace A {
    public enum DIASDELASEMANA{
        LUNES,MARTES,MIERCOLES,JUEVES,VIERNES
    }
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestVariousEnumInsideANamespace()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
namespace A {
    namespace B{
        public enum DIASDELASEMANA{
            LUNES,MARTES,MIERCOLES,JUEVES,VIERNES
        }
        public enum DIASDELASEMANA{
            LUNES,MARTES,MIERCOLES,JUEVES,VIERNES
        };
        public enum DIASDELASEMANA{
            LUNES,MARTES=5,MIERCOLES=4,JUEVES,VIERNES
        }
    }
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestEnumOutsideANamespace()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public enum DIASDELASEMANA{
    LUNES,MARTES,MIERCOLES,JUEVES,VIERNES
}
namespace A {
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestEnumAfterUsings()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public enum DIASDELASEMANA{
    LUNES,MARTES,MIERCOLES,JUEVES,VIERNES
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestEnumExpressionWrongDeclaration()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public enum DIASDELASEMANA{
    LUNES,MARTES,MIERCOLES,JUEVES,VIERNES,,
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestVariousEnumExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
enum DIASDELASEMANA{
    LUNES=1,MARTES=2,MIERCOLES=3,JUEVES=4,VIERNES=5,
}
enum DIASDELASEMANA{
    LUNES,MARTES,MIERCOLES,JUEVES,VIERNES,
};
enum DIASDELASEMANA{
    LUNES,MARTES,MIERCOLES,JUEVES,VIERNES,
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestVariousAssignOperatorsInEnumExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public enum DIASDELASEMANA{
    LUNES=1,MARTES=2,MIERCOLES==3,JUEVES=4,VIERNES=5,
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestBadArgumentsListInInterfaceMethodExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public interface DIASDELASEMANA{
    void metodo(int argument, float argument argument);
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestVariousCommasInArgumentListInInterfaceMethodExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public interface DIASDELASEMANA{
    void metodo(int argument, float argument,,,int argument);
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestVariousInterfaceExpressions()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public interface DIASDELASEMANA{
    void metodo(int argument, float argument);
};
public interface DIASDELASEMANA{
    void metodo(int argument, float argument);
}
public interface DIASDELASEMANA{
    void metodo(int argument, float argument);
    int metodo(int argument, float argument);
    float metodo(int argument, float argument);
    string metodo();
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
        [TestMethod]
        public void TestVariousInterfaceExpressionsInsideANamespace()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public interface DIASDELASEMANA{
    void metodo(int argument, float argument);
};
namespace A{
    private interface DIASDELASEMANA{
        void metodo(int argument, float argument);
    }
    protected interface DIASDELASEMANA{
        void metodo(int argument, float argument);
        int metodo(int argument, float argument);
        float metodo(int argument, float argument);
        string metodo();
    }
}");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestInterfaceExpressionWithouteEncapsulationModifier()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
interface DIASDELASEMANA{
    void metodo(int argument, float argument);
};");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestInterfaceExpressionWithouteCloseCurlyBracket()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
interface DIASDELASEMANA{
    void metodo(int argument, float argument);
;");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestInterfaceExpressionInheritance()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
interface DIASDELASEMANA : Padre, Madre, Tia{
    void metodo(int argument, float argument);
};");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestInterfaceExpressionWrongInheritanceExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
interface DIASDELASEMANA : Padre,, Tia{
    void metodo(int argument, float argument);
};");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
    }
}
