﻿using System;

namespace Byndyusoft.SAMParser.TrigFunctions
{
    public class TangentFunction : IParserFunction
    {
        public double Evaluate(string expression, ref int from)
        {
            double arg = Parser.Calculate(expression, ref from, Parser.END_ARG);
            return Math.Tan(arg);
        }
    }
}
