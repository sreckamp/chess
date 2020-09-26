using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Extensions
{
    public static class PointExtension
    {
        public static Point CartesianOffset(this Point pt, Point offset)
        {
            return new Point(pt.X + offset.X, pt.Y + offset.Y);
        }

        public static Point Divide(this Point pt, int divisor)
        {
            return new Point(pt.X / divisor, pt.Y / divisor);
        }

        public static Point PolarOffset(this Point pt, Direction dir, int count)
        {
            var xOffset = 0;
            var yOffset = 0;

            if(dir == Direction.NorthWest || dir == Direction.North || dir == Direction.NorthEast)
            {
                yOffset = count;
            }
            else if(dir == Direction.SouthWest || dir == Direction.South || dir == Direction.SouthEast)
            {
                yOffset = -count;
            }

            if(dir == Direction.NorthWest || dir == Direction.West || dir == Direction.SouthWest)
            {
                xOffset = -count;
            }
            else if(dir == Direction.NorthEast || dir == Direction.East || dir == Direction.SouthEast)
            {
                xOffset = count;
            }

            return new Point(pt.X + xOffset, pt.Y + yOffset);
        }
    }
}