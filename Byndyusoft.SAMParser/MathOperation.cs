using System;

namespace Byndyusoft.SAMParser
{
    internal class MathOperation
    {
        internal int Priority;
        internal Func<double, double, double> Action;
    }
}
