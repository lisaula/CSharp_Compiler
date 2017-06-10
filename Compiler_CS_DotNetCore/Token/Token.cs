﻿namespace Compiler
{
    public class Token
    {
        public TokenType type;
        public int column;
        public int row;
        public string lexema { get; set; }
        
        public Token(TokenType type, string lexema, int row, int column)
        {
            this.type = type;
            this.lexema = lexema;
            this.row = row;
            this.column = column;
        }
        public Token()
        {
            lexema = null;
        }

        public override bool Equals(object obj)
        {
            if(obj is Token)
            {
                var t = obj as Token;
                return t.lexema == lexema;
            }
            return false;
        }

        public override string ToString()
        {
            return " row: " +row+ " column: "+column;
        }
    }
}