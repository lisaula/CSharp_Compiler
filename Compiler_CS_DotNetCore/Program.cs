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
            var s = @"public class kevin : Javier
{
    bool nada = javier?? h is kevin? h as thomas: h as carlos;
++--+=-===!=
}";
            var inputString = new InputString(s);
            var dfa = new LexicalAnalyzer(inputString);

            Token token = dfa.getNextToken();
            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = dfa.getNextToken();
            }
            System.Console.Out.WriteLine(token);
            //var array = new int[3][][];
            //var parser = new Parser(lexer);
            //parser.parse();
            System.Console.ReadKey();
        }
    }
}
