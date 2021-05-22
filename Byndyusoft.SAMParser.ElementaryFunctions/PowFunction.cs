using System;

namespace Byndyusoft.SAMParser.ElementaryFunctions
{
    public class PowFunction : IParserFunction
    {
        public double Evaluate(string expression, ref int from)
        {
            double arg1 = Parser.Calculate(expression, ref from, ',');
            double arg2 = Parser.Calculate(expression, ref from, Parser.END_ARG);

            return Math.Pow(arg1, arg2);
        }
    }
}
