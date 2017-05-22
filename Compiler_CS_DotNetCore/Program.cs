using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\test_statements.txt");
            var inputString = new InputString(@"
public class kevin : Nexer{
    public method(int a){
    }

    public method(int a) : base(a){
        this.local = a;
        x[5] = 10;
        ++x;
        x++;
        (Persona)new Persona();
        5;
        (5+A).CompareTo(10);
        this.prototype.jamon(a,a).value;
        this.prototype.jamon(a,a).value[5];
        Dictionary<int,float> hasmap = new Dictionary<int,float>();
        var nuevo = new Dictionary<int,float>();
    }
}
");
            var lexer = new LexicalAnalyzer(inputString);
            /*Token token = dfa.getNextToken();
            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = dfa.getNextToken();
            }
            System.Console.Out.WriteLine(token);*/
            var array = new int[3][][];
            var parser = new Parser(lexer);
            parser.parse();
            System.Console.ReadKey();
        }
    }
}
