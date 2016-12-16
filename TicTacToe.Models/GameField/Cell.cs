using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.GameField
{
    /// <summary>
    /// Represents a cell on the game field that can be marked by a player.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Creates a new instance of <see cref="Cell"/> with specified position.
        /// </summary>
        /// <param name="position">Position of the cell.</param>
        public Cell(Position position)
        {
            Position = position;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Cell"/> with specified position and marked by specified player.
        /// </summary>
        /// <param name="position">Position of this cell.</param>
        /// <param name="player">Player that marked this cell.</param>
        public Cell(Position position, Player player)
        {
            Position = position;
            Player = player;
        }

        /// <summary>
        /// Gets or sets the player that marked this cell.
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this cell is empty and can be marked.
        /// </summary>
        public bool IsEmpty => Player == null;

        /// <summary>
        /// Gets the position of this cell on game field.
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Marks this cell with symbol of specified player.
        /// </summary>
        /// <param name="player">The player that marks the cell.</param>
        internal void Mark(Player player)
        {
            Player = player;
        }
    }
}
