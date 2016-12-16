using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;
using Xunit;

namespace TicTacToe.Models.Tests.GameField
{
    public class FieldTests
    {
        [Theory]
        [InlineData(GameSettings.MINIMAL_FIELD_SIZE)]
        [InlineData(4)]
        [InlineData(10)]
        public void Should_Create_Field_Of_Specified_Size(int value)
        {
            Field field = new Field(value);

            Assert.Equal(value, field.Size);

            for (int row = 0; row < value; row += 1)
            {
                for (int col = 0; col < value; col += 1)
                {
                    Cell cell = field.GetCell(col, row);
                    Assert.NotNull(cell);
                }
            }
        }

        [Fact]
        public void Wrong_Row_Should_Throw_IndexOutOfRangeException()
        {
            Field field = new Field(GameSettings.MINIMAL_FIELD_SIZE);

            Exception exception = Record.Exception(() => field.GetCell(GameSettings.MINIMAL_FIELD_SIZE, GameSettings.MINIMAL_FIELD_SIZE + 1));
            Assert.IsType<IndexOutOfRangeException>(exception);   
        }

        [Fact]
        public void Wrong_Column_Should_Throw_IndexOutOfRangeException()
        {
            Field field = new Field(GameSettings.MINIMAL_FIELD_SIZE);

            Exception exception = Record.Exception(() => field.GetCell(GameSettings.MINIMAL_FIELD_SIZE + 1, GameSettings.MINIMAL_FIELD_SIZE));
            Assert.IsType<IndexOutOfRangeException>(exception);
        }
    }
}
