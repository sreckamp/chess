using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Rules
{
    public sealed class KnightOffsetEnumerableTests
    {
        [Test]
        public void Enumerate_WhenInCenterOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 5);
            var start = new Point(2,2);
            var enumerable = new KnightOffsetEnumerable<Point>(start, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new [] {
                (new Point(1, 4), new Point(1, 4)),
                (new Point(3, 4), new Point(3, 4)),
                (new Point(0, 3), new Point(0, 3)),
                (new Point(4, 3), new Point(4, 3)),
                (new Point(0, 1), new Point(0, 1)),
                (new Point(4, 1), new Point(4, 1)),
                (new Point(3, 0), new Point(3, 0)),
                (new Point(1, 0), new Point(1, 0))
            });
        }
        
        [Test]
        public void Enumerate_WhenOffTopOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 5, 4);
            var start = new Point(2,2);
            var enumerable = new KnightOffsetEnumerable<Point>(start, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new [] {
                (new Point(0, 3), new Point(0, 3)),
                (new Point(4, 3), new Point(4, 3)),
                (new Point(0, 1), new Point(0, 1)),
                (new Point(4, 1), new Point(4, 1)),
                (new Point(3, 0), new Point(3, 0)),
                (new Point(1, 0), new Point(1, 0))
            });
        }
        
        [Test]
        public void Enumerate_WhenOffBottomOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 1, 5, 4);
            var start = new Point(2,2);
            var enumerable = new KnightOffsetEnumerable<Point>(start, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new [] {
                (new Point(1, 4), new Point(1, 4)),
                (new Point(3, 4), new Point(3, 4)),
                (new Point(0, 3), new Point(0, 3)),
                (new Point(4, 3), new Point(4, 3)),
                (new Point(0, 1), new Point(0, 1)),
                (new Point(4, 1), new Point(4, 1))
            });
        }
        
        [Test]
        public void Enumerate_WhenOffRightOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(0, 0, 4, 5);
            var start = new Point(2,2);
            var enumerable = new KnightOffsetEnumerable<Point>(start, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new [] {
                (new Point(0, 1), new Point(0, 1)),
                (new Point(0, 3), new Point(0, 3)),
                (new Point(1, 0), new Point(1, 0)),
                (new Point(1, 4), new Point(1, 4)),
                (new Point(3, 0), new Point(3, 0)),
                (new Point(3, 4), new Point(3, 4))
            });
        }
        
        [Test]
        public void Enumerate_WhenOffLeftOfBoard_ShouldHaveCorrectResult()
        {
            // Arrange
            var box = new Rectangle(1, 0, 4, 5);
            var start = new Point(2,2);
            var enumerable = new KnightOffsetEnumerable<Point>(start, point => point, point => box.Contains(point));
            
            // Act
            // Assert
            enumerable.Should().Contain(new [] {
                (new Point(1, 0), new Point(1, 0)),
                (new Point(1, 4), new Point(1, 4)),
                (new Point(3, 0), new Point(3, 0)),
                (new Point(3, 4), new Point(3, 4)),
                (new Point(4, 1), new Point(4, 1)),
                (new Point(4, 3), new Point(4, 3))
            });
        }
    }
}
