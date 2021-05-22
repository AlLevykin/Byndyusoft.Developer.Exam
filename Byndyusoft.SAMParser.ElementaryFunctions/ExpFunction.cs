using System;

namespace Byndyusoft.SAMParser.ElementaryFunctions
{
    public class ExpFunction : IParserFunction
    {
        public double Evaluate(string expression, ref int from)
        {
            double arg = Parser.Calculate(expression, ref from, Parser.END_ARG);
            return Math.Exp(arg);
        }
    }
}
