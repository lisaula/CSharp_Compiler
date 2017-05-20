using System.Collections.Generic;

namespace Compiler
{
    public class Transition
    {
        public List<string> conditions;
        private State from;
        public State to;
        public bool aggregateToLexema;
        public bool consumeEntrance;
        public Transition(State from, State to)
        {
            this.conditions = new List<string>();
            this.from = from;
            this.to = to;
        }
        public Transition()
        {
            this.conditions = new List<string>();
        }

        public bool isWhiteSpace()
        {
            foreach(string s in conditions)
            {
                if (s == "whitespace")
                    return true;
            }
            return false;
        }

        public void setSettings(bool aggregate)
        {
            aggregateToLexema = aggregate;
        }

        public void addStates(State from, State to)
        {
            this.from = from;
            this.to = to;
        }
        public void addCondition(string condition)
        {
            conditions.Add(condition);
        }
        public bool evaluate(Symbol s)
        {
            foreach(string condition in conditions)
            {
                if(condition == "letra")
                {
                    if (char.IsLetter(s.character))
                        return true;
                } else if(condition == "digito")
                {
                    if (char.IsDigit(s.character))
                        return true;
                } else if(condition == "whitespace"){
                    if (char.IsWhiteSpace(s.character) || s.character == '\r')
                        return true;
                }else if (condition == "cualquiera - {fin de linea}")
                {
                    if(s.character != '\0' && s.character != '\r' && s.character != '\n')
                       return true;
                }
                else if (condition == "cualquiera - {EOF,*}")
                {
                    if (s.character != '\0' && s.character != '*')
                        return true;
                }
                else if (condition == "cualquiera - {EOF,/,*}")
                {
                    if (s.character != '\0' && s.character != '/' && s.character != '*')
                        return true;
                }
                else if (condition == "cualquiera -{\\}")
                {
                    if(s.character != '\\' && s.character != '\0')
                    return true;
                }else if (condition == "cualquiera -{\"}")
                {
                    if (s.character != '\"')
                        return true;
                }else if(condition == "cualquiera - {\",\0}")
                {
                    if (s.character != '\"' && s.character != '\0')
                        return true;
                }
                if (condition == s.character.ToString())
                    return true;
                
            }
            return false;
        }
    }
}