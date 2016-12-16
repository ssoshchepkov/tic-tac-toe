using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;

namespace TicTacToe.Models
{
    /// <summary>
    /// Represents result of an ended game.
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Creates a new instance of <see cref="GameResult"/> with specified winner and victory line.
        /// </summary>
        /// <param name="winner">The Player that won the game.</param>
        /// <param name="victoryLine">Victory line of cells</param>
        public GameResult(Player winner, IReadOnlyList<Cell> victoryLine)
        {
            if (winner == null)
                throw new ArgumentNullException(nameof(winner), "Winner cannot be null. For a tie use parameterless constructor");

            VictoryLine = victoryLine;
            Winner = winner;
        }

        /// <summary>
        /// Creates a new instance of <see cref="GameResult"/> that represents a tie.
        /// </summary>
        public GameResult() { }

        public IReadOnlyList<Cell> VictoryLine { get; }

        /// <summary>
        /// Gets the winner of the game. Null for the tie.
        /// </summary>
        public Player Winner { get; }

        /// <summary>
        /// Gets a value indicating whether this game has a winner.
        /// </summary>
        public bool HasWinner => Winner != null;
    }
}
