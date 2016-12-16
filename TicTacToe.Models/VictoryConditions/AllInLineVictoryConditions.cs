using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;

namespace TicTacToe.Models.VictoryConditions
{
    public class AllInLineVictoryConditions : IVictoryConditions
    {
        private readonly Field _field;

        private readonly Cell[] _victoryLine;

        public AllInLineVictoryConditions(Field field)
        {
            _field = field;
            _victoryLine = new Cell[_field.Size];
        }

        public Cell[] FindVictoryLine(Cell lastMarkedCell)
        {
            if(IsVictory(lastMarkedCell))
            {
                return _victoryLine;
            }
            return null;
        }

        public bool IsVictory(Cell lastMarkedCell)
        {
            if (CheckRow(lastMarkedCell) || CheckCol(lastMarkedCell) || CheckDiagonal(lastMarkedCell))
            {
                return true;
            }

            return false;
        }

        private bool CheckRow(Cell lastMarkedCell)
        {
            return CheckLine(lastMarkedCell, col => _field.GetCell(col, lastMarkedCell.Position.Row));
        }

        private bool CheckCol(Cell lastMarkedCell)
        {
            return CheckLine(lastMarkedCell, row => _field.GetCell(lastMarkedCell.Position.Col, row));
        }

        private bool CheckDiagonal(Cell lastMarkedCell)
        {
            // Top-left diagonal:
            if (lastMarkedCell.Position.Col == lastMarkedCell.Position.Row)
            {
                if (CheckLine(lastMarkedCell, i => _field.GetCell(i, i))) return true;
            }

            //Bottom-left diagonal:
            if (lastMarkedCell.Position.Col + lastMarkedCell.Position.Row + 1 == _field.Size)
            {
                if (CheckLine(lastMarkedCell, i => _field.GetCell(i, _field.Size - i - 1))) return true;
            }

            return false;
        }


        private bool CheckLine(Cell lastMarkedCell, Func<int, Cell> cellGetter)
        {
            for (int i = 0; i < _field.Size; i += 1)
            {
                var cell = cellGetter(i);
                if (cell.Player != lastMarkedCell.Player)
                {
                    return false;
                }
                _victoryLine[i] = cell;
            }

            return true;
        }
    }
}
