using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.VictoryConditions;
using Xunit;

namespace TicTacToe.Models.Tests
{
    public class GameSettingsTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(-5)]
        public void Small_FileSize_Should_Throw_ArgumentOutOfRangeException(int value)
        {
            Exception exception = Record.Exception(() => new GameSettings(value, Substitute.For<IVictoryConditions>()));
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public void Adding_Duplicate_Player_Should_Throw_ArgumentException()
        {
            Player p = new Player(Symbols.X);
            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, Substitute.For<IVictoryConditions>());

            // Adding the player for the first time.
            settings.AddPlayer(p);

            // Attempting to add the player for the second time.
            Exception exception = Record.Exception(() => settings.AddPlayer(p));
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void Adding_Player_With_Duplicate_Symbol_Should_Throw_ArgumentException()
        {
            // Two players, both X:
            Player p1 = new Player(Symbols.X);
            Player p2 = new Player(Symbols.X);

            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, Substitute.For<IVictoryConditions>());

            settings.AddPlayer(p1);

            Exception exception = Record.Exception(() => settings.AddPlayer(p2));

            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void Added_Player_Should_Be_Saved()
        {
            Player p1 = new Player(Symbols.X);
         
            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, Substitute.For<IVictoryConditions>());

            settings.AddPlayer(p1);

            Assert.NotEmpty(settings.Players);
            Assert.Same(p1, settings.Players[0]);
        }

        [Fact]
        public void Added_Players_Should_Be_Saved_Sequentally()
        {
            Player p1 = new Player(Symbols.X);
            Player p2 = new Player(Symbols.O);

            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, Substitute.For<IVictoryConditions>());

            settings.AddPlayer(p1);
            settings.AddPlayer(p2);

            Assert.Same(p1, settings.Players[0]);
            Assert.Same(p2, settings.Players[1]);
        }

        [Fact]
        public void Null_VictoryConditions_Shold_Throw_ArgumentNullException()
        {
            Exception exception = Record.Exception(() => new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, null));
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void VictoryConditions_Should_Be_Saved()
        {
            var conditions = Substitute.For<IVictoryConditions>();
            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, conditions);

            Assert.Same(conditions, settings.VictoryConditions);
        }

        [Theory]
        [InlineData(GameSettings.MINIMAL_FIELD_SIZE)]
        [InlineData(4)]
        [InlineData(10)]
        public void FieldSize_Should_Be_Saved(int value)
        {
            GameSettings settings = new GameSettings(value, Substitute.For<IVictoryConditions>());

            Assert.Equal(value, settings.FieldSize);
        }

        [Fact]
        public void For_Less_Than_Two_Player_Validate_Should_Throw_ArgumentOutOfRangeException()
        {
            GameSettings settings = new GameSettings(GameSettings.MINIMAL_FIELD_SIZE, Substitute.For<IVictoryConditions>());

            settings.AddPlayer(new Player(Symbols.X)); // add one player

            Exception exception = Record.Exception(() => settings.Validate());
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }
    }
}
