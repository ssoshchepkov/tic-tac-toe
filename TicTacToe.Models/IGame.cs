using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;

namespace TicTacToe.Models
{
    /// <summary>
    /// The interface of Tic-tac-toe game engine.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Marks specified cell with the current player's symbol.
        /// </summary>
        /// <param name="cell">The cell to be marked with the current player's symbol.</param>
        void MarkCell(Cell cell);

        /// <summary>
        /// Gets result of this game.
        /// </summary>
        /// <remarks>Result is null if the game is still in progress.</remarks>
        GameResult Result { get; }

        /// <summary>
        /// Gets current active player.
        /// </summary>
        Player CurrentPlayer { get; }

        /// <summary>
        /// Gets field of this game.
        /// </summary>
        IField Field { get; }

        /// <summary>
        ///  Gets a value indicating whether this game is over.
        /// </summary>
        bool IsGameOver { get; }

        /// <summary>
        /// Gets or sets a zero-based number of current game turn.
        /// </summary>
        int Turn { get; }
    }
}
