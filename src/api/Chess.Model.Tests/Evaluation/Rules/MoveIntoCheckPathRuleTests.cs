using System;
using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Evaluation.Models;
using Chess.Model.Evaluation.Rules;
using Chess.Model.Models;
using Moq;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Tests.Evaluation.Rules
{
    public class MoveIntoCheckPathRuleTests
    {
        [Test, Ignore("todo")]
        public void Apply_WithNoMoves_ShouldShouldCallChainButNotMark()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>()))
                .Verifiable();

            var path = new Path
            {
                Direction = Direction.None,
                AllowMove = true,
                AllowTake = true,
                Squares = Array.Empty<(Point, Piece)>()
            };

            var dut = new MoveIntoCheckPathRule(chainPathRuleMock.Object);

            // Act
            dut.Apply(markingsMock.Object, path);

            // Assert
            markingsMock.Verify(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>()), Times.Never);
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }

        [Test, Ignore("todo")]
        public void Apply_WithNoDirection_ShouldCallChainAndMarkTwice()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>()))
                .Verifiable();

            var path = new Path
            {
                Direction = Direction.None,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 3),
                Piece = new Piece(PieceType.Knight, Color.White, Direction.South),
                Squares = new[]
                {
                    (new Point(5, 4), new Piece(PieceType.King, Color.Black, Direction.North))
                }
            };

            var dut = new MoveIntoCheckPathRule(chainPathRuleMock.Object);

            // Act
            dut.Apply(markingsMock.Object, path);

            // Assert
            markingsMock.Verify(provider => provider.Mark(new Point(3, 3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(5, 4), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
    }
}