using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.GameField
{
    /// <summary>
    /// Represents a game field.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Gets the specified cell of this field.
        /// </summary>
        /// <param name="col">Column (x coordinate) of the cell</param>
        /// <param name="row">Row (y coordinate) of the cell</param>
        /// <returns>The specified cell.</returns>
        Cell GetCell(int col, int row);

        /// <summary>
        /// Gets the size of this field.
        /// </summary>
        int Size { get; }
    }
}
