// using System.Drawing;
// using System.Linq;
// using Chess.Model.Models;
// using Chess.Model.Models.Board;
// using Chess.Model.Move;
// using Chess.Model.Rules;
// using FluentAssertions;
// using NUnit.Framework;
// using Color = Chess.Model.Models.Color;
//
// // ReSharper disable PossibleMultipleEnumeration
//
// namespace Chess.Model.Tests.Rules
// {
//     public class CastlePathSourceTests
//     {
//         [Test]
//         public void GetPaths_WithCardinalRooksThreeSquaresAwayAndNotKing_ShouldHaveNoCastles()
//         {
//             // Arrange
//             var board = new GameBoard(7, 0)
//             {
//                 [3, 3] = new Piece(PieceType.Queen, Color.White, Direction.South),
//                 [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(3, 3);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().BeEmpty();
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksThreeSquaresAwayAndKingMoved_ShouldHaveNoCastles()
//         {
//             // Arrange
//             var board = new GameBoard(7, 0)
//             {
//                 [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             board[3,3].Moved();
//
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(3, 3);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().BeEmpty();
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksThreeSquaresAwayKingNorth_ShouldHaveEastAndWestCastles()
//         {
//             // Arrange
//             var board = new GameBoard(7, 0)
//             {
//                 [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(3, 3);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//
//             // Assert
//             result.Should().HaveCount(2);
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(1,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(2,3)
//                 );
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.East &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(5,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(6,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(4,3)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksThreeSquaresAwayKingWest_ShouldHaveNorthAndSouthCastles()
//         {
//             // Arrange
//             var board = new GameBoard(7, 0)
//             {
//                 [3, 3] = new Piece(PieceType.King, Color.White, Direction.West),
//                 [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.West),
//                 [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.West),
//                 [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.West),
//                 [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.West)
//             };
//
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(3, 3);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().HaveCount(2);
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.South &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(3,1) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(3,0) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(3,2)
//             );
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.North &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(3,5) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(3,6) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(3,4)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksThreeSquaresAwayKingEast_ShouldHaveNorthAndSouthCastles()
//         {
//             // Arrange
//             var board = new GameBoard(7, 0)
//             {
//                 [3, 3] = new Piece(PieceType.King, Color.White, Direction.East),
//                 [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.East),
//                 [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.East),
//                 [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.East),
//                 [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.East)
//             };
//
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(3, 3);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().HaveCount(2);
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.South &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3, 3) &&
//                 path.Moves.First().To == new Point(3, 1) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(3, 0) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(3, 2)
//             );
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.North &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(3,5) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(3,6) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(3,4)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksThreeSquaresAwayKingSouth_ShouldHaveEastAndWestCastles()
//         {
//             // Arrange
//             var board = new GameBoard(7, 0)
//             {
//                 [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(3, 3);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().HaveCount(2);
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(1,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(2,3)
//             );
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.East &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(5,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(6,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(4,3)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksFiveSquaresAwayKingSouth_ShouldHaveEastAndWestCastles()
//         {
//             // Arrange
//             var board = new GameBoard(11, 0)
//             {
//                 [5, 5] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 5] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [10, 5] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [5, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [5, 10] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(5, 5);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().HaveCount(2);
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(5,5) &&
//                 path.Moves.First().To == new Point(3,5) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0,5) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(4,5)
//             );
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.East &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(5,5) &&
//                 path.Moves.First().To == new Point(7,5) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(10,5) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(6,5)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksThreeSquaresAwayWhereEastMoved_ShouldHaveWestCastle()
//         {
//             // Arrange
//             var board = new GameBoard(7, 0)
//             {
//                 [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [6, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             board[6, 3].Moved();
//
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(3, 3);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().ContainSingle(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(1,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(2,3)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksThreeSquaresAwayAndEastIsBlack_ShouldOnlyHaveWestCastle()
//         {
//             // Arrange
//             var board = new GameBoard(7, 0)
//             {
//                 [3, 3] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 3] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [6, 3] = new Piece(PieceType.Rook, Color.Black, Direction.North),
//                 [3, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [3, 6] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             var source = new CastlePathSource();
//
//             var square = board.GetSquare(3, 3);
//             
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().ContainSingle(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(3,3) &&
//                 path.Moves.First().To == new Point(1,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0,3) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(2,3)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksFourSquaresAwayWhereKingAttacked_ShouldHaveNoCastles()
//         {
//             // Arrange
//             var board = new GameBoard(9, 0)
//             {
//                 [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             var square = board.GetSquare(4, 4);
//             square.Mark(new SimpleMarker( MarkerType.Cover,
//                 new Square
//                 {
//                     Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
//                 }, 
//                 Direction.South));
//             
//             var source = new CastlePathSource();
//
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().BeEmpty();
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksFourSquaresAwayWhereAdjacentEastKingAttacked_ShouldHaveWestCastle()
//         {
//             // Arrange
//             var board = new GameBoard(9, 0)
//             {
//                 [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             board.GetSquare(5, 4).Mark(new SimpleMarker( MarkerType.Cover,
//                 new Square
//                 {
//                     Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
//                 }, 
//                 Direction.South));
//             var square = board.GetSquare(4, 4);
//             
//             var source = new CastlePathSource();
//
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().ContainSingle(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(4,4) &&
//                 path.Moves.First().To == new Point(2,4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0,4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(3,4)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksFourSquaresAwayWhereAdjacentAdjacentEastKingAttacked_ShouldHaveWestCastle()
//         {
//             // Arrange
//             var board = new GameBoard(9, 0)
//             {
//                 [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//
//             board.GetSquare(6,4).Mark(new SimpleMarker( MarkerType.Cover,
//                 new Square
//                 {
//                     Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
//                 }, 
//                 Direction.South));
//             var square = board.GetSquare(4, 4);
//             
//             var source = new CastlePathSource();
//
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().ContainSingle(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(4,4) &&
//                 path.Moves.First().To == new Point(2,4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0,4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(3,4)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksFourSquaresAwayWhereAdjacentEastRookAttacked_ShouldHaveEastAndWestCastles()
//         {
//             // Arrange
//             var board = new GameBoard(9, 0)
//             {
//                 [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             board.GetSquare(7,4).Mark(new SimpleMarker( MarkerType.Cover,
//                 new Square
//                 {
//                     Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
//                 }, 
//                 Direction.South));
//
//             var square = board.GetSquare(4, 4);
//             
//             var source = new CastlePathSource();
//
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().HaveCount(2);
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(4, 4) &&
//                 path.Moves.First().To == new Point(2, 4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0, 4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(3, 4)
//             );
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.East &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(4,4) &&
//                 path.Moves.First().To == new Point(6,4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(8,4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(5,4)
//             );
//         }
//
//         [Test]
//         public void GetPaths_WithCardinalRooksFourSquaresAwayWhereEastRookAttacked_ShouldHaveEastAndWestCastles()
//         {
//             // Arrange
//             var board = new GameBoard(9, 0)
//             {
//                 [4, 4] = new Piece(PieceType.King, Color.White, Direction.South),
//                 [0, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [8, 4] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 0] = new Piece(PieceType.Rook, Color.White, Direction.South),
//                 [4, 8] = new Piece(PieceType.Rook, Color.White, Direction.South)
//             };
//
//             board.GetSquare(8,4).Mark(new SimpleMarker( MarkerType.Cover,
//                 new Square
//                 {
//                     Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)
//                 }, 
//                 Direction.South));
//             var square = board.GetSquare(4, 4);
//             
//             var source = new CastlePathSource();
//
//             // Act
//             var result = source.GetPaths(square, board);
//             
//             // Assert
//             result.Should().HaveCount(2);
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.West &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(4, 4) &&
//                 path.Moves.First().To == new Point(2, 4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(0, 4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(3, 4)
//             );
//             result.Should().Contain(path =>
//                 path.AllowMove &&
//                 !path.AllowTake &&
//                 path.Direction == Direction.East &&
//                 path.Moves.Count() == 1 &&
//                 path.Moves.First() is CastleMove &&
//                 path.Moves.First().Piece.Color == Color.White &&
//                 path.Moves.First().Piece.Type == PieceType.King &&
//                 path.Moves.First().From == new Point(4,4) &&
//                 path.Moves.First().To == new Point(6,4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Color == Color.White &&
//                 ((CastleMove) path.Moves.First()).RookMove.Piece.Type == PieceType.Rook &&
//                 ((CastleMove) path.Moves.First()).RookMove.From == new Point(8,4) &&
//                 ((CastleMove) path.Moves.First()).RookMove.To == new Point(5,4)
//             );
//         }
//     }
// }
