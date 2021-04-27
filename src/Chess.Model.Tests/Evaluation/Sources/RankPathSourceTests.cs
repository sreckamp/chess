using System.Drawing;
using Chess.Model.Evaluation.Models;
using Chess.Model.Evaluation.Sources;
using Chess.Model.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Tests.Evaluation.Sources
{
    public class RankPathSourceTests
    {
        [Test]
        public void GetPaths_WithRookSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithKnightSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();

        }

        [Test]
        public void GetPaths_WithBishopSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();

        }

        [Test]
        public void GetPaths_WithPawnSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();

        }

        [Test]
        public void GetPaths_WithQueenSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();

        }

        [Test]
        public void GetPaths_WithKingSouthThatMoved_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            piece.Moved();
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();

        }

        [Test]
        public void GetPaths_WithKingSouth_ShouldHaveTwoPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.East)).Returns(
                () => new[]
                {
                    (new Point(4, 3), Piece.Empty),
                    (new Point(5, 3), Piece.Empty),
                    (new Point(6, 3), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.West)).Returns(
                () => new[]
                {
                    (new Point(2, 3), Piece.Empty),
                    (new Point(1, 3), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3))
                    );
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3))
                    );
                }
            );
        }

        [Test]
        public void GetPaths_WithKingNorth_ShouldHaveTwoPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>(MockBehavior.Strict);
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.East)).Returns(
                () => new[]
                {
                    (new Point(4, 3), Piece.Empty),
                    (new Point(5, 3), Piece.Empty),
                    (new Point(6, 3), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.West)).Returns(
                () => new[]
                {
                    (new Point(2, 3), Piece.Empty),
                    (new Point(1, 3), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.King, Color.White, Direction.North);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.East);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(4, 3)),
                        sq => sq.Item1.Should().Be(new Point(5, 3)),
                        sq => sq.Item1.Should().Be(new Point(6, 3))
                    );
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(1, 3)),
                        sq => sq.Item1.Should().Be(new Point(0, 3))
                    );
                }
            );
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths_WithKingWest_ShouldHaveTwoPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>(MockBehavior.Strict);
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.North)).Returns(
                () => new[]
                {
                    (new Point(3, 4), Piece.Empty),
                    (new Point(3, 5), Piece.Empty),
                    (new Point(3, 6), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.South)).Returns(
                () => new[]
                {
                    (new Point(3, 2), Piece.Empty),
                    (new Point(3, 1), Piece.Empty),
                    (new Point(3, 0), Piece.Empty)
                }
            );

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.King, Color.White, Direction.West);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6))
                    );
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0))
                    );
                }
            );
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths_WithKingEast_ShouldHaveTwoPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>(MockBehavior.Strict);
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.North)).Returns(
                () => new[]
                {
                    (new Point(3, 4), Piece.Empty),
                    (new Point(3, 5), Piece.Empty),
                    (new Point(3, 6), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(3, 3), Direction.South)).Returns(
                () => new[]
                {
                    (new Point(3, 2), Piece.Empty),
                    (new Point(3, 1), Piece.Empty),
                    (new Point(3, 0), Piece.Empty)
                }
            );

            var start = new Point(3, 3);
            var piece = new Piece(PieceType.King, Color.White, Direction.East);
            
            var dut = new RankPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.North);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(3, 4)),
                        sq => sq.Item1.Should().Be(new Point(3, 5)),
                        sq => sq.Item1.Should().Be(new Point(3, 6))
                    );
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(3, 1)),
                        sq => sq.Item1.Should().Be(new Point(3, 0))
                    );
                }
            );
            mock.VerifyAll();
        }
    }
}
