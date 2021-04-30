using System;
using System.Drawing;
using System.Net.Mime;
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

        public static bool IsBetween(this Point test, Point start, Point end) =>
            test.X >= Math.Min(start.X, end.X) && test.X <= Math.Max(start.X, end.X)
            && test.Y >= Math.Min(start.Y, end.Y) && test.Y <= Math.Max(start.Y, end.Y)
            && test.IsOnLine(start, end);

        public static bool IsOnLine(this Point test, Point start, Point end) =>
            test.Y * (start.X - end.X) == test.X * (start.Y - end.Y) - start.Y * end.X + start.X * end.Y;
        // y1-y2 = m(x1-x2)
        // m = (y1-y2)/(x1-x2)
        // b = y1 - x1(y1-y2)/(x1-x2)
        // y(x1-x2) = (y1-y2)x+y1(x1-x2)-x1(y1-y2)
        // y(x1-x2) = x(y1-y2)+y1x1-y1x2-x1y1+x1y2
        // y(x1-x2) = x(y1-y2)-y1x2+x1y2

        private static double Distance(Point start, Point end)
        {
            return Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        }
    }
}