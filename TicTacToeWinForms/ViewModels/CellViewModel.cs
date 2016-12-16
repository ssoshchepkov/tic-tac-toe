using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.Models.GameField;

namespace TicTacToeWinForms.ViewModels
{
    public class CellViewModel : ReactiveObject
    {
        private readonly Cell _model;
        private readonly Game _game;

        private readonly GameFieldViewModel _parent;

        public CellViewModel(GameFieldViewModel parent, int col, int row, Game game)
        {
            _game = game;
            _parent = parent;
            _model = _game.Field.GetCell(col, row);

            var canExecute = this.WhenAnyValue(vm => vm.IsEmpty, vm => vm._parent.IsGameOver, 
                (empty, gameover) => empty && !gameover);

            MarkCellCommand = ReactiveCommand.Create(() =>
            {
                _game.MarkCell(_model);
                this.RaisePropertyChanged(nameof(IsEmpty));
                this.RaisePropertyChanged(nameof(Caption));
            }, canExecute);
        }

        public bool IsEmpty => _model.IsEmpty;

        public string Caption
        {
            get { return IsEmpty ? string.Empty : _model.Player.Symbol; }
        }

        private bool _isWinning;

        public bool IsWinning
        {
            get { return _isWinning; }
            set { this.RaiseAndSetIfChanged(ref _isWinning, value); }
        }

        public ReactiveCommand MarkCellCommand { get; private set; }
    }
}
