using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;
using Xunit;

namespace TicTacToe.Models.Tests
{
    public class GameResultTests
    {
        [Fact]
        public void Null_Winner_Shold_Throw_ArgumentNullException()
        {
            Exception exception = Record.Exception(() => new GameResult(null, new List<Cell>()));
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Parameterless_Constructor_Should_Have_No_Winner()
        {
            var result = new GameResult();
            Assert.False(result.HasWinner);
        }

        [Fact]
        public void Parameterized_Constructor_Should_Have_Winner()
        {
            Player player = new Player(Symbols.X);
            IReadOnlyList<Cell> line = new List<Cell>();

            var result = new GameResult(player, line);

            Assert.True(result.HasWinner);
        }

        [Fact]
        public void Constructor_Parameters_Should_Be_Saved()
        {
            Player player = new Player(Symbols.X);
            IReadOnlyList<Cell> line = new List<Cell>();

            var result = new GameResult(player, line);

            Assert.Same(player, result.Winner);
            Assert.Same(line, result.VictoryLine);
        }
    }
}
