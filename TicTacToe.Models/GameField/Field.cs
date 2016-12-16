using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.GameField
{
    public class Field
    {
        public int Size { get; }

        private Cell[,] _cells;

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

        public Cell GetCell(int col, int row)
        {
            return _cells[col, row];
        }
    }
}
