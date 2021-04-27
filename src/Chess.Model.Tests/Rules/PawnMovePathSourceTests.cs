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
    public sealed class PawnMovePathSourceTests
    {
        [Test]
        public void GetPaths_WithRookSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);

            var dut = new PawnMovePathSource();

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
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);

            var dut = new PawnMovePathSource();

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
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);

            var dut = new PawnMovePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithKingSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);

            var dut = new PawnMovePathSource();

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

            var dut = new PawnMovePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths_PawnSouthWhenInCenterOfBoard_ShouldHaveOnePathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.North)).Returns(
                () => new[]
                {
                    (new Point(2, 3), Piece.Empty),
                    (new Point(2, 4), Piece.Empty),
                    (new Point(2, 5), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);

            var dut = new PawnMovePathSource();

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
                        sq => sq.Item1.Should().Be(new Point(2, 3)),
                        sq => sq.Item1.Should().Be(new Point(2, 4))
                    );
                }
            );
        }

        [Test]
        public void GetPaths_PawnSouthWhenInCenterOfBoardWhenMoved_ShouldHaveOnePathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.North)).Returns(
                () => new[]
                {
                    (new Point(2, 3), Piece.Empty),
                    (new Point(2, 4), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            piece.Moved();

            var dut = new PawnMovePathSource();

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
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(2, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_PawnWestWhenInCenterOfBoard_ShouldHaveOnePathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.East)).Returns(
                () => new[]
                {
                    (new Point(3, 2), Piece.Empty),
                    (new Point(4, 2), Piece.Empty),
                    (new Point(5, 2), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.West);

            var dut = new PawnMovePathSource();

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
                        sq => sq.Item1.Should().Be(new Point(3, 2)),
                        sq => sq.Item1.Should().Be(new Point(4, 2))
                    );
                }
            );
        }

        [Test]
        public void GetPaths_PawnNorthWhenInCenterOfBoard_ShouldHaveOnePathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.South)).Returns(
                () => new[]
                {
                    (new Point(2, 1), Piece.Empty),
                    (new Point(2, 0), Piece.Empty),
                    (new Point(2, -1), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.North);

            var dut = new PawnMovePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.South);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(2, 1)),
                        sq => sq.Item1.Should().Be(new Point(2, 0))
                    );
                }
            );
        }

        [Test]
        public void GetPaths_PawnEastWhenInCenterOfBoard_ShouldHaveOnePathsWithOneCount()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.West)).Returns(
                () => new[]
                {
                    (new Point(1, 2), Piece.Empty),
                    (new Point(0, 2), Piece.Empty),
                    (new Point(-1, 2), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.East);

            var dut = new PawnMovePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.West);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeFalse();
                    path.Squares.Should().SatisfyRespectively(
                        sq => sq.Item1.Should().Be(new Point(1, 2)),
                        sq => sq.Item1.Should().Be(new Point(0, 2))
                    );
                }
            );
        }

        [Test]
        public void GetPaths_PawnSouthWhenAgainstTopOfBoard_ShouldHaveNoPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.North))
                .Returns(Enumerable.Empty<(Point, Piece)>());

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);

            var dut = new PawnMovePathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        // [Test]
        // public void GetPaths_WhenNotMoved_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
        //     var board = new GameBoard(5, 0) {[2, 1] = piece};
        //     var source = new PawnMovePathSource();
        //
        //     var square = board.GetSquare(2, 1);
        //
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(1);
        //     result.First().Direction.Should().Be(Direction.North);
        //     result.First().AllowMove.Should().BeTrue();
        //     result.First().AllowTake.Should().BeFalse();
        //     result.First().Moves.Should().HaveCount(2);
        //     result.First().Moves.First().Should().BeOfType<SimpleMove>();
        //     result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(2, 1));
        //     result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(2, 2));
        //     result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
        //     result.First().Moves.Last().Should().BeOfType<PawnOpenMove>();
        //     result.First().Moves.Last().As<PawnOpenMove>().From.Should().Be(new Point(2, 1));
        //     result.First().Moves.Last().As<PawnOpenMove>().To.Should().Be(new Point(2, 3));
        //     result.First().Moves.Last().As<PawnOpenMove>().Piece.Should().Be(piece);
        // }
        //
        // [Test]
        // public void GetPaths_WhenMoved_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
        //     piece.Moved();
        //     var board = new GameBoard(5, 0) {[2, 1] = piece};
        //     var source = new PawnMovePathSource();
        //
        //     var square = board.GetSquare(2, 1);
        //
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(1);
        //     result.First().Direction.Should().Be(Direction.North);
        //     result.First().AllowMove.Should().BeTrue();
        //     result.First().AllowTake.Should().BeFalse();
        //     result.First().Moves.Should().HaveCount(1);
        //     result.First().Moves.First().Should().BeOfType<SimpleMove>();
        //     result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(2, 1));
        //     result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(2, 2));
        //     result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
        // }
        //
        // [Test]
        // public void GetPaths_NotMovedNearTopOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
        //     var board = new GameBoard(3, 0) {[1, 1] = piece};
        //     var source = new PawnMovePathSource();
        //
        //     var square = board.GetSquare(1, 1);
        //
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(1);
        //     result.First().Direction.Should().Be(Direction.North);
        //     result.First().AllowMove.Should().BeTrue();
        //     result.First().AllowTake.Should().BeFalse();
        //     result.First().Moves.Should().HaveCount(1);
        //     result.First().Moves.First().Should().BeOfType<PawnPromotionMove>();
        //     result.First().Moves.First().As<PawnPromotionMove>().From.Should().Be(new Point(1, 1));
        //     result.First().Moves.First().As<PawnPromotionMove>().To.Should().Be(new Point(1, 2));
        //     result.First().Moves.First().As<PawnPromotionMove>().Piece.Should().Be(piece);
        // }
        //
        // [Test]
        // public void GetPaths_NotMovedTopEdgeOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
        //     var board = new GameBoard(3, 0) {[1, 2] = piece};
        //     var source = new PawnMovePathSource();
        //
        //     var square = board.GetSquare(1, 2);
        //
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().BeEmpty();
        // }
        //
        // [Test]
        // public void GetPaths_NotMovedBottomEdgeOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
        //     var board = new GameBoard(3, 0) {[1, 0] = piece};
        //     var source = new PawnMovePathSource();
        //
        //     var square = board.GetSquare(1, 0);
        //
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(1);
        //     result.First().Direction.Should().Be(Direction.North);
        //     result.First().AllowMove.Should().BeTrue();
        //     result.First().AllowTake.Should().BeFalse();
        //     result.First().Moves.Should().HaveCount(2);
        //     result.First().Moves.First().Should().BeOfType<SimpleMove>();
        //     result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(1, 0));
        //     result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(1, 1));
        //     result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
        //     result.First().Moves.Last().Should().BeOfType<PawnOpenMove>();
        //     result.First().Moves.Last().As<PawnOpenMove>().From.Should().Be(new Point(1, 0));
        //     result.First().Moves.Last().As<PawnOpenMove>().To.Should().Be(new Point(1, 2));
        //     result.First().Moves.Last().As<PawnOpenMove>().Piece.Should().Be(piece);
        // }
        //
        // [Test]
        // public void GetPaths_NotMovedRightEdgeOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
        //     var board = new GameBoard(4, 0) {[3, 1] = piece};
        //     var source = new PawnMovePathSource();
        //
        //     var square = board.GetSquare(3, 1);
        //
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(1);
        //     result.First().Direction.Should().Be(Direction.North);
        //     result.First().AllowMove.Should().BeTrue();
        //     result.First().AllowTake.Should().BeFalse();
        //     result.First().Moves.Should().HaveCount(2);
        //     result.First().Moves.First().Should().BeOfType<SimpleMove>();
        //     result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(3, 1));
        //     result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(3, 2));
        //     result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
        //     result.First().Moves.Last().Should().BeOfType<PawnOpenMove>();
        //     result.First().Moves.Last().As<PawnOpenMove>().From.Should().Be(new Point(3, 1));
        //     result.First().Moves.Last().As<PawnOpenMove>().To.Should().Be(new Point(3, 3));
        //     result.First().Moves.Last().As<PawnOpenMove>().Piece.Should().Be(piece);
        // }
        //
        // [Test]
        // public void GetPaths_NotMovedLeftEdgeOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
        //     var board = new GameBoard(4, 0) {[0, 1] = piece};
        //     var source = new PawnMovePathSource();
        //
        //     var square = board.GetSquare(0, 1);
        //
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(1);
        //     result.First().Direction.Should().Be(Direction.North);
        //     result.First().AllowMove.Should().BeTrue();
        //     result.First().AllowTake.Should().BeFalse();
        //     result.First().Moves.Should().HaveCount(2);
        //     result.First().Moves.First().Should().BeOfType<SimpleMove>();
        //     result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(0, 1));
        //     result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(0, 2));
        //     result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
        //     result.First().Moves.Last().Should().BeOfType<PawnOpenMove>();
        //     result.First().Moves.Last().As<PawnOpenMove>().From.Should().Be(new Point(0, 1));
        //     result.First().Moves.Last().As<PawnOpenMove>().To.Should().Be(new Point(0, 3));
        //     result.First().Moves.Last().As<PawnOpenMove>().Piece.Should().Be(piece);
        // }
    }
}