using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;
using Xunit;

namespace TicTacToe.Models.Tests.GameField
{
    public class CellTests
    {
        [Fact]
        public void Should_Save_Position()
        {
            Position position = new Position(1, 1);
            Cell cell = new Cell(position);

            Assert.Same(position, cell.Position);
        }
    }
}
