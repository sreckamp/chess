using System.Linq;
using Chess.Model.Models;
using Chess.Model.Rules;
using FluentAssertions;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

// ReSharper disable PossibleMultipleEnumeration

namespace Chess.Model.Tests.Rules
{
    public class KingPathAttackSourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetPaths_WhenNotKing_ShouldBeEmpty()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[1, 1] = new Piece(PieceType.Queen, Color.White, Direction.South)};
            var source = new KingPathSource();

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
            var board = new GameBoard(5, 0) {[2, 2] = new Piece(PieceType.King, Color.White, Direction.South)};
            var source = new KingPathSource();

            var square = board.GetSquare(2, 2);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(8);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthWest 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 3);
            result.Should().Contain(path =>  
                path.Direction == Direction.North 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 3);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast 
                && path.Moves.First().To.X == 3 && path.Moves.First().To.Y == 3);
            result.Should().Contain(path =>  
                path.Direction == Direction.West 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.East 
                && path.Moves.First().To.X == 3 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.SouthWest 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.South 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.SouthEast 
                && path.Moves.First().To.X == 3 && path.Moves.First().To.Y == 1);
        }
        
        [Test]
        public void GetPaths_WhenOffTopOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[1, 2] = new Piece(PieceType.King, Color.White, Direction.South)};
            var source = new KingPathSource();

            var square = board.GetSquare(1, 2);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(5);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.West 
                && path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.East 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.SouthWest 
                && path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.South 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.SouthEast 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 1);
        }
        
        [Test]
        public void GetPaths_WhenOffBottomOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[1, 0] = new Piece(PieceType.King, Color.White, Direction.South)};
            var source = new KingPathSource();

            var square = board.GetSquare(1, 0);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(5);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthWest 
                && path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.North 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.West 
                && path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 0);
            result.Should().Contain(path =>  
                path.Direction == Direction.East 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 0);
        }
        
        [Test]
        public void GetPaths_WhenOffRightOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[2, 1] = new Piece(PieceType.King, Color.White, Direction.South)};
            var source = new KingPathSource();

            var square = board.GetSquare(2, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(5);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.North 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthWest 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.West 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.South 
                && path.Moves.First().To.X == 2 && path.Moves.First().To.Y == 0);
            result.Should().Contain(path =>  
                path.Direction == Direction.SouthWest 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 0);
        }
        
        [Test]
        public void GetPaths_WhenOffLeftOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var board = new GameBoard(3, 0) {[0, 1] = new Piece(PieceType.King, Color.White, Direction.South)};
            var source = new KingPathSource();

            var square = board.GetSquare(0, 1);
            
            // Act
            var result = source.GetPaths(square, board);

            // Assert
            result.Should().HaveCount(5);
            result.Should().OnlyContain(path => path.Moves.Count() == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.North 
                && path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.NorthEast 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 2);
            result.Should().Contain(path =>  
                path.Direction == Direction.East 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 1);
            result.Should().Contain(path =>  
                path.Direction == Direction.South 
                && path.Moves.First().To.X == 0 && path.Moves.First().To.Y == 0);
            result.Should().Contain(path =>  
                path.Direction == Direction.SouthEast 
                && path.Moves.First().To.X == 1 && path.Moves.First().To.Y == 0);
        }
    }
}