using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToeWinForms.Services;
using TicTacToeWinForms.ViewModels;
using Xunit;
using System.Reactive.Linq;

namespace TicTacToeWinForms.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public async Task ExitCommand_Should_Call_ApplicationService_Exit()
        {
            var appService = Substitute.For<IApplicationService>();

            MainViewModel vm = new MainViewModel(appService, Substitute.For<IMessageBoxService>(), Substitute.For<IGameSettings>());

            await vm.ExitCommand.Execute();

            appService.Received().Exit();
        }

        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException()
        {
            Exception ex1 = Record.Exception(() => new MainViewModel(null, Substitute.For<IMessageBoxService>(), Substitute.For<IGameSettings>()));
            Assert.IsType<ArgumentNullException>(ex1);

            Exception ex2 = Record.Exception(() => new MainViewModel(Substitute.For<IApplicationService>(), null, Substitute.For<IGameSettings>()));
            Assert.IsType<ArgumentNullException>(ex2);

            Exception ex3 = Record.Exception(() => new MainViewModel(Substitute.For<IApplicationService>(), Substitute.For<IMessageBoxService>(), null));
            Assert.IsType<ArgumentNullException>(ex3);
        }

        [Fact]
        public async Task NewGameCommand_Should_Create_GameField()
        {
            MainViewModel vm = new MainViewModel(Substitute.For<IApplicationService>(), Substitute.For<IMessageBoxService>(), 
                Substitute.For<IGameSettings>());

            Assert.Null(vm.GameField);

            await vm.NewGameCommand.Execute();

            Assert.NotNull(vm.GameField);
        }

        [Fact]
        public async Task NewGameCommand_Should_Recreate_GameField()
        {
            MainViewModel vm = new MainViewModel(Substitute.For<IApplicationService>(), Substitute.For<IMessageBoxService>(),
                Substitute.For<IGameSettings>());

            Assert.Null(vm.GameField);

            await vm.NewGameCommand.Execute();

            Assert.NotNull(vm.GameField);

            var oldGameField = vm.GameField;

            await vm.NewGameCommand.Execute();

            Assert.NotSame(oldGameField, vm.GameField);
        }
    }
}
