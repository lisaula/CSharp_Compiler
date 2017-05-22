using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\test_statements2.txt");
            var s = @"
public class kevin : Nexer{
    public void metodo(){
        for(i.system.hola nombre =0; i < 10; i++, x++){
            System.Console.Writeline(""hola"");
            System.Console.Writeline = 5;
        }
    }
}
";
            var inputString = new InputString(s);
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
