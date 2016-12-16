using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.Models.GameField;

namespace TicTacToeWinForms.ViewModels
{
    /// <summary>
    /// ViewModel for the cell.
    /// </summary>
    public class CellViewModel : ReactiveObject
    {
        private readonly Cell _model;
        private readonly IGame _game;

        private readonly GameFieldViewModel _parent;

        /// <summary>
        /// Creates a new instance of <see cref="CellViewModel"/> for specified position.
        /// </summary>
        /// <param name="parent">Parent ViewModel of a field.</param>
        /// <param name="col">Cell column.</param>
        /// <param name="row">Cell row.</param>
        /// <param name="game">Current game.</param>
        public CellViewModel(GameFieldViewModel parent, int col, int row, IGame game)
        {
            _game = game;
            _parent = parent;
            _model = _game.Field.GetCell(col, row);

            // MarkCellCommand can only be executed when this cell is unmarked and the game is not over.
            var canExecute = this.WhenAnyValue(vm => vm.IsEmpty, vm => vm._parent.IsGameOver, 
                (empty, gameover) => empty && !gameover);

            MarkCellCommand = ReactiveCommand.Create(() =>
            {
                _game.MarkCell(_model);
                // The cell is not empty anymore...
                this.RaisePropertyChanged(nameof(IsEmpty));
                // ...and has a player's symbol as a caption.
                this.RaisePropertyChanged(nameof(Caption));
            }, canExecute);
        }

        public bool IsEmpty => _model.IsEmpty;

        /// <summary>
        /// Gets caption on this cell.
        /// </summary>
        public string Caption => _model.Player?.Symbol;

        private bool _isWinning;

        /// <summary>
        /// Gets or sets a value indicating whether this cell belongs to victory line.
        /// </summary>
        public bool IsWinning
        {
            get { return _isWinning; }
            set { this.RaiseAndSetIfChanged(ref _isWinning, value); }
        }

        /// <summary>
        /// Gets or sets command that marks this cell.
        /// </summary>
        public ReactiveCommand<Unit, Unit> MarkCellCommand { get; private set; }
    }
}
