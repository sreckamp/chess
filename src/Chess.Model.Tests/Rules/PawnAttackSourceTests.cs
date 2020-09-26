using System.Linq;
using Chess.Model.Models;
using Chess.Model.Rules;
using FluentAssertions;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

// ReSharper disable PossibleMultipleEnumeration

namespace Chess.Model.Tests.Rules
{
    public class PawnAttackSourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

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
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthWest 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 3);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast 
                && path.Moves.First().To.X == 3 && path.Moves.First().To.Y == 3);
        }
        
        [Test]
        public void GetPaths_WhenOffTopOfBoard_ShouldHaveCorrectResult()
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
        public void GetPaths_WhenOffBottomOfBoard_ShouldHaveCorrectResult()
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
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthWest 
                && path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 1);
        }
        
        [Test]
        public void GetPaths_WhenOffRightOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[2, 1] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(2, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(1);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthWest 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 2);
        }
        
        [Test]
        public void GetPaths_WhenOffLeftOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[0, 1] = new Piece(PieceType.Pawn, Color.White, Direction.South)};
            var source = new PawnTakePathSource();

            var square = board.GetSquare(0, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(1);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 2);
        }
    }
}