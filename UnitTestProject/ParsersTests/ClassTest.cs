using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;

/*
     * que pueda heredar
     * varias clases un en mismo namespace y afuera del namespace
     * fields, inline fields, multiple assig fields
     * methods, methods
     * constructores que llaman a su padre
     * multiple herencia
     * diferentes encapsulamientos 
*/
namespace UnitTestProject.ParsersTests
{
    [TestClass]
    public class ClassTest
    {
        [TestMethod]
        public void TestClassWithInheritance()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer{
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestVariosClassInsideAndOutsideNamespace()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer{
}
public class kevin : Nexer{
}
public class kevin : Nexer{
}
public class kevin : Nexer{
};
namespace A{
    public class kevin {
    }
    public class B {
    }

    public class Nexer{
    }
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithInlineFields()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer{
    public int x, y, z, c = 3;
    public static float y1, z1, r;
    public float x1 = y1, z= z1,r = r2,f = 4;
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithMethods()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin{
    public int x, y, z, c = 3;
    protected void main(int argument){}
    private bool isReal(float argument, bool exits){}
    kevin(string nombre){}
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithParentConstructorCall()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer{
    public int x, y, z, c = 3;
    protected void main(int argument){}
    private bool isReal(float argument, bool exits){}
    kevin(string nombre) : base(nombre){}
    kevin(string nombre, string appellido) : base(nombre,appellido){}
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassMultipleInheritance()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer, Javier, Aqui{
 
}
class kevin : Nexe, Dos, Tres, Cuatro{
 
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithDifferentsEncapsulationModifiers()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer, Javier, Aqui{
    public kevin();
}
private class kevin : Nexe, Dos, Tres, Cuatro{
    private kevin();
}

protected class kevin : Nexe, Dos, Tres, Cuatro{
    kevin(){}
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestClassWithWrongInheritanceExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer, Javier,{
    public kevin();
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestClassWithWrongConstructorExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer, Javier{
    public ();
}
");
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
    }
}
