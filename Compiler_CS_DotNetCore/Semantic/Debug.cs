using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler_CS_DotNetCore.Semantic
{
    public static class Debug
    {
#if DEBUG
        public static bool print = false;
#endif
        [System.Diagnostics.Conditional("DEBUG")]
        public static void printMessage(string message)
        {
#if DEBUG
            if (print)
            {
                Console.WriteLine(message);
            }
        }
#endif
    }
}
