using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.VictoryConditions;

namespace TicTacToe.Models
{
    /// <summary>
    /// Contains the basic game informatin: size of field, number of players, sequence of turns, voctory conditions etc.
    /// </summary>
    public interface IGameSettings
    {
        /// <summary>
        /// Gets height / width of the square game field.
        /// </summary>
        int FieldSize { get; }

        /// <summary>
        /// Gets read-only list of players.
        /// </summary>
        IReadOnlyList<Player> Players { get; }

        /// <summary>
        /// Gets an algoritm that determines victory conditions.
        /// </summary>
        IVictoryConditions VictoryConditions { get; }

        /// <summary>
        /// Validates settings.
        /// </summary>
        void Validate();
    }
}
