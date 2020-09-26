using Chess.Model.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Models
{
    public class BoardTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Enumerate_WithoutCorners_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0);
            
            board.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                }
            );
        }

        [Test]
        public void Enumerate_WithCorners_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(4, 1);
            
            board.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(3);
                }
            );
        }
    }
}
