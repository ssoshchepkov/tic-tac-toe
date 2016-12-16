using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TicTacToe.Models.Tests
{
    public class PlayerTests
    {
        [Theory]
        [InlineData(Symbols.O)]
        [InlineData(Symbols.X)]
        [InlineData("*")]
        public void Player_Should_Contain_Specified_Symbol(string symbol)
        {
            Player player = new Player(symbol);
            Assert.Equal(symbol, player.Symbol);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("")]
        public void Invalid_Symbol_Should_Throw_ArgumentException(string value)
        {
            Exception exception = Record.Exception(() => new Player(value));
            Assert.IsType<ArgumentException>(exception);
        }
    }
}
