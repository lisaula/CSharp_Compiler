﻿using System;

namespace Compiler.Tree
{
    public class IntType : PrimitiveType
    {
        public IntType(Token token) : base(token)
        {
        }
        public IntType()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is IntType;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }

        public override string GetComparativeType()
        {
            throw new NotImplementedException();
        }
    }
}