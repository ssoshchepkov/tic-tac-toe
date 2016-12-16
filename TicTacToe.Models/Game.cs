using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;
using TicTacToe.Models.VictoryConditions;

namespace TicTacToe.Models
{
    public class Game
    {
        const int FIELD_SIZE = 3;

        private int _currentPlayerIndex;

        public int Turn { get; private set; }

        public bool IsGameOver => Result != null;

        /// <summary>
        /// Zero-indexed number of the last game turn.
        /// </summary>
        private readonly int _maxTurn;

        private readonly IVictoryConditions _victoryConditions;

        public Game(GameSettings settings)
        {
            _players = new List<Player>();
            _players.Add(new Player(Symbols.X));
            _players.Add(new Player(Symbols.O));

            Field = new Field(settings.FieldSize);

            _victoryConditions = new AllInLineVictoryConditions(Field);

            // turns needed to mark all cells
            _maxTurn = Field.Size * Field.Size - 1;
        }

        public Field Field { get; }

        private List<Player> _players;

        public Player CurrentPlayer => _players[_currentPlayerIndex];

        public GameResult Result { get; private set; }

        public bool HasWinner => Result?.HasWinner ?? false;

        public void MarkCell(Cell cell)
        {
            cell.SetPlayer(CurrentPlayer);

            var victoryLine = _victoryConditions.FindVictoryLine(cell);

            if (victoryLine != null)
            {
                Result = new GameResult(CurrentPlayer, victoryLine.ToList());
                return;
            }

            // Players marked all the field. A tie.
            if (Turn == _maxTurn)
            {
                Result = new GameResult();
            }
            else
            {
                NextTurn();
            }
        }

        private void NextTurn()
        {
            if(_currentPlayerIndex == _players.Count - 1)
            {
                _currentPlayerIndex = 0;
            }else
            {
                _currentPlayerIndex += 1;
            }

            Turn += 1;
        }
    }
}
