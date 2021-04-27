using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Evaluation.Sources;
using Chess.Model.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Tests.Rules
{
    public sealed class PawnPathSourceTests
    {
        [Test]
        public void GetPaths_WithRookSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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

            var dut = new PawnPathSource();

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
        
        [Test]
        public void GetPaths2_WithRookSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);
            
            var dut = new PawnPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths2_WithBishopSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);
            
            var dut = new PawnPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths2_WithQueenSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new PawnPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths2_WithKingSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new PawnPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
            mock.VerifyAll();
        }

        [Test]
        public void GetPaths2_WithKnightSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new PawnPathSource();

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
            
            var dut = new PawnPathSource();

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
            
            var dut = new PawnPathSource();

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
            
            var dut = new PawnPathSource();

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
            
            var dut = new PawnPathSource();

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
        public void GetPaths2_PawnSouthWhenAgainstTopOfBoard_ShouldHaveNoPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthWest))
                .Returns( Enumerable.Empty<(Point, Piece)>());
            mock.Setup(provider => provider.EnumerateStraightLine(new Point(2, 2), Direction.NorthEast))
                .Returns( Enumerable.Empty<(Point, Piece)>());

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new PawnPathSource();

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
            
            var dut = new PawnPathSource();

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
            
            var dut = new PawnPathSource();

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
        
        // [Test]
        // public void GetPaths_WhenInCenterOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var board = new GameBoard(5, 0) {[2, 2] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
        //     var source = new PawnPathSource();
        //
        //     var square = board.GetSquare(2, 2);
        //     
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(2);
        //     result.Should().OnlyContain(path => path.Moves.Count() == 1);
        //     result.Should().SatisfyRespectively(
        //         path => path.Moves.Should().AllBeOfType<SimpleMove>(),
        //         path => path.Moves.Should().AllBeOfType<SimpleMove>());
        //     result.Should().Contain(path =>
        //         path.Direction == Direction.NorthWest &&
        //         !path.AllowMove &&
        //         path.AllowTake &&
        //         path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 3);
        //     result.Should().Contain(path =>  
        //         path.Direction == Direction.NorthEast &&
        //         !path.AllowMove &&
        //         path.AllowTake &&
        //         path.Moves.First().To.X == 3 && path.Moves.First().To.Y == 3);
        // }
        //
        // [Test]
        // public void GetPaths_WhenAgainstTopOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var board = new GameBoard(3, 0) {[1, 2] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
        //     var source = new PawnPathSource();
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
        // public void GetPaths_WhenOneAwayFromTopOfBoard_ShouldHavePawnPromotionRules()
        // {
        //     // Arrange
        //     var board = new GameBoard(3, 0) {[1, 1] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
        //     var source = new PawnPathSource();
        //
        //     var square = board.GetSquare(1, 1);
        //     
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(2);
        //     result.Should().OnlyContain(path => path.Moves.Count() == 1);
        //     result.Should().SatisfyRespectively(
        //         path => path.Moves.Should().AllBeOfType<PawnPromotionMove>(),
        //         path => path.Moves.Should().AllBeOfType<PawnPromotionMove>());
        //     result.Should().Contain(path =>  
        //         path.Direction == Direction.NorthWest &&
        //         !path.AllowMove &&
        //         path.AllowTake &&
        //         path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 2);
        //     result.Should().Contain(path =>  
        //         path.Direction == Direction.NorthEast &&
        //         !path.AllowMove &&
        //         path.AllowTake &&
        //         path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 2);
        // }
        //
        // [Test]
        // public void GetPaths_WhenAgainstBottomOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var board = new GameBoard(3, 0) {[1, 0] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
        //     var source = new PawnPathSource();
        //
        //     var square = board.GetSquare(1, 0);
        //     
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().HaveCount(2);
        //     result.Should().OnlyContain(path => path.Moves.Count() == 1);
        //     result.Should().SatisfyRespectively(
        //         path => path.Moves.Should().AllBeOfType<SimpleMove>(),
        //         path => path.Moves.Should().AllBeOfType<SimpleMove>());
        //     result.Should().Contain(path =>  
        //         path.Direction == Direction.NorthWest &&
        //         !path.AllowMove &&
        //         path.AllowTake &&
        //         path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 1);
        //     result.Should().Contain(path =>  
        //         path.Direction == Direction.NorthEast &&
        //         !path.AllowMove &&
        //         path.AllowTake &&
        //         path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 1);
        // }
        //
        // [Test]
        // public void GetPaths_WhenAgainstRightOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var board = new GameBoard(4, 0) {[3, 1] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
        //     var source = new PawnPathSource();
        //
        //     var square = board.GetSquare(3, 1);
        //     
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().ContainSingle();
        //     result.First().Moves.Should().AllBeOfType<SimpleMove>();
        //     result.First().Direction.Should().Be(Direction.NorthWest);
        //     result.First().AllowMove.Should().BeFalse();
        //     result.First().AllowTake.Should().BeTrue();
        //     result.First().Moves.Should().ContainSingle();
        //     result.First().Moves.First().To.Should().Be(new Point(2,2));
        // }
        //
        // [Test]
        // public void GetPaths_WhenAgainstLeftOfBoard_ShouldHaveCorrectResult()
        // {
        //     // Arrange
        //     var board = new GameBoard(4, 0) {[0, 1] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
        //     var source = new PawnPathSource();
        //
        //     var square = board.GetSquare(0, 1);
        //     
        //     // Act
        //     var result = source.GetPaths(square, board);
        //
        //     // Assert
        //     result.Should().ContainSingle();
        //     result.First().Moves.Should().AllBeOfType<SimpleMove>();
        //     result.First().Direction.Should().Be(Direction.NorthEast);
        //     result.First().AllowMove.Should().BeFalse();
        //     result.First().AllowTake.Should().BeTrue();
        //     result.First().Moves.Should().ContainSingle();
        //     result.First().Moves.First().To.Should().Be(new Point(1,2));
        // }
    }
}
