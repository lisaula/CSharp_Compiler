using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\Compiler_CS_DotNetCore\Parser\test.txt");
            var inputString = new InputString(@"public class Something
    {
        public void metodo(){
            hola = ""hola"";
            hola[2] = ""hola"";
            int[] list2 = new int[4] { 5, 6, 7, 8};
            int[] list3 = new int[4] { 1, 3, 2, 1 };
            int[] list4 = new int[4] { 5, 4, 3, 2 };

            int[][] lists = new int[][] {  list1 ,  list2 ,  list3 ,  list4  };
        }
    }");
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
