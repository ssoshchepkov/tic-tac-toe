using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToeWinForms.Services;

namespace TicTacToeWinForms.ViewModels
{
    /// <summary>
    /// ViewModel for the game field.
    /// </summary>
    public class GameFieldViewModel : ReactiveObject
    {
        /// <summary>
        /// The current game.
        /// </summary>
        private readonly IGame _game;

        /// <summary>
        /// Reactive list of cell viewmodels. This list is used for cell change tracking.
        /// </summary>
        private ReactiveList<CellViewModel> _cellsList;

        /// <summary>
        /// Creates a new instance of <see cref="GameFieldViewModel"/> for specified game.
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="messageBoxService">Message box service.</param>
        public GameFieldViewModel(IGame game, IMessageBoxService messageBoxService)
        {
            _game = game;

            _cellsList = new ReactiveList<CellViewModel>() { ChangeTrackingEnabled = true };

            Cells = new CellViewModel[game.Field.Size, game.Field.Size];

            // creating viewmodels for every cell on the field.
            for (int row = 0; row < game.Field.Size; row += 1)
            {
                for (int col = 0; col < game.Field.Size; col += 1)
                {
                    var cell = new CellViewModel(this, col, row, game);
                    Cells[col, row] = cell;
                    _cellsList.Add(cell);
                }
            }

            // Subscribing for changes of IsEmpty property for every cell:
            _cellsList.ItemChanged.Where(x => x.PropertyName == nameof(CellViewModel.IsEmpty))
                      .Subscribe(x => 
            {
                // If IsEmpty property was changed than turn was switched...
                this.RaisePropertyChanged(nameof(CurrentTurn));
                this.RaisePropertyChanged(nameof(CurrentPlayerSymbol));

                //...and probably this game is over
                if(_game.IsGameOver)
                {
                    // if it's not a tie we should mark the victory line.
                    if (_game.Result.HasWinner) 
                    {
                        foreach (var cell in _game.Result.VictoryLine)
                        {
                            Cells[cell.Position.Col, cell.Position.Row].IsWinning = true;
                        }
                    }

                    // Notify all subscribers that the game is over.
                    this.RaisePropertyChanged(nameof(IsGameOver));

                    string msg = MessagesResource.TieMessage;

                    if (_game.Result.HasWinner)
                    {
                        msg = string.Format(MessagesResource.WinnerMessage, CurrentPlayerSymbol);
                    }

                    // And show the final message box.
                    messageBoxService.Show(msg, MessagesResource.GameOverPopupTitle);
                }
            });
        }

        public bool IsGameOver => _game.IsGameOver;

        /// <summary>
        /// Gets 2-dimensional array of cell viewmodels.
        /// </summary>
        public CellViewModel[,] Cells { get; }

        public int FieldSize => _game.Field.Size;

        /// <summary>
        /// Gets the current game turn number in human-friendly indexing (1-based).
        /// </summary>
        public int CurrentTurn => _game.Turn + 1;

        /// <summary>
        /// Gets the symbol of the current player.
        /// </summary>
        public string CurrentPlayerSymbol => _game.CurrentPlayer.Symbol;
    }
}
