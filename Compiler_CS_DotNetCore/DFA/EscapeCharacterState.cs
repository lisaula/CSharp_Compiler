﻿using System;

namespace Compiler
{
    internal class EscapeCharacterState : State
    {
        public EscapeCharacterState(string name, bool isInital, bool isFinal) : base(name, isInital, isFinal)
        {
        }

        public override Token makeToken(string lexema, int lexemaRow, int lexemaColumn)
        {
            throw new NotImplementedException();
        }
    }
}