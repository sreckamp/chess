using System.Drawing;
using Chess.Model.Extensions;
using Chess.Model.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Model.Tests.Extensions
{
    public class PointExtensionTests
    {
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
        
        [Test]
        public void IsBetween_WhenAtStart_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(1,3);
            var end = new Point(1,2);
            var pt = new Point(1,3);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenAtEnd_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(1,3);
            var end = new Point(1,2);
            var pt = new Point(1,2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenPointIsAtSameStartAndEnd_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(1,2);
            var end = new Point(1,2);
            var pt = new Point(1,2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalEndGreaterThanStart_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(-1,0);
            var end = new Point(1,0);
            var pt = new Point(0,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalStartGreaterThanEnd_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(1,0);
            var end = new Point(-1,0);
            var pt = new Point(0,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalEndGreaterThanStartAndPointIsLess_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(-1,0);
            var end = new Point(1,0);
            var pt = new Point(-2,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalStartGreaterThanEndAndPointIsLess_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(1,0);
            var end = new Point(-1,0);
            var pt = new Point(-2,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalEndGreaterThanStartAndPointIsGreater_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(-1,0);
            var end = new Point(1,0);
            var pt = new Point(2,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalStartGreaterThanEndAndPointIsGreater_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(1,0);
            var end = new Point(-1,0);
            var pt = new Point(2,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreVerticalEndGreaterThanStart_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(0,-1);
            var end = new Point(0,1);
            var pt = new Point(0,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreVerticalStartGreaterThanEnd_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(0,1);
            var end = new Point(0,-1);
            var pt = new Point(0,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreVerticalEndGreaterThanStartAndPointIsLess_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(0,-1);
            var end = new Point(0,1);
            var pt = new Point(0,-2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreVerticalStartGreaterThanEndAndPointIsLess_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(0,1);
            var end = new Point(0,-1);
            var pt = new Point(0,-2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreVerticalEndGreaterThanStartAndPointIsGreater_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(0,-1);
            var end = new Point(0,1);
            var pt = new Point(0,2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreVerticalStartGreaterThanEndAndPointIsGreater_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(0,1);
            var end = new Point(0,-1);
            var pt = new Point(0,2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthEastEndGreaterThanStart_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(-1,-1);
            var end = new Point(1,1);
            var pt = new Point(0,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthEastStartGreaterThanEnd_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(1,1);
            var end = new Point(-1,-1);
            var pt = new Point(0,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthEastEndGreaterThanStartAndPointIsLess_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(-1,-1);
            var end = new Point(1,1);
            var pt = new Point(-2,-2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthEastStartGreaterThanEndAndPointIsLess_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(1,1);
            var end = new Point(-1,-1);
            var pt = new Point(-2,-2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthEastEndGreaterThanStartAndPointIsGreater_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(-1,-1);
            var end = new Point(1,1);
            var pt = new Point(2,2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthEastStartGreaterThanEndAndPointIsGreater_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(1,1);
            var end = new Point(-1,-1);
            var pt = new Point(2,2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthWestEndGreaterThanStart_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(-1,1);
            var end = new Point(1,-1);
            var pt = new Point(0,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthWestStartGreaterThanEnd_ShouldReturnTrue()
        {
            // Arrange
            var start = new Point(1,-1);
            var end = new Point(-1,1);
            var pt = new Point(0,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthWestEndGreaterThanStartAndPointIsLess_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(-1,1);
            var end = new Point(1,-1);
            var pt = new Point(-2,2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthWestStartGreaterThanEndAndPointIsLess_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(1,-1);
            var end = new Point(-1,1);
            var pt = new Point(-2,2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthWestEndGreaterThanStartAndPointIsGreater_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(-1,1);
            var end = new Point(1,-1);
            var pt = new Point(2,-2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreNorthWestStartGreaterThanEndAndPointIsGreater_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(1,-1);
            var end = new Point(-1,1);
            var pt = new Point(2,-2);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalStartGreaterThanEndAndPointIsSlightlyLow_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(1000,0);
            var end = new Point(-1000,0);
            var pt = new Point(0,-1);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalStartGreaterThanEndAndPointIsSlightlyHigh_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(1000,0);
            var end = new Point(-1000,0);
            var pt = new Point(0,1);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalEndGreaterThanStartAndPointIsSlightlyLow_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(-1000,0);
            var end = new Point(1000,0);
            var pt = new Point(0,-1);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalEndGreaterThanStartAndPointIsSlightlyHigh_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(-1000,0);
            var end = new Point(1000,0);
            var pt = new Point(0,1);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreVerticalStartGreaterThanEndAndPointIsSlightlyLeft_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(0,1000);
            var end = new Point(0,-1000);
            var pt = new Point(-1,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalStartGreaterThanEndAndPointIsSlightlyRight_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(0,1000);
            var end = new Point(0,-1000);
            var pt = new Point(1,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsBetween_WhenStartAndEndAreVerticalEndGreaterThanStartAndPointIsSlightlyLeft_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(0,-1000);
            var end = new Point(0,1000);
            var pt = new Point(-1,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void IsBetween_WhenStartAndEndAreHorizontalEndGreaterThanStartAndPointIsSlightlyRight_ShouldReturnFalse()
        {
            // Arrange
            var start = new Point(0,-1000);
            var end = new Point(0,1000);
            var pt = new Point(1,0);
            
            // Act
            var result = pt.IsBetween(start, end);
            
            // Assert
            result.Should().BeFalse();
        }
    }
}