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
    /// <summary>
    /// ViewModel for the main applcation window.
    /// </summary>
    public class MainViewModel : ReactiveObject
    {
        /// <summary>
        /// Creates a new instance of <see cref="MainViewModel"/> with specified services and game settings.
        /// </summary>
        /// <param name="applicationService">Applicaion service.</param>
        /// <param name="messageBoxService">Message box service.</param>
        /// <param name="settings">Game settings.</param>
        public MainViewModel(IApplicationService applicationService, IMessageBoxService messageBoxService, IGameSettings settings)
        {
            if (applicationService == null)
                throw new ArgumentNullException(nameof(applicationService), $"{nameof(applicationService)} can't be null!");

            if (messageBoxService == null)
                throw new ArgumentNullException(nameof(messageBoxService), $"{nameof(messageBoxService)} can't be null!");

            if (settings == null) throw new ArgumentNullException(nameof(settings), $"{nameof(settings)} can't be null!");

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

        /// <summary>
        /// Gets or sets commmand that closes the application.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ExitCommand { get; private set; }

        /// <summary>
        /// Gets or sets command that starts a new game.
        /// </summary>
        public ReactiveCommand<Unit, Unit> NewGameCommand { get; private set; }

        /// <summary>
        /// ViewModel for current game.
        /// </summary>
        private GameFieldViewModel _gameField;

        /// <summary>
        /// Gets or sets ViewModel for current game.
        /// </summary>
        public GameFieldViewModel GameField
        {
            get { return _gameField; }
            private set { this.RaiseAndSetIfChanged(ref _gameField, value); }
        }

        /// <summary>
        /// Gets game settings.
        /// </summary>
        public IGameSettings Settings { get; }
    }
}
