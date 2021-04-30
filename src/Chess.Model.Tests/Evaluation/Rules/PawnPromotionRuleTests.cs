// using System.Drawing;
// using Chess.Model.Models;
// using Chess.Model.Models.Board;
// using Chess.Model.Rules;
// using Moq;
// using NUnit.Framework;
// using Color = Chess.Model.Models.Color;
//
// namespace Chess.Model.Tests.Rules
// {
//     public class PawnPromotionRuleTests
//     {
//         [Test]
//         public void Apply_WithRookSouth_ShouldHaveNoPathsAndNotCallProvider()
//         {
//             // Arrange
//             var mock = new Mock<IPieceEnumerationProvider>();
//
//             var start = new Point(2, 2);
//             var piece = new Piece(PieceType.Rook, Color.White, Direction.South);
//             
//             var dut = new PawnPromotionRule();
//
//             // Act
//             var result = dut.GetPaths(start, piece, mock.Object);
//
//             // Assert
//             result.Should().BeEmpty();
//             mock.VerifyAll();
//         }
//
//         [Test]
//         public void Apply_WithBishopSouth_ShouldHaveNoPathsAndNotCallProvider()
//         {
//             // Arrange
//             var mock = new Mock<IPieceEnumerationProvider>();
//
//             var start = new Point(2, 2);
//             var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);
//             
//             var dut = new PawnPromotionRule();
//
//             // Act
//             var result = dut.GetPaths(start, piece, mock.Object);
//
//             // Assert
//             result.Should().BeEmpty();
//             mock.VerifyAll();
//         }
//
//         [Test]
//         public void Apply_WithQueenSouth_ShouldHaveNoPathsAndNotCallProvider()
//         {
//             // Arrange
//             var mock = new Mock<IPieceEnumerationProvider>();
//
//             var start = new Point(2, 2);
//             var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
//             
//             var dut = new PawnPromotionRule();
//
//             // Act
//             var result = dut.GetPaths(start, piece, mock.Object);
//
//             // Assert
//             result.Should().BeEmpty();
//             mock.VerifyAll();
//         }
//
//         [Test]
//         public void Apply_WithKingSouth_ShouldHaveNoPathsAndNotCallProvider()
//         {
//             // Arrange
//             var mock = new Mock<IPieceEnumerationProvider>();
//
//             var start = new Point(2, 2);
//             var piece = new Piece(PieceType.King, Color.White, Direction.South);
//             
//             var dut = new PawnPromotionRule();
//
//             // Act
//             var result = dut.GetPaths(start, piece, mock.Object);
//
//             // Assert
//             result.Should().BeEmpty();
//             mock.VerifyAll();
//         }
//
//         [Test]
//         public void Apply_WithKnightSouth_ShouldHaveNoPathsAndNotCallProvider()
//         {
//             // Arrange
//             var mock = new Mock<IPieceEnumerationProvider>();
//
//             var start = new Point(2, 2);
//             var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
//             
//             var dut = new PawnPromotionRule();
//
//             // Act
//             var result = dut.GetPaths(start, piece, mock.Object);
//
//             // Assert
//             result.Should().BeEmpty();
//             mock.VerifyAll();
//         }
//
//         [Test]
//         public void Apply_PawnSouthWhenInCenterOfBoard_ShouldHaveTwoPathsWithOneCount()
//         {
//             // Arrange
//             var mock = new Mock<IPieceEnumerationProvider>();
//
//             var start = new Point(2, 2);
//             var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
//             
//             var dut = new PawnPromotionRule();
//
//             // Act
//             var result = dut.GetPaths(start, piece, mock.Object);
//
//             // Assert
//             result.Should().BeEmpty();
//             mock.VerifyAll();
//         }
//         
//         [Test]
//         public void Apply_PawnWestWhenInCenterOfBoard_ShouldHaveTwoPathsWithOneCount()
//         {
//             // Arrange
//             var mock = new Mock<IPieceEnumerationProvider>();
//
//             var start = new Point(2, 2);
//             var piece = new Piece(PieceType.Pawn, Color.White, Direction.West);
//             
//             var dut = new PawnPromotionRule();
//
//             // Act
//             var result = dut.GetPaths(start, piece, mock.Object);
//
//             // Assert
//             result.Should().BeEmpty();
//             mock.VerifyAll();
//         }
//     }
// }