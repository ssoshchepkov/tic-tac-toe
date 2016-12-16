using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;

namespace TicTacToe.Models.VictoryConditions
{
    /// <summary>
    /// Represents strategy of determination that victory conditions are fulfilled for the game. Different implementations of this interface 
    /// can be used in different modifications of this game.
    /// </summary>
    public interface IVictoryConditions
    {
        /// <summary>
        /// Checks if the current player has won by specified game field and the last marked cell.
        /// </summary>
        /// <param name="field">Current game field.</param>
        /// <param name="lastMarkedCell">The last cell marked by the current player.</param>
        /// <returns>True is the current player has won.</returns>
        bool IsVictory(IField field, Cell lastMarkedCell);

        /// <summary>
        /// Returns the victory line for the current player if this player has won. Otherwise returns null.
        /// </summary>
        /// <param name="field">Current game field.</param>
        /// <param name="lastMarkedCell">The last cell marked by the current player.</param>
        /// <returns>Victory line if the game is ended by victory. Otherwise null.</returns>
        IReadOnlyList<Cell> FindVictoryLine(IField field, Cell lastMarkedCell);
    }
}
