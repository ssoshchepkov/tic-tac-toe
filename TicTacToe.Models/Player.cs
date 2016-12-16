using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    /// <summary>
    /// Represents the game player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Symbol that used to mark squares.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Creates a new player with specified symbol to mark squares.
        /// </summary>
        /// <param name="symbol">Player's symbol for square marking. Can't be null, empty or whitespace-only.</param>
        public Player(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol), "The symbol can't be null or empty / whitespace-only string");

            Symbol = symbol;
        }
    }
}
