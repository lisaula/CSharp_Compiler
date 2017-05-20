using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class RegularState : State
    {
        public RegularState(string name, bool isInital, bool isFinal) : base(name, isInital, isFinal)
        {
        }
        public override Token makeToken(string lexema, int lexemaRow, int lexemaColumn)
        {
            throw new NotImplementedException();
        }
    }
}
