using Xunit;

namespace Byndyusoft.SAMParser.Tests
{
    public class ParserShould
    {
        [Theory]
        [InlineData("10", 10)]
        [InlineData("1+2-3", 0)]
        [InlineData("1-2", 1 - 2)]
        [InlineData("(1-(2))", (1 - (2)))]
        [InlineData("3+2*6-1", 3 + 2 * 6 - 1)]
        [InlineData("3-2*6-1", 3 - 2 * 6 - 1)]
        [InlineData("1-2-3-(4-(5-(6-7)))", 1 - 2 - 3 - (4 - (5 - (6 - 7))))]
        [InlineData("(-1+7)*(9-2)", (-1 + 7) * (9 - 2))]
        [InlineData("((16-3)-3)+15/2*5", ((16 - 3) - 3) + 15 / 2.0 * 5)]
        [InlineData("1+15/2*5", 1 + 15 / 2.0 * 5)]
        [InlineData("3-2/6-1", 3 - 2 / 6.0 - 1)]
        [InlineData("3*50-3*2^4*3", (3 * 50) - 3 * 2 * 2 * 2 * 2 * 3)]
        [InlineData("5-1/2^2-3", 1.75)]
        [InlineData("(((1/4/2-(8/2/3+5))))", (((1 / 4.0 / 2.0 - (8 / 2.0 / 3.0 + 5)))))]
        public void Should_GetProcessedExpression(string expression, double res)
        {
            Assert.Equal<double>(Parser.Process(expression), res);
        }

        [Theory]
        [InlineData('+')]
        [InlineData('-')]
        [InlineData('*')]
        [InlineData('/')]
        [InlineData('^')]
        public void Should_ValidOperation(char sign)
        {
            Assert.True(Parser.ValidOperation(sign));
        }

        [Theory]
        [InlineData('+', 2)]
        [InlineData('-', 2)]
        [InlineData('*', 3)]
        [InlineData('/', 3)]
        [InlineData('^', 4)]
        public void Should_GetPriority(char sign, int priority)
        {
            Assert.Equal<int>(Parser.GetPriority(sign), priority);
        }

        [Theory]
        [InlineData('+', '+', true)]
        [InlineData('+', '-', true)]
        [InlineData('+', '*', false)]
        [InlineData('+', '/', false)]
        [InlineData('+', '^', false)]
        [InlineData('-', '+', true)]
        [InlineData('-', '-', true)]
        [InlineData('-', '*', false)]
        [InlineData('-', '/', false)]
        [InlineData('-', '^', false)]
        [InlineData('*', '+', true)]
        [InlineData('*', '-', true)]
        [InlineData('*', '*', true)]
        [InlineData('*', '/', true)]
        [InlineData('*', '^', false)]
        [InlineData('/', '+', true)]
        [InlineData('/', '-', true)]
        [InlineData('/', '*', true)]
        [InlineData('/', '/', true)]
        [InlineData('/', '^', false)]
        [InlineData('^', '+', true)]
        [InlineData('^', '-', true)]
        [InlineData('^', '*', true)]
        [InlineData('^', '/', true)]
        [InlineData('^', '^', true)]
        public void Should_ComparePriority(char leftOperation, char rightOperation, bool res)
        {
            var leftCell = new Cell(1, leftOperation);
            var rightCell = new Cell(1, rightOperation);
            Assert.Equal<bool>(Parser.CanMergeCells(leftCell, rightCell), res);
        }

        [Theory]
        [InlineData(0, '-', '1')]
        [InlineData(0, '\n', '1')]
        [InlineData(0, ')', '1')]
        [InlineData(0, '2', '1')]
        [InlineData(1, '2', '1')]
        public void Should_Collecting(int stringLength, char ch, char to)
        {
            Assert.True(Parser.StillCollecting(stringLength, ch, to));
        }

        [Theory]
        [InlineData(1, '-', '1')]
        [InlineData(1, '(', '1')]
        [InlineData(1, '+', '1')]
        [InlineData(1, '*', '1')]
        [InlineData(1, '/', '1')]
        [InlineData(1, '^', '1')]
        public void Should_DetectExpressionEnd(int stringLength, char ch, char to)
        {
            Assert.False(Parser.StillCollecting(stringLength, ch, to));
        }
    }
}
