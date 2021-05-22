using Xunit;

namespace Byndyusoft.SAMParser.Tests
{
    public class CellShould
    {
        [Theory]
        [InlineData(0.1, '+')]
        public void Should_CreateCellWell(double value, char action)
        {
            var c = new Cell(value, action);
            Assert.Equal<double>(c.Value, value);
            Assert.Equal<char>(c.Operation, action);
        }
    }
}
