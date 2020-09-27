using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Move;
using Chess.Model.Rules;
using FluentAssertions;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

// ReSharper disable PossibleMultipleEnumeration

namespace Chess.Model.Tests.Rules
{
    public sealed class PawnMovePathSourceTests
    {
        [Test]
        public void GetPaths_WhenNotPawn_ShouldBeEmpty()
        {
            // Arrange
            var board = new GameBoard(4, 0) {[2, 1] = new Piece(PieceType.Queen, Color.White, Direction.South)};
            var source = new PawnMovePathSource();

            var square = board.GetSquare(1, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WhenNotMoved_ShouldHaveCorrectResult()
        {
            // Arrange
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            var board = new GameBoard(5, 0) {[2, 1] = piece};
            var source = new PawnMovePathSource();

            var square = board.GetSquare(2, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.North);
            result.First().AllowMove.Should().BeTrue();
            result.First().AllowTake.Should().BeFalse();
            result.First().Moves.Should().HaveCount(2);
            result.First().Moves.First().Should().BeOfType<SimpleMove>();
            result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(2,1));
            result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(2,2));
            result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
            result.First().Moves.Last().Should().BeOfType<PawnOpenMove>();
            result.First().Moves.Last().As<PawnOpenMove>().From.Should().Be(new Point(2,1));
            result.First().Moves.Last().As<PawnOpenMove>().To.Should().Be(new Point(2,3));
            result.First().Moves.Last().As<PawnOpenMove>().Piece.Should().Be(piece);
        }
        
        [Test]
        public void GetPaths_WhenMoved_ShouldHaveCorrectResult()
        {
            // Arrange
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            piece.Moved();
            var board = new GameBoard(5, 0) {[2, 1] = piece};
            var source = new PawnMovePathSource();

            var square = board.GetSquare(2, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.North);
            result.First().AllowMove.Should().BeTrue();
            result.First().AllowTake.Should().BeFalse();
            result.First().Moves.Should().HaveCount(1);
            result.First().Moves.First().Should().BeOfType<SimpleMove>();
            result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(2,1));
            result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(2,2));
            result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
        }
        
        [Test]
        public void GetPaths_NotMovedNearTopOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            var board = new GameBoard(3, 0) {[1, 1] = piece};
            var source = new PawnMovePathSource();

            var square = board.GetSquare(1, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.North);
            result.First().AllowMove.Should().BeTrue();
            result.First().AllowTake.Should().BeFalse();
            result.First().Moves.Should().HaveCount(1);
            result.First().Moves.First().Should().BeOfType<PawnPromotionMove>();
            result.First().Moves.First().As<PawnPromotionMove>().From.Should().Be(new Point(1,1));
            result.First().Moves.First().As<PawnPromotionMove>().To.Should().Be(new Point(1,2));
            result.First().Moves.First().As<PawnPromotionMove>().Piece.Should().Be(piece);
        }
        
        [Test]
        public void GetPaths_NotMovedTopEdgeOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            var board = new GameBoard(3, 0) {[1, 2] = piece};
            var source = new PawnMovePathSource();

            var square = board.GetSquare(1, 2);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().BeEmpty();
        }
        
        [Test]
        public void GetPaths_NotMovedBottomEdgeOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            var board = new GameBoard(3, 0) {[1, 0] = piece};
            var source = new PawnMovePathSource();

            var square = board.GetSquare(1, 0);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.North);
            result.First().AllowMove.Should().BeTrue();
            result.First().AllowTake.Should().BeFalse();
            result.First().Moves.Should().HaveCount(2);
            result.First().Moves.First().Should().BeOfType<SimpleMove>();
            result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(1,0));
            result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(1,1));
            result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
            result.First().Moves.Last().Should().BeOfType<PawnOpenMove>();
            result.First().Moves.Last().As<PawnOpenMove>().From.Should().Be(new Point(1,0));
            result.First().Moves.Last().As<PawnOpenMove>().To.Should().Be(new Point(1,2));
            result.First().Moves.Last().As<PawnOpenMove>().Piece.Should().Be(piece);
        }
        
        [Test]
        public void GetPaths_NotMovedRightEdgeOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            var board = new GameBoard(4, 0) {[3, 1] = piece};
            var source = new PawnMovePathSource();

            var square = board.GetSquare(3, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.North);
            result.First().AllowMove.Should().BeTrue();
            result.First().AllowTake.Should().BeFalse();
            result.First().Moves.Should().HaveCount(2);
            result.First().Moves.First().Should().BeOfType<SimpleMove>();
            result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(3,1));
            result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(3,2));
            result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
            result.First().Moves.Last().Should().BeOfType<PawnOpenMove>();
            result.First().Moves.Last().As<PawnOpenMove>().From.Should().Be(new Point(3,1));
            result.First().Moves.Last().As<PawnOpenMove>().To.Should().Be(new Point(3,3));
            result.First().Moves.Last().As<PawnOpenMove>().Piece.Should().Be(piece);
        }
        
        [Test]
        public void GetPaths_NotMovedLeftEdgeOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            var board = new GameBoard(4, 0) {[0, 1] = piece};
            var source = new PawnMovePathSource();

            var square = board.GetSquare(0, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.North);
            result.First().AllowMove.Should().BeTrue();
            result.First().AllowTake.Should().BeFalse();
            result.First().Moves.Should().HaveCount(2);
            result.First().Moves.First().Should().BeOfType<SimpleMove>();
            result.First().Moves.First().As<SimpleMove>().From.Should().Be(new Point(0,1));
            result.First().Moves.First().As<SimpleMove>().To.Should().Be(new Point(0,2));
            result.First().Moves.First().As<SimpleMove>().Piece.Should().Be(piece);
            result.First().Moves.Last().Should().BeOfType<PawnOpenMove>();
            result.First().Moves.Last().As<PawnOpenMove>().From.Should().Be(new Point(0,1));
            result.First().Moves.Last().As<PawnOpenMove>().To.Should().Be(new Point(0,3));
            result.First().Moves.Last().As<PawnOpenMove>().Piece.Should().Be(piece);
        }
    }
}