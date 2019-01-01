﻿namespace Core
{
    public class PositionalArgument : Argument
    {
        public PositionalArgument(string value) : base(value)
        {
        }

        public override string ToString()
        {
            return Value;
        }
    }
}