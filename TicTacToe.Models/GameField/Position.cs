using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.GameField
{
    /// <summary>
    /// Represents position of a cell on a game field.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Gets row of the cell.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Gets column of the cell.
        /// </summary>
        public int Col { get; }

        /// <summary>
        /// Creates a new instance of <see cref="Position"/> with specified coordinates.
        /// </summary>
        /// <param name="col">Column</param>
        /// <param name="row">Row</param>
        public Position(int col, int row)
        {
            Row = row;
            Col = col;
        }
    }
}
