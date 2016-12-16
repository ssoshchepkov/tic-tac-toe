using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class Player
    {
        public string Symbol { get; }

        public Player(string symbol)
        {
            Symbol = symbol;
        }
    }
}
