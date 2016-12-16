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
    public class CellViewModelTests
    {
        [Fact]
        public void Caption_For_Empty_Cell_Should_Be_Empty()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(1);

            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>())
                .Returns(x => new Cell(new Position(0, 0)));

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());

            // Get the cell
            var cell = vm.Cells[0, 0];

            Assert.True(string.IsNullOrWhiteSpace(cell.Caption));
        }

        [Fact]
        public void Caption_Should_Return_Player_Symbol()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(1);

            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>())
                .Returns(x => new Cell(new Position(0, 0), new Player(Symbols.O)));

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());

            // Get the cell
            var cell = vm.Cells[0, 0];

            Assert.Equal(Symbols.O, cell.Caption);
        }

        [Fact]
        public async Task MarkCellCommand_Should_Fire_Property_Changes()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(1);
            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
                new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)))
            );

            List<string> receivedEvents = new List<string>();

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());
          
            // Get an empty cell
            var cell = vm.Cells[0, 0];

            cell.PropertyChanged += (s, e) =>
            {
                receivedEvents.Add(e.PropertyName);
            };

            // Mark it
            await cell.MarkCellCommand.Execute();

            Assert.Contains(nameof(CellViewModel.Caption), receivedEvents);
            Assert.Contains(nameof(CellViewModel.IsEmpty), receivedEvents);
        }

        [Fact]
        public async Task MarkCellCommand_Should_Call_MarkCell()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(1);
            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
                new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)))
            );

            List<string> receivedEvents = new List<string>();

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());

            var cell = vm.Cells[0, 0];
            await cell.MarkCellCommand.Execute();

            game.Received().MarkCell(Arg.Any<Cell>());
        }

        [Fact]
        public async Task  MarkCellCommand_Should_Be_Executable_For_Empty_Cell()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(1);
            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
                new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)))
            );

            List<string> receivedEvents = new List<string>();

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());

            var cell = vm.Cells[0, 0];
            var canExecute = await cell.MarkCellCommand.CanExecute.FirstAsync();

            Assert.True(canExecute);
        }

        [Fact]
        public async Task MarkCellCommand_Should_Not_Be_Executable_For_Marked_Cell()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(1);
            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
                new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)), new Player(Symbols.O))
            );

            List<string> receivedEvents = new List<string>();

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());

            var cell = vm.Cells[0, 0];
            var canExecute = await cell.MarkCellCommand.CanExecute.FirstAsync();

            Assert.False(canExecute);
        }

        [Fact]
        public async Task MarkCellCommand_Should_Not_Be_Executable_After_Game_Over()
        {
            var game = Substitute.For<IGame>();
            game.Field.Size.Returns(3);
            game.Field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
                new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)))
            );
            game.IsGameOver.Returns(true);
            game.Result.Returns(new GameResult());

            GameFieldViewModel vm = new GameFieldViewModel(game, Substitute.For<IMessageBoxService>());

            var cell = vm.Cells[0, 0];
          
            var canExecute = await cell.MarkCellCommand.CanExecute.FirstAsync();
            Assert.False(canExecute);
        }
    }
}
