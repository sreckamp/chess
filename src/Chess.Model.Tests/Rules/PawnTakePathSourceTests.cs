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
    public sealed class PawnTakePathSourceTests
    {
        [Test]
        public void GetPaths_WithRookSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths_WithBishopSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths_WithQueenSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths_WithKingSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths_WithKnightSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths_PawnSouthWhenInCenterOfBoard_ShouldHaveTwoPathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest)).Returns(
                () => new[]
                {
                    (new Point(1, 3), Piece.Empty),
                    (new Point(0, 4), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast)).Returns(
                () => new[]
                {
                    (new Point(3, 3), Piece.Empty),
                    (new Point(4, 4), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 3)));
                }
            );
        }
        
        [Test]
        public void GetPaths_PawnWestWhenInCenterOfBoard_ShouldHaveTwoPathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthEast)).Returns(
                () => new[]
                {
                    (new Point(3, 1), Piece.Empty),
                    (new Point(4, 0), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast)).Returns(
                () => new[]
                {
                    (new Point(3, 3), Piece.Empty),
                    (new Point(4, 4), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.West);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 3)));
                }
            );
        }
        
        [Test]
        public void GetPaths_PawnNorthWhenInCenterOfBoard_ShouldHaveTwoPathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthWest)).Returns(
                () => new[]
                {
                    (new Point(1, 1), Piece.Empty),
                    (new Point(0, 0), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthEast)).Returns(
                () => new[]
                {
                    (new Point(3, 1), Piece.Empty),
                    (new Point(4, 0), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.North);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 1)));
                }
            );
        }
        
        [Test]
        public void GetPaths_PawnEastWhenInCenterOfBoard_ShouldHaveTwoPathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest)).Returns(
                () => new[]
                {
                    (new Point(1, 3), Piece.Empty),
                    (new Point(0, 4), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.SouthWest)).Returns(
                () => new[]
                {
                    (new Point(1, 1), Piece.Empty),
                    (new Point(0, 0), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.East);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.SouthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 1)));
                }
            );
        }
        
        [Test]
        public void GetPaths_PawnSouthWhenAgainstTopOfBoard_ShouldHaveNoPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest))
                .Returns( Enumerable.Empty<(Point, Piece)>());
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast))
                .Returns( Enumerable.Empty<(Point, Piece)>());

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }
        
        [Test]
        public void GetPaths_PawnSouthWhenAgainstRightOfBoard_ShouldHaveOnePath()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest)).Returns(
                () => new[]
                {
                    (new Point(1, 3), Piece.Empty),
                    (new Point(0, 4), Piece.Empty)
                }
            );
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast))
                .Returns( Enumerable.Empty<(Point, Piece)>());

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthWest);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 3)));
                }
            );
        }
        
        [Test]
        public void GetPaths_PawnSouthWhenAgainstLeftOfBoard_ShouldHaveOnePath()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest))
                .Returns( Enumerable.Empty<(Point, Piece)>());
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast)).Returns(
                () => new[]
                {
                    (new Point(3, 3), Piece.Empty),
                    (new Point(4, 4), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new PawnTakePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.NorthEast);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeFalse();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 3)));
                }
            );
        }
    }
}