using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public abstract class State
    {
        public string name;
        public bool isInitial, isFinal;
        public List<Transition> transitions;
        public bool resetLexema;

        public State(string name, bool isInital, bool isFinal)
        {
            this.name = name;
            this.isFinal = isFinal;
            this.isInitial = isInital;
            transitions = new List<Transition>();
            resetLexema = false;
        }

        public void ignoreLexema()
        {
            resetLexema = true;
        }
        public void addTransition(Transition t)
        {
            transitions.Add(t);
        }

        public abstract Token makeToken(string lexema, int lexemaRow, int lexemaColumn);
    }
}
