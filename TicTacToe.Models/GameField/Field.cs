using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.GameField
{
    /// <summary>
    /// Represents a game field implementation.
    /// </summary>
    public class Field : IField
    {
        /// <summary>
        /// Gets the size of this field.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Inner representation of the field as a 2-dimensional array of cells.
        /// </summary>
        private Cell[,] _cells;

        /// <summary>
        /// Creates a new instance of <see cref="Field"/> with specified size.
        /// </summary>
        /// <param name="size">The size of this field.</param>
        public Field(int size)
        {
            Size = size;

            _cells = new Cell[Size, Size];

            for (int row = 0; row < Size; row += 1)
            {
                for (int col = 0; col < Size; col += 1)
                {
                    _cells[col, row] = new Cell(new Position(col, row));
                }
            }
        }

        /// <summary>
        /// Gets the specified cell of this field.
        /// </summary>
        /// <param name="col">Column (x coordinate) of the cell</param>
        /// <param name="row">Row (y coordinate) of the cell</param>
        /// <returns>The specified cell.</returns>
        public Cell GetCell(int col, int row)
        {
            return _cells[col, row];
        }
    }
}
