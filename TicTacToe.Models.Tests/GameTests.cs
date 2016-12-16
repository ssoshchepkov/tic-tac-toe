using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;
using TicTacToe.Models.VictoryConditions;
using Xunit;

namespace TicTacToe.Models.Tests
{
    public class GameTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException()
        {
            Exception exception = Record.Exception(() => new Game(null));
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Constructor_Should_Validate_Settings()
        {
            IGameSettings settings = Substitute.For<IGameSettings>();
            var game = new Game(settings);
            settings.Received().Validate();
        }

        [Theory]
        [InlineData(GameSettings.MINIMAL_FIELD_SIZE)]
        [InlineData(4)]
        [InlineData(10)]
        public void Constructor_Should_Create_Field_Of_Specified_Size(int size)
        {
            var settings = Substitute.For<IGameSettings>();
            settings.FieldSize.Returns(size);

            var game = new Game(settings);

            Assert.NotNull(game.Field);
            Assert.Equal(size, game.Field.Size);
        }

        [Fact]
        public void First_Turn_Should_Be_For_First_Player()
        {
            Player p1 = new Player(Symbols.X);
            Player p2 = new Player(Symbols.O);
            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, Substitute.For<IVictoryConditions>());

            settings.AddPlayer(p1);
            settings.AddPlayer(p2);

            var game = new Game(settings);

            Assert.Same(p1, game.CurrentPlayer);
        }

        [Fact]
        public void MarkCell_Should_Set_Cell_Player_Property()
        {
            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, Substitute.For<IVictoryConditions>());

            settings.AddPlayer(new Player(Symbols.X));
            settings.AddPlayer(new Player(Symbols.O));

            var game = new Game(settings);

            var cell = game.Field.GetCell(0, 0);

            Assert.True(cell.IsEmpty);

            Player markingPlayer = game.CurrentPlayer;

            game.MarkCell(cell);

            Assert.False(cell.IsEmpty);
            Assert.Same(markingPlayer, cell.Player);
        }

        [Fact]
        public void MarkCell_Should_Switch_Turn()
        {
            var victoryConditions = Substitute.For<IVictoryConditions>();
            // FindVictoryLine should fail
            victoryConditions.FindVictoryLine(Arg.Any<Field>(), Arg.Any<Cell>()).Returns(x => null);

            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, victoryConditions);

            settings.AddPlayer(new Player(Symbols.X));
            settings.AddPlayer(new Player(Symbols.O));

            var game = new Game(settings);

            var cell = game.Field.GetCell(0, 0);

            Assert.True(cell.IsEmpty);

            Player markingPlayer = game.CurrentPlayer;
            int currentTurn = game.Turn;

            game.MarkCell(cell);

            Assert.Equal(currentTurn + 1, game.Turn);
            Assert.NotSame(markingPlayer, game.CurrentPlayer);
        }

        [Fact]
        public void Mark_Same_Cell_Twice_Should_Throw_ArgumentException()
        {
            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, Substitute.For<IVictoryConditions>());

            settings.AddPlayer(new Player(Symbols.X));
            settings.AddPlayer(new Player(Symbols.O));

            var game = new Game(settings);

            var cell = game.Field.GetCell(0, 0);

            game.MarkCell(cell);

            Exception exception = Record.Exception(() => game.MarkCell(cell));
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void Should_Be_Tie_When_All_Cells_Are_Marked()
        {
            var victoryConditions = Substitute.For<IVictoryConditions>();
            // FindVictoryLine should fail
            victoryConditions.FindVictoryLine(Arg.Any<Field>(), Arg.Any<Cell>()).Returns(x => null);

            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, victoryConditions);

            settings.AddPlayer(new Player(Symbols.X));
            settings.AddPlayer(new Player(Symbols.O));

            var game = new Game(settings);

            for (int row = 0; row < game.Field.Size; row += 1)
            {
                for (int col = 0; col < game.Field.Size; col += 1)
                {
                    var cell = game.Field.GetCell(col, row);
                    game.MarkCell(cell);
                }
            }

            Assert.True(game.IsGameOver);
            Assert.NotNull(game.Result);
            Assert.False(game.Result.HasWinner);
        }

        [Fact]
        public void Should_End_Game_After_Find_Victory_Line()
        {
            var resultList = new List<Cell>() { new Cell(new Position(1, 1)) };

            var victoryConditions = Substitute.For<IVictoryConditions>();
            // FindVictoryLine should return successful result
            victoryConditions.FindVictoryLine(Arg.Any<Field>(), Arg.Any<Cell>()).Returns(x => resultList);

            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, victoryConditions);

            settings.AddPlayer(new Player(Symbols.X));
            settings.AddPlayer(new Player(Symbols.O));

            var game = new Game(settings);

            var currentPlayer = game.CurrentPlayer;
            var turn = game.Turn;

            var cell = game.Field.GetCell(0, 0);
            game.MarkCell(cell);

            // The game is over and turn not switched.
            Assert.True(game.IsGameOver);
            Assert.NotNull(game.Result);
            Assert.Same(currentPlayer, game.CurrentPlayer);
            Assert.Equal(turn, game.Turn);

            // The winner and victory sequence are correct.
            Assert.True(game.Result.HasWinner);
            Assert.Same(currentPlayer, game.Result.Winner);
            Assert.Same(resultList, game.Result.VictoryLine);
        }
    }
}
