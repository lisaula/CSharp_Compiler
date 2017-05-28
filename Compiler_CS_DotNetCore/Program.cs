using Compiler.Tree;
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
                        enum Adentro{
                            LUNES,
                            MARTES = 5,
                            MIERCOLES
                        }
                    }
                    public interface NuevoInterface: Padre.Clase, Clase2 {
                        int[,][] methodo(int paramenter);
                        Dictionary<int,string>[,,] methodo2 (string[] arrayString);
                    }
                }
enum NUevoEnum{
    JAIME
}

public abstract class NuevaClase : Padre, Lista.Padre {
}

private class ClasePrivada {
    public int x = 3, y=5, k=4;

    public override int metodo(int paramenterA, string[] paremeterB){}

    public Constructor(int a ) : base(a){}
    private Constructor2 (int a ){}
}
                ";
            var inputString = new InputString(s);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            CompilationNode tree;
            try
            {
                tree = parser.parse();
                SerializeTree(tree);
            }
            catch(System.Exception e)
            {
                System.Console.Out.WriteLine(e);
            }
            System.Console.ReadKey();
        }

        private static void SerializeTree(CompilationNode tree)
        {
            System.Type[] types = { typeof(UsingNode), typeof(NamespaceNode), typeof(EnumDefinitionNode)
            , typeof(EnumNode), typeof(InterfaceNode), typeof(ClassDefinitionNode), typeof(FieldNode)
            , typeof(MethodNode), typeof(ConstructorNode),typeof(ConstructorInitializerNode), typeof(IdentifierNode), typeof(Token)
            , typeof(ExpressionNode), typeof(Parameter), typeof(ModifierNode), typeof(PrimitiveType), typeof(DictionaryTypeNode)};
            var serializer = new XmlSerializer(typeof(CompilationNode),types);
            var logPath = System.IO.Path.GetTempFileName();
            var logFile = System.IO.File.Create("hola.xml");
            var writer = new System.IO.StreamWriter(logFile);
            serializer.Serialize(writer, tree);
        }
    }
}
