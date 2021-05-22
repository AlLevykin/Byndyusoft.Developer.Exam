namespace Byndyusoft.SAMParser
{
    internal class IdentityFunction : IParserFunction
    {
        public double Evaluate(string expression, ref int from)
        {
            return Parser.Calculate(expression, ref from, ')');
        }
    }
}
