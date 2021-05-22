using System;
using System.Collections.Generic;
using System.Text;

namespace Byndyusoft.SAMParser
{
    public interface IParserFunction
    {
        public double Evaluate(string expression, ref int from);
    }
}
