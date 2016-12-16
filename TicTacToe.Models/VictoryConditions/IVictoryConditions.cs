using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;

namespace TicTacToe.Models.VictoryConditions
{
    public interface IVictoryConditions
    {
        bool IsVictory(Cell lastMarkedCell);

        Cell[] FindVictoryLine(Cell lastMarkedCell);
    }
}
