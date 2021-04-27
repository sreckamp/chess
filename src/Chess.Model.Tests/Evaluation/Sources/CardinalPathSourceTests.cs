using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Evaluation.Sources;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Tests.Rules
{
    public class CardinalPathSourceTests
    {
        private Mock<IPieceEnumerationProvider> m_mock;

        [SetUp]
        public void Setup()
        {
            m_mock = new Mock<IPieceEnumerationProvider>(MockBehavior.Strict);
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.North)).Returns(
                () => new[]
                {
                    (new Point(3, 4), Piece.Empty),
                    (new Point(3, 5), Piece.Empty),
                    (new Point(3, 6), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.East)).Returns(
                () => new[]
                {
                    (new Point(4, 3), Piece.Empty),
                    (new Point(5, 3), Piece.Empty),
                    (new Point(6, 3), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.South)).Returns(
                () => new[]
                {
                    (new Point(3, 2), Piece.Empty),
                    (new Point(3, 1), Piece.Empty),
                    (new Point(3, 0), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.West)).Returns(
                () => new[]
                {
                    (new Point(2, 3), Piece.Empty),
                    (new Point(1, 3), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );
        }

        [Test]
        public void GetPaths_WithRookSouth_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithKnightSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);

            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithBishopSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);

            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithPawnSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);

            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithQueenSouth_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenWest_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.West);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenNorth_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.North);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenEast_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.East);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenSouthAgainstWest_ShouldHaveThreePaths()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.West))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenSouthAgainstNorth_ShouldHaveThreePaths()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.North))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenSouthAgainstEast_ShouldHaveThreePaths()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.East))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenSouthAgainstSouth_ShouldHaveThreePaths()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.South))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new CardinalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }
    }
}