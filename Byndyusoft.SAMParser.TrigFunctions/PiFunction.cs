using System;

namespace Byndyusoft.SAMParser.TrigFunctions
{
    public class PiFunction : IParserFunction
    {
        public double Evaluate(string expression, ref int from)
        {
            return Math.PI;
        }
    }
}
