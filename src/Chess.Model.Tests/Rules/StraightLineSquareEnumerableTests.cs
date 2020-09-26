using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Rules
{
    public class StraightLineSquareEnumerableTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Enumerate_WhenNorthWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.NorthWest;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

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
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(4);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenNorth_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.North;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(4);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenNorthEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.NorthEast;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(4);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.West;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(2);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenNone_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.None;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

            // Act
            // Assert
            enumerable.Should().BeEmpty();
        }
        
        [Test]
        public void Enumerate_WhenEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.East;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(2);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenSouthWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.SouthWest;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(0);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenSouth_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.South;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(0);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenSouthEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.SouthEast;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(0);
                }
            );
        }

        [Test]
        public void Enumerate_WhenNorthWestIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.NorthWest;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(4);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenNorthIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.North;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(4);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenNorthEastIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.NorthEast;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(3);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(4);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenWestIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.West;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(2);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenNoneIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.None;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenEastIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.East;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
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
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(2);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenSouthWestIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.SouthWest;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(1);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(0);
                    square.Location.Y.Should().Be(0);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenSouthIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.South;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(0);
                }
            );
        }
        
        [Test]
        public void Enumerate_WhenSouthEastIncludingStart_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0);
            var start = new Point(2,2);
            const Direction direction = Direction.SouthEast;
            var enumerable = new BoardStraightLineEnumerable(board, start, direction, true);

            // Act
            // Assert
            enumerable.Should().SatisfyRespectively(
                square =>
                {
                    square.Location.X.Should().Be(2);
                    square.Location.Y.Should().Be(2);
                },
                square =>
                {
                    square.Location.X.Should().Be(3);
                    square.Location.Y.Should().Be(1);
                },
                square =>
                {
                    square.Location.X.Should().Be(4);
                    square.Location.Y.Should().Be(0);
                }
            );
        }
    }
}
