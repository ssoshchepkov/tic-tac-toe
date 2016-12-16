using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToeWinForms.Services;

namespace TicTacToeWinForms.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel(IApplicationService applicationService, IMessageBoxService messageBoxService, GameSettings settings)
        {
            Settings = settings;

            ExitCommand = ReactiveCommand.Create(() =>
            {
                applicationService.Exit();
            });

            NewGameCommand = ReactiveCommand.Create(() =>
            {
                GameField = new GameFieldViewModel(new Game(settings), messageBoxService);
            });
        }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; private set; }

        public ReactiveCommand NewGameCommand { get; private set; }

        private GameFieldViewModel _gameField;

        public GameFieldViewModel GameField
        {
            get { return _gameField; }
            private set { this.RaiseAndSetIfChanged(ref _gameField, value); }
        }

        public GameSettings Settings { get; }
    }
}
