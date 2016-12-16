using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.GameField
{
    public class Cell
    {
        public Cell(Position position)
        {
            Position = position;
        }

        public Player Player { get; private set; }

        public bool IsEmpty => Player == null;

        public Position Position { get; }

        internal void SetPlayer(Player player)
        {
            Player = player;
        }
    }
}
