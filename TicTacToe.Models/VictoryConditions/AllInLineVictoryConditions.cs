using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;

namespace TicTacToe.Models.VictoryConditions
{
    /// <summary>
    /// Represents strategy of determination that victory conditions are fulfilled for the game. This implementation is for default Tic-tac-toe game.
    /// The player should mark all the cells in a whole horizontal, vertical, or diagonal row.
    /// </summary>
    public class AllInLineVictoryConditions : IVictoryConditions
    {
        /// <summary>
        /// Temporary array for searching victory line.
        /// </summary>
        private Cell[] _victoryLine;

        /// <summary>
        /// Creates a new instance of <see cref="AllInLineVictoryConditions"/>.
        /// </summary>
        public AllInLineVictoryConditions()
        {
        }

        /// <summary>
        /// Returns the victory line for the current player if this player has won. Otherwise returns null.
        /// </summary>
        /// <param name="field">Current game field.</param>
        /// <param name="lastMarkedCell">The last cell marked by the current player.</param>
        /// <returns>Victory line if the game is ended by victory. Otherwise null.</returns>
        public IReadOnlyList<Cell> FindVictoryLine(IField field, Cell lastMarkedCell)
        {
            _victoryLine = new Cell[field.Size];
            IReadOnlyList<Cell> result = null;

            if (IsVictory(field, lastMarkedCell)) result = _victoryLine;
            _victoryLine = null;

            return result;
        }

        /// <summary>
        /// Checks if the current player has won by specified game field and the last marked cell.
        /// </summary>
        /// <param name="field">Current game field.</param>
        /// <param name="lastMarkedCell">The last cell marked by the current player.</param>
        /// <returns>True is the current player has won.</returns>
        public bool IsVictory(IField field, Cell lastMarkedCell)
        {
            if (CheckRow(field, lastMarkedCell) || 
                CheckCol(field, lastMarkedCell) || 
                CheckDiagonal(field, lastMarkedCell))
            {
                return true;
            }

            return false;
        }

        private bool CheckRow(IField field, Cell lastMarkedCell)
        {
            return CheckLine(field, lastMarkedCell.Player, col => field.GetCell(col, lastMarkedCell.Position.Row));
        }

        private bool CheckCol(IField field, Cell lastMarkedCell)
        {
            return CheckLine(field, lastMarkedCell.Player, row => field.GetCell(lastMarkedCell.Position.Col, row));
        }

        private bool CheckDiagonal(IField field, Cell lastMarkedCell)
        {
            // Top-left diagonal:
            if (lastMarkedCell.Position.Col == lastMarkedCell.Position.Row)
            {
                if (CheckLine(field, lastMarkedCell.Player, i => field.GetCell(i, i))) return true;
            }

            //Bottom-left diagonal:
            if (lastMarkedCell.Position.Col + lastMarkedCell.Position.Row + 1 == field.Size)
            {
                if (CheckLine(field, lastMarkedCell.Player, i => field.GetCell(i, field.Size - i - 1))) return true;
            }

            return false;
        }

        /// <summary>
        /// Checks the whole line for cells marked by one player.
        /// </summary>
        /// <param name="field">Game field</param>
        /// <param name="player">Potential winner</param>
        /// <param name="cellGetter">Function that gets cell for the specified line.</param>
        /// <returns>True if the player has won.</returns>
        private bool CheckLine(IField field, Player player, Func<int, Cell> cellGetter)
        {
            for (int i = 0; i < field.Size; i += 1)
            {
                var cell = cellGetter(i);
                if (cell.Player != player)
                {
                    return false;
                }
                _victoryLine[i] = cell;
            }

            return true;
        }
    }
}
