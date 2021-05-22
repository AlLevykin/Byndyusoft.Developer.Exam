using System;
using System.Collections.Generic;
using System.Text;

namespace Byndyusoft.SAMParser
{
    internal class StrToDoubleFunction : IParserFunction
    {
        public double Evaluate(string expression, ref int from)
        {
            if (!Double.TryParse(expression, out double res))
            {
                throw new ArgumentException("Could not parse token [" + expression + "]");
            }
            return res;
        }
    }
}
