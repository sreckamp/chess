using Chess.Model.Models;
using Chess.Model.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Rules
{
    public class CardinalPathSourceTests
    {
        [Test]
        public void GetCastlePoints_WithCardinalRooksThreeSquaresAwayAndNotKing_ShouldHaveNoCastles()
        {
            // Arrange
            var board = new GameBoard(7, 0)
            {
                [3, 3] = new Piece(PieceType.Queen, Color.White, Direction.South),
                [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };
            var source = new CastlePathSource();

            var square = board.GetSquare(3, 3);
            
            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().BeEmpty();
        }
    }
}