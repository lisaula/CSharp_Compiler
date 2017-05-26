﻿using Compiler.Tree;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\compiiiss1.txt");
            var s = @"
                using Luis.Carlos.Isaula;
                using Javier;
                namespace Hola.Mama.Como.Estas{
                    using Adentro.Nuevo;
                    namespace B{
                        using adentro2;
                        public enum Adentro{
                            LUNES,
                            MARTES = 5,
                            MIERCOLES
                        }
                    }
                    public interface NuevoInterface {
                        void methodo(int paramenter);
                    }
                }
enum NUevoEnum{
    JAIME
}
                ";
            var inputString = new InputString(s);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            CompilationNode tree;
            try
            {
                tree = parser.parse();
            }
            catch(System.Exception e)
            {
                System.Console.Out.WriteLine(e);
            }
            System.Console.ReadKey();
        }
    }
}
