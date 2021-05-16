using System;
using Xunit;
using Byndyusoft;

namespace Byndyusoft.ExpressionParsers.Tests
{
    public class SAMParserShould
    {
        [Theory]
        [InlineData("1+2-3", 0)]
        public void Should_GetEvaluatedExpression(string expression, decimal res)
        {
            Assert.Equal<decimal>(SAMParser.Evaluate(expression), res);
        }
    }
}
