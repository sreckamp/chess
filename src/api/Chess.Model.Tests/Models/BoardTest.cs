using Chess.Model.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Models
{
    public class BoardTest
    {
        [Test]
        public void ctor_WithTwoPlayers_ShouldHave8x8WithWhiteAt0And1AndBlackAt6And7()
        {
            // Arrange
            // Act
            var board = new GameBoard(8, 0);

            // Assert
            board.Size.Should().Be(8);
            board.Corners.Should().Be(0);

            board.Should().OnlyContain(square => square.Item2.IsEmpty);
        }
    }
}