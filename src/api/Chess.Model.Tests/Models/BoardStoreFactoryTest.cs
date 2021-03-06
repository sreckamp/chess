﻿using System.Linq;
using Chess.Model.Models;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable PossibleMultipleEnumeration

namespace Chess.Model.Tests.Models
{
    public class BoardStoreFactoryTest
    {
        [Test]
        public void Create_WithTwoPlayers_ShouldHave8x8With0CornersWithWhiteAtSouthAndBlackAtNorth()
        {
            // Arrange
            var factory = BoardFactory.Instance;
            
            // Act
            var board = factory.Create(Version.TwoPlayer);

            // Assert
            board.Size.Should().Be(8);
            board.Corners.Should().Be(0);
            board.Where(sq => sq.Item1.Y < 2 || sq.Item1.Y > 5).Should().OnlyContain(square => !square.Item2.IsEmpty);
            board.Where(sq => sq.Item1.Y < 2 || sq.Item1.Y > 5).Should().OnlyContain(square => !square.Item2.HasMoved);
            board.Where(sq => sq.Item1.Y > 1 && sq.Item1.Y < 6).Should().OnlyContain(square => square.Item2.IsEmpty);
            board.Where(sq => sq.Item1.Y < 2).Should().OnlyContain(square => square.Item2.Color == Color.White);
            board.Where(sq => sq.Item1.Y > 5).Should().OnlyContain(square => square.Item2.Color == Color.Black);
            board.Where(sq => sq.Item1.Y < 2).Should().OnlyContain(square => square.Item2.Edge == Direction.South);
            board.Where(sq => sq.Item1.Y > 5).Should().OnlyContain(square => square.Item2.Edge == Direction.North);
            board.Where(sq => sq.Item1.Y == 1 || sq.Item1.Y == 6).Should().OnlyContain(square => square.Item2.Type == PieceType.Pawn);

            board[0,0].Type.Should().Be(PieceType.Rook);
            board[1,0].Type.Should().Be(PieceType.Knight);
            board[2,0].Type.Should().Be(PieceType.Bishop);
            board[3,0].Type.Should().Be(PieceType.Queen);
            board[4,0].Type.Should().Be(PieceType.King);
            board[5,0].Type.Should().Be(PieceType.Bishop);
            board[6,0].Type.Should().Be(PieceType.Knight);
            board[7,0].Type.Should().Be(PieceType.Rook);
            
            board[0,7].Type.Should().Be(PieceType.Rook);
            board[1,7].Type.Should().Be(PieceType.Knight);
            board[2,7].Type.Should().Be(PieceType.Bishop);
            board[3,7].Type.Should().Be(PieceType.Queen);
            board[4,7].Type.Should().Be(PieceType.King);
            board[5,7].Type.Should().Be(PieceType.Bishop);
            board[6,7].Type.Should().Be(PieceType.Knight);
            board[7,7].Type.Should().Be(PieceType.Rook);
        }
        
        [Test]
        public void Create_WithFourPlayers_ShouldHave14x14With3CornersWithWhiteAtSouthAndSilverAtEastAndBlackAtNorthAndGoldAtEast()
        {
            // Arrange
            var factory = BoardFactory.Instance;
            
            // Act
            var board = factory.Create(Version.FourPlayer);

            // Assert
            board.Size.Should().Be(14);
            board.Corners.Should().Be(3);
            var onBoard = board.Where(sq => board.IsOnBoard(sq.Item1));  
            onBoard.Where(sq => sq.Item1.Y < 2 || sq.Item1.Y > 11 ||
                           sq.Item1.X < 2 || sq.Item1.X > 11)
                                                .Should().OnlyContain(square => !square.Item2.IsEmpty);
            onBoard.Where(sq => sq.Item1.Y < 2 || sq.Item1.Y > 11 ||
                                sq.Item1.X < 2 || sq.Item1.X > 11)
                                                .Should().OnlyContain(square => !square.Item2.HasMoved);
            onBoard.Where(sq => sq.Item1.X > 1 && sq.Item1.X < 12 &&
                                sq.Item1.Y > 1 && sq.Item1.Y < 12).Should().OnlyContain(square => square.Item2.IsEmpty);
            onBoard.Where(sq => sq.Item1.Y < 2).Should().OnlyContain(square => square.Item2.Color == Color.White);
            onBoard.Where(sq => sq.Item1.Y < 2).Should().OnlyContain(square => square.Item2.Edge == Direction.South);
            onBoard.Where(sq => sq.Item1.X < 2).Should().OnlyContain(square => square.Item2.Color == Color.Silver);
            onBoard.Where(sq => sq.Item1.X < 2).Should().OnlyContain(square => square.Item2.Edge == Direction.West);
            onBoard.Where(sq => sq.Item1.Y > 11).Should().OnlyContain(square => square.Item2.Color == Color.Black);
            onBoard.Where(sq => sq.Item1.Y > 11).Should().OnlyContain(square => square.Item2.Edge == Direction.North);
            onBoard.Where(sq => sq.Item1.X > 11).Should().OnlyContain(square => square.Item2.Color == Color.Gold);
            onBoard.Where(sq => sq.Item1.X > 11).Should().OnlyContain(square => square.Item2.Edge == Direction.East);
            onBoard.Where(sq => sq.Item1.X == 1 || sq.Item1.X == 12 ||
                                        sq.Item1.Y == 1 || sq.Item1.Y == 12).Should().OnlyContain(square => square.Item2.Type == PieceType.Pawn);

            board[3,0].Type.Should().Be(PieceType.Rook);
            board[4,0].Type.Should().Be(PieceType.Knight);
            board[5,0].Type.Should().Be(PieceType.Bishop);
            board[6,0].Type.Should().Be(PieceType.King);
            board[7,0].Type.Should().Be(PieceType.Queen);
            board[8,0].Type.Should().Be(PieceType.Bishop);
            board[9,0].Type.Should().Be(PieceType.Knight);
            board[10,0].Type.Should().Be(PieceType.Rook);
            
            board[0,3].Type.Should().Be(PieceType.Rook);
            board[0,4].Type.Should().Be(PieceType.Knight);
            board[0,5].Type.Should().Be(PieceType.Bishop);
            board[0,6].Type.Should().Be(PieceType.Queen);
            board[0,7].Type.Should().Be(PieceType.King);
            board[0,8].Type.Should().Be(PieceType.Bishop);
            board[0,9].Type.Should().Be(PieceType.Knight);
            board[0,10].Type.Should().Be(PieceType.Rook);
            
            board[3,13].Type.Should().Be(PieceType.Rook);
            board[4,13].Type.Should().Be(PieceType.Knight);
            board[5,13].Type.Should().Be(PieceType.Bishop);
            board[6,13].Type.Should().Be(PieceType.Queen);
            board[7,13].Type.Should().Be(PieceType.King);
            board[8,13].Type.Should().Be(PieceType.Bishop);
            board[9,13].Type.Should().Be(PieceType.Knight);
            board[10,13].Type.Should().Be(PieceType.Rook);

            board[13,3].Type.Should().Be(PieceType.Rook);
            board[13,4].Type.Should().Be(PieceType.Knight);
            board[13,5].Type.Should().Be(PieceType.Bishop);
            board[13,6].Type.Should().Be(PieceType.King);
            board[13,7].Type.Should().Be(PieceType.Queen);
            board[13,8].Type.Should().Be(PieceType.Bishop);
            board[13,9].Type.Should().Be(PieceType.Knight);
            board[13,10].Type.Should().Be(PieceType.Rook);
        }
    }
}