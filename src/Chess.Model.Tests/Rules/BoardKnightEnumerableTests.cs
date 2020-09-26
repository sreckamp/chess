using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Rules
{
    public class BoardKnightEnumerableTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Enumerate_WhenInCenterOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            var enumerable = new BoardKnightEnumerable(board, start);
            
            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(4);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(4);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(3);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenOffTopOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,3);
            var enumerable = new BoardKnightEnumerable(board, start);
            
            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(4);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(4);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenOffBottomOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,1);
            var enumerable = new BoardKnightEnumerable(board, start);
            
            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(2);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenOffRightOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(3,2);
            var enumerable = new BoardKnightEnumerable(board, start);
            
            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(4);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(4);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(3);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenOffLeftOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(1,2);
            var enumerable = new BoardKnightEnumerable(board, start);
            
            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(4);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(4);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(0);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(0);
                }
            );
        }
    }
}
