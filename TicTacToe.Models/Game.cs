using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;

namespace TicTacToe.Models
{
    /// <summary>
    /// Tic-tac-toe game engine.
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// Index of current active player for <see cref="GameSettings.Players"/> list.
        /// </summary>
        private int _currentPlayerIndex;

        /// <summary>
        /// Gets or sets a zero-based number of current game turn.
        /// </summary>
        public int Turn { get; private set; }

        /// <summary>
        ///  Gets a value indicating whether this game is over.
        /// </summary>
        public bool IsGameOver => Result != null;

        /// <summary>
        /// Zero-based number of the last game turn.
        /// </summary>
        private readonly int _maxTurn;

        /// <summary>
        /// Game settings.
        /// </summary>
        private readonly IGameSettings _settings;

        /// <summary>
        ///  Creates a new instance of <see cref="Game"/> with specified game settings.
        /// </summary>
        /// <param name="settings">Settings of this game.</param>
        public Game(IGameSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings), "Settings can't be null!");

            settings.Validate();

            _settings = settings;

            Field = new Field(_settings.FieldSize);

            // turns needed to mark all cells
            _maxTurn = Field.Size * Field.Size - 1;
        }

        /// <summary>
        /// Gets field of this game.
        /// </summary>
        public IField Field { get; }

        /// <summary>
        /// Gets current active player.
        /// </summary>
        public Player CurrentPlayer => _settings.Players[_currentPlayerIndex];

        /// <summary>
        /// Gets result of this game.
        /// </summary>
        /// <remarks>Result is null if the game is still in progress.</remarks>
        public GameResult Result { get; private set; }

        /// <summary>
        /// Marks specified cell with the current player's symbol.
        /// </summary>
        /// <param name="cell">The cell to be marked with the current player's symbol.</param>
        public void MarkCell(Cell cell)
        {
            if (!cell.IsEmpty) throw new ArgumentException(nameof(cell), "This cell is already marked!");

            if (IsGameOver) throw new InvalidOperationException("This game is over.");

            cell.Mark(CurrentPlayer);

            // Let's try to find winning line.
            var victoryLine = _settings.VictoryConditions.FindVictoryLine(Field, cell);

            // Found something. The game is over.
            if (victoryLine != null)
            {
                Result = new GameResult(CurrentPlayer, victoryLine);
                return;
            }

            // Players marked all the field. A tie.
            if (Turn == _maxTurn)
            {
                Result = new GameResult();
            }
            else
            {
                NextTurn();
            }
        }

        /// <summary>
        /// Switch game state to the next turn.
        /// </summary>
        private void NextTurn()
        {
            if(_currentPlayerIndex == _settings.Players.Count - 1)
            {
                _currentPlayerIndex = 0;
            }else
            {
                _currentPlayerIndex += 1;
            }

            Turn += 1;
        }
    }
}