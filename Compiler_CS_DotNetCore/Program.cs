using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\compiiiss1.txt");
            var inputString = new InputString(txt);
            var lexer = new LexicalAnalyzer(inputString);
            /*
            Token token = dfa.getNextToken();
            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = dfa.getNextToken();
            }
            System.Console.Out.WriteLine(token);*/
            //var array = new int[3][][];
            var parser = new Parser(lexer);
            try
            {
                parser.parse();
            }catch(System.Exception e)
            {
                System.Console.Out.WriteLine(e.ToString());
            }
            System.Console.ReadKey();
        }
    }
}
