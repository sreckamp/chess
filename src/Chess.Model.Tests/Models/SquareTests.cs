using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using FluentAssertions;
using NUnit.Framework;
// ReSharper disable PossibleMultipleEnumeration

namespace Chess.Model.Tests.Models
{
    // public class SquareTests
    // {
    //     [Test]
    //     public void Mark_GetMarkers_WhenSameType_ShouldReturnMarker()
    //     {
    //         // Arrange
    //         var square = new Square();
    //         var marker = new SimpleMarker(MarkerType.Cover, new Square
    //         {
    //             Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)                
    //         }, Direction.None);
    //
    //         // Act
    //         square.Mark(marker);
    //         var result = square.GetMarkers(MarkerType.Cover);
    //
    //         // Assert
    //         result.Should().HaveCount(1);
    //         result.First().Should().Be(marker);
    //     }
    //
    //     [Test]
    //     public void Mark_GetMarkers_WhenDifferentTypes_ShouldReturnNoMarkers()
    //     {
    //         // Arrange
    //         var square = new Square();
    //         var marker = new SimpleMarker(MarkerType.Cover, new Square
    //         {
    //             Piece = new Piece(PieceType.Bishop, Color.Black, Direction.North)                
    //         }, Direction.None);
    //
    //         // Act
    //         square.Mark(marker);
    //         var result = square.GetMarkers(MarkerType.Pin);
    //
    //         // Assert
    //         result.Should().BeEmpty();
    //     }
    // }
}
