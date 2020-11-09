using System.Drawing;
using Chess.Model.Move;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Move
{
    public class CastleMoveTests
    {
        [Test]
        public void ToString_WhenKingSideWhite_ShouldBeOO()
        {
            // Arrange
            var move = new CastleMove(
                new SimpleMove(new Point(4, 0), new Point(6, 0)),
                new SimpleMove(new Point(7, 0), new Point(5, 0))
            );

            // Act
            var result = move.ToString();

            // Assert
            result.Should().Be("O-O");
        }

        [Test]
        public void ToString_WhenQueenSideWhite_ShouldBeOOO()
        {
            // Arrange
            var move = new CastleMove(
                new SimpleMove(new Point(4, 0), new Point(2, 0)),
                new SimpleMove(new Point(0, 0), new Point(3, 0))
            );

            // Act
            var result = move.ToString();

            // Assert
            result.Should().Be("O-O-O");
        }

        [Test]
        public void ToString_WhenKingSideBlack_ShouldBeOO()
        {
            // Arrange
            var move = new CastleMove(
                new SimpleMove(new Point(4, 7), new Point(6, 7)),
                new SimpleMove(new Point(7, 7), new Point(5, 7))
            );

            // Act
            var result = move.ToString();

            // Assert
            result.Should().Be("O-O");
        }

        [Test]
        public void ToString_WhenQueenSideBlack_ShouldBeOOO()
        {
            // Arrange
            var move = new CastleMove(
                new SimpleMove(new Point(4, 7), new Point(2, 7)),
                new SimpleMove(new Point(0, 7), new Point(3, 7))
            );

            // Act
            var result = move.ToString();

            // Assert
            result.Should().Be("O-O-O");
        }
    }
}
