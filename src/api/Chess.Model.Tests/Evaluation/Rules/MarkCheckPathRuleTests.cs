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

            var path = new Path
            {
                Direction = Direction.None,
                AllowMove = true,
                AllowTake = true,
                Squares = Array.Empty<(Point, Piece)>()
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);

            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>()), Times.Never);
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
        
        [Test]
        public void Apply_WithNoDirection_ShouldCallChainAndMarkAttackerAndTarget()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
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

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(2));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(5,4), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
        
        [Test]
        public void Apply_WithDirection_ShouldCallChainAndMarkSquaresBeforeAndAfterKing()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(6));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,2), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,1), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,0), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }

        [Test]
        public void Apply_WithDirectionAndPieceKingColorAfter_ShouldCallChainAndMarkSquaresBeforeAndKingOnly()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Pawn, Color.Black, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(3));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }

        [Test]
        public void Apply_WithDirectionAndPieceAttackingColorAfter_ShouldCallChainAndMarkSquaresBeforeAndKingTheOneAfter()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Pawn, Color.White, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(4));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,2), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
        
        [Test]
        public void Apply_WithDirectionAndPieceNotKingColorAfter_ShouldCallChainAndMarkSquaresBeforeAndKingTheOneAfter()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Pawn, Color.Silver, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(4));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,2), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
        
        [Test]
        public void Apply_WithDirectionAndPieceKingColorAfterWithGap_ShouldCallChainAndMarkSquaresBeforeAndKingTheOneAfter()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.Pawn, Color.Black, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(4));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,2), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
        
        [Test]
        public void Apply_WithDirectionAndPieceAttackingColorAfterWithGap_ShouldCallChainAndMarkSquaresBeforeAndKingTheTwoAfter()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.Pawn, Color.White, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(5));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,2), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,1), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }

        [Test]
        public void Apply_WithDirectionAndPieceNotKingColorAfterWithGap_ShouldCallChainAndMarkSquaresBeforeAndKingTheTwoAfter()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.Pawn, Color.Silver, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(5));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,2), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,1), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }

        [Test]
        public void Apply_WithDirectionAndOtherColorKingAfterOpponentKing_ShouldCallChainAndMarkSquaresBeforeAndKingTheOneAfter()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.King, Color.Silver, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(4));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,2), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }

        [Test]
        public void Apply_WithDirectionAndAttackingKingAfterOpponentKing_ShouldCallChainAndMarkSquaresBeforeAndKingTheOneAfter()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.Black, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.King, Color.White, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Exactly(4));
            markingsMock.Verify(provider => provider.Mark(new Point(3,5), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,4), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,3), It.IsAny<CheckMarker>()));
            markingsMock.Verify(provider => provider.Mark(new Point(3,2), It.IsAny<CheckMarker>()));
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }

        [Test]
        public void Apply_WithDirectionAndAttackingKingBeforeOpponentKing_ShouldCallChainAndNotMarkAnything()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.King, Color.White, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.King, Color.Black, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Never);
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
        
        [Test]
        public void Apply_WithDirectionAndAttackingPiece_ShouldCallChainAndNotMarkAnything()
        {
            // Arrange
            var markingsMock = new Mock<IMarkingsProvider>();
            markingsMock.Setup(provider => provider.Mark(It.IsAny<Point>(), It.IsAny<IMarker>())).Verifiable();
            var dictionaryMock = new Mock<IDictionary<Color, Point>>();
            markingsMock.SetupGet(provider => provider.KingLocations).Returns(dictionaryMock.Object);
            var chainPathRuleMock = new Mock<IPathRule>();
            chainPathRuleMock.Setup(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), It.IsAny<Path>())).Verifiable();

            var path = new Path {
                Direction = Direction.South,
                AllowMove = true,
                AllowTake = true,
                Start = new Point(3, 5),
                Piece = new Piece(PieceType.Rook, Color.White, Direction.South),
                Squares = new []
                {
                    (new Point(3, 4), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 3), new Piece(PieceType.Bishop, Color.White, Direction.North)),
                    (new Point(3, 2), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 1), new Piece(PieceType.Empty, Color.None, Direction.None)),
                    (new Point(3, 0), new Piece(PieceType.Empty, Color.None, Direction.None))
                }
            };

            var dut = new MarkCheckPathRule(chainPathRuleMock.Object);
            
            // Act
            dut.Apply(markingsMock.Object, path);
            
            // Assert
            markingsMock.Verify(provider =>
                provider.Mark(It.IsAny<Point>(), It.IsAny<CheckMarker>()), Times.Never);
            chainPathRuleMock.Verify(pathRule => pathRule.Apply(It.IsAny<IMarkingsProvider>(), path));
        }
    }
}
