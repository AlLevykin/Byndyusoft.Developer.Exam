using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Byndyusoft.SAMParser.Tests")]

namespace Byndyusoft.SAMParser
{
    internal class Cell
    {
        internal Cell(double value, char operation)
        {
            Value = value;
            Operation = operation;
        }

        internal double Value { get; set; }
        internal char Operation { get; set; }
    }
}
