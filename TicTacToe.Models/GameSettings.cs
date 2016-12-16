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
    public class GameSettings : IGameSettings
    {
        /// <summary>
        /// Size of a minimal game field.
        /// </summary>
        public const int MINIMAL_FIELD_SIZE = 3;

        /// <summary>
        /// Minimum amount of players required for the game.
        /// </summary>
        private const int MIN_PLAYER_COUNT = 2;

        /// <summary>
        /// Creates a new instance of <see cref="GameSettings"/> that contains information for game initialization.
        /// </summary>
        /// <param name="fieldSize">Height / width of the square game field. Should be <see cref="MINIMAL_FIELD_SIZE"/> or more.</param>
        /// <param name="victoryConditions">Victory conditions for the game. Can't be null.</param>
        public GameSettings(int fieldSize, IVictoryConditions victoryConditions)
        {
            if (fieldSize < MINIMAL_FIELD_SIZE)
                throw new ArgumentOutOfRangeException(nameof(fieldSize), $"Field size should be more or equal than {MINIMAL_FIELD_SIZE}");

            if(victoryConditions == null)
                throw new ArgumentNullException(nameof(victoryConditions), "victoryConditions can't be null!");

            VictoryConditions = victoryConditions;
            FieldSize = fieldSize;
            _players = new List<Player>();
        }

        /// <summary>
        /// Adds a new player and determines the sequence of turns. Player that added first goes first etc.
        /// </summary>
        /// <param name="player">The player to add to the game.</param>
        public void AddPlayer(Player player)
        {
            foreach (var p in _players)
            {
                if (p == player) throw new ArgumentException("Duplicate player");
                if(p.Symbol == player.Symbol) throw new ArgumentException($"The player with symbol '{player.Symbol}' already exists.");
            }
            _players.Add(player);
        }

        /// <summary>
        /// Gets height / width of the square game field.
        /// </summary>
        public int FieldSize { get; }

        /// <summary>
        /// Internal list of players
        /// </summary>
        private readonly List<Player> _players;

        /// <summary>
        /// Gets read-only list of players.
        /// </summary>
        public IReadOnlyList<Player> Players => _players;

        /// <summary>
        /// Gets an algoritm that determines victory conditions.
        /// </summary>
        public IVictoryConditions VictoryConditions { get; }

        public void Validate()
        {
            if (Players.Count() < MIN_PLAYER_COUNT)
                throw new ArgumentOutOfRangeException($"The game should have at least {MIN_PLAYER_COUNT} players!");
        }
    }
}
