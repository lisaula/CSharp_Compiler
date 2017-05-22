using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class LexicalAnalyzer
    {
        private DFA dfa;
        private State currentState;
        private IInput inputString;
        private Symbol currentSymbol;

        public LexicalAnalyzer(IInput input)
        {
            inputString = input;
            this.currentSymbol = inputString.GetNextSymbol();
            dfa = new DFA();
            init();
        }

        public void init()
        {
            //dfa.addState("q0", true, false);
            var q0 = new RegularState("q0", true, false);
            q0.ignoreLexema();
            dfa.addState(q0);
            //ID
            dfa.addState(new IDState("ID",false,true));
            var tID = new Transition();
            tID.setSettings(true);
            tID.addCondition("letra");
            tID.addCondition("_");
            dfa.addTransition(tID, "q0", "ID");

            var t = new Transition();
            t.setSettings(true);
            t.addCondition("letra");
            t.addCondition("digito");
            t.addCondition("_");
            dfa.addTransition(t, "ID", "ID");

            //literal binaria
            dfa.addState("0", false, true);
            dfa.addTransition("0", "q0", "0",true);

            //literal hex
            dfa.addState("x", false, true);
            dfa.addTransition("x", "0", "x", true);
            var tx = new Transition();
            tx.setSettings(true);
            tx.addCondition("digito");
            tx.addCondition("a"); tx.addCondition("A");
            tx.addCondition("b"); tx.addCondition("B");
            tx.addCondition("c"); tx.addCondition("C");
            tx.addCondition("d"); tx.addCondition("D");
            tx.addCondition("e"); tx.addCondition("E");
            tx.addCondition("f"); tx.addCondition("F");
            dfa.addTransition(tx, "x", "x");

            //literal bin
            dfa.addState("b", false, true);
            dfa.addTransition("b", "0", "b", true);
            var tb = new Transition();
            tb.setSettings(true);
            tb.addCondition("0");
            tb.addCondition("1");
            dfa.addTransition(tb, "b", "b");



            //literal numerico
            dfa.addState(new NumericState("LIT_NUMERIC", false, true));
            dfa.addState(".", false, false);
            dfa.addTransition("digito", "0", "LIT_NUMERIC", true);
            dfa.addTransition("digito", "q0", "LIT_NUMERIC", true);
            var tnumber = new Transition();
            tnumber.setSettings(true);
            tnumber.addCondition("digito");
            tnumber.addCondition("digito");
            tnumber.addCondition("f");
            tnumber.addCondition("F");
            dfa.addTransition(tnumber, "LIT_NUMERIC", "LIT_NUMERIC");
            dfa.addTransition(".", "LIT_NUMERIC", ".",true);
            dfa.addTransition(".", "0", ".", true);
            dfa.addTransition("digito", ".", "LIT_NUMERIC", true);

            //whitespace
            dfa.addState("whitespace", false, false);
            dfa.addTransition("whitespace", "q0", "q0", false);

            //operadores de un simbolo
            dfa.addState(new OneSymbolOperator("OneSymbolOperator", false, true));
            var top= new Transition();
            top.setSettings(true);
            top.addCondition("~");
            top.addCondition(";");
            top.addCondition(":");
            top.addCondition("(");
            top.addCondition(")");
            top.addCondition("{");
            top.addCondition("}");
            top.addCondition("[");
            top.addCondition("]");
            top.addCondition(".");
            top.addCondition(",");
            dfa.addTransition(top, "q0", "OneSymbolOperator");

            //comentarios

            //hacer estados /, y *
            dfa.addState("div-op", false, true);


            var coment = new RegularState("comentario_linea", false, false);
            coment.ignoreLexema();
            dfa.addState(coment);
            dfa.addTransition("/", "div-op", "comentario_linea",false);
            dfa.addTransition("cualquiera - {fin de linea}", "comentario_linea", "comentario_linea", false);
            var tcl = new Transition();
            tcl.setSettings(false);
            tcl.addCondition("\r");
            tcl.addCondition("\n");
            dfa.addTransition(tcl, "comentario_linea", "q0");

            coment = new RegularState("comentario_bloque", false, false);
            coment.ignoreLexema();
            dfa.addState(coment);
            dfa.addTransition("*", "div-op", "comentario_bloque", false);
            dfa.addTransition("cualquiera - {EOF,*}", "comentario_bloque", "comentario_bloque", false);
            dfa.addState("*", false, false);
            dfa.addTransition("*", "comentario_bloque", "*", false);
            dfa.addTransition("/", "*", "q0", false);
            dfa.addTransition("cualquiera - {EOF,/,*}", "*", "comentario_bloque", false);
            dfa.addTransition("*", "*", "*", false);

            //operadores de dos symbolos
            dfa.addState(new TwoSymbolOperator("ASSIGN", false, true));
            //+,+=,++
            dfa.addState("sum-op", false, true);
            dfa.addState("increment", false, true);

            dfa.addTransition("+", "q0", "sum-op",true);
            dfa.addTransition("=", "sum-op", "ASSIGN", true);
            dfa.addTransition("+", "sum-op", "increment", true);
            //-,-=,--
            dfa.addState("sus-op", false, true);
            dfa.addState("decrement", false, true);

            dfa.addTransition("-", "q0", "sus-op", true);
            dfa.addTransition("=", "sus-op", "ASSIGN", true);
            dfa.addTransition("-", "sus-op", "decrement", true);
            // /,/=

            dfa.addTransition("/", "q0", "div-op", true);
            dfa.addTransition("=", "div-op", "ASSIGN", true);
            
            // *, *=
            dfa.addState("mul-op", false, true);

            dfa.addTransition("*", "q0", "mul-op", true);
            dfa.addTransition("=", "mul-op", "ASSIGN", true);

            // !, !=
            dfa.addState("not-op", false, true);

            dfa.addTransition("!", "q0", "not-op", true);
            dfa.addTransition("=", "not-op", "ASSIGN", true);

            // &, &&, &=
            dfa.addState("and-op", false, true);
            dfa.addState("and-bin", false, true);

            dfa.addTransition("&", "q0", "and-bin", true);
            dfa.addTransition("&", "and-bin", "and-op", true);
            dfa.addTransition("=", "and-bin", "ASSIGN", true);

            // |, |=, ||
            dfa.addState("or-op", false, true);
            dfa.addState("or-bin", false, true);

            dfa.addTransition("|", "q0", "or-bin", true);
            dfa.addTransition("|", "or-bin", "or-op", true);
            dfa.addTransition("=", "or-bin", "ASSIGN", true);

            // %, %=
            dfa.addState("mod-op", false, true);

            dfa.addTransition("%", "q0", "mod-op", true);
            dfa.addTransition("=", "mod-op", "ASSIGN", true);

            // ^, ^=
            dfa.addState("xor-op", false, true);

            dfa.addTransition("^", "q0", "xor-op", true);
            dfa.addTransition("=", "xor-op", "ASSIGN", true);

            // ?, ??
            dfa.addState("ter-op", false, true);
            dfa.addState("coalescing-op", false, true);

            dfa.addTransition("?", "q0", "ter-op", true);
            dfa.addTransition("?", "ter-op", "coalescing-op", true);

            //>, >>, >>=, <=
            dfa.addState("greater-op", false, true);
            dfa.addState("sr-bin", false, true);

            dfa.addTransition(">", "q0", "greater-op", true);
            dfa.addTransition(">", "greater-op", "sr-bin", true);
            dfa.addTransition("=", "greater-op", "ASSIGN", true);
            dfa.addTransition("=", "sr-bin", "ASSIGN", true);

            //<, <<, <<=, <=
            dfa.addState("less-op", false, true);
            dfa.addState("sl-bin", false, true);

            dfa.addTransition("<", "q0", "less-op", true);
            dfa.addTransition("<", "less-op", "sl-bin", true);
            dfa.addTransition("=", "less-op", "ASSIGN", true);
            dfa.addTransition("=", "sl-bin", "ASSIGN", true);

            //=, ==
            dfa.addState("=", false, true);

            dfa.addTransition("=", "q0", "=", true);
            dfa.addTransition("=", "=", "ASSIGN", true);

            //char
            dfa.addState("consumeChar",false,false);
            dfa.addState("EscapeCharacter",false,false);
            dfa.addState("oneCharState", false, false);
            dfa.addState("returnChar", false, true);
            dfa.addState("single", false, false);

            dfa.addTransition("\'", "q0", "consumeChar",true);
            dfa.addTransition("\\", "consumeChar", "EscapeCharacter", true);
            var tcc = new Transition();
            tcc.setSettings(true);
            tcc.addCondition("a");
            tcc.addCondition("b");
            tcc.addCondition("f");
            tcc.addCondition("n");
            tcc.addCondition("r");
            tcc.addCondition("t");
            tcc.addCondition("v");
            tcc.addCondition("\\");
            tcc.addCondition("\"");
            tcc.addCondition("?");

            dfa.addTransition(tcc, "EscapeCharacter", "oneCharState");
            dfa.addTransition("\'", "EscapeCharacter", "single", true);
            dfa.addTransition("\'", "single", "returnChar", true);

            dfa.addTransition("\'", "consumeChar", "returnChar", true);
            dfa.addTransition("cualquiera -{\\}", "consumeChar", "oneCharState", true);
            dfa.addTransition("\'", "oneCharState", "returnChar", true);

            //strings
            dfa.addState("CosumeString", false, false);
            dfa.addState("ReturnString", false, true);
            dfa.addState("doubleQuotes", false, false);
            dfa.addState("EscapeCharacterString", false, false);
            dfa.addTransition("\"", "q0", "CosumeString", true);
            dfa.addTransition("\"", "CosumeString", "ReturnString", true);
            dfa.addTransition("\\", "CosumeString", "EscapeCharacterString", true);
            var tcs = new Transition();
            tcs.setSettings(true);
            tcs.addCondition("a");
            tcs.addCondition("b");
            tcs.addCondition("f");
            tcs.addCondition("n");
            tcs.addCondition("r");
            tcs.addCondition("t");
            tcs.addCondition("v");
            tcs.addCondition("\\");
            tcs.addCondition("\'");
            tcs.addCondition("?");
            dfa.addTransition(tcs, "EscapeCharacterString", "CosumeString");
            dfa.addTransition("cualquiera -{\\}", "CosumeString", "CosumeString", true);
            dfa.addTransition("\"", "EscapeCharacterString", "doubleQuotes", true);
            dfa.addTransition("\"", "doubleQuotes", "ReturnString", true);
            dfa.addTransition("cualquiera -{\"}", "doubleQuotes", "CosumeString", true);

            //verbatin
            dfa.addState("@", false, false);
            dfa.addState("\"", false, false);
            dfa.addState("CosumeVerbatin", false, false);
            dfa.addState("ReturnVerbatin", false, true);
            dfa.addTransition("@", "q0", "@", true);
            dfa.addTransition("\"", "@", "\"", true);
            dfa.addTransition("cualquiera - {\",\0}", "\"", "CosumeVerbatin", true);
            dfa.addTransition("\"", "\"", "ReturnVerbatin", true);
            dfa.addTransition("cualquiera - {\",\0}", "CosumeVerbatin", "CosumeVerbatin", true);
            dfa.addTransition("\"", "CosumeVerbatin", "ReturnVerbatin", true);
            dfa.addTransition("\"", "ReturnVerbatin", "CosumeVerbatin", true);


            //EOF
            dfa.addState("EOF", false, true);
            dfa.addTransition("\0", "q0", "EOF", true);
            dfa.addTransition("\0", "comentario_linea", "EOF", true);
        }

        public void resetCurrentState()
        {
            List<State> initial = dfa.states.Where(item => item.isInitial == true).ToList();
            currentState = initial[0];
        }

        public Token getNextToken()
        {
            resetCurrentState();
            var lexema = new StringBuilder();
            int lexemaRow=0, lexemaColumn=0;
            bool isFirst = true;
            while (true)
            {
                List<Transition> transitions = currentState.transitions.Where(item => item.evaluate(currentSymbol)).ToList();
                if (transitions.Count > 0)
                {
                    Transition t = transitions[0];
                    if (t.evaluate(currentSymbol))
                    {
                        currentState = t.to;
                        if (currentState.resetLexema)
                        {
                            isFirst = true;
                            lexema.Clear();
                        }
                        if (!t.isWhiteSpace() && !currentState.resetLexema)
                        {
                            if (isFirst)
                            {
                                lexemaRow = currentSymbol.rowCount;
                                lexemaColumn = currentSymbol.colCount;
                                isFirst = false;
                            }
                        }
                        if (t.aggregateToLexema)
                        {
                            lexema.Append(currentSymbol.character);
                        }
                        currentSymbol = inputString.GetNextSymbol();
                    }
                }
                else
                {
                    return makeToken(currentState, lexema.ToString(), lexemaRow, lexemaColumn);
                }
            }
        }

        private Token makeToken(State currentState, string lexema, int lexemaRow, int lexemaColumn)
        {
            if (currentState.isFinal)
            {
                if (currentState.name == "ID" || currentState.name == "LIT_NUMERIC" 
                    ||currentState.name=="OneSymbolOperator"
                    || currentState.name == "ASSIGN") 
                {
                    return currentState.makeToken(lexema.ToString(), lexemaRow, lexemaColumn);
                }

                if (currentState.name == "less-op")
                {
                    return new Token(
                        TokenType.OP_LESS_THAN,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "=")
                {
                    return new Token(
                        TokenType.OP_ASSIGN,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "sl-bin")
                {
                    return new Token(
                        TokenType.OP_BIN_LS,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "greater-op")
                {
                    return new Token(
                        TokenType.OP_GREATER_THAN,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "sr-bin")
                {
                    return new Token(
                        TokenType.OP_BIN_RS,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "coalescing-op")
                {
                    return new Token(
                        TokenType.OP_NULLABLE,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "ter-op")
                {
                    return new Token(
                        TokenType.OP_TER_NULLABLE,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "xor-op")
                {
                    return new Token(
                        TokenType.OP_BIN_XOR,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "mod-op")
                {
                    return new Token(
                        TokenType.OP_MODULO,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "or-op")
                {
                    return new Token(
                        TokenType.OP_LOG_OR,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

                if (currentState.name == "or-bin")
                {
                    return new Token(
                        TokenType.OP_BIN_OR,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "and-op")
                {
                    return new Token(
                        TokenType.OP_LOG_AND,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

                if (currentState.name == "and-bin")
                {
                    return new Token(
                        TokenType.OP_BIN_AND,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "not-op")
                {
                    return new Token(
                        TokenType.OP_DENIAL,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

                if (currentState.name == "mul-op")
                {
                    return new Token(
                        TokenType.OP_MULTIPLICATION,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

                if (currentState.name == "sum-op")
                {
                    return new Token(
                        TokenType.OP_SUM,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "increment")
                {
                    return new Token(
                        TokenType.OP_INCREMENT,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

                if (currentState.name == "sus-op")
                {
                    return new Token(
                        TokenType.OP_SUBSTRACT,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

                if (currentState.name == "decrement")
                {
                    return new Token(
                        TokenType.OP_DECREMENT,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

                if (currentState.name == "div-op")
                {
                    return new Token(
                        TokenType.OP_DIVISION,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }


                if (currentState.name == "EOF")
                {
                    return new Token(
                        TokenType.EOF,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if(currentState.name == "returnChar")
                {
                    return new Token(
                        TokenType.LIT_CHAR,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if(currentState.name == "ReturnString")
                {
                    return new Token(
                        TokenType.LIT_STRING,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "x")
                {
                    return new Token(
                        TokenType.LIT_INT,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if (currentState.name == "b")
                {
                    return new Token(
                        TokenType.LIT_INT,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }
                if(currentState.name == "ReturnVerbatin")
                {
                    return new Token(
                        TokenType.LIT_VERBATIN,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

                if (currentState.name == "0")
                {
                    return new Token(
                        TokenType.LIT_INT,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                }

            }
            throw new LexicalException("Error lexico: Lexema: "+lexema+" row: "+lexemaRow+" col: "+lexemaColumn);
        }
    }
}
