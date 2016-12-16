using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class GameSettings
    {
        public GameSettings(int fieldSize)
        {
            FieldSize = fieldSize;
        }

        public int FieldSize { get; }
    }
}
