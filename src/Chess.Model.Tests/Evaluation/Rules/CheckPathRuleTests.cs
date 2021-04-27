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
    public class CheckPathRuleTests
    {
        [Test]
        public void Apply_WithNoMoves_ShouldShouldCallChainButNotMark()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path("Test") {
                Direction = Direction.None,
                AllowMove = true,
                AllowTake = true,
                Squares = new (Point, Piece)[0]
            };

            var dut = new CheckPathRule(chainPathRuleMock.Object);

            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>()), Times.Never);
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
        
        [Test]
        public void Apply_WithNoDirection_ShouldCallChainAndMarkTwice()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var setMock = new Mock<ISet<Color>>();
            markingsMock.SetupGet(provider => provider.InCheck).Returns(setMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path("Test") {
                Direction = Direction.None,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 3),
                Piece = new Piece(PieceType.Knight, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(5, 4), new Piece(PieceType.King, Color.Black, Direction.North))
                }
            };

            var dut = new CheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(5,4), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
    }
}
