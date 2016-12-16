using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;

namespace TicTacToe.Models
{
    public class GameResult
    {
        public GameResult(Player winner, IList<Cell> victoryLine)
        {
            VictoryLine = victoryLine;
            Winner = winner;
        }

        public GameResult() { }

        public IList<Cell> VictoryLine { get; }
        public Player Winner { get; }

        public bool HasWinner => Winner != null;
    }
}
