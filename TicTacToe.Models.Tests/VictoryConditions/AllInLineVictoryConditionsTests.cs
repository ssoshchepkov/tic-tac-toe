using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.GameField;
using TicTacToe.Models.VictoryConditions;
using Xunit;

namespace TicTacToe.Models.Tests.VictoryConditions
{
    public class AllInLineVictoryConditionsTests
    {
        [Fact]
        public void Should_Return_Nothing_For_No_Winners()
        {
            Player player = new Player(Symbols.X);

            var field = Substitute.For<IField>();
            field.Size.Returns(3);
            // Setting up the filed.
            field.GetCell(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
            {
                // The cell(1,1) is marked by X.
                if(x.ArgAt<int>(0) == 1 && x.ArgAt<int>(1) == 1)
                    return new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)), player);
                // The others are unmarked.
                return new Cell(new Position(x.ArgAt<int>(0), x.ArgAt<int>(1)));
            });

            Cell lastCell = field.GetCell(1, 1);

            Assert.Same(player, lastCell.Player);

            AllInLineVictoryConditions conditions = new AllInLineVictoryConditions();

            var result = conditions.FindVictoryLine(field, lastCell);

            Assert.Null(result);
        }

        [Fact]
        public void Should_Return_Row()
        {
            Player player = new Player(Symbols.X);

            var field = Substitute.For<IField>();
            field.Size.Returns(3);

            Cell c00 = new Cell(new Position(0, 0));
            Cell c10 = new Cell(new Position(1, 0));
            Cell c20 = new Cell(new Position(2, 0));

            Cell c01 = new Cell(new Position(0, 1), player);
            Cell c11 = new Cell(new Position(1, 1), player);
            Cell c21 = new Cell(new Position(2, 1), player);

            Cell c02 = new Cell(new Position(0, 2));
            Cell c12 = new Cell(new Position(1, 2));
            Cell c22 = new Cell(new Position(2, 2));

            field.GetCell(0, 0).Returns(x => c00);
            field.GetCell(1, 0).Returns(x => c10);
            field.GetCell(2, 0).Returns(x => c20);

            field.GetCell(0, 1).Returns(x => c01);
            field.GetCell(1, 1).Returns(x => c11);
            field.GetCell(2, 1).Returns(x => c21);

            field.GetCell(0, 2).Returns(x => c02);
            field.GetCell(1, 2).Returns(x => c12);
            field.GetCell(2, 2).Returns(x => c22);

            Cell lastCell = field.GetCell(1, 1);

            AllInLineVictoryConditions conditions = new AllInLineVictoryConditions();

            var result = conditions.FindVictoryLine(field, lastCell);

            Assert.NotNull(result);
         
            Assert.Contains(field.GetCell(0, 1), result);
            Assert.Contains(field.GetCell(1, 1), result);
            Assert.Contains(field.GetCell(2, 1), result);
        }

        [Fact]
        public void Should_Return_Column()
        {
            Player player = new Player(Symbols.X);

            var field = Substitute.For<IField>();
            field.Size.Returns(3);

            Cell c00 = new Cell(new Position(0, 0));
            Cell c10 = new Cell(new Position(1, 0), player);
            Cell c20 = new Cell(new Position(2, 0));

            Cell c01 = new Cell(new Position(0, 1));
            Cell c11 = new Cell(new Position(1, 1), player);
            Cell c21 = new Cell(new Position(2, 1));

            Cell c02 = new Cell(new Position(0, 2));
            Cell c12 = new Cell(new Position(1, 2), player);
            Cell c22 = new Cell(new Position(2, 2));

            field.GetCell(0, 0).Returns(x => c00);
            field.GetCell(1, 0).Returns(x => c10);
            field.GetCell(2, 0).Returns(x => c20);

            field.GetCell(0, 1).Returns(x => c01);
            field.GetCell(1, 1).Returns(x => c11);
            field.GetCell(2, 1).Returns(x => c21);

            field.GetCell(0, 2).Returns(x => c02);
            field.GetCell(1, 2).Returns(x => c12);
            field.GetCell(2, 2).Returns(x => c22);

            Cell lastCell = field.GetCell(1, 1);

            AllInLineVictoryConditions conditions = new AllInLineVictoryConditions();

            var result = conditions.FindVictoryLine(field, lastCell);

            Assert.NotNull(result);

            Assert.Contains(field.GetCell(1, 0), result);
            Assert.Contains(field.GetCell(1, 1), result);
            Assert.Contains(field.GetCell(1, 2), result);
        }

        [Fact]
        public void Should_Return_Top_Left_Diagonal()
        {
            Player player = new Player(Symbols.X);

            var field = Substitute.For<IField>();
            field.Size.Returns(3);

            Cell c00 = new Cell(new Position(0, 0), player);
            Cell c10 = new Cell(new Position(1, 0));
            Cell c20 = new Cell(new Position(2, 0));

            Cell c01 = new Cell(new Position(0, 1));
            Cell c11 = new Cell(new Position(1, 1), player);
            Cell c21 = new Cell(new Position(2, 1));

            Cell c02 = new Cell(new Position(0, 2));
            Cell c12 = new Cell(new Position(1, 2));
            Cell c22 = new Cell(new Position(2, 2), player);

            field.GetCell(0, 0).Returns(x => c00);
            field.GetCell(1, 0).Returns(x => c10);
            field.GetCell(2, 0).Returns(x => c20);

            field.GetCell(0, 1).Returns(x => c01);
            field.GetCell(1, 1).Returns(x => c11);
            field.GetCell(2, 1).Returns(x => c21);

            field.GetCell(0, 2).Returns(x => c02);
            field.GetCell(1, 2).Returns(x => c12);
            field.GetCell(2, 2).Returns(x => c22);

            Cell lastCell = field.GetCell(1, 1);

            AllInLineVictoryConditions conditions = new AllInLineVictoryConditions();

            var result = conditions.FindVictoryLine(field, lastCell);

            Assert.NotNull(result);

            Assert.Contains(field.GetCell(0, 0), result);
            Assert.Contains(field.GetCell(1, 1), result);
            Assert.Contains(field.GetCell(2, 2), result);
        }

        [Fact]
        public void Should_Return_Bottom_Left_Diagonal()
        {
            Player player = new Player(Symbols.X);

            var field = Substitute.For<IField>();
            field.Size.Returns(3);

            Cell c00 = new Cell(new Position(0, 0));
            Cell c10 = new Cell(new Position(1, 0));
            Cell c20 = new Cell(new Position(2, 0), player);

            Cell c01 = new Cell(new Position(0, 1));
            Cell c11 = new Cell(new Position(1, 1), player);
            Cell c21 = new Cell(new Position(2, 1));

            Cell c02 = new Cell(new Position(0, 2), player);
            Cell c12 = new Cell(new Position(1, 2));
            Cell c22 = new Cell(new Position(2, 2));

            field.GetCell(0, 0).Returns(x => c00);
            field.GetCell(1, 0).Returns(x => c10);
            field.GetCell(2, 0).Returns(x => c20);

            field.GetCell(0, 1).Returns(x => c01);
            field.GetCell(1, 1).Returns(x => c11);
            field.GetCell(2, 1).Returns(x => c21);

            field.GetCell(0, 2).Returns(x => c02);
            field.GetCell(1, 2).Returns(x => c12);
            field.GetCell(2, 2).Returns(x => c22);

            Cell lastCell = field.GetCell(1, 1);

            AllInLineVictoryConditions conditions = new AllInLineVictoryConditions();

            var result = conditions.FindVictoryLine(field, lastCell);

            Assert.NotNull(result);

            Assert.Contains(field.GetCell(0, 2), result);
            Assert.Contains(field.GetCell(1, 1), result);
            Assert.Contains(field.GetCell(2, 0), result);
        }
    }
}
