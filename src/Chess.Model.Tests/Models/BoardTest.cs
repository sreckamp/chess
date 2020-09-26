using Chess.Model.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Models
{
    public class BoardTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ctor_WithTwoPlayers_ShouldHave8x8WithWhiteAt0And1AndBlackAt6And7()
        {
            // Arrange
            // Act
            var board = new GameBoard(8, 0);

            // Assert
            board.Size.Should().Be(8);
            board.Corners.Should().Be(0);
            for(var y=0; y < board.Size; y++)
            {
                for(var x=0; x < board.Size; x++)
                {
                    board.GetSquare(x, y).Location.X.Should().Be(x);
                    board.GetSquare(x, y).Location.Y.Should().Be(y);
                }
            }

            board.Should().OnlyContain(square => square.IsEmpty);
        }
    }
}