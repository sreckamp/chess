using System.Drawing;
using Chess.Model.Evaluation.Enumerables;
using Chess.Model.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Rules
{
    public class StraightLineEnumerableTests
    {
        [Test]
        public void Enumerate_WhenNorthWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            const Direction direction = Direction.NorthWest;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new []
            {
                (new Point(1, 3), new Point(1, 3)),
                (new Point(0, 4), new Point(0, 4))
            });
        }
        
        [Test]
        public void Enumerate_WhenNorth_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            const Direction direction = Direction.North;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new []
            {
                (new Point(2, 3), new Point(2, 3)),
                (new Point(2, 4), new Point(2, 4))
            });
        }
        
        [Test]
        public void Enumerate_WhenNorthEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            const Direction direction = Direction.NorthEast;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new []
            {
                (new Point(3, 3), new Point(3, 3)),
                (new Point(4, 4), new Point(4, 4))
            });
        }

        [Test]
        public void Enumerate_WhenWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2, 2);
            const Direction direction = Direction.West;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));

            // Act
            // Assert
            enumerable.Should().Contain(new[]
            {
                (new Point(1, 2), new Point(1, 2)),
                (new Point(0, 2), new Point(0, 2))
            });
        }

        [Test]
        public void Enumerate_WhenNone_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            const Direction direction = Direction.None;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().BeEmpty();
        }
        
        [Test]
        public void Enumerate_WhenEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            const Direction direction = Direction.East;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new []
            {
                (new Point(3, 2), new Point(3, 2)),
                (new Point(4, 2), new Point(4, 2))
            });
        }
        
        [Test]
        public void Enumerate_WhenSouthWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            const Direction direction = Direction.SouthWest;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new []
            {
                (new Point(1, 1), new Point(1, 1)), 
                (new Point(0, 0), new Point(0, 0))
            });
        }
        
        [Test]
        public void Enumerate_WhenSouth_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            const Direction direction = Direction.South;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new []
            {
                (new Point(2, 1), new Point(2, 1)), 
                (new Point(2, 0), new Point(2, 0))
            });
        }
        
        [Test]
        public void Enumerate_WhenSouthEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            const Direction direction = Direction.SouthEast;

            var enumerable =
                new StraightLineEnumerable<Point>(start, direction, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new []
            {
                (new Point(3, 1), new Point(3, 1)), 
                (new Point(4, 0), new Point(4, 0))
            });
        }
    }
}
