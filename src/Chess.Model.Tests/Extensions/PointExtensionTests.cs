using System.Drawing;
using Chess.Model.Extensions;
using Chess.Model.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Extensions
{
    public class PointExtensionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CartesianOffset_WhenPositiveX_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            var offset = new Point(7,0);
            
            // Act
            var result = pt.CartesianOffset(offset);
            
            // Assert
            result.X.Should().Be(8);
            result.Y.Should().Be(3);
        }
        
        [Test]
        public void CartesianOffset_WhenPositiveY_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            var offset = new Point(0,3);
            
            // Act
            var result = pt.CartesianOffset(offset);
            
            // Assert
            result.X.Should().Be(1);
            result.Y.Should().Be(6);
        }
        
        [Test]
        public void CartesianOffset_WhenPositiveXAndY_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            var offset = new Point(5,7);
            
            // Act
            var result = pt.CartesianOffset(offset);
            
            // Assert
            result.X.Should().Be(6);
            result.Y.Should().Be(10);
        }
        
        [Test]
        public void CartesianOffset_WhenNegativeX_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            var offset = new Point(-6,0);
            
            // Act
            var result = pt.CartesianOffset(offset);
            
            // Assert
            result.X.Should().Be(-5);
            result.Y.Should().Be(3);
        }
        
        [Test]
        public void CartesianOffset_WhenNegativeY_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            var offset = new Point(0,-5);
            
            // Act
            var result = pt.CartesianOffset(offset);
            
            // Assert
            result.X.Should().Be(1);
            result.Y.Should().Be(-2);
        }

        [Test]
        public void CartesianOffset_WhenNegativeXAndY_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            var offset = new Point(-5,-7);
            
            // Act
            var result = pt.CartesianOffset(offset);
            
            // Assert
            result.X.Should().Be(-4);
            result.Y.Should().Be(-4);
        }

        [Test]
        public void PolarOffset_WhenDirectionNone_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.None;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(1);
            result.Y.Should().Be(3);
        }

        [Test]
        public void PolarOffset_WhenDirectionNorthWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.NorthWest;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(-4);
            result.Y.Should().Be(8);
        }

        [Test]
        public void PolarOffset_WhenDirectionNorth_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.North;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(1);
            result.Y.Should().Be(8);
        }

        [Test]
        public void PolarOffset_WhenDirectionNorthEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.NorthEast;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(6);
            result.Y.Should().Be(8);
        }

        [Test]
        public void PolarOffset_WhenDirectionEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.East;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(6);
            result.Y.Should().Be(3);
        }

        [Test]
        public void PolarOffset_WhenDirectionSouthEast_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.SouthEast;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(6);
            result.Y.Should().Be(-2);
        }

        [Test]
        public void PolarOffset_WhenDirectionSouth_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.South;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(1);
            result.Y.Should().Be(-2);
        }

        [Test]
        public void PolarOffset_WhenDirectionSouthWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.SouthWest;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(-4);
            result.Y.Should().Be(-2);
        }

        [Test]
        public void PolarOffset_WhenDirectionWest_ShouldHaveCorrectResult()
        {
            // Arrange
            var pt = new Point(1,3);
            const Direction direction = Direction.West;
            const int count = 5;
            
            // Act
            var result = pt.PolarOffset(direction, count);
            
            // Assert
            result.X.Should().Be(-4);
            result.Y.Should().Be(3);
        }
    }
}