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
    public class KingPathSourceTests
    {
        private Mock<IPieceEnumerationProvider> m_mock;

        [SetUp]
        public void Setup()
        {
            m_mock = new Mock<IPieceEnumerationProvider>(MockBehavior.Strict);
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.North)).Returns(
                () => new[]
                {
                    (new Point(2, 3), Piece.Empty),
                    (new Point(2, 4), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast)).Returns(
                () => new[]
                {
                    (new Point(3, 3), Piece.Empty),
                    (new Point(4, 4), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.East)).Returns(
                () => new[]
                {
                    (new Point(3, 2), Piece.Empty),
                    (new Point(4, 2), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthEast)).Returns(
                () => new[]
                {
                    (new Point(3, 1), Piece.Empty),
                    (new Point(4, 0), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.South)).Returns(
                () => new[]
                {
                    (new Point(2, 1), Piece.Empty),
                    (new Point(2, 0), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthWest)).Returns(
                () => new[]
                {
                    (new Point(1, 1), Piece.Empty),
                    (new Point(0, 0), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.West)).Returns(
                () => new[]
                {
                    (new Point(1, 2), Piece.Empty),
                    (new Point(0, 2), Piece.Empty)
                }
            );
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest)).Returns(
                () => new[]
                {
                    (new Point(1, 3), Piece.Empty),
                    (new Point(0, 4), Piece.Empty)
                }
            );
        }

        [Test]
        public void GetPaths_WhenRook_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);
            
            var dut = new KingPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WhenKnight_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new KingPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WhenBishop_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);
            
            var dut = new KingPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WhenQueen_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new KingPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WhenPawn_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new KingPathSource();

            // Act
            var result = dut.GetPaths(start, piece, m_mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WhenInCenterOfBoard_ShouldHaveEightPaths()
        {
            // Arrange
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new KingPathSource();

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
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 2)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WhenOffTopOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.North))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new KingPathSource();

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
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 2)));
                }
            );
        }
        
        [Test]
        public void GetPaths_WhenOffBottomOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthWest))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.South))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthEast))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new KingPathSource();

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
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 2)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 3)));
                }
            );
        }
        
        [Test]
        public void GetPaths_WhenOffRightOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.East))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthEast))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new KingPathSource();

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
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 2)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 3)));
                }
            );
        }
        
        [Test]
        public void GetPaths_WhenOffLeftOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.West))
                .Returns(Enumerable.Empty<(Point, Piece)>());
            m_mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthWest))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new KingPathSource();

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
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 2)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 1)));
                }
            );
        }
    }
}