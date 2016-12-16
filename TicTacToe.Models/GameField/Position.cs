using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.GameField
{
    public class Position
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int col, int row)
        {
            Row = row;
            Col = col;
        }
    }
}
