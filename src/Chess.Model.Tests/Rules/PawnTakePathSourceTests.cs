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
    public sealed class PawnTakePathSourceTests
    {
        [Test]
        public void GetPaths_WhenNotPawn_ShouldBeEmpty()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[1, 1] = new Piece(PieceType.Queen, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(1, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WhenInCenterOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(5, 0) {[2, 2] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(2, 2);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(2);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().SatisfyRespectively(
                path => path.Moves.Should().AllBeOfType<SimpleMove>(),
                path => path.Moves.Should().AllBeOfType<SimpleMove>());
            result.Should().Contain(path =>
                path.Direction == Direction.NorthWest &&
                !path.AllowMove &&
                path.AllowTake &&
                path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 3);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast &&
                !path.AllowMove &&
                path.AllowTake &&
                path.Moves.First().To.X == 3 && path.Moves.First().To.Y == 3);
        }
        
        [Test]
        public void GetPaths_WhenAgainstTopOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[1, 2] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(1, 2);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().BeEmpty();
        }
        
        [Test]
        public void GetPaths_WhenOneAwayFromTopOfBoard_ShouldHavePawnPromotionRules()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[1, 1] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(1, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(2);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().SatisfyRespectively(
                path => path.Moves.Should().AllBeOfType<PawnPromotionMove>(),
                path => path.Moves.Should().AllBeOfType<PawnPromotionMove>());
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthWest &&
                !path.AllowMove &&
                path.AllowTake &&
                path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast &&
                !path.AllowMove &&
                path.AllowTake &&
                path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 2);
        }
        
        [Test]
        public void GetPaths_WhenAgainstBottomOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[1, 0] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(1, 0);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(2);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().SatisfyRespectively(
                path => path.Moves.Should().AllBeOfType<SimpleMove>(),
                path => path.Moves.Should().AllBeOfType<SimpleMove>());
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthWest &&
                !path.AllowMove &&
                path.AllowTake &&
                path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast &&
                !path.AllowMove &&
                path.AllowTake &&
                path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 1);
        }
        
        [Test]
        public void GetPaths_WhenAgainstRightOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(4, 0) {[3, 1] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(3, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().ContainSingle();
            result.First().Moves.Should().AllBeOfType<SimpleMove>();
            result.First().Direction.Should().Be(Direction.NorthWest);
            result.First().AllowMove.Should().BeFalse();
            result.First().AllowTake.Should().BeTrue();
            result.First().Moves.Should().ContainSingle();
            result.First().Moves.First().To.Should().Be(new Point(2,2));
        }
        
        [Test]
        public void GetPaths_WhenAgainstLeftOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(4, 0) {[0, 1] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(0, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().ContainSingle();
            result.First().Moves.Should().AllBeOfType<SimpleMove>();
            result.First().Direction.Should().Be(Direction.NorthEast);
            result.First().AllowMove.Should().BeFalse();
            result.First().AllowTake.Should().BeTrue();
            result.First().Moves.Should().ContainSingle();
            result.First().Moves.First().To.Should().Be(new Point(1,2));
        }
    }
}