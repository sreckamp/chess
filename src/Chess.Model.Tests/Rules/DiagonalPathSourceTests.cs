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
    public class DiagonalPathSourceTests
    {
        private Mock<IPieceEnumerationProvider> m_mock;

        [SetUp]
        public void Setup()
        {
            m_mock = new Mock<IPieceEnumerationProvider>(MockBehavior.Strict);
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.NorthEast)).Returns(
                () => new[]
                {
                    (new Point(4, 4), Piece.Empty),
                    (new Point(5, 5), Piece.Empty),
                    (new Point(6, 6), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.SouthEast)).Returns(
                () => new[]
                {
                    (new Point(4, 2), Piece.Empty),
                    (new Point(5, 1), Piece.Empty),
                    (new Point(6, 0), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.SouthWest)).Returns(
                () => new[]
                {
                    (new Point(2, 2), Piece.Empty),
                    (new Point(1, 1), Piece.Empty),
                    (new Point(0, 0), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.NorthWest)).Returns(
                () => new[]
                {
                    (new Point(2, 4), Piece.Empty),
                    (new Point(1, 5), Piece.Empty),
                    (new Point(0, 6), Piece.Empty)
                }
            );
        }

        [Test]
        public void GetPaths_WithRookSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithKnightSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithBishopSouth_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 4)),
                        sq => sq.Item1.Should().Be(new Point(5, 5)),
                        sq => sq.Item1.Should().Be(new Point(6, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 2)),
                        sq => sq.Item1.Should().Be(new Point(5, 1)),
                        sq => sq.Item1.Should().Be(new Point(6, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 2)),
                        sq => sq.Item1.Should().Be(new Point(1, 1)),
                        sq => sq.Item1.Should().Be(new Point(0, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 4)),
                        sq => sq.Item1.Should().Be(new Point(1, 5)),
                        sq => sq.Item1.Should().Be(new Point(0, 6)));
                }
            );
        }

        [Test]
        public void GetPaths_WithPawnSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new DiagonalPathSource();

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
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 4)),
                        sq => sq.Item1.Should().Be(new Point(5, 5)),
                        sq => sq.Item1.Should().Be(new Point(6, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 2)),
                        sq => sq.Item1.Should().Be(new Point(5, 1)),
                        sq => sq.Item1.Should().Be(new Point(6, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 2)),
                        sq => sq.Item1.Should().Be(new Point(1, 1)),
                        sq => sq.Item1.Should().Be(new Point(0, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 4)),
                        sq => sq.Item1.Should().Be(new Point(1, 5)),
                        sq => sq.Item1.Should().Be(new Point(0, 6)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenWest_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.West);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 4)),
                        sq => sq.Item1.Should().Be(new Point(5, 5)),
                        sq => sq.Item1.Should().Be(new Point(6, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 2)),
                        sq => sq.Item1.Should().Be(new Point(5, 1)),
                        sq => sq.Item1.Should().Be(new Point(6, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 2)),
                        sq => sq.Item1.Should().Be(new Point(1, 1)),
                        sq => sq.Item1.Should().Be(new Point(0, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 4)),
                        sq => sq.Item1.Should().Be(new Point(1, 5)),
                        sq => sq.Item1.Should().Be(new Point(0, 6)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenNorth_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.North);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 4)),
                        sq => sq.Item1.Should().Be(new Point(5, 5)),
                        sq => sq.Item1.Should().Be(new Point(6, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 2)),
                        sq => sq.Item1.Should().Be(new Point(5, 1)),
                        sq => sq.Item1.Should().Be(new Point(6, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 2)),
                        sq => sq.Item1.Should().Be(new Point(1, 1)),
                        sq => sq.Item1.Should().Be(new Point(0, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 4)),
                        sq => sq.Item1.Should().Be(new Point(1, 5)),
                        sq => sq.Item1.Should().Be(new Point(0, 6)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenEast_ShouldHaveFourPaths()
        {
            // Arrange
            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.East);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 4)),
                        sq => sq.Item1.Should().Be(new Point(5, 5)),
                        sq => sq.Item1.Should().Be(new Point(6, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 2)),
                        sq => sq.Item1.Should().Be(new Point(5, 1)),
                        sq => sq.Item1.Should().Be(new Point(6, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 2)),
                        sq => sq.Item1.Should().Be(new Point(1, 1)),
                        sq => sq.Item1.Should().Be(new Point(0, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 4)),
                        sq => sq.Item1.Should().Be(new Point(1, 5)),
                        sq => sq.Item1.Should().Be(new Point(0, 6)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenSouthAgainstWest_ShouldHaveTwoPaths()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.SouthWest))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.NorthWest))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);

            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 4)),
                        sq => sq.Item1.Should().Be(new Point(5, 5)),
                        sq => sq.Item1.Should().Be(new Point(6, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 2)),
                        sq => sq.Item1.Should().Be(new Point(5, 1)),
                        sq => sq.Item1.Should().Be(new Point(6, 0)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenSouthAgainstNorth_ShouldHaveTwoPaths()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.NorthEast))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.NorthWest))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 2)),
                        sq => sq.Item1.Should().Be(new Point(5, 1)),
                        sq => sq.Item1.Should().Be(new Point(6, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 2)),
                        sq => sq.Item1.Should().Be(new Point(1, 1)),
                        sq => sq.Item1.Should().Be(new Point(0, 0)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenSouthAgainstEast_ShouldHaveTwoPaths()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.NorthEast))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.SouthEast))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 2)),
                        sq => sq.Item1.Should().Be(new Point(1, 1)),
                        sq => sq.Item1.Should().Be(new Point(0, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 4)),
                        sq => sq.Item1.Should().Be(new Point(1, 5)),
                        sq => sq.Item1.Should().Be(new Point(0, 6)));
                }
            );
        }

        [Test]
        public void GetPaths_WithQueenSouthAgainstSouth_ShouldHaveTwoPaths()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.SouthWest))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.SouthEast))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new DiagonalPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);
           
            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 4)),
                        sq => sq.Item1.Should().Be(new Point(5, 5)),
                        sq => sq.Item1.Should().Be(new Point(6, 6)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 4)),
                        sq => sq.Item1.Should().Be(new Point(1, 5)),
                        sq => sq.Item1.Should().Be(new Point(0, 6)));
                }
            );
        }
    }
}