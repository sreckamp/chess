using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Move;
using FluentAssertions;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Tests.Move
{
    public class CastleMoveTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToString_WhenKingSideWhite_ShouldBeOO()
        {
            // Arrange
            var move = new CastleMove
            {
                Piece = new Piece(PieceType.King, Color.None, Direction.None),
                From = new Point(4, 0),
                To = new Point(6, 0),
                RookMove = new SimpleMove
                {
                    Piece = new Piece(PieceType.Rook, Color.None, Direction.None),
                    From = new Point(7, 0),
                    To = new Point(5, 0),
                },
            };
            
            // Act
            var result = move.ToString();

            // Assert
            result.Should().Be("O-O");
        }

        [Test]
        public void ToString_WhenQueenSideWhite_ShouldBeOOO()
        {
            // Arrange
            var move = new CastleMove
            {
                Piece = new Piece(PieceType.King, Color.None, Direction.None),
                From = new Point(4, 0),
                To = new Point(2, 0),
                RookMove = new SimpleMove
                {
                    Piece = new Piece(PieceType.Rook, Color.None, Direction.None),
                    From = new Point(0, 0),
                    To = new Point(3, 0),
                },
            };
            
            // Act
            var result = move.ToString();

            // Assert
            result.Should().Be("O-O-O");
        }

        [Test]
        public void ToString_WhenKingSideBlack_ShouldBeOO()
        {
            // Arrange
            var move = new CastleMove
            {
                Piece = new Piece(PieceType.King, Color.None, Direction.None),
                From = new Point(4, 7),
                To = new Point(6, 7),
                RookMove = new SimpleMove
                {
                    Piece = new Piece(PieceType.Rook, Color.None, Direction.None),
                    From = new Point(7, 7),
                    To = new Point(5, 7),
                },
            };
            
            // Act
            var result = move.ToString();

            // Assert
            result.Should().Be("O-O");
        }

        [Test]
        public void ToString_WhenQueenSideBlack_ShouldBeOOO()
        {
            // Arrange
            var move = new CastleMove
            {
                Piece = new Piece(PieceType.King, Color.None, Direction.None),
                From = new Point(4, 7),
                To = new Point(2, 7),
                RookMove = new SimpleMove
                {
                    Piece = new Piece(PieceType.Rook, Color.None, Direction.None),
                    From = new Point(0, 7),
                    To = new Point(3, 7),
                },
            };
            
            // Act
            var result = move.ToString();

            // Assert
            result.Should().Be("O-O-O");
        }
    }
}