using System.Drawing;

namespace Chess.Model
{
    public static class PointExtension
    {
        public static Point Offset(this Point pt, Direction dir, int count)
        {
            var xOffset = 0;
            var yOffset = 0;

            if(dir == Direction.NorthWest || dir == Direction.North || dir == Direction.NorthEast)
            {
                yOffset = -1;
            }
            else if(dir == Direction.SouthWest || dir == Direction.South || dir == Direction.SouthEast)
            {
                yOffset = 1;
            }

            if(dir == Direction.NorthWest || dir == Direction.West || dir == Direction.SouthWest)
            {
                xOffset = -1;
            }
            else if(dir == Direction.NorthEast || dir == Direction.East || dir == Direction.SouthEast)
            {
                xOffset = 1;
            }

            return new Point(pt.X + xOffset, pt.Y+yOffset);
        }
    }
}