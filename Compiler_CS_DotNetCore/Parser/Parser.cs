using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public partial class Parser
    {
        private LexicalAnalyzer lexer;
        private Token current_token;
        private List<Token> look_ahead;
        public Parser(LexicalAnalyzer lexer)
        {
            this.lexer = lexer;
            current_token = lexer.getNextToken();
            look_ahead = new List<Token>();
            doDebugOnlyCode = false ;
        }
        
    }
}
