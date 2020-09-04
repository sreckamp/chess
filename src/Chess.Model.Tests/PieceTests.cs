using NUnit.Framework;
using FluentAssertions;

namespace Chess.Model.Tests
{
    public class PieceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreatePawn_WhenDirectionNorth_ShouldHaveAttacksToNorthWestAndNorthEast()
        {
            var pawn = Piece.CreatePawn(Color.None, Direction.North);
            
            pawn.AttackRules[Direction.NorthEast].MaxCount.Should().Be(1);
            pawn.AttackRules[Direction.NorthWest].MaxCount.Should().Be(1);

            pawn.AttackRules[Direction.North].MaxCount.Should().Be(0);
            pawn.AttackRules[Direction.East].MaxCount.Should().Be(0);
            pawn.AttackRules[Direction.SouthEast].MaxCount.Should().Be(0);
            pawn.AttackRules[Direction.South].MaxCount.Should().Be(0);
            pawn.AttackRules[Direction.SouthWest].MaxCount.Should().Be(0);
            pawn.AttackRules[Direction.West].MaxCount.Should().Be(0);
        }
    }
}