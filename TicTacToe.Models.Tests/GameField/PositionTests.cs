using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;
using Xunit;

namespace TicTacToe.Models.Tests.GameField
{
    public class PositionTests
    {
        [Fact]
        public void Coordinates_Should_Be_Saved()
        {
            int col = 10, row = 20;

            Position position = new Position(col, row);

            Assert.Equal(col, position.Col);
            Assert.Equal(row, position.Row);
        }
    }
}
