using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.Models.GameField;
using TicTacToeWinForms.Services;
using TicTacToeWinForms.ViewModels;
using Xunit;
using System.Reactive.Linq;

namespace TicTacToeWinForms.Tests
{
    public class GameFieldViewModelTests
    {
        [Fact]
        public void Should_Show_Correct_Turn()
        {
            const int turn = 0;

            var game = Substitute.For<IGame>();
            game.Turn.Returns(turn);

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());

            Assert.Equal(turn + 1, vm.CurrentTurn);
        }

        [Fact]
        public void Should_Show_Correct_Player_Symbol()
        {
            var game = Substitute.For<IGame>();
            game.CurrentPlayer.Returns(new Player(Symbols.X));

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());

            Assert.Equal(Symbols.X, vm.CurrentPlayerSymbol);
        }

        [Fact]
        public void Constructor_Should_Create_Cells()
        {
            const int size = 3;

            var settings = Substitute.For<IGameSettings>();
            settings.FieldSize.Returns(size);

            GameFieldViewModel vm = new GameFieldViewModel(new Game(settings),
                Substitute.For<IMessageBoxService>());

            Assert.Equal(size - 1, vm.Cells.GetUpperBound(0));
            Assert.Equal(size - 1, vm.Cells.GetUpperBound(1));

            for (int row = 0; row < size; row += 1)
            {
                for (int col = 0; col < size; col += 1)
                {
                    Assert.NotNull(vm.Cells[col, row]);
                }
            }
        }

        [Fact]
        public async Task Cell_Mark_Should_Fire_Property_Changes()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(3);
            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
                new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)))
            );

            List<string> receivedEvents = new List<string>();

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());
            vm.PropertyChanged += (s, e) =>
            {
                receivedEvents.Add(e.PropertyName);
            };

            // Get an empty cell
            var cell = vm.Cells[0, 0];
            // Mark it
            await cell.MarkCellCommand.Execute();

            Assert.Contains(nameof(GameFieldViewModel.CurrentTurn), receivedEvents);
            Assert.Contains(nameof(GameFieldViewModel.CurrentPlayerSymbol), receivedEvents);
        }

        [Fact]
        public async Task On_Game_Over_Should_Show_Message_Box()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(3);
            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
                new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)))
            );
            game.IsGameOver.Returns(true);
            game.Result.Returns(new GameResult());

            var messageboxService = Substitute.For<IMessageBoxService>();
            
            GameFieldViewModel vm = new GameFieldViewModel(game, messageboxService);

            // Get an empty cell
            var cell = vm.Cells[0, 0];
            // Mark it
            await cell.MarkCellCommand.Execute();

            messageboxService.Received().Show(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task On_Player_Win_Should_Mark_Victory_Cells()
        {
            Cell victoryCell = new Cell(new Position(0, 0));
            Player player = new Player(Symbols.X);

            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(3);
            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
            {
                if(x.ArgAt<int>(0) == 0 && x.ArgAt<int>(1) == 0)
                {
                    return victoryCell;
                }
                return new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)));
            });
            // Game should end...
            game.IsGameOver.Returns(true);
            game.CurrentPlayer.Returns(player);
            // ... with player's victory and victory line of one cell
            game.Result.Returns(new GameResult(player, new List<Cell> { victoryCell }));

            var messageboxService = Substitute.For<IMessageBoxService>();

            GameFieldViewModel vm = new GameFieldViewModel(game, messageboxService);

            // Get an empty cell
            var cell = vm.Cells[0, 0];

            Assert.False(cell.IsWinning);

            // Mark it
            await cell.MarkCellCommand.Execute();

            Assert.True(cell.IsWinning);
        }
    }
}
