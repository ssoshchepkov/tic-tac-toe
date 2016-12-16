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
    public class GameFieldViewModel : ReactiveObject
    {
        private readonly Game _game;

        public ReactiveList<CellViewModel> CellsList { get; protected set; }

        public GameFieldViewModel(Game game, IMessageBoxService messageBoxService)
        {
            _game = game;

            CellsList = new ReactiveList<CellViewModel>() { ChangeTrackingEnabled = true };

            Cells = new CellViewModel[game.Field.Size, game.Field.Size];

            for (int row = 0; row < game.Field.Size; row += 1)
            {
                for (int col = 0; col < game.Field.Size; col += 1)
                {
                    var cell = new CellViewModel(this, col, row, game);
                    Cells[col, row] = cell;
                    CellsList.Add(cell);
                }
            }

            CellsList.ItemChanged.Where(x => x.PropertyName == nameof(CellViewModel.IsEmpty))
                     .Subscribe(x => 
            {
                this.RaisePropertyChanged(nameof(CurrentTurn));
                this.RaisePropertyChanged(nameof(CurrentPlayerSymbol));

                if(_game.IsGameOver)
                {
                    if (_game.Result.HasWinner)
                    {
                        foreach (var cell in _game.Result.VictoryLine)
                        {
                            Cells[cell.Position.Col, cell.Position.Row].IsWinning = true;
                        }
                    }

                    this.RaisePropertyChanged(nameof(IsGameOver));

                    string msg = MessagesResource.TieMessage;

                    if (_game.HasWinner)
                    {
                        msg = string.Format(MessagesResource.WinnerMessage, CurrentPlayerSymbol);
                    }

                    messageBoxService.Show(msg, MessagesResource.GameOverPopupTitle);
                }
            });
        }

        public bool IsGameOver => _game.IsGameOver;

        public CellViewModel[,] Cells { get; set; }

        public int FieldSize => _game.Field.Size;

        /// <summary>
        /// Current game turn number in human-friendly indexing (1-based).
        /// </summary>
        public int CurrentTurn => _game.Turn + 1;

        public string CurrentPlayerSymbol => _game.CurrentPlayer.Symbol;
    }
}
