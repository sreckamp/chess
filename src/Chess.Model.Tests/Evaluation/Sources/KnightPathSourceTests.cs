using System.Drawing;
using Chess.Model.Evaluation.Models;
using Chess.Model.Evaluation.Sources;
using Chess.Model.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Tests.Evaluation.Sources
{
    public sealed class KnightPathSourceTests
    {
        [Test]
        public void GetPaths_WithRookSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Rook, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithBishopSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Bishop, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetPaths_WithPawnSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Pawn, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();

        }

        [Test]
        public void GetPaths_WithQueenSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Queen, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();

        }

        [Test]
        public void GetPaths_WithKingSouth_ShouldHaveNoPathsAndNotCallProvider()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            var start = new Point(2, 2);
            var piece = new Piece(PieceType.King, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().BeEmpty();

        }

        [Test]
        public void GetPaths_WithKnightSouth_ShouldHaveEightPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateKnight(new Point(2, 2))).Returns(
                () => new[]
                {
                    (new Point(1, 4), Piece.Empty),
                    (new Point(3, 4), Piece.Empty),
                    (new Point(4, 3), Piece.Empty),
                    (new Point(4, 1), Piece.Empty),
                    (new Point(1, 0), Piece.Empty),
                    (new Point(3, 0), Piece.Empty),
                    (new Point(0, 1), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithKnightWest_ShouldHaveEightPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateKnight(new Point(2, 2))).Returns(
                () => new[]
                {
                    (new Point(1, 4), Piece.Empty),
                    (new Point(3, 4), Piece.Empty),
                    (new Point(4, 3), Piece.Empty),
                    (new Point(4, 1), Piece.Empty),
                    (new Point(1, 0), Piece.Empty),
                    (new Point(3, 0), Piece.Empty),
                    (new Point(0, 1), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.West);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithKnightNorth_ShouldHaveEightPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateKnight(new Point(2, 2))).Returns(
                () => new[]
                {
                    (new Point(1, 4), Piece.Empty),
                    (new Point(3, 4), Piece.Empty),
                    (new Point(4, 3), Piece.Empty),
                    (new Point(4, 1), Piece.Empty),
                    (new Point(1, 0), Piece.Empty),
                    (new Point(3, 0), Piece.Empty),
                    (new Point(0, 1), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.North);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithKnightEast_ShouldHaveEightPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateKnight(new Point(2, 2))).Returns(
                () => new[]
                {
                    (new Point(1, 4), Piece.Empty),
                    (new Point(3, 4), Piece.Empty),
                    (new Point(4, 3), Piece.Empty),
                    (new Point(4, 1), Piece.Empty),
                    (new Point(1, 0), Piece.Empty),
                    (new Point(3, 0), Piece.Empty),
                    (new Point(0, 1), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.East);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithKnightSouthNearWestEdge_ShouldHaveSixPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateKnight(new Point(2, 2))).Returns(
                () => new[]
                {
                    (new Point(1, 4), Piece.Empty),
                    (new Point(3, 4), Piece.Empty),
                    (new Point(4, 3), Piece.Empty),
                    (new Point(4, 1), Piece.Empty),
                    (new Point(1, 0), Piece.Empty),
                    (new Point(3, 0), Piece.Empty),
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 0)));
                }
            );
        }

        [Test]
        public void GetPaths_WithKnightSouthNearNorthEdge_ShouldHaveSixPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateKnight(new Point(2, 2))).Returns(
                () => new[]
                {
                    (new Point(4, 3), Piece.Empty),
                    (new Point(4, 1), Piece.Empty),
                    (new Point(1, 0), Piece.Empty),
                    (new Point(3, 0), Piece.Empty),
                    (new Point(0, 1), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithKnightSouthNearEastEdge_ShouldHaveSixPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateKnight(new Point(2, 2))).Returns(
                () => new[]
                {
                    (new Point(1, 4), Piece.Empty),
                    (new Point(3, 4), Piece.Empty),
                    (new Point(1, 0), Piece.Empty),
                    (new Point(3, 0), Piece.Empty),
                    (new Point(0, 1), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 0)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }

        [Test]
        public void GetPaths_WithKnightSouthNearSouthEdge_ShouldHaveSixPaths()
        {
            // Arrange
            var mock = new Mock<IPieceEnumerationProvider>();
            mock.Setup(provider => provider.EnumerateKnight(new Point(2, 2))).Returns(
                () => new[]
                {
                    (new Point(1, 4), Piece.Empty),
                    (new Point(3, 4), Piece.Empty),
                    (new Point(4, 3), Piece.Empty),
                    (new Point(4, 1), Piece.Empty),
                    (new Point(0, 1), Piece.Empty),
                    (new Point(0, 3), Piece.Empty)
                }
            );

            var start = new Point(2, 2);
            var piece = new Piece(PieceType.Knight, Color.White, Direction.South);
            
            var dut = new KnightPathSource();

            // Act
            var result = dut.GetPaths(start, piece, mock.Object);

            // Assert
            result.Should().SatisfyRespectively(
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(1, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(3, 4)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 3)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(4, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 1)));
                },
                path =>
                {
                    path.Direction.Should().Be(Direction.None);
                    path.Start.Should().Be(start);
                    path.Piece.Should().Be(piece);
                    path.AllowMove.Should().BeTrue();
                    path.AllowTake.Should().BeTrue();
                    path.Squares.Should().SatisfyRespectively(sq => sq.Item1.Should().Be(new Point(0, 3)));
                }
            );
        }
    }
}
