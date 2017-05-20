using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public partial class Parser
    {
        private void expression()
        {
            DebugInfoMethod("expression");
            if (!pass(TokenType.LIT_INT))
                throwError("int");
            consumeToken();
        }
    }
}
