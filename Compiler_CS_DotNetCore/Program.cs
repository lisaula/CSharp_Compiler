using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\Compiler_CS_DotNetCore\Parser\test.cs");
            var inputString = new InputString(@"public class kevin
                {
                    int LUNES = ((5 + 3) + (4 + 3));
                    int MARTES = ((5 * 9 / 3) - 7 + (2 * 7 + 4) / ( (128 >> 5 * 5) - (1 << 7 * 46) / 3 )) + 15;
                    int MIERCOLES = 0;
                    int x = y = z = w = MARTES / 2;
                }");
            var lexer = new LexicalAnalyzer(inputString);
            /*Token token = dfa.getNextToken();
            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = dfa.getNextToken();
            }
            System.Console.Out.WriteLine(token);*/
            var parser = new Parser(lexer);
            parser.parse();
            System.Console.ReadKey();
        }
    }
}
