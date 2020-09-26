using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;
using Chess.Model.Rules;
using FluentAssertions;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

// ReSharper disable PossibleMultipleEnumeration

namespace Chess.Model.Tests.Rules
{
    public class KingPathMoveSourceTests
    {
        [SetUp]
        public void Setup()
        {
        }

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

        [Test]
        public void GetCastlePoints_WithCardinalRooksThreeSquaresAwayAndKingMoved_ShouldHaveNoCastles()
        {
            // Arrange
            var board = new GameBoard(7, 0)
            {
                [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };

            board[3,3].Moved();

            var source = new CastlePathSource();

            var square = board.GetSquare(3, 3);
            
            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksThreeSquaresAwayKingNorth_ShouldHaveEastAndWestCastles()
        {
            // Arrange
            var board = new GameBoard(7, 0)
            {
                [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
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
            result.Should().HaveCount(2);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.Last().Direction.Should().Be(Direction.None);
            result.Last().Moves.Should().HaveCount(1);
            var castles = result.First().Moves.Union(result.Last().Moves);
            castles.Should().OnlyContain(move => move is CastleMove);
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 1 &&
                ((CastleMove) move).To.Y == 3 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 3 &&
                ((CastleMove) move).RookMove.To.X == 2 &&
                ((CastleMove) move).RookMove.To.Y == 3
            );
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 5 &&
                ((CastleMove) move).To.Y == 3 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 6 &&
                ((CastleMove) move).RookMove.From.Y == 3 &&
                ((CastleMove) move).RookMove.To.X == 4 &&
                ((CastleMove) move).RookMove.To.Y == 3
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksThreeSquaresAwayKingWest_ShouldHaveNorthAndSouthCastles()
        {
            // Arrange
            var board = new GameBoard(7, 0)
            {
                [3, 3] = new Piece(PieceType.King, Color.White, Direction.West),
                [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.West),
                [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.West),
                [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.West),
                [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.West)
            };

            var source = new CastlePathSource();

            var square = board.GetSquare(3, 3);
            
            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(2);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.Last().Direction.Should().Be(Direction.None);
            result.Last().Moves.Should().HaveCount(1);
            var castles = result.First().Moves.Union(result.Last().Moves);
            castles.Should().OnlyContain(move => move is CastleMove);
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 3 &&
                ((CastleMove) move).To.Y == 1 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 3 &&
                ((CastleMove) move).RookMove.From.Y == 0 &&
                ((CastleMove) move).RookMove.To.X == 3 &&
                ((CastleMove) move).RookMove.To.Y == 2
            );
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 3 &&
                ((CastleMove) move).To.Y == 5 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 3 &&
                ((CastleMove) move).RookMove.From.Y == 6 &&
                ((CastleMove) move).RookMove.To.X == 3 &&
                ((CastleMove) move).RookMove.To.Y == 4
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksThreeSquaresAwayKingEast_ShouldHaveNorthAndSouthCastles()
        {
            // Arrange
            var board = new GameBoard(7, 0)
            {
                [3, 3] = new Piece(PieceType.King, Color.White, Direction.East),
                [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.East),
                [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.East),
                [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.East),
                [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.East)
            };

            var source = new CastlePathSource();

            var square = board.GetSquare(3, 3);
            
            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(2);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.Last().Direction.Should().Be(Direction.None);
            result.Last().Moves.Should().HaveCount(1);
            var castles = result.First().Moves.Union(result.Last().Moves);
            castles.Should().OnlyContain(move => move is CastleMove);
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 3 &&
                ((CastleMove) move).To.Y == 1 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 3 &&
                ((CastleMove) move).RookMove.From.Y == 0 &&
                ((CastleMove) move).RookMove.To.X == 3 &&
                ((CastleMove) move).RookMove.To.Y == 2
            );
            castles.Should().Contain(move =>
                ((CastleMove) move). Piece.Color == Color.White &&
                ((CastleMove) move). Piece.Type == PieceType.King &&
                ((CastleMove) move). From.X == 3 &&
                ((CastleMove) move). From.Y == 3 &&
                ((CastleMove) move). To.X == 3 &&
                ((CastleMove) move). To.Y == 5 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 3 &&
                ((CastleMove) move).RookMove.From.Y == 6 &&
                ((CastleMove) move).RookMove.To.X == 3 &&
                ((CastleMove) move).RookMove.To.Y == 4
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksThreeSquaresAwayKingSouth_ShouldHaveEastAndWestCastles()
        {
            // Arrange
            var board = new GameBoard(7, 0)
            {
                [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
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
            result.Should().HaveCount(2);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.Last().Direction.Should().Be(Direction.None);
            result.Last().Moves.Should().HaveCount(1);
            var castles = result.First().Moves.Union(result.Last().Moves);
            castles.Should().OnlyContain(move => move is CastleMove);
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 1 &&
                ((CastleMove) move).To.Y == 3 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 3 &&
                ((CastleMove) move).RookMove.To.X == 2 &&
                ((CastleMove) move).RookMove.To.Y == 3
            );
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 5 &&
                ((CastleMove) move).To.Y == 3 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 6 &&
                ((CastleMove) move).RookMove.From.Y == 3 &&
                ((CastleMove) move).RookMove.To.X == 4 &&
                ((CastleMove) move).RookMove.To.Y == 3
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksFiveSquaresAwayKingSouth_ShouldHaveEastAndWestCastles()
        {
            // Arrange
            var board = new GameBoard(11, 0)
            {
                [5, 5] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 5] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [10, 5] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [5, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [5, 10] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };

            var source = new CastlePathSource();

            var square = board.GetSquare(5, 5);
            
            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(2);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.Last().Direction.Should().Be(Direction.None);
            result.Last().Moves.Should().HaveCount(1);
            var castles = result.First().Moves.Union(result.Last().Moves);
            castles.Should().OnlyContain(move => move is CastleMove);
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 5 &&
                ((CastleMove) move).From.Y == 5 &&
                ((CastleMove) move).To.X == 3 &&
                ((CastleMove) move).To.Y == 5 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 5 &&
                ((CastleMove) move).RookMove.To.X == 4 &&
                ((CastleMove) move).RookMove.To.Y == 5
            );
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 5 &&
                ((CastleMove) move).From.Y == 5 &&
                ((CastleMove) move).To.X == 7 &&
                ((CastleMove) move).To.Y == 5 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 10 &&
                ((CastleMove) move).RookMove.From.Y == 5 &&
                ((CastleMove) move).RookMove.To.X == 6 &&
                ((CastleMove) move).RookMove.To.Y == 5
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksThreeSquaresAwayWhereEastMoved_ShouldHaveWestCastle()
        {
            // Arrange
            var board = new GameBoard(7, 0)
            {
                [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };

            board[6, 3].Moved();

            var source = new CastlePathSource();

            var square = board.GetSquare(3, 3);
            
            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.First().Moves.Should().OnlyContain(move => move is CastleMove);
            result.First().Moves.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 1 &&
                ((CastleMove) move).To.Y == 3 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 3 &&
                ((CastleMove) move).RookMove.To.X == 2 &&
                ((CastleMove) move).RookMove.To.Y == 3
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksThreeSquaresAwayAndEastIsBlack_ShouldOnlyHaveWestCastle()
        {
            // Arrange
            var board = new GameBoard(7, 0)
            {
                [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [6, 3] = new Piece(PieceType.Rook, Color.Black, Direction.North),
                [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };

            var source = new CastlePathSource();

            var square = board.GetSquare(3, 3);
            
            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.First().Moves.Should().OnlyContain(move => move is CastleMove);
            result.First().Moves.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 3 &&
                ((CastleMove) move).From.Y == 3 &&
                ((CastleMove) move).To.X == 1 &&
                ((CastleMove) move).To.Y == 3 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 3 &&
                ((CastleMove) move).RookMove.To.X == 2 &&
                ((CastleMove) move).RookMove.To.Y == 3
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksFourSquaresAwayWhereKingAttacked_ShouldHaveNoCastles()
        {
            // Arrange
            var board = new GameBoard(9, 0)
            {
                [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };

            var square = board.GetSquare(4, 4);
            square.Mark(new SimpleMarker( MarkerType.Cover,
                new Square
                {
                    Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
                }, 
                Direction.South));
            
            var source = new CastlePathSource();

            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksFourSquaresAwayWhereAdjacentEastKingAttacked_ShouldHaveWestCastle()
        {
            // Arrange
            var board = new GameBoard(9, 0)
            {
                [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };

            board.GetSquare(5, 4).Mark(new SimpleMarker( MarkerType.Cover,
                new Square
                {
                    Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
                }, 
                Direction.South));
            var square = board.GetSquare(4, 4);
            
            var source = new CastlePathSource();

            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.First().Moves.Should().OnlyContain(move => move is CastleMove);
            result.First().Moves.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 4 &&
                ((CastleMove) move).From.Y == 4 &&
                ((CastleMove) move).To.X == 2 &&
                ((CastleMove) move).To.Y == 4 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 4 &&
                ((CastleMove) move).RookMove.To.X == 3 &&
                ((CastleMove) move).RookMove.To.Y == 4
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksFourSquaresAwayWhereAdjacentAdjacentEastKingAttacked_ShouldHaveWestCastle()
        {
            // Arrange
            var board = new GameBoard(9, 0)
            {
                [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };


            board.GetSquare(6,4).Mark(new SimpleMarker( MarkerType.Cover,
                new Square
                {
                    Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
                }, 
                Direction.South));
            var square = board.GetSquare(4, 4);
            
            var source = new CastlePathSource();

            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(1);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.First().Moves.Should().OnlyContain(move => move is CastleMove);
            result.First().Moves.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 4 &&
                ((CastleMove) move).From.Y == 4 &&
                ((CastleMove) move).To.X == 2 &&
                ((CastleMove) move).To.Y == 4 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 4 &&
                ((CastleMove) move).RookMove.To.X == 3 &&
                ((CastleMove) move).RookMove.To.Y == 4
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksFourSquaresAwayWhereAdjacentEastRookAttacked_ShouldHaveEastAndWestCastles()
        {
            // Arrange
            var board = new GameBoard(9, 0)
            {
                [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };

            board.GetSquare(7,4).Mark(new SimpleMarker( MarkerType.Cover,
                new Square
                {
                    Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
                }, 
                Direction.South));

            var square = board.GetSquare(4, 4);
            
            var source = new CastlePathSource();

            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(2);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.Last().Direction.Should().Be(Direction.None);
            result.Last().Moves.Should().HaveCount(1);
            var castles = result.First().Moves.Union(result.Last().Moves);
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 4 &&
                ((CastleMove) move).From.Y == 4 &&
                ((CastleMove) move).To.X == 2 &&
                ((CastleMove) move).To.Y == 4 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 4 &&
                ((CastleMove) move).RookMove.To.X == 3 &&
                ((CastleMove) move).RookMove.To.Y == 4
            );
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 4 &&
                ((CastleMove) move).From.Y == 4 &&
                ((CastleMove) move).To.X == 6 &&
                ((CastleMove) move).To.Y == 4 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 8 &&
                ((CastleMove) move).RookMove.From.Y == 4 &&
                ((CastleMove) move).RookMove.To.X == 5 &&
                ((CastleMove) move).RookMove.To.Y == 4
            );
        }

        [Test]
        public void GetCastlePoints_WithCardinalRooksFourSquaresAwayWhereEastRookAttacked_ShouldHaveEastAndWestCastles()
        {
            // Arrange
            var board = new GameBoard(9, 0)
            {
                [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
                [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
                [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
            };

            board.GetSquare(8,4).Mark(new SimpleMarker( MarkerType.Cover,
                new Square
                {
                    Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
                }, 
                Direction.South));
            var square = board.GetSquare(4, 4);
            
            var source = new CastlePathSource();

            // Act
            var result = source.GetPaths(square, board);
            
            // Assert
            result.Should().HaveCount(2);
            result.First().Direction.Should().Be(Direction.None);
            result.First().Moves.Should().HaveCount(1);
            result.Last().Direction.Should().Be(Direction.None);
            result.Last().Moves.Should().HaveCount(1);
            var castles = result.First().Moves.Union(result.Last().Moves);
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 4 &&
                ((CastleMove) move).From.Y == 4 &&
                ((CastleMove) move).To.X == 2 &&
                ((CastleMove) move).To.Y == 4 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 0 &&
                ((CastleMove) move).RookMove.From.Y == 4 &&
                ((CastleMove) move).RookMove.To.X == 3 &&
                ((CastleMove) move).RookMove.To.Y == 4
            );
            castles.Should().Contain(move =>
                ((CastleMove) move).Piece.Color == Color.White &&
                ((CastleMove) move).Piece.Type == PieceType.King &&
                ((CastleMove) move).From.X == 4 &&
                ((CastleMove) move).From.Y == 4 &&
                ((CastleMove) move).To.X == 6 &&
                ((CastleMove) move).To.Y == 4 &&
                ((CastleMove) move).RookMove.Piece.Color == Color.White &&
                ((CastleMove) move).RookMove.Piece.Type == PieceType.Rook &&
                ((CastleMove) move).RookMove.From.X == 8 &&
                ((CastleMove) move).RookMove.From.Y == 4 &&
                ((CastleMove) move).RookMove.To.X == 5 &&
                ((CastleMove) move).RookMove.To.Y == 4
            );
        }
    }
}