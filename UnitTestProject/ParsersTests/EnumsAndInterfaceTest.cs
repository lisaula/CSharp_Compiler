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
public enum DIASDELASEMANA{
    LUNES=1,MARTES=2,MIERCOLES=3,JUEVES=4,VIERNES=5,
}
public enum DIASDELASEMANA{
    LUNES,MARTES,MIERCOLES,JUEVES,VIERNES,
};
public enum DIASDELASEMANA{
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
    }
}
