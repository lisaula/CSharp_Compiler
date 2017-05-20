using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class DFA
    {
        public List<State> states;
        public List<Transition> transitions;
        public DFA()
        {
            states = new List<State>();
            transitions = new List<Transition>();
        }

        public void addState(string name, bool isInitial, bool isFinal)
        {
            states.Add(new RegularState(name, isInitial, isFinal));
        }

        public void addState(State s)
        {
            states.Add(s);
        }

        public void addTransition(string condition, string fromName, string toName, bool agregate)
        {
            List<State> from = states.Where(item => item.name == fromName).ToList();
            List<State> to = states.Where(item => item.name == toName).ToList();
            if (from.Count < 1)
                throw new StateNotFoundException(fromName);
            if (to.Count < 1)
                throw new StateNotFoundException(fromName);
            var transition = new Transition(from[0], to[0]);
            transition.addCondition(condition);
            transition.setSettings(agregate);
            transitions.Add(transition);
            from[0].addTransition(transition);
        }

        public void addTransition(Transition t, string fromName, string toName)
        {
            List<State> from = states.Where(item => item.name == fromName).ToList();
            List<State> to = states.Where(item => item.name == toName).ToList();
            if (from.Count < 1)
                throw new StateNotFoundException(fromName);
            if (to.Count < 1)
                throw new StateNotFoundException(fromName);
            t.addStates(from[0], to[0]);
            transitions.Add(t);
            from[0].addTransition(t);
        }

        
    }
}
