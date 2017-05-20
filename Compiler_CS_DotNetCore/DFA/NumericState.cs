using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class NumericState : State
    {
        public NumericState(string name, bool isInital, bool isFinal) : base(name, isInital, isFinal)
        {
        }

        public override Token makeToken(string lexema, int lexemaRow, int lexemaColumn)
        {
            int endLexema = lexema.Length - 1;
            if (char.ToUpperInvariant(lexema[endLexema]) =='F' && lexema[endLexema-1] != 'f')
            {
                return new Token(
                        TokenType.LIT_FLOAT,
                        lexema,
                        lexemaRow,
                        lexemaColumn
                        );
            }

            return new Token(
                        TokenType.LIT_INT,
                        lexema,
                        lexemaRow,
                        lexemaColumn
                        );
        }
    }
}
